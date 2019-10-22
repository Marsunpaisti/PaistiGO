using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaistiGO.BspParser;

namespace PaistiGO
{
    static class BspLoader
    {
        private static BSPFile loadedMap = null;
        private static string loadedMapFile;
        private static string loadedMapName = "None";

        public static bool IsCorrectMapLoaded()
        {
            return Local.MapFile == loadedMapFile && loadedMap != null;
        }

        public static bool LoadMap(string mapFile)
        {
            if (mapFile != null && File.Exists(mapFile) && Local.InGame)
            {
                loadedMap = new BSPFile(mapFile);
                loadedMapFile = Local.MapFile;
                loadedMapName = Local.MapName;
                return true;
            }
            return false;
        }

        public static BSPFile GetBSP()
        {
            return loadedMap;
        }

        public static string LoadedMapName()
        {
            return loadedMapName;
        }

        private static DateTime lastChecked = DateTime.Now;
        public static bool checkBsp(int intervalms)
        {
            double elapsed = (DateTime.Now - lastChecked).TotalMilliseconds;
            if (elapsed < intervalms) return false;
            lastChecked = DateTime.Now;

            if (Local.InGame && Local.MapFile != null && !BspLoader.IsCorrectMapLoaded())
            {
                BspLoader.LoadMap(Local.MapFile);
                return true;
            }

            return false;
        }
    }
}
