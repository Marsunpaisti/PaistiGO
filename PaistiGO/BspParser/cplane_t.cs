﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PaistiGO.Structs;

namespace PaistiGO.BspParser
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cplane_t
    {
        public Vector3 m_Normal;
        public float m_Distance;
        public byte m_Type;
        public byte m_SignBits;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private byte[] m_Pad;
    }
}
