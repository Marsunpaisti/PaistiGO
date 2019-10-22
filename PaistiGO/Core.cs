using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

namespace PaistiGO
{
    class Core
    {
        private MainWindow mainWindow;
        private bool isRunning = false;

        public Core(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void StartMainLoop(CancellationToken cancelToken)
        {
            if (this.isRunning) return;
            this.isRunning = true;
            Debugging.Log("Core started");

            Debugging.Log("Downloading offset dump");
            HazeDownloader.DownloadDump();
            OffsetObject o = HazeDownloader.ReadOffsetFile();
            Globals.Offsets = o;

            Debugging.Log("Attaching to process");
            bool attached = false;
            for (int i = 0; i < 5; i++)
            {
                attached = Memory.Attach();
                if (attached)
                {
                    Debugging.Log("Attached successfully!");
                    Debugging.Log(String.Format("Client Hex: {0} Int: {1}", Memory.client.ToString("x"), Memory.client));
                    Debugging.Log(String.Format("Engine Hex: {0} Int: {1}", Memory.engine.ToString("x"), Memory.client));
                    break;
                } else
                {
                    Debugging.Log(String.Format("Failed to attach! (Attempt #{0})", i + 1));
                    if (i != 4)
                    {
                        Debugging.Log("Retrying...");
                        Thread.Sleep(2000);
                    }
                }
            }

            if (!attached)
            {
                Debugging.Log("Failed to attach after retries! Closing program!");
                mainWindow.AttachFailed();
                return;
            }

            MainLoop(cancelToken);
        }

        public void MainLoop(CancellationToken cancelToken)
        {
            while (true)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    Debugging.Log("Stopping mainloop due to canceltoken request.");
                    return;
                }
                RefreshCheck.refreshId += 1;

                debugPrint();
                if (BspLoader.checkBsp(2000))
                {
                    Debugging.Log("Loaded BSP for map " + Local.MapName);
                }
                
                Thread.Sleep(50);
            }
        }

        private bool lastInGame = false;

        private void debugPrint()
        {
            string dText = "";
            dText += "In game: " + Local.InGame + "\n";


            var m = Local.MapFile;
            if (m != null)
            {
                dText += "Map name: " + Local.MapName + "\n";
                dText += "Map file: " + m + "\n";
            } else
            {
                dText += "Map name: " + "N/A" + "\n";
                dText += "Map file: N/A\n";
            }
            
            dText += "Weapon: " + Local.ActiveWeapon.WeaponName + "\n";
            dText += "Pos: " + Local.Position + "\n";
            dText += "Fov: " + Local.Fov + "\n";
            dText += "Viewangle: " + Local.ViewAngle + "\n";
            dText += "Punch: " + Local.PunchAngle + "\n";
            dText += "BSP: " + BspLoader.LoadedMapName() + "\n";
            foreach (Entity e in Entity.EntityArray)
            {
                dText += e.print() + "\n";
            }



            mainWindow.setDebugText(dText);
        }
    }
}
