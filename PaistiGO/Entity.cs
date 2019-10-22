using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaistiGO.Globals;
using static PaistiGO.Structs;

namespace PaistiGO
{
    public class Entity : IDisposable
    {
        public void Dispose()
        {

        }

        private static Entity[] _GetPlayers;

        private static int rGetPlayers = 0;
        public static Entity[] EntityArray
        {
            get
            {
                if (rGetPlayers.NeedRefresh())
                {
                    List<Entity> returnArray = new List<Entity>();

                    for (int i = 1; i < 64; i++)
                    {
                        Entity player = new Entity(i);

                        if (player.Ptr == 0) continue;

                        if (player.Ptr == Local.Ptr)
                        {
                            Local.Index = i;
                            continue;
                        }

                        returnArray.Add(player);
                    }

                    _GetPlayers = returnArray.ToArray();
                }

                return _GetPlayers;
            }
        }

        public int Index;
        private static int _Ptr;
        private static int _Health;
        private static int _Team;
        private static int _GlowIndex;
        private static float _Speed;
        private static Vector3 _VectorVelocity;
        private static Vector3 _Postition;
        private static BaseWeapon WeaponGet = new BaseWeapon();
        private static BaseWeapon _ActiveWeapon;
        private static bool _SpottedByMask;
        private static bool _Spotted;
        private static bool _Visible;
        private static bool _Dormant;
        private static string _Name;

        private int rPtr = 0;
        public int Ptr
        {
            get
            {
                if (rPtr.NeedRefresh())
                    _Ptr = Memory.Read<int>(Memory.client + Offsets.signatures.dwEntityList + (Index - 1) * 0x10);

                return _Ptr;
            }
        }

        public int Observe => Memory.Read<int>(Ptr + Offsets.netvars.m_hObserverTarget);
        public int Rank
        {
            get
            {
                int gameResource = Memory.Read<int>(Memory.client + Offsets.signatures.dwPlayerResource);
                return Memory.Read<int>(gameResource + Offsets.netvars.m_iCompetitiveRanking + Index * 0x4);
            }
        }

        public bool isTeam
        {
            get
            {
                if (Team == Local.Team) return true;
                else return false;
            }
        }

        private int rHealth = 0;
        public int Health
        {
            get
            {
                if (rHealth.NeedRefresh())
                    _Health = Memory.Read<int>(Ptr + Offsets.netvars.m_iHealth);

                return _Health;
            }
        }

        private int rTeam = 0;
        public int Team
        {
            get
            {
                if (rTeam.NeedRefresh())
                    _Team = Memory.Read<int>(Ptr + Offsets.netvars.m_iTeamNum);

                return _Team;
            }
        }

        private int rActiveWeapon = 0;
        public BaseWeapon ActiveWeapon
        {
            get
            {
                if (rActiveWeapon.NeedRefresh())
                    _ActiveWeapon = WeaponGet.ActiveWeapon(Ptr);

                return _ActiveWeapon;
            }
        }

        private int rGlowIndex = 0;
        public int GlowIndex
        {
            get
            {
                if (rGlowIndex.NeedRefresh())
                    _GlowIndex = Memory.Read<int>(Ptr + Offsets.netvars.m_iGlowIndex);

                return _GlowIndex;
            }
        }

        private int rSpeed = 0;
        public float Speed
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

        private int rVectorVelocity = 0;
        public Vector3 VectorVelocity
        {
            get
            {
                if (rVectorVelocity.NeedRefresh())
                    _VectorVelocity = Memory.Read<Vector3>(Ptr + Offsets.netvars.m_vecVelocity);

                return _VectorVelocity;
            }
        }

        private int rSpottedByMask = 0;
        public bool SpottedByMask
        {
            get
            {
                if (rSpottedByMask.NeedRefresh())
                    _SpottedByMask = Memory.Read<bool>(Ptr + Offsets.netvars.m_bSpottedByMask);

                return _SpottedByMask;
            }
        }

        private int rDormant = 0;
        public bool Dormant
        {
            get
            {
                if (rDormant.NeedRefresh())
                    _Dormant = Memory.Read<bool>(Ptr + Offsets.signatures.m_bDormant);

                return _Dormant;
            }
        }

        private int rSpotted = 0;
        public bool Spotted
        {
            get
            {
                if (rSpotted.NeedRefresh())
                    _Spotted = Memory.Read<bool>(Ptr + Offsets.netvars.m_bSpotted);

                return _Spotted;
            }
            set
            {
                Memory.Write<bool>(Ptr + Offsets.netvars.m_bSpotted, value);
            }
        }

        private int rPostition = 0;
        public Vector3 Position
        {
            get
            {
                if (rPostition.NeedRefresh())
                    _Postition = Memory.Read<Vector3>(Ptr + Offsets.netvars.m_vecOrigin);

                return _Postition;
            }
        }
        
        public Vector3 BonePosition(int Bone)
        {
            int bMatrix = Memory.Read<int>(Ptr + Offsets.netvars.m_dwBoneMatrix);

            Vector3 bonePos = new Vector3()
            {
                x = Memory.Read<float>(bMatrix + (0x30 * Bone) + 0x0C),
                y = Memory.Read<float>(bMatrix + (0x30 * Bone) + 0x1C),
                z = Memory.Read<float>(bMatrix + (0x30 * Bone) + 0x2C),

            };

            return bonePos;
        }

        private int rName = 0;
        public string Name
        {
            get
            {
                if (rName.NeedRefresh())
                {
                    int radarBasePtr = Offsets.signatures.dwRadarBase;
                    int radarStructSize = 0x168;
                    int radarStructPos = 0x18;


                    Encoding enc = Encoding.UTF8;

                    int radarBase = Memory.Read<int>(Memory.client + Offsets.signatures.dwRadarBase);

                    int radarPtr = Memory.Read<int>(radarBase + radarBasePtr);

                    int ind = Index + 1;

                    var nameAddr = radarPtr + ind * radarStructSize + radarStructPos;
                    _Name = Memory.ReadString(nameAddr, 64, enc);
                }
                return _Name;
            }
        }

        public Entity(int index)
        {
            Index = index;
        }

        private static int rVisible = 0;
        public bool BSPVisible
        {
            get
            {
                if (rVisible.NeedRefresh())
                {
                    if (!BspLoader.IsCorrectMapLoaded()) return true;
                    if (Dormant)
                        _Visible = false;
                    else
                        _Visible = BspLoader.GetBSP().IsVisible(Local.EyePos, BonePosition(6));
                }

                return _Visible;
            }
        }

        public string print()
        {
            string entString = "Player " + Index + "\t Distance:\t" + MathFuncs.VectorDistance(Position, Local.Position) + "\t Visible:\t" + BSPVisible;
            return entString;
        }

    }
}
