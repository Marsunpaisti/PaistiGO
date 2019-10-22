using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaistiGO.Globals;

namespace PaistiGO
{
    public class BaseWeapon : IDisposable
    {
        public void Dispose()
        {

        }

        private int Ptr;

        public int Base => Memory.Read<int>(Memory.client + Offsets.signatures.dwEntityList + (Ptr - 1) * 0x10);

        private int rWeaponId = 0;
        private short _weaponId;
        public short WeaponID
        {
            get
            {
                if (rWeaponId.NeedRefresh())
                {
                    _weaponId = Memory.Read<short>(Base + Offsets.netvars.m_iItemDefinitionIndex);
                }
                return _weaponId;
            }
            set
            {
                Memory.Write<short>(Base + Offsets.netvars.m_iItemDefinitionIndex, value);
            }

        }

        public int AccountID
        {
            get
            {
                return Memory.Read<int>(Base + Offsets.netvars.m_iAccountID);
            }
            set
            {
                Memory.Write<int>(Base + Offsets.netvars.m_iAccountID, value);
            }
        }

        public int XuIDHigh
        {
            get
            {
                return Memory.Read<int>(Base + Offsets.netvars.m_OriginalOwnerXuidHigh);
            }
            set
            {
                Memory.Write<int>(Base + Offsets.netvars.m_OriginalOwnerXuidHigh, value);
            }
        }

        public int XuIDLow
        {
            get
            {
                return Memory.Read<int>(Base + Offsets.netvars.m_OriginalOwnerXuidLow);
            }
            set
            {
                Memory.Write<int>(Base + Offsets.netvars.m_OriginalOwnerXuidLow, value);
            }
        }

        public int Seed
        {
            get
            {
                return Memory.Read<int>(Base + Offsets.netvars.m_nFallbackSeed);
            }
            set
            {
                Memory.Write<int>(Base + Offsets.netvars.m_nFallbackSeed, value);
            }
        }

        public char[] Name
        {
            set
            {
                char[] writeChar = new char[32];

                for (int i = 0; i < writeChar.Length; i++)
                {
                    if (i < value.Length)
                        writeChar[i] = value[i];
                }

                byte[] writebytes = Encoding.Default.GetBytes(writeChar);

                Memory.WriteBytes(Base + Offsets.netvars.m_szCustomName, writebytes);
            }
        }

        private int rAmmo = 0;
        private int _Ammo;
        public int Ammo
        {
            get
            {
                if (rAmmo.NeedRefresh())
                {
                    _Ammo = Memory.Read<int>(Ptr + Offsets.netvars.m_iClip1);
                }
                return _Ammo;
            }
        }

        private int rNextPrimaryAttack = 0;
        private float _nextPrimaryAttack;
        public float nextPrimaryAttack
        {
            get
            {
                if (rNextPrimaryAttack.NeedRefresh())
                {
                    _nextPrimaryAttack = Memory.Read<float>(Ptr + Offsets.netvars.m_flNextPrimaryAttack);
                }
                return _nextPrimaryAttack;
            }
        }

        private int rCanFire = 0;
        private bool _canFire;
        public bool CanFire
        {
            get
            {
                if (rCanFire.NeedRefresh())
                {
                    _canFire = nextPrimaryAttack <= 0 || nextPrimaryAttack < Memory.Read<int>(Memory.client + Offsets.netvars.m_nTickBase); ;
                }
                return _canFire;
            }
        }

        public BaseWeapon()
        {

        }

        private BaseWeapon(int ptr)
        {
            Ptr = ptr;
        }

        public BaseWeapon MyWeapons(int playerPtr, int index)
        {
            int _Ptr = Memory.Read<int>(playerPtr + Offsets.netvars.m_hMyWeapons + (index - 1) * 0x4) & 0xFFF;
            return new BaseWeapon(_Ptr);
        }

        public BaseWeapon ActiveWeapon(int playerPtr)
        {
            int _Ptr = Memory.Read<int>(playerPtr + Offsets.netvars.m_hActiveWeapon) & 0xFFF;
            return new BaseWeapon(_Ptr);
        }


        public bool isBomb()
        {
            if (WeaponID == 49) return true;
            else return false;
        }

        public bool isGrenade()
        {
            switch (WeaponID)
            {
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                    return true;
                default:
                    return false;
            }
        }

        public bool isKnife()
        {
            switch (WeaponID)
            {
                case 41:
                case 42:
                case 59:
                case 500:
                case 505:
                case 506:
                case 507:
                case 508:
                case 509:
                case 512:
                case 514:
                case 515:
                case 516:
                case 519:
                case 520:
                case 522:
                case 523:
                    return true;
                default:
                    return false;
            }
        }

        public bool isPistol()
        {
            switch (WeaponID)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 30:
                case 32:
                case 36:
                case 61:
                case 63:
                case 64:
                    return true;
                default:
                    return false;
            }

        }

        public bool isSniper()
        {
            switch (WeaponID)
            {
                case 9:
                case 11:
                case 38:
                case 40:
                    return true;
                default:
                    return false;
            }
        }

        public bool isRifile()
        {
            switch (WeaponID)
            {
                case 7:
                case 8:
                case 10:
                case 13:
                case 16:
                case 39:
                case 60:
                    return true;

                default:
                    return false;
            }
        }

        public bool isSMG()
        {
            switch (WeaponID)
            {
                case 17:
                case 19:
                case 24:
                case 26:
                case 33:
                case 34:
                    return true;
                default:
                    return false;
            }
        }

        public bool isShotgun()
        {
            switch (WeaponID)
            {
                case 25:
                case 27:
                case 29:
                case 35:
                    return true;
                default:
                    return false;
            }
        }

        public bool isLMG()
        {
            switch (WeaponID)
            {
                case 14:
                case 28:
                    return true;
                default:
                    return false;
            }
        }

        public string WeaponName
        {
            get
            {
                if (isKnife()) return "Knife";

                switch (WeaponID)
                {
                    case 1: return "Desert Eagle";
                    case 2: return "Duel Berettas";
                    case 3: return "Five-SeveN";
                    case 4: return "Glock-18";
                    case 7: return "AK-47";
                    case 8: return "AUG";
                    case 9: return "AWP";
                    case 10: return "FAMAS";
                    case 11: return "G3SG1";
                    case 13: return "Galil AR";
                    case 14: return "M249";
                    case 16: return "M4A4";
                    case 17: return "MAC-10";
                    case 19: return "P90";
                    case 24: return "UMP-45";
                    case 25: return "XM1014";
                    case 26: return "PP-Bizon";
                    case 27: return "MAG-7";
                    case 28: return "Negev";
                    case 29: return "Sawed-Off";
                    case 30: return "Tec-9";
                    case 31: return "Zeus x27";
                    case 32: return "P2000";
                    case 33: return "MP7";
                    case 34: return "MP9";
                    case 35: return "Nova";
                    case 36: return "P250";
                    case 38: return "SCAR-20";
                    case 39: return "SG 553";
                    case 40: return "SSG 08";
                    case 43: return "Flashbang";
                    case 44: return "HE Grenade";
                    case 45: return "Smoke Grenade";
                    case 46: return "Molotov";
                    case 47: return "Decoy";
                    case 48: return "Incendiary";
                    case 49: return "C4";
                    case 69: return "M4A1-S";
                    case 61: return "USP-S";
                    case 63: return "CZ75-Auto";
                    case 64: return "R8 Revolver";
                    default: return WeaponID.ToString();
                }

            }
        }

        public string Icon
        {
            get
            {
                if (isKnife()) return "\uE03B";

                switch (WeaponID)
                {
                    case 1: return "\uE001";
                    case 2: return "\uE002";
                    case 3: return "\uE003";
                    case 4: return "\uE004";
                    case 7: return "\uE007";
                    case 8: return "\uE008";
                    case 9: return "\uE009";
                    case 10: return "\uE00A";
                    case 11: return "\uE00B";
                    case 13: return "\uE00D";
                    case 14: return "\uE00E";
                    case 16: return "\uE010";
                    case 17: return "\uE011";
                    case 19: return "\uE013";
                    case 24: return "\uE018";
                    case 25: return "\uE019";
                    case 26: return "\uE01A";
                    case 27: return "\uE01B";
                    case 28: return "\uE01C";
                    case 29: return "\uE01D";
                    case 30: return "\uE01E";
                    case 31: return "\uE01F";
                    case 32: return "\uE020";
                    case 33: return "\uE021";
                    case 34: return "\uE022";
                    case 35: return "\uE023";
                    case 36: return "\uE024";
                    case 38: return "\uE026";
                    case 39: return "\uE027";
                    case 40: return "\uE028";
                    case 43: return "\uE02B";
                    case 44: return "\uE02C";
                    case 45: return "\uE02D";
                    case 46: return "\uE02E";
                    case 47: return "\uE02F";
                    case 48: return "\uE030";
                    case 49: return "\uE031";
                    case 69: return "\uE045";
                    case 61: return "\uE03D";
                    case 63: return "\uE03F";
                    case 64: return "\uE040";
                    default: return WeaponID.ToString();
                }

            }
        }
    }
}
