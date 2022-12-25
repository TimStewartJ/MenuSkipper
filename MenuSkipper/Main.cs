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
			MelonLogger.Msg("WarehouseReady was called.", Color.green);
			SceneBootstrapper_Bonelab sceneBootstrapper_Bonelab = UnityEngine.Object.FindObjectOfType<SceneBootstrapper_Bonelab>();
            Il2CppSystem.Collections.Generic.List<Crate> crates = AssetWarehouse.Instance.GetCrates();
			foreach (Crate crate in crates)
			{
				bool flag = crate.Title.Contains("BONELAB Hub");
				if (flag)
				{
					MelonLogger.Msg("Flag was passed",Color.green);
					sceneBootstrapper_Bonelab.VoidG114CrateRef = new LevelCrateReference(crate.Barcode.ID);
					sceneBootstrapper_Bonelab.MenuHollowCrateRef = new LevelCrateReference(crate.Barcode.ID);
				}
			}
		}
    }
}
