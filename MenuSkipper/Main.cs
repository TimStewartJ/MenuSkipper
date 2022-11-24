using MelonLoader;
using SLZ.Bonelab;
using SLZ.Marrow.Warehouse;
using System;
using UnityEngine;

namespace MenuSkipper
{
    public static class BuildInfo
    {
        public const string Name = "MenuSkipper";
        public const string Author = "timmie";
        public const string Company = null;
        public const string Version = "0.1.1";
        public const string DownloadLink = null;
    }

    public class MenuSkipper : MelonMod
    {
        public override void OnSceneWasInitialized(int buildindex, string sceneName)
        {
            if (sceneName.ToUpper().Contains("BOOTSTRAP"))
            {
                AssetWarehouse.OnReady(new Action(WarehouseReady));
            }
        }

        private void WarehouseReady()
        {
            var bootstrapperComponent = GameObject.FindObjectOfType<SceneBootstrapper_Bonelab>();
            
            var crates = AssetWarehouse.Instance.GetCrates();

            foreach (Crate crate in crates)
            {
                if (crate.Title.Contains("BONELAB Hub"))
                {
                    bootstrapperComponent.VoidG114CrateRef = new LevelCrateReference(crate.Barcode.ID);
                    bootstrapperComponent.MenuHollowCrateRef = new LevelCrateReference(crate.Barcode.ID);
                }
            }
        }
    }
}
