﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaistiGO.BspParser
{
    public class Enums
    {

        [Flags]
        public enum ContentsFlag
        {
            CONTENTS_EMPTY = 0,
            CONTENTS_SOLID = 0x1,
            CONTENTS_WINDOW = 0x2,
            CONTENTS_AUX = 0x4,
            CONTENTS_GRATE = 0x8,
            CONTENTS_SLIME = 0x10,
            CONTENTS_WATER = 0x20,
            CONTENTS_MIST = 0x40,
            CONTENTS_OPAQUE = 0x80,
            CONTENTS_TESTFOGVOLUME = 0x100,
            CONTENTS_UNUSED = 0x200,
            CONTENTS_UNUSED6 = 0x400,
            CONTENTS_TEAM1 = 0x800,
            CONTENTS_TEAM2 = 0x1000,
            CONTENTS_IGNORE_NODRAW_OPAQUE = 0x2000,
            CONTENTS_MOVEABLE = 0x4000,
            CONTENTS_AREAPORTAL = 0x8000,
            CONTENTS_PLAYERCLIP = 0x10000,
            CONTENTS_MONSTERCLIP = 0x20000,
            CONTENTS_CURRENT_0 = 0x40000,
            CONTENTS_CURRENT_90 = 0x80000,
            CONTENTS_CURRENT_180 = 0x100000,
            CONTENTS_CURRENT_270 = 0x200000,
            CONTENTS_CURRENT_UP = 0x400000,
            CONTENTS_CURRENT_DOWN = 0x800000,
            CONTENTS_ORIGIN = 0x1000000,
            CONTENTS_MONSTER = 0x2000000,
            CONTENTS_DEBRIS = 0x4000000,
            CONTENTS_DETAIL = 0x8000000,
            CONTENTS_TRANSLUCENT = 0x10000000,
            CONTENTS_LADDER = 0x20000000,
            CONTENTS_HITBOX = 0x40000000
        }


        public enum LumpType
        {
            LUMP_ENTITIES = 0,
            LUMP_PLANES = 1,
            LUMP_TEXDATA = 2,
            LUMP_VERTEXES = 3,
            LUMP_VISIBILITY = 4,
            LUMP_NODES = 5,
            LUMP_TEXINFO = 6,
            LUMP_FACES = 7,
            LUMP_LIGHTING = 8,
            LUMP_OCCLUSION = 9,

            LUMP_LEAFS = 10,
            LUMP_FACEIDS = 11,
            LUMP_EDGES = 12,
            LUMP_SURFEDGES = 13,
            LUMP_MODELS = 14,
            LUMP_WORLDLIGHTS = 15,
            LUMP_LEAFFACES = 16,
            LUMP_LEAFBRUSHES = 17,
            LUMP_BRUSHES = 18,
            LUMP_BRUSHSIDES = 19,

            LUMP_AREAS = 20,
            LUMP_AREAPORTALS = 21,
            LUMP_PROPCOLLISION = 22,
            LUMP_PROPHULLS = 23,
            LUMP_PROPHULLVERTS = 24,
            LUMP_PROPTRIS = 25,
            LUMP_DISPINFO = 26,
            LUMP_ORIGINALFACES = 27,
            LUMP_PHYSDISP = 28,
            LUMP_PHYSCOLLIDE = 29,

            LUMP_VERTNORMALS = 30,
            LUMP_VERTNORMALINDICES = 31,
            LUMP_DISP_LIGHTMAP_ALPHAS = 32,
            LUMP_DISP_VERTS = 33,
            LUMP_DISP_LIGHTMAP_SAMPLE_POSITIONS = 34,
            LUMP_GAME_LUMP = 35,
            LUMP_LEAFWATERDATA = 36,
            LUMP_PRIMITIVES = 37,
            LUMP_PRIMVERTS = 38,
            LUMP_PRIMINDICES = 39,

            LUMP_PAKFILE = 40,
            LUMP_CLIPPORTALVERTS = 41,
            LUMP_CUBEMAPS = 42,
            LUMP_TEXDATA_STRING_DATA = 43,
            LUMP_TEXDATA_STRING_TABLE = 44,
            LUMP_OVERLAYS = 45,
            LUMP_LEAFMINDISTTOWATER = 46,
            LUMP_FACE_MACRO_TEXTURE_INFO = 47,
            LUMP_DISP_TRIS = 48,
            LUMP_PROP_BLOB = 49,

            LUMP_WATEROVERLAYS = 50,
            LUMP_LEAF_AMBIENT_INDEX_HDR = 51,
            LUMP_LEAF_AMBIENT_INDEX = 52,
            LUMP_LIGHTING_HDR = 53,
            LUMP_WORLDLIGHTS_HDR = 54,
            LUMP_LEAF_AMBIENT_LIGHTING_HDR = 55,
            LUMP_LEAF_AMBIENT_LIGHTING = 56,
            LUMP_XZIPPAKFILE = 57,
            LUMP_FACES_HDR = 58,
            LUMP_MAP_FLAGS = 59,

            LUMP_OVERLAY_FADES = 60,
            LUMP_OVERLAY_SYSTEM_LEVELS = 61,
            LUMP_PHYSLEVEL = 62,
            LUMP_DISP_MULTIBLEND = 63
        }

        [FlagsAttribute]
        public enum SurfFlag
        {
            SURF_LIGHT = 0x1,
            SURF_SKY2D = 0x2,
            SURF_SKY = 0x4,
            SURF_WARP = 0x8,
            SURF_TRANS = 0x10,
            SURF_NOPORTAL = 0x20,
            SURF_TRIGGER = 0x40,
            SURF_NODRAW = 0x80,
            SURF_HINT = 0x100,
            SURF_SKIP = 0x200,
            SURF_NOLIGHT = 0x400,
            SURF_BUMPLIGHT = 0x800,
            SURF_NOSHADOWS = 0x1000,
            SURF_NODECALS = 0x2000,
            SURF_NOCHOP = 0x4000,
            SURF_HITBOX = 0x8000
        }
    }
}
