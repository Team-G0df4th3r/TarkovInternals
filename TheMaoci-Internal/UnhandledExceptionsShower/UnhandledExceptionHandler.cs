using System;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using EFT.Interactive;
using UnityEngine.SceneManagement;
using System.Reflection;

namespace UnhandledException
{
    public class UnhandledException : MonoBehaviour
    {
        public UnhandledException() { }

        #region Vital Variables
            float timestamp = 0;
            private GameObject GameObjectHolder;
            private GameWorld _GameWorld = null;
        #endregion

        #region [MAIN] - Awake
        private void Awake()
        {
            UnityEngine.Debug.unityLogger.logEnabled = false;
            // recalculate shit for diffrent screen sizes
            Constants.Locations.RedalculateDistances();
            Main.G_Scene.Game_Scene = new Scene(); // inicializate this shit cause or random error spams
        }
        #endregion

        #region [MAIN] - Load
        public void Load()
        {
            GameObjectHolder = new GameObject();
            GameObjectHolder.AddComponent<UnhandledException>();
            DontDestroyOnLoad(GameObjectHolder);

        }
        #endregion

        #region [FUNCTION] - Clear
        private void Clear()
        {
            Main.Clear();
            _GameWorld = null;
        }
        #endregion

        #region [MAIN] - Unload
        public void Unload()
        {
            //not used cause we are pure internal - aka no injections
            Clear();
            GC.Collect();
            GameObjectHolder.DestroyAllChildren();
            Destroy(GameObjectHolder);
            Resources.UnloadUnusedAssets();
            Destroy(this);
        }
        #endregion

        #region [FUNCTION] - Hotkeys - function
        private void Hotkeys() {

            #region Draw sensitive data - no errors allowed here
            if (Input.GetKeyUp(KeyCode.Keypad0))
            {
                Switches.Draw_ESP = !Switches.Draw_ESP;
            }
            if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                Switches.Draw_Corpses = !Switches.Draw_Corpses;
            }
            if (Input.GetKeyUp(KeyCode.Keypad7))
            {
                Switches.Draw_Loot = !Switches.Draw_Loot;
            }
            if (Input.GetKeyUp(KeyCode.Keypad4))
            {
                Switches.Draw_Grenades = !Switches.Draw_Grenades;
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                Switches.Recoil_Reducer = !Switches.Recoil_Reducer;
            }
            if (Input.GetKeyDown(KeyCode.Keypad9)) {
                Switches.Spawn_FullBright = !Switches.Spawn_FullBright;
            }
            if (Input.GetKeyDown(KeyCode.Mouse3)) // You can change it or create a GUI for change it in game
            {
                Switches.AimingAtNikita = !Switches.AimingAtNikita;
            }
            if (Switches.IKnowWhatImDoing) {
                if (Input.GetKeyDown(KeyCode.F10)) {
                    if (Input.GetKeyDown(KeyCode.F10) && Main._localPlayer != null)
                    {
                        // we can add an prevention to too fast teleporting adding like once per second etc.
                        //if (Time.time >= _secTime) {
                        //_secTime = Time.time + 1f;
                        Main._localPlayer.Transform.position = Main._localPlayer.Transform.position + Camera.main.transform.forward * 1f;
                        //}
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F11)) {
                GClass433.SetFov(120f, 1f);
                GClass433.ApplyFoV(120, 100, 120);
            }
            #endregion

            #region Draw non sensitive data
            if (Input.GetKeyUp(KeyCode.Keypad5))
            {
                Switches.Draw_Crosshair = !Switches.Draw_Crosshair;
            }
            if (Input.GetKeyUp(KeyCode.Keypad2))
            {
                Switches.DisplayHelpPlayerInfo = !Switches.DisplayHelpPlayerInfo;
            }
            if (Input.GetKeyUp(KeyCode.Home))
            {
                Switches.Display_HelpInfo = !Switches.Display_HelpInfo;
            }
            if (Input.GetKeyDown(KeyCode.Insert)) {
                Switches.Display_HUDGui = !Switches.Display_HUDGui;
            }
            #endregion

            /* unused yet
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                Debug.unityLogger.logEnabled = !Debug.unityLogger.logEnabled;
            }
            */
        }
        #endregion

        #region [MAIN] - Update
        private void Update()
        {
            try
            {
                Main.G_Scene.SaveScene();
                Hotkeys();
                // make sure scene is map scene and is loaded and ready
                if (Main.G_Scene.isInMatch() && Main.G_Scene.isActiveAndLoaded())
                {
                    
                    // delay start of script for 20 seconds on start of match cause match is starting way before deploy is displaying - it will cause less errors displaying
                    // lower time from 20f if its holdup too long
                    if (timestamp == 0f)
                        timestamp = Time.time + 20f;
                    if (Time.time > timestamp)
                    {
                        if (_GameWorld == null)
                        {
                            //done only once on start of each map
                            _GameWorld = FindObjectOfType<GameWorld>();
                        }
                        else
                        {
                            // LiquidAce and his idea of grabbing data directly from GameWorld cause its better and less retarded - Thanks Mate
                            #region Players
                            if (Switches.Draw_ESP)
                            {
                                /* make sure to not call it all the time but only on ESP Enabled
                                 * it creates list of alive objects (which is propably only alive objects but i check it anyway here)
                                 * also it creates class with players count in diffrent distances between you
                                 */
                                AliveCount.Reset();
                                Main.tPlayer = new List<Player>();
                                foreach (Player p in _GameWorld.RegisteredPlayers)
                                {
                                    if (p.PointOfView == EPointOfView.FirstPerson)
                                    {
                                        Main._localPlayer = p;
                                        Cons.LocalPlayer.Weapon.SetRecoil();
                                        Cons.LocalPlayer.Weapon.UpdateAmmo();
                                        Cons.LocalPlayer.Status.UpdateStatus();
                                    }
                                    else
                                    {
                                        if (p.HealthController.IsAlive)
                                        {
                                            
                                            AliveCount.All++;
                                            float distance = FastMath.FD(Camera.main.transform.position, p.Transform.position);
                                            if (distance <= 100f)
                                            {
                                                AliveCount.dist_0_100++;
                                            }
                                            if (distance > 100f && distance <= 250f)
                                            {
                                                AliveCount.dist_100_250++;
                                            }
                                            if (distance > 1f && distance <= Constants.Locations.RenderDistance.d_1000 && Camera.main.WorldToScreenPoint(p.Transform.position).z > 0.01f)
                                            {
                                                Main.tPlayer.Add(p);
                                            }
                                        }
                                    }
                                }
                                Main._players = Main.tPlayer;
                            }
                            #endregion
                            #region Grenades
                            if (Switches.Draw_Grenades)
                            {
                                /* Grenade scanner - scans for grenades if this function is enabled
                                 * also added as much RAM free functions as possible
                                 */
                                Main.tGrenades = new List<Throwable>();
                                List<Throwable>.Enumerator grenades = _GameWorld.Grenades.GetValuesEnumerator().GetEnumerator();
                                while (grenades.MoveNext())
                                {
                                    Main.tGrenades.Add(grenades.Current);
                                }
                                Main._grenades = Main.tGrenades;
                                grenades.Dispose();
                            }
                            #endregion
                            #region Corpses
                            if (Switches.Draw_Corpses)
                            {
                                /* Corspes scanner - scans for corpses in the map and creates a list of them
                                 * also contains RAM free things to not cause out of memory violations 0xc0...05
                                 * also online Corpses are diffrent then Offline Corpses and i check for both of them
                                 * EFT.Interactive.ObservedCorpse - as online corpse
                                 * EFT.Interactive.Corpse - as offline corpse
                                 */
                                Main.tCorpses = new List<LootItem>();
                                List<LootItem>.Enumerator temporalCorpsesEnum = _GameWorld.LootItems.GetValuesEnumerator().GetEnumerator();
                                while (temporalCorpsesEnum.MoveNext())
                                {
                                    LootItem temp = temporalCorpsesEnum.Current;
                                    if (temp.GetType() == Types.Corpse || temp.GetType() == Types.ObserverCorpse)
                                        Main.tCorpses.Add(temp);
                                }
                                Main._corpses = Main.tCorpses;
                                temporalCorpsesEnum.Dispose();
                            }
                            #endregion
                            #region AllLoot
                            if (Switches.Draw_Loot)
                            {
                                /* Map Loot Scanner - scans and creates a list of loot on map - RAM free as always
                                 * EFT.Interactive.ObservedLootItem - as online LootItem
                                 * EFT.Interactive.LootItem - as offline LootItem
                                 */
                                Main.tItems = new List<LootItem>();
                                List<LootItem>.Enumerator temporalItemsEnum = _GameWorld.LootItems.GetValuesEnumerator().GetEnumerator();
                                while (temporalItemsEnum.MoveNext())
                                {
                                    LootItem temp = temporalItemsEnum.Current;
                                    if (temp.GetType() == Types.LootItem || temp.GetType() == Types.ObservedLootItem)
                                    {
                                        if(Cons.LootSearcher == "")
                                            Main.tItems.Add(temp);
                                        try
                                        {
                                            if (Cons.LootSearcher == temp.Item.ShortName.Localized())
                                            {
                                                Main.tItems.Add(temp);
                                            }
                                        }
                                        catch (Exception e) {}
                                    }
                                }
                                Main._lootItems = Main.tItems;
                                temporalItemsEnum.Dispose();
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
                            // recoil reducer (reduces recoil 50% - 100%)
                            FUNC_Addons.Update.RecoilReducer();
                            FUNC_Addons.Update.FullBright();
                        }
                    }
                }
                else
                {
                    AliveCount.Reset();
                    Main.Clear();
                    timestamp = 0f;
                    _GameWorld = null;
                }
            }
            catch (Exception e) {
                ErrorHandler.Catch("MainLoopUpdate", e);
            }
        }
        #endregion

        #region [MAIN] - OnGui
        private void OnGUI()
        {
            try {
                FUNC_Additional_Drawing.DisplayMenu();
                if (Switches.Display_HelpInfo)
                {
                    FUNC_Additional_Drawing.HelpMenu();
                }
                if (Switches.Display_HUDGui)
                    FUNC_Additional_Drawing.DrawHUDMenu();
                //Updates Each Frame
                if (Main.G_Scene.isInMatch() && Main.G_Scene.isActiveAndLoaded())
                {
                    Drawing.P(new Vector2(1f, 1f), Color.red, 1f);
                    string enabled = "";
                    if (Switches.Draw_ESP)
                    {
                        enabled = enabled + "P";
                        FUNC_DrawObjects.DrawPlayers(Main._players, Main._localPlayer);
                    }
                    if (Switches.Draw_Grenades)
                    {
                        enabled = enabled + "G";
                        FUNC_DrawObjects.DrawDTG(Main._grenades, Main._localPlayer);
                    }
                    if (Switches.Draw_Loot)
                    {
                        enabled = enabled + "L";
                        FUNC_DrawObjects.DrawDLI(Main._lootItems);
                    }
                    if (Switches.Draw_Corpses)
                    {
                        enabled = enabled + "C";
                        FUNC_DrawObjects.DrawPDB(Main._corpses);
                    }
                    if (Switches.AimingAtNikita) {
                        enabled = enabled + "A";
                        FUNC_AimHelper.Aimbot_Method();
                        Drawing.Circle(Cons.ScreenWidth, Cons.ScreenHeight, Cons.Aim.AAN_FOV);
                    }
                    if (Switches.Draw_ESP || Switches.Draw_Grenades || Switches.Draw_Loot || Switches.Draw_Corpses || Switches.AimingAtNikita)
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
                    FUNC_Addons.OnGUI.FullBright();
                }
                else
                {
                    Switches.SetToOff();
                    Drawing.P(new Vector2(1f, 1f), Color.white, 1f);
                }

                #region Help Me... Nygga - DISABLED CAUSE SHIT
                    //Drawing.L(new Vector2(184f, 1065f), new Vector2(245f, 1065f), Color.black, 20f);
                #endregion
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("MainLoopUpdate", e);
            }
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
    public class Main
    {
        public class G_Scene
        {
            public static Scene Game_Scene;
            private static string GetSceneName() {
                return Game_Scene.name;
            }
            public static bool isActiveAndLoaded() {
                return Game_Scene.isLoaded;
            }
            public static bool isInMatch() {
                return  GetSceneName() != "EnvironmentUIScene" && 
                        GetSceneName() != "MenuUIScene" && 
                        GetSceneName() != "CommonUIScene" && 
                        GetSceneName() != "MainScene" && 
                        GetSceneName() != "";
            }
            public static void SaveScene() {
                Game_Scene = SceneManager.GetActiveScene();
            }
        }
        public static List<Player> _players;
        public static List<Throwable> _grenades;
        public static List<LootItem> _corpses;
        public static List<LootItem> _lootItems;
        public static Player _localPlayer;
        public static List<Player> tPlayer;
        public static List<Throwable> tGrenades;
        public static List<LootItem> tCorpses;
        public static List<LootItem> tItems;
        public static void Clear()
        {
            _players = null;
            _grenades = null;
            _corpses = null;
            _lootItems = null;
            _localPlayer = null;
            tPlayer = null;
            tGrenades = null;
            tCorpses = null;
            tItems = null;
        }
    }
    public class Switches
    {
        public static bool Draw_ESP = false;
        public static bool Draw_Corpses = false;
        public static bool Draw_Grenades = false;
        public static bool Draw_Loot = false;
        public static bool Draw_Crosshair = false;
        public static bool Display_HelpInfo = false;
        public static bool Switch_Colors = false;
        public static bool DisplayHelpPlayerInfo = false;
        public static bool Spawn_FullBright = false;
        public static bool LOD_Controll = false;
        public static bool AimingAtNikita = false;
        public static bool Display_HUDGui = false;
        public static bool Recoil_Reducer = false;
        public static bool Aim_Smoothing = true;
        public static bool StreamerMode = false;
        public static bool SnapLines = false;
        public static bool IKnowWhatImDoing = false;
        public static bool Dump = false;
        public static void SetToOff()
        {
            Draw_ESP = false;
            Draw_Corpses = false;
            Draw_Grenades = false;
            Draw_Loot = false;
            Draw_Crosshair = false;
            Display_HelpInfo = false;
            Switch_Colors = false;
            DisplayHelpPlayerInfo = false;
            Spawn_FullBright = false;
            LOD_Controll = false;
            AimingAtNikita = false;
            Display_HUDGui = false;
            Recoil_Reducer = false;
        }
    }
    public class FullBright
    {
        public static GameObject lightGameObject;
        public static Light FullBrightLight;
        public static bool _LightEnabled = true;
        public static bool _LightCreated;
        public static bool lightCalled;
    }
    public class AliveCount
    {
        public static int All = 0;
        public static int dist_0_25 = 0;
        public static int dist_25_50 = 0;
        public static int dist_0_100 = 0;
        public static int dist_100_250 = 0;
        public static int dist_250_1000 = 0;
        public static void Reset()
        {
            All = 0;
            dist_0_25 = 0;
            dist_25_50 = 0;
            dist_0_100 = 0;
            dist_100_250 = 0;
            dist_250_1000 = 0;
        }
    }
}