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
    struct dplane_t
    {
        public Vector3 m_Normal;
        public float m_Distance;
        public byte m_Type;
    }
}
