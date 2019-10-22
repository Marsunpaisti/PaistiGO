using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaistiGO.Structs;
using static PaistiGO.Globals;
using System.Threading;

namespace PaistiGO
{
    public static class Local
    {
        private static int _Index;
        private static int _Ptr;
        private static int _ClientState;
        private static int _Health;
        private static int _Team;
        private static int _GlowIndex;
        private static float _Speed;
        private static BaseWeapon WeaponGet = new BaseWeapon();
        private static BaseWeapon _ActiveWeapon;
        private static List<BaseWeapon> _WeaponList;
        private static Vector3 _VectorVelocity;
        private static Vector3 _Postition;
        private static Vector2 _ViewAngle;
        private static Vector2 _PunchAngle;
        private static Vector3 _EyePos;
        private static bool _InGame;
        private static int _Flags;
        private static int _CrosshairID;
        private static int _Fov;
        private static bool _GotKill;


        public static int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        private static int _oldKills;
        public static bool GotKill
        {
            get
            {
                int newKills = Memory.Read<int>(Offsets.signatures.dwPlayerResource + 0xBE8 + (Index + 1) * 0x4);
                if (_oldKills < newKills) { 
                    _oldKills = newKills;
                    return true;
                }
                return false;
            }
        }

        private static int rInGame = 0;
        public static bool InGame
        {
            get
            {
                if (rInGame.NeedRefresh())
                {
                    _InGame = Memory.Read<int>(ClientState + Offsets.signatures.dwClientState_State) == 6;
                }
                return _InGame;
            }
        }

        private static int rFov = 0;
        public static int Fov
        {
            get
            {
                if (rFov.NeedRefresh())
                {
                    if (Local.ActiveWeapon.WeaponID == 8 || Local.ActiveWeapon.WeaponID == 39)
                        _Fov = Memory.Read<int>(Ptr + 0x330C);
                    else
                        _Fov = Memory.Read<int>(Ptr + Offsets.netvars.m_iFOVStart); // - 4);
                }

                return _Fov;
            }

            /*
            set
            {
                if (value == _Fov) return;

                if (Local.ActiveWeapon.WeaponID == 8 || Local.ActiveWeapon.WeaponID == 39)
                {
                    for (int i = 0; i < 1000; i++)
                        Memory.Write<int>(Ptr + 0x330C, value);
                }
                else
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        Memory.Write<int>(Ptr + Offsets.netvars.m_iFOVStart - 4, value);
                        Memory.Write<int>(Ptr + 0x330C, 90);
                    }
                }

            }
            */

        }

        public static int rMapName = 0;
        private static string _MapName;
        public static string MapName
        {
            get
            {
                if (rMapName.NeedRefresh())
                {
                    _MapName = Memory.ReadString(Local.ClientState + Offsets.signatures.dwClientState_Map, 32, Encoding.ASCII);
                }
                return _MapName;
            }
        }

        public static string MapFile
        {
            get
            {
                try
                {
                    string f = Memory.ReadString(Local.ClientState + Offsets.signatures.dwClientState_MapDirectory, 32, Encoding.ASCII);

                    if (!f.ToLower().Contains("map"))
                        return null;

                    string file = Memory.attachedProcess.MainModule.FileName;
                    file = file.Substring(0, file.Length - 9) + @"\csgo\";

                    if (!f.EndsWith(".bsp")) file = string.Format("{0}{1}.bsp", file, f);
                    else file = string.Format("{0}{1}", file, f);

                    return file;
                }
                catch
                {
                    return null;
                }
            }
        }

        private static int rActiveWeapon = 0;
        public static BaseWeapon ActiveWeapon
        {
            get
            {
                if (rActiveWeapon.NeedRefresh())
                    _ActiveWeapon = WeaponGet.ActiveWeapon(Ptr);

                return _ActiveWeapon;
            }
        }

        private static int rWeaponList = 0;
        public static List<BaseWeapon> WeaponList
        {
            get
            {
                if (rWeaponList.NeedRefresh())
                {
                    _WeaponList = new List<BaseWeapon>();
                    for (int i = 0; i < 64; i++)
                        _WeaponList.Add(WeaponGet.MyWeapons(Ptr, i));
                }
                return _WeaponList;
            }
        }


        private static int rClientState = 0;
        public static int ClientState
        {
            get
            {
                if (rClientState.NeedRefresh())
                    _ClientState = Memory.Read<int>(Memory.engine + Offsets.signatures.dwClientState);

                return _ClientState;
            }
        }

        private static int rPtr = 0;
        public static int Ptr
        {
            get
            {
                if (rPtr.NeedRefresh())
                    _Ptr = Memory.Read<int>(Memory.client + Offsets.signatures.dwLocalPlayer);

                return _Ptr;
            }
        }

        private static int rHealth = 0;
        public static int Health
        {
            get
            {
                if (rHealth.NeedRefresh())
                    _Health = Memory.Read<int>(Ptr + Offsets.netvars.m_iHealth);

                return _Health;
            }
        }

        private static int rTeam = 0;
        public static int Team
        {
            get
            {
                if (rTeam.NeedRefresh())
                    _Team = Memory.Read<int>(Ptr + Offsets.netvars.m_iTeamNum);

                return _Team;
            }
        }

        public static int ShotsFired
        {
            get
            {
                return Memory.Read<int>(Ptr + Offsets.netvars.m_iShotsFired);
            }
        }

        private static int rGlowIndex = 0;
        public static int GlowIndex
        {
            get
            {
                if (rGlowIndex.NeedRefresh())
                    _GlowIndex = Memory.Read<int>(Ptr + Offsets.netvars.m_iGlowIndex);

                return _GlowIndex;
            }
        }

        private static int rSpeed = 0;
        public static float Speed
        {
            get
            {
                if (rSpeed.NeedRefresh())
                    _Speed = (float)Math.Sqrt(
                            VectorVelocity.x * VectorVelocity.x +
                            VectorVelocity.y * VectorVelocity.y +
                            VectorVelocity.z * VectorVelocity.z
                            );

                return _Speed;
            }

        }

        private static int rVectorVelocity = 0;
        public static Vector3 VectorVelocity
        {
            get
            {
                if (rVectorVelocity.NeedRefresh())
                    _VectorVelocity = Memory.Read<Vector3>(Ptr + Offsets.netvars.m_vecVelocity);

                return _VectorVelocity;
            }
        }

        private static int rPosition = 0;
        public static Vector3 Position
        {
            get
            {
                if (rPosition.NeedRefresh())
                    _Postition = Memory.Read<Vector3>(Ptr + Offsets.netvars.m_vecOrigin);

                return _Postition;
            }
        }

        private static int rEyePos = 0;
        public static Vector3 EyePos
        {
            get
            {
                if (rEyePos.NeedRefresh())
                {
                    Vector3 pos = Position;
                    pos.z += Memory.Read<float>(Ptr + 0x10C);
                    _EyePos = pos;
                }
                
                return _EyePos;
            }
        }

        private static int rCrosshairID = 0;
        public static int CrosshairID
        {
            get
            {
                if (rCrosshairID.NeedRefresh())
                    _CrosshairID = Memory.Read<int>(Ptr + Offsets.netvars.m_iCrosshairId);

                return _CrosshairID;
            }
        }

        private static int rViewAngle = 0;
        public static Vector2 ViewAngle
        {
            get
            {
                if (rViewAngle.NeedRefresh())
                    _ViewAngle = Memory.Read<Vector2>(ClientState + Offsets.signatures.dwClientState_ViewAngles);

                return _ViewAngle;
            }
            set
            {
                if (value.Equals(_ViewAngle)) return;


                Memory.Write<Vector2>(ClientState + Offsets.signatures.dwClientState_ViewAngles, MathFuncs.ClampAngle(value));

                _ViewAngle = value;
            }
        }

        public static int rFlags = 0;
        public static int Flags
        {
            get
            {
                if (rFlags.NeedRefresh())
                    _Flags = Memory.Read<int>(Ptr + Offsets.netvars.m_fFlags);

                return _Flags;
            }

        }

        private static int rPunchAngle = 0;
        public static Vector2 PunchAngle
        {
            get
            {
                if (rPunchAngle.NeedRefresh())
                    _PunchAngle = Memory.Read<Vector2>(Ptr + Offsets.netvars.m_aimPunchAngle);

                return _PunchAngle;
            }
        }

        public static Vector2 ViewAndPunch
        {
            get
            {
                return ViewAngle + PunchAngle;
            }
        }

        public static bool Scoped
        {
            get
            {
                return Memory.Read<bool>(Local.Ptr + Offsets.netvars.m_bIsScoped);
            }
            set
            {
                for (int i = 0; i < 1000; i++)
                    Memory.Write<bool>(Local.Ptr + Offsets.netvars.m_bIsScoped, value);
            }
        }

        public static void Jump()
        {
            Memory.Write<int>(Memory.client + Offsets.signatures.dwForceJump, 5);
            Thread.Sleep(15);
            Memory.Write<int>(Memory.client + Offsets.signatures.dwForceJump, 4);
        }

        public static void Attack()
        {
            if (!ActiveWeapon.CanFire) return;

            Memory.Write<int>(Memory.client + Offsets.signatures.dwForceAttack, 5);
            Thread.Sleep(15);
            Memory.Write<int>(Memory.client + Offsets.signatures.dwForceAttack, 4);
        }


        public static void Attack2()
        {
            Memory.Write<int>(Memory.client + Offsets.signatures.dwForceAttack2, 5);
            Thread.Sleep(15);
            Memory.Write<int>(Memory.client + Offsets.signatures.dwForceAttack2, 4);
        }
    }
}
