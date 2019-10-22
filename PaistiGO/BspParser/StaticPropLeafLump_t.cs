using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PaistiGO.BspParser
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StaticPropLeafLump_t
    {
        public int m_LeafEntries;
    }
}
