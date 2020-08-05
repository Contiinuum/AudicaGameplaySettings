using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using Harmony;
using System.Collections;

namespace AudicaModding
{
    public class AudicaMod : MelonMod
    {
        public static MenuState.State menuState;
        public static MenuState.State oldMenuState;

        public static GameObject shellInstance = null;
        public static ShellPage SP = null;
        public static OptionsMenu OM = null;
        public static Vector3 DebugTextPosition = new Vector3(0f, -1f, 0f);

        public static bool panelCreated = false;

        public static class BuildInfo
        {
            public const string Name = "GameplaySettings";  // Name of the Mod.  (MUST BE SET)
            public const string Author = "Continuum"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "0.1.0"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
        }
        
		 public override void OnApplicationStart()
         {
            HarmonyInstance instance = HarmonyInstance.Create("AudicaMod");
            Hooks.ApplyHooks(instance);
         }

        public static void CreatePanel()
        {
            panelCreated = true;
            GameObject HMXshellpage = GameObject.Find("ShellPage_Settings");
            shellInstance = GameObject.Instantiate(HMXshellpage, DebugTextPosition, Quaternion.Euler(0, 100, 0));
            shellInstance.name = "Custom_Page";
            SP = shellInstance.GetComponent<ShellPage>();

            Transform page = shellInstance.transform.GetChild(0);

            for (int i = 0; i < page.childCount; i++)
            {
                Transform child = page.GetChild(i);
                if (child.gameObject.name.Contains("backParent"))
                {
                    child.gameObject.SetActive(false);
                }
            }

            Transform ShellPanelCenter = page.transform.GetChild(0);
            Transform Settings = ShellPanelCenter.transform.GetChild(2);
            Transform Options = Settings.transform.GetChild(0);
            OM = Options.GetComponent<OptionsMenu>();

            MelonCoroutines.Start(SetPanelActive(true));
        }

        public static IEnumerator SetPanelActive(bool active)
        {          
            SP.SetPageActive(active, true);
            if (active)
            {
                yield return new WaitForSeconds(0.005f);
                OM.ShowPage(OptionsMenu.Page.Gameplay);
            }
            yield return null;        
        }
    }
}
















































