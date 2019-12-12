using System;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using EFT.Interactive;
using UnityEngine.SceneManagement;
using UnhandledExceptionHandler.Functions;

namespace UnhandledExceptionHandler
{
    class Logger
    {
        public static void Init()
        {
            new UnhandledException().Load();
        }
    }
    public class Main
    {
        public static List<Player> _players;
        public static List<Throwable> _grenades;
        public static List<LootItem> _corpses;
        public static List<LootItem> _lootItems;
        public static Player _localPlayer;
        public static List<Player> tPlayer;
        public static List<Throwable> tGrenades;
        public static List<LootItem> tCorpses;
        public static List<LootItem> tItems;
    }
    public class UnhandledException : MonoBehaviour
    {
        public UnhandledException() { }
        #region All Variables
            float timestamp = 0;
            private Scene m_Scen;
            private string m_Scen_name;
            private GameObject GameObjectHolder;
            private GameWorld _GameWorld = null;
            private bool Draw_ESP = false,
                Draw_Corpses = false,
                Draw_Grenades = false,
                Draw_Loot = false,
                Draw_Crosshair = false,
                Display_HelpInfo = false,
                Switch_Colors = false,
                DisplayDebugData = false,
                Spawn_FullBright = false,
                LOD_Controll = false,
                AimingAtNikita = false,
                Display_HUDGui = false;
        #region Full bright
            public GameObject lightGameObject;
            public Light FullBrightLight;
            public bool _LightEnabled = true;
            public bool _LightCreated;
            public bool lightCalled;
        #endregion

        private bool[] saved_onGui = new bool[4] { false, false, false, false };
        private bool Recoil_Reducer = false;
        private int Players_Alive_all = 0;
        private int Players_Alive_hun = 0;
        private int Players_Alive_ht2 = 0;
        #endregion

        #region Awake
        private void Awake()
        {
            UnityEngine.Debug.unityLogger.logEnabled = false;
            // recalculate shit for diffrent screen sizes
            Cons.RedalculateDistances();
            m_Scen = new Scene(); // inicializate this shit cause or random error spams
        }
        #endregion

        #region Load
        public void Load()
        {
            GameObjectHolder = new GameObject();
            GameObjectHolder.AddComponent<UnhandledException>();
            DontDestroyOnLoad(GameObjectHolder);

        }
        #endregion

        #region Clear
        private void Clear()
        {
        }
        #endregion

        #region Unload
        public void Unload()
        {
            Clear();
            GC.Collect();
            Destroy(GameObjectHolder);
            Resources.UnloadUnusedAssets();
            Destroy(this);
        }
        #endregion

        #region Hotkeys - function
        private void Hotkeys() {

            #region Draw sensitive data - no errors allowed here
                if (Input.GetKeyUp(KeyCode.Keypad0))
                {
                    Draw_ESP = !Draw_ESP;
                }
                if (Input.GetKeyUp(KeyCode.Keypad1))
                {
                    Draw_Corpses = !Draw_Corpses;
                }
                if (Input.GetKeyUp(KeyCode.Keypad7))
                {
                    Draw_Loot = !Draw_Loot;
                }
                if (Input.GetKeyUp(KeyCode.Keypad4))
                {
                    Draw_Grenades = !Draw_Grenades;
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Recoil_Reducer = !Recoil_Reducer;
                }
                if (Input.GetKeyDown(KeyCode.Keypad9)) {
                    Spawn_FullBright = !Spawn_FullBright;
                }
            #endregion

            #region Draw non sensitive data
                if (Input.GetKeyUp(KeyCode.Keypad5))
                {
                    Draw_Crosshair = !Draw_Crosshair;
                }
                if (Input.GetKeyUp(KeyCode.Keypad2))
                {
                    DisplayDebugData = !DisplayDebugData;
                }
                if (Input.GetKeyUp(KeyCode.Home))
                {
                    Display_HelpInfo = !Display_HelpInfo;
                }
                if (Input.GetKeyDown(KeyCode.Insert)) {
                    Display_HUDGui = !Display_HUDGui;
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

        #region Recoil Reducer
        private void RecoilReducer() {
            if(Main._localPlayer != null)
                if (Recoil_Reducer)
                {
                    if(Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 0.5f)
                        Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0.5f;
                }
                else if(Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 1.0f)
                {
                    Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 1.0f;
                }
        }
        #endregion

        #region - Help Menu -
        private void HelpMenu() {
            if (Display_HelpInfo) {
                string[] text = new string[10] {
                    "Help Menu: (Turn off/on 'Home' key)",
                    "'Num 0' - E.S.P - Players",
                    "'Num 1' - E.S.P - Corpses",
                    "'Num 2' - PlayerInfo - Health, Alive Objects, etc.",
                    "'Num 3' - Recoil 50%/100%",
                    "'Num 4' - E.S.P - Grenades",
                    "'Num 5' - Crosshair",
                    "'Num 7' - E.S.P - Loot (very laggy)",
                    "* make sure to not use it all the time",
                    ""
                };
                GUI.color = Color.white;
                for (int i = 0; i < text.Length; i++) {
                    EDS.Text(
                        new Rect(
                            500f,
                            200f + (20f * i),
                            Cons.boxSize.box_200,
                            Cons.boxSize.box_20
                            ),
                        text[i],
                        Statics.Colors.White
                        );
                }
            }
        }
        #endregion

        #region Displaying Usefull data (left, top corner)
        private void DisplayMenu() {
            if (DisplayDebugData)
            {
                if (Main._localPlayer != null)
                {
                    GUI.color = new Color(1f,1f,1f,.8f);
                    #region Alive Number Display
                        DF.DrawAlive(Players_Alive_all, Players_Alive_hun, Players_Alive_ht2, new Vector2(1f, 185f));
                    #endregion
                    #region Player Health
                        DF.DrawRecoil(Main._localPlayer);
                        //DF.HealthInfo(_localPlayer);
                    #endregion
                }
                #region Error Logs Enabled Display Message
                if (Debug.unityLogger.logEnabled == true)
                {
                    GUI.color = new Color(1f, 1f, 1f, .8f);
                    GUI.Label(new Rect(250f, Cons.ScreenHeight - 25f, 200f, 20f), "disable logger!");
                }
                #endregion
            }
            #region draw crosshair
            if (Draw_Crosshair)
            {
                if (!Main._localPlayer.ProceduralWeaponAnimation.IsAiming)
                {
                    GUI.color = Color.yellow;
                    EDS.P(new Vector2(Screen.width / 2f - 1f, Screen.height / 2f - 1f), Color.yellow, 2f);
                }
            }
            #endregion
        }
        #endregion

        #region isMatch()
        private bool inMatch()
        {
            // checking if you are not in main menu and all other endraid menu scenes
            return m_Scen_name != "EnvironmentUIScene" && m_Scen_name != "MenuUIScene" && m_Scen_name != "CommonUIScene" && m_Scen_name != "MainScene";
        }
        #endregion

        #region LOD Controller // TODO for testing only
        public LODGroup group;  
        private void SetLODToLow() {
            if (LOD_Controll)
            {
                group.ForceLOD(6);
            }
            else 
            {
                group.ForceLOD(0);
            }
        }
        #endregion

        #region FullBright
        private void setFullBright_update()
        {
            if (Main._localPlayer != null)
            {
                if (Spawn_FullBright)
                {
                    Vector3 playerPos = Main._localPlayer.Transform.position;
                    playerPos.y += 1f;

                    lightGameObject.transform.position = playerPos;
                    FullBrightLight.range = 1000;
                }
                else
                {
                    GameObject.Destroy(FullBrightLight);
                    lightCalled = false;
                }
            }
        }
        private void setFullBright_onGui()
        {
            if (!lightCalled)
            {
                lightGameObject = new GameObject("Fullbright");
                FullBrightLight = lightGameObject.AddComponent<Light>();
                FullBrightLight.color = Color.white;
                lightCalled = true;
            }
        }
        #endregion

        #region Update
        private void Update()
        {
            try
            {
                this.m_Scen = SceneManager.GetActiveScene();
                this.m_Scen_name = m_Scen.name;
                Hotkeys();

                if (Input.GetKeyDown(KeyCode.Mouse3)) // You can change it or create a GUI for change it in game
                {
                    AimingAtNikita = !AimingAtNikita;
                }

                if (inMatch())
                {
                    if (timestamp == 0f)
                        timestamp = Time.time + 20f;
                    if (Time.time > timestamp)
                    {
                        if (_GameWorld == null)
                        {
                            _GameWorld = FindObjectOfType<GameWorld>();
                        }
                        else
                        {
                            // thanks for LiquidAce for explaining this mess with GameWorld !!!
                            Players_Alive_all = 0;
                            Players_Alive_hun = 0;
                            Players_Alive_ht2 = 0;
                            #region Players
                            if (Draw_ESP)
                            {
                                Main.tPlayer = new List<Player>();
                                foreach (Player p in _GameWorld.RegisteredPlayers)
                                {
                                    if (p.PointOfView == EPointOfView.FirstPerson)
                                    {
                                        Main._localPlayer = p;
                                    }
                                    else
                                    {
                                        if (p.HealthController.IsAlive)
                                        {
                                            
                                            Players_Alive_all++;
                                            float distance = FMath.FD(Camera.main.transform.position, p.Transform.position);
                                            if (distance <= 100f)
                                            {
                                                Players_Alive_hun++;
                                            }
                                            if (distance > 100f && distance <= 250f)
                                            {
                                                Players_Alive_ht2++;
                                            }
                                            if (distance > 1f && distance <= Cons.RenderDistance.d_1000 && Camera.main.WorldToScreenPoint(p.Transform.position).z > 0.01f)
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
                            if (Draw_Grenades)
                            {
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
                            if (Draw_Corpses)
                            {
                                Main.tCorpses = new List<LootItem>();
                                List<LootItem>.Enumerator temporalCorpsesEnum = _GameWorld.LootItems.GetValuesEnumerator().GetEnumerator();
                                while (temporalCorpsesEnum.MoveNext())
                                {
                                    //"EFT.Interactive.ObservedCorpse" as online corpse
                                    //"EFT.Interactive.Corpse" as offline corpse
                                    LootItem temp = temporalCorpsesEnum.Current;
                                    if (temp.GetType() == Types.Corpse || temp.GetType() == Types.ObserverCorpse)
                                        Main.tCorpses.Add(temp);
                                }
                                Main._corpses = Main.tCorpses;
                                temporalCorpsesEnum.Dispose();
                            }
                            #endregion
                            #region AllLoot
                            if (Draw_Loot)
                            {
                                Main.tItems = new List<LootItem>();
                                List<LootItem>.Enumerator temporalItemsEnum = _GameWorld.LootItems.GetValuesEnumerator().GetEnumerator();
                                while (temporalItemsEnum.MoveNext())
                                {
                                    //"EFT.Interactive.ObservedLootItem" as online LootItem
                                    //"EFT.Interactive.LootItem" as offline LootItem
                                    LootItem temp = temporalItemsEnum.Current;
                                    if (temp.GetType() == Types.LootItem || temp.GetType() == Types.LootItem)
                                        Main.tItems.Add(temp);
                                }
                                Main._lootItems = Main.tItems;
                                temporalItemsEnum.Dispose();
                            }
                            #endregion
                            #region not used - loot pool map
                            /*
                             - Items Patterns located on maps -
                             if (Draw_Loot)
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
                            RecoilReducer();
                            setFullBright_update();
                        }
                    }
                }
                else
                {
                    Players_Alive_all = 0;
                    Players_Alive_hun = 0;
                    Players_Alive_ht2 = 0;
                    timestamp = 0f;
                    _GameWorld = null;
                    Main._players = null;
                    Main._grenades = null;
                    Main._corpses = null;
                    Main._lootItems = null;
                }
            }
            catch (Exception e) {
                ErrorHandler.Catch("MainLoopUpdate", e);
            }
        }
        #endregion

        #region -- DRAW GUI WINDOW --
        private void DrawHUDMenu() {
            Color guiBackup = GUI.color;
            GUI.color = Color.black;
            GUI.Box(new Rect(10f, 10f, 150f, 200f), "Unknown.Exception.Handler");
            GUI.color = Color.white;
            Vector2 initial = new Vector2(15f, 20f);
            Draw_ESP = GUI.Toggle(new Rect(initial.x, initial.y * 2, Cons.boxSize.box_100, Cons.boxSize.box_20), Draw_ESP, "E.S.P");
            Draw_Grenades = GUI.Toggle(new Rect(initial.x, initial.y * 3, Cons.boxSize.box_100, Cons.boxSize.box_20), Draw_Grenades, "Grenade");
            Draw_Corpses = GUI.Toggle(new Rect(initial.x, initial.y * 4, Cons.boxSize.box_100, Cons.boxSize.box_20), Draw_Corpses, "Dead Bodies");
            Draw_Loot = GUI.Toggle(new Rect(initial.x, initial.y * 5, Cons.boxSize.box_100, Cons.boxSize.box_20), Draw_Loot, "Map Loot");
            Draw_Crosshair = GUI.Toggle(new Rect(initial.x, initial.y * 6, Cons.boxSize.box_100, Cons.boxSize.box_20), Draw_Crosshair, "Crosshair");
            Spawn_FullBright = GUI.Toggle(new Rect(initial.x, initial.y * 7, Cons.boxSize.box_100, Cons.boxSize.box_20), Spawn_FullBright, "FullBright");
            LOD_Controll = GUI.Toggle(new Rect(initial.x, initial.y * 7, Cons.boxSize.box_100, Cons.boxSize.box_20), LOD_Controll, "LOD Control");
            DisplayDebugData = GUI.Toggle(new Rect(initial.x, initial.y * 8, Cons.boxSize.box_100, Cons.boxSize.box_20), DisplayDebugData, "Player Data");

            //FINSHED
            GUI.color = guiBackup;
        }
        #endregion

        #region OnGui
        private void OnGUI()
        {
            try {
                //Updates Each Frame
                if (inMatch())
                {
                    EDS.P(new Vector2(1f, 1f), Color.red, 1f);
                    string enabled = "";
                    if (Draw_ESP)
                    {
                        enabled = enabled + "P";
                        Drawing_Data.DrawPlayers(Main._players, Main._localPlayer, Cons.RenderDistance.d_1000, Switch_Colors);
                    }
                    if (Draw_Grenades)
                    {
                        enabled = enabled + "G";
                        Drawing_Data.DrawDTG(Main._grenades, Main._localPlayer, Cons.RenderDistance.d_100);
                    }
                    if (Draw_Loot)
                    {
                        enabled = enabled + "L";
                        Drawing_Data.DrawDLI(Main._lootItems, Cons.RenderDistance.d_250);
                    }
                    if (Draw_Corpses)
                    {
                        enabled = enabled + "C";
                        Drawing_Data.DrawPDB(Main._corpses, Cons.RenderDistance.d_250);
                    }
                    if (AimingAtNikita) {
                        enabled = enabled + "A";
                        AFunc.TargetLock(Main._players, Main._localPlayer, 5, Cons.RenderDistance.d_250, 100);
                    }
                    if (DisplayDebugData)
                    {
                        EDS.Text(
                            new Rect(
                                1f,
                                300f,
                                Cons.boxSize.box_200,
                                Cons.boxSize.box_20
                                ),
                            enabled,
                            Statics.Colors.White
                        );
                    }
                    SetLODToLow(); // TODO for testing only
                    setFullBright_onGui();
                }
                else
                {
                    Draw_ESP = false;
                    Draw_Grenades = false;
                    Draw_Loot = false;
                    Draw_Corpses = false;
                    GUI.color = Color.white;
                    EDS.P(new Vector2(1f, 1f), Color.white, 1f);
                }
                DisplayMenu();
                HelpMenu();
                DrawHUDMenu();
                #region hide me nygga - DISABLED CAUSE SHIT
                    //EDS.L(new Vector2(184f, 1065f), new Vector2(245f, 1065f), Color.black, 20f);
                #endregion
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("MainLoopUpdate", e);
            }
        }
        #endregion
    }
}