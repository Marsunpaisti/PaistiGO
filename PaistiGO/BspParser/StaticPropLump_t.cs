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
    public struct StaticPropLump_t
    {
        // v4
        public Vector3 Origin;       // origin
        public Vector3 Angles;       // orientation (pitch roll yaw)
        public ushort PropType;    // index into model name dictionary
        public ushort FirstLeaf;   // index into leaf array
        public ushort LeafCount;
        public byte Solid;       // solidity type
        public byte Flags;
        public int Skin;        // model skin numbers
        public float FadeMinDist;
        public float FadeMaxDist;
        public Vector3 LightingOrigin;  // for lighting
                                        // since v5
        public float ForcedFadeScale; // fade distance scale
                                      // v6 and v7 only
        //public ushort MinDXLevel;      // minimum DirectX version to be visible
        //public ushort MaxDXLevel;      // maximum DirectX version to be visible
                                       // since v8
        public byte MinCPULevel;
        public byte MaxCPULevel;
        public byte MinGPULevel;
        public byte MaxGPULevel;
        // since v7
        public int DiffuseModulation; // per instance color and alpha modulation
        // since v10
        public float unknown;
        // since v9
        public bool DisableX360;     // if true, don't show on XBox 360
    }
}
