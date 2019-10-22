using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PaistiGO
{
    static class Memory
    {
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        public static Process attachedProcess = null;
        public static IntPtr processHandle = IntPtr.Zero;
        public static IntPtr client = IntPtr.Zero;
        public static IntPtr engine = IntPtr.Zero;

        public static object HandleType { get; private set; }

        public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        public static byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        public static T Read<T>(IntPtr address) where T : struct
        {
            if (processHandle == IntPtr.Zero)
            {
                throw new NullReferenceException("Call to readMemory without handle");
            }

            int size = 1;
            if (typeof(T) != typeof(bool))
            {
                size = Marshal.SizeOf(typeof(T));
            }

            byte[] buffer = new byte[size];
            int bytesRead = 0;
            WinAPI.ReadProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);
            return ByteArrayToStructure<T>(buffer);
        }

        public static T Read<T>(Int64 address) where T : struct
        {
            return Read<T>((IntPtr)address);
        }

        public static T Read<T>(int address) where T : struct
        {
            return Read<T>((IntPtr)address);
        }


        public static void Write<T>(IntPtr address, object value)
        {
            if (processHandle == IntPtr.Zero)
            {
                throw new NullReferenceException("Call to WriteMemory without handle");
            }

            byte[] buffer = StructureToByteArray(value);
            int bytesWritten = 0;
            WinAPI.WriteProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesWritten);
        }

        public static void WriteBytes(IntPtr address, byte[] value)
        {
            int nBytesRead = 0;
            WinAPI.WriteProcessMemory(processHandle, address, value, value.Length, ref nBytesRead);
        }

        public static void WriteBytes(int address, byte[] value)
        {
            WriteBytes((IntPtr)address, value);
        }

        public static void Write<T>(int address, object value)
        {
            Write<T>((IntPtr)address, value);
        }

        public static void Write<T>(Int64 address, object value)
        {
            Write<T>((IntPtr)address, value);
        }

        public static string ReadString(IntPtr address, int size, Encoding enc)
        {
            if (processHandle == IntPtr.Zero)
            {
                throw new NullReferenceException("Call to ReadString without handle");
            }

            byte[] buffer = new byte[size];
            int bytesRead = 0;

            bool success = WinAPI.ReadProcessMemory(processHandle, (IntPtr)address, buffer, size, ref bytesRead);
            string text = enc.GetString(buffer);
            if (text.Contains('\0'))
                text = text.Substring(0, text.IndexOf('\0'));
            return text;
        }

        public static string ReadString(Int64 address, int size, Encoding enc)
        {
            return ReadString((IntPtr)address, size, enc);
        }


        private static bool FindModules(Process proc)
        {
            foreach (ProcessModule module in proc.Modules)
            {
                if ((module.ModuleName == "client_panorama.dll"))
                    Memory.client = module.BaseAddress;

                if ((module.ModuleName == "engine.dll"))
                    Memory.engine = module.BaseAddress;
            }

            return Memory.client != IntPtr.Zero && Memory.engine != IntPtr.Zero;
        }

        public static bool Attach()
        {
            Process[] processes = Process.GetProcessesByName("csgo");
            if (processes.Length > 0)
            {
                attachedProcess = processes[0];
                processHandle = WinAPI.OpenProcess(Enums.ProcessAccessFlags.All, false, attachedProcess.Id);
                return FindModules(attachedProcess);
            }

            return false;
        }

        public static void Detach()
        {
            if (processHandle != IntPtr.Zero)
            {
                WinAPI.CloseHandle(processHandle);
            }
            attachedProcess = null;
            processHandle = IntPtr.Zero;
            engine = IntPtr.Zero;
            client = IntPtr.Zero;
        }
    }
}
