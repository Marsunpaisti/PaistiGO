﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PaistiGO.BspParser
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dedge_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public ushort[] m_V;
    }
}
