using MelonLoader;
using SLZ.Marrow.SceneStreaming;
using SLZ.Marrow.Warehouse;
using UnityEngine;

namespace MenuSkipper
{
    public static class BuildInfo
    {
        public const string Name = "MenuSkipper";
        public const string Author = "timmie";
        public const string Company = null;
        public const string Version = "0.0.1";
        public const string DownloadLink = null;
    }

    public class MenuSkipper : MelonMod
    {
        private static bool skippedMenu = false;
        private static string hubBarcode;
        private static string defaultLoadBarcode;

        private float timer;
        private float endTime = 0.25f;
        private bool timerDone;
        private bool startTiming;

        public override void OnSceneWasInitialized(int buildindex, string sceneName)
        {
            if (SceneStreamer.Session.Level == null)
            {
                return;
            }

            if (hubBarcode == null)
            {
                var crates = AssetWarehouse.Instance.GetCrates();

                foreach (Crate crate in crates)
                {
                    if (crate.Title.Contains("BONELAB Hub"))
                    {
                        hubBarcode = crate.Barcode.ID;
                    }

                    if (crate.Title.Contains("Load Default"))
                    {
                        defaultLoadBarcode = crate.Barcode.ID;
                    }
                }
            }            

            if (!skippedMenu && SceneStreamer.Session.Status == StreamStatus.DONE && (SceneStreamer.Session.Level.Title.Contains("Void G114") || SceneStreamer.Session.Level.Barcode.ID.Contains("Main Menu")))
            {
                skippedMenu = true;
                startTiming = true;
            }
        }

        public override void OnUpdate()
        {
            if (startTiming && !timerDone)
            {
                timer += Time.deltaTime;
                if (timer > endTime)
                {
                    timerDone = true;

                    SceneStreamer.Load(hubBarcode, defaultLoadBarcode);
                }
            }
        }
    }
}
