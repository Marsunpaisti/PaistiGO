using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaistiGO
{
    internal static class Enums
    {
        internal enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        private enum EncodingType
        {
            ASCII,
            Unicode,
            UTF7,
            UTF8
        }

        public enum WEAPONID
        {
            Deagle = 1,
            DuelBerettas = 2,
            FiveSeven = 3,
            Glock18 = 4,
            AK47 = 7,
            AUG = 8,
            AWP = 9,
            FAMAS = 10,
            G3SG1 = 11,
            GalilAR = 13,
            M249 = 14,
            M4A4 = 16,
            Mac10 = 17,
            P90 = 19,
            UMP45 = 24,
            XM1014 = 25,
            PPBizon = 26,
            Mag7 = 27,
            Negev = 28,
            SawedOff = 29,
            Tec9 = 30,
            p2000 = 32,
            MP7 = 33,
            MP9 = 34,
            Nova = 35,
            p250 = 36,
            Scar20 = 38,
            SG556 = 39,
            SSG08 = 40,
            M4A1S = 60,
            USPS = 61,
            CZ75A = 63,
            R8Revolver = 64,
        };
    }
}
