using System;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using EFT.UI;
using EFT.Interactive;
using UnityEngine.SceneManagement;
using System.Reflection;
using System.IO;
#pragma warning disable CS0168
namespace UnhandledException
{
    public class UnhandledException : MonoBehaviour
    {
        public UnhandledException() { }

        #region Vital Variables
            float timestamp = 0; // starting match delay 20 seconds by default
            float delay = 20f;
        #endregion

        #region [MAIN] - Awake
        private void Awake()
        {
            UnityEngine.Debug.unityLogger.logEnabled = false;
            // recalculate shit for diffrent screen sizes
            if (Cons.ScreenWidth.Full != 1920 && Cons.ScreenHeight.Full != 1080)
            {
                Constants.Locations.RedalculateDistances();
            }
            Cons.Main.G_Scene.Game_Scene = new Scene(); // inicializate this shit cause or random error spams
        }
        #endregion

        #region [MAIN] - Load
        public void Load()
        {
            Cons.Main.GameObjectHolder = new GameObject();
            Cons.Main.GameObjectHolder.AddComponent<UnhandledException>();
            DontDestroyOnLoad(Cons.Main.GameObjectHolder);

        }
        #endregion

        #region [FUNCTION] - Clear
        private void Clear()
        {
            Cons.Main.Clear();
            Cons.Main._GameWorld = null;
        }
        #endregion

        #region [MAIN] - Unload
        public void Unload()
        {
            //not used cause we are pure internal - aka no injections
            Clear();
            GC.Collect();
            Cons.Main.GameObjectHolder.DestroyAllChildren();
            Destroy(Cons.Main.GameObjectHolder);
            Resources.UnloadUnusedAssets();
            Destroy(this);
        }
        #endregion

        /*
         * AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(AssemblyDirectory, "metalgui"));
         * this.skin = bundle.LoadAsset<GUISkin>("MetalGUISkin");
         * GUI.skin = this.skin;
         */

        float LWIAY_timer = 0f;
        #region [MAIN] - Update
        private void Update()
        {
            try
            {
                Cons.Main.G_Scene.SaveScene();
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("SaveScene", e);
            }
            try 
            { 
                FUNC.Update.Hotkeys();
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("Hotkeys", e);
            }
            try 
            { 
                FUNC.Update.Buttons();
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("Buttons", e);
            }
            // make sure scene is map scene and is loaded and ready
            if (Cons.Main.G_Scene.isInMatch() && Cons.Main.G_Scene.isActiveAndLoaded() && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
            {
                if (Cons.Switches.ChangeSessionID)
                {
                    if (LWIAY_timer < Time.time)
                    {
                        MonoBehaviourSingleton<PreloaderUI>.Instance.SetSessionId("NotForSale"); // use this for memes only
                        LWIAY_timer = Time.time + 1f;
                    }
                }
                else
                {
                    if (Cons.Switches.StreamerMode)
                    {
                        MonoBehaviourSingleton<PreloaderUI>.Instance.SetStreamMode(true); // this should disable this stupid text ;)
                    }
                    else
                    {
                        // MonoBehaviourSingleton<PreloaderUI>.Instance.SetStreamMode(false); // this will not work cause of braindead BSG coders
                    }
                }
                // delay start of script for 20 seconds on start of match cause match is starting way before deploy is displaying - it will cause less errors displaying
                // lower time from 20f if its holdup too long
                if (timestamp == 0f)
                    timestamp = Time.time + delay;

                if (Time.time > timestamp)
                {
                    if (Cons.Main._GameWorld == null)
                    {
                        //done only once on start of each map
                        try
                        {
                            Cons.Main._GameWorld = FindObjectOfType<GameWorld>();
                                
                        }
                        catch (Exception e)
                        {
                            ErrorHandler.Catch("Get_GameWorld", e);
                        }
                    }
                    else
                    {
                        // LiquidAce and his idea of grabbing data directly from GameWorld cause its better and less retarded - Thanks Mate
                        #region Players
                        if (Cons.Switches.Draw_ESP)
                        {
                            /* make sure to not call it all the time but only on ESP Enabled
                                * it creates list of alive objects (which is propably only alive objects but i check it anyway here)
                                * also it creates class with players count in diffrent distances between you
                                */
                            E5P.Players.Update.PlayerList();
                        }
                        #endregion
                        #region Grenades
                        if (Cons.Switches.Draw_Grenades)
                        {
                            /* Grenade scanner - scans for grenades if this function is enabled
                                * also added as much RAM free functions as possible
                                */
                            E5P.Throwables.Update.ThrowableList();
                        }
                        #endregion
                        #region Corpses
                        if (Cons.Switches.Draw_Corpses)
                        {
                            /* Corspes scanner - scans for corpses in the map and creates a list of them
                                * also contains RAM free things to not cause out of memory violations 0xc0...05
                                * also online Corpses are diffrent then Offline Corpses and i check for both of them
                                * EFT.Interactive.ObservedCorpse - as online corpse
                                * EFT.Interactive.Corpse - as offline corpse
                                */
                            E5P.Players.Update.DeadBodyList();
                        }
                        #endregion
                        #region AllLoot
                        if (Cons.Switches.Draw_Loot)
                        {
                            /* Map Loot Scanner - scans and creates a list of loot on map - RAM free as always
                                * EFT.Interactive.ObservedLootItem - as online LootItem
                                * EFT.Interactive.LootItem - as offline LootItem
                                */
                            E5P.Items.Update.ItemsList();
                        }
                        if (Cons.Switches.Draw_Containers)
                        {
                            E5P.Items.Update.ContainerList();
                        }
                        #endregion
                        #region Exfiltrations
                        if (Cons.Switches.Draw_Exfil)
                        {
                            /* Exfiltration - scans for Exfils in the map and creates a list of them
                                * also contains RAM free things to not cause out of memory violations 0xc0...05
                                * also online Corpses are diffrent then Offline Corpses and i check for both of them
                                */
                            E5P.Exfils.Update.ExfilsList();
                        }
                        #endregion
                        #region not used - loot pool map
                        /*
                        - Items Patterns located on maps - also displays invisible loot deleted from map and broken loot below maps
                        if (Switches.Draw_Loot)
                        {
                            List<GClass711> tItems = new List<GClass711>();
                            foreach (GClass711 li in _GameWorld.AllLoot)
                            {
                                tItems.Add(li);
                            }
                            _lootItems = tItems;
                        }
                        */
                    #endregion
                    // recoil reducer (break recoil animations)
                    Cons.LocalPlayer.Weapon.NoRecoil();
                    if (Cons.Main._localPlayer != null)
                        Cons.AimPoint = Raycast.BarrelRaycast().point;

                    //FUNC.Update.RecoilReducer(); // incase we dont use that so we can leave it like this for now
                    try
                    { 
                            FUNC.Update.FullBright();
                        }
                        catch (Exception e)
                        {
                            ErrorHandler.Catch("Update_FullBright", e);
                        }
                    }
                }
            }
            else
            {
                Cons.AliveCount.Reset();
                Cons.Main.Clear();
                timestamp = 0f;
                Cons.Main._GameWorld = null;
            }
        }
        #endregion

        #region [MAIN] - OnGui
        private void OnGUI()
        {
            FUNC_Additional_Drawing.DisplayMenu();
            if (Cons.Switches.Display_HelpInfo)
            {
                FUNC_Additional_Drawing.HelpMenu();
            }
            if (Cons.Switches.Display_HUDGui)
            {
                FUNC_Additional_Drawing.DrawHUDMenu();
            }
            //Updates Each Frame
            if (Cons.Main.G_Scene.isInMatch() && Cons.Main.G_Scene.isActiveAndLoaded())
            {
                Drawing.P(new Vector2(1f, 1f), Color.red, 1f);
                string enabled = "";
                if (Cons.Switches.Draw_ESP)
                {
                    enabled = enabled + "P";
                    E5P.Players.Draw.Players();
                }
                if (Cons.Switches.Draw_Grenades)
                {
                    enabled = enabled + "G";
                    E5P.Throwables.Draw.Grenades();
                }
                if (Cons.Switches.Draw_Loot)
                {
                    enabled = enabled + "L";
                    E5P.Items.Draw.Items();
                }
                if (Cons.Switches.Draw_Corpses)
                {
                    enabled = enabled + "C";
                    E5P.Players.Draw.DeadBodies();
                }
                if (Cons.Switches.Draw_Exfil)
                {
                    enabled = enabled + "E";
                    E5P.Exfils.Draw.Exfils();
                }
                if (Cons.Switches.Draw_Containers)
                {
                    enabled = enabled + "K";
                    E5P.Items.Draw.Containers();
                }

                if (Cons.Switches.AimingAtNikita) 
                {
                    enabled = enabled + "A";
                    FUNC_AimHelper.Aimbot_Method();
                    //Drawing.Circle(Cons.ScreenWidth, Cons.ScreenHeight, Cons.Aim.AAN_FOV);
                }
                if (Cons.Switches.Draw_ESP || Cons.Switches.Draw_Grenades || Cons.Switches.Draw_Loot || Cons.Switches.Draw_Corpses || Cons.Switches.AimingAtNikita)
                {
                    Drawing.Text(
                        new Rect(
                            1f,
                            300f,
                            Constants.Locations.boxSize.box_200,
                            Constants.Locations.boxSize.box_20
                            ),
                        enabled,
                        Constants.Colors.White
                    );
                }
                //SetLODToLow(); // TODO for testing only
                try
                { 
                    FUNC.OnGUI.FullBright();
                }
                catch (Exception e)
                {
                    ErrorHandler.Catch("Draw_FullBright", e);
                }
            }
            else
            {
                Cons.Switches.SetToOff();
                Drawing.P(new Vector2(1f, 1f), Color.white, 1f);
            }
            #region Help Me... Nygga - DISABLED CAUSE SHIT
                    //Drawing.L(new Vector2(184f, 1065f), new Vector2(245f, 1065f), Color.black, 20f);
                #endregion
        }
        #endregion
    }
    public class Exceptlon
    {
        public static void Catch()
        {
            new UnhandledException().Load();
        }
    }
    
}
#pragma warning disable CS0168