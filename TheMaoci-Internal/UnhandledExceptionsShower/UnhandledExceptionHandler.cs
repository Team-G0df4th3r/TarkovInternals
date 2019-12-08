using System;
using System.IO;
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
    public class Cons {
        public static Vector2 InitialHealthBox = new Vector2(19f,32f);
        public static float[,] HealthPosition = new float[,] {
            { 49f, 0f },   // Head
            { 40f, 38f },  // Chest
            { 87f, 55f },  // Right Arm
            { 0f, 55f },   // Left Arm
            { 64f, 134f }, // Right Leg
            { 22f, 121f }, // Left leg
            { 36f, 71f }   // Stomach
        };
        public static float[] RenderDistances = new float[5] { 1000f, 750f, 500f, 250f, 100f };
        public static float[] UpdateIntervals = new float[6] { 1f, 2f, 5f, 10f, 25f, 50f };
        public static float[] boxSize = new float[21] { 0f, 10f, 20f, 30f, 40f, 50f, 60f, 70f, 80f, 90f, 100f, 110f, 120f, 130f, 140f, 150f, 160f, 170f, 180f, 190f, 200f };
        public static int ScreenWidth = Screen.width;
        public static int ScreenHeight = Screen.height;
        public static string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static float CalcSizeW(float size){ return ScreenWidth * size / 1920; }
        public static float CalcSizeH(float size){ return ScreenHeight * size / 1080; }
        public static string Health(Player LocalPlayer, EFT.HealthSystem.EBodyPart bodypart)
        {
            int health_curr = (int)LocalPlayer.HealthController.GetBodyPartHealth(bodypart).Current;
            int health_max = (int)LocalPlayer.HealthController.GetBodyPartHealth(bodypart).Maximum;
            if (health_curr > 0)
            {
                return health_curr.ToString() + "/" + health_max.ToString();
            }
            return "[OFF]";
        }
    }
    public class UnhandledException : MonoBehaviour
    {
        public UnhandledException() { }
        #region All Variables
            List<Player> _players;
            List<Throwable> _grenades;
            List<LootItem> _corpses;
            List<LootItem> _lootItems;
            Player _localPlayer;

            Type corpses_online = new ObservedCorpse().GetType();
            Type corpses_offline = new Corpse().GetType();
            Type LootItems_online = new ObservedLootItem().GetType();
            Type LootItems_offline = new LootItem().GetType();
            Color WhiteText = new Color(1f, 1f, 1f, 1f);
            float timestamp = 0;
            private Scene m_Scen;
            private string m_Scen_name;
            private GameObject GameObjectHolder;
            private GameWorld _GameWorld = null;
            private bool ongui_draw_esp = false,
                ongui_draw_corpses = false,
                ongui_draw_grenades = false,
                ongui_draw_lootitem = false,
                enableCrosshair = false,
                DisplayHelpInfo = false,
                DisplayCorpses = false,
                Switch_Colors = false,
                DisplayDebugData = false;
            private int AliveObject_count;
            private bool Recoil_Reducer = false;
            private int[] Players_Alive = new int[3];
            private float[,] HealthPosition = Cons.HealthPosition; // i dotn know if i can change Cons variables in runtime ...
        #endregion

        #region Awake
        private void Awake()
        {
            UnityEngine.Debug.unityLogger.logEnabled = false;
            // recalculate HealthPositions for diffrent ecreen sizes
            if (Cons.ScreenWidth != 1920 && Cons.ScreenHeight != 1080)
            {
                for (int x = 0; x < HealthPosition.Length; x++)
                {
                    HealthPosition[x, 0] = Cons.CalcSizeW(HealthPosition[x, 0]);
                    HealthPosition[x, 1] = Cons.CalcSizeH(HealthPosition[x, 1]);
                }
            }
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
                    ongui_draw_esp = !ongui_draw_esp;
                }
                if (Input.GetKeyUp(KeyCode.Keypad1))
                {
                    ongui_draw_corpses = !ongui_draw_corpses;
                }
                if (Input.GetKeyUp(KeyCode.Keypad7))
                {
                    ongui_draw_lootitem = !ongui_draw_lootitem;
                }
                if (Input.GetKeyUp(KeyCode.Keypad4))
                {
                    ongui_draw_grenades = !ongui_draw_grenades;
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Recoil_Reducer = !Recoil_Reducer;
                }
            #endregion

            #region Draw non sensitive data
                if (Input.GetKeyUp(KeyCode.Keypad5))
                {
                    enableCrosshair = !enableCrosshair;
                }
                if (Input.GetKeyUp(KeyCode.Keypad2))
                {
                    DisplayDebugData = !DisplayDebugData;
                }
                if (Input.GetKeyUp(KeyCode.Home))
                {
                    DisplayHelpInfo = !DisplayHelpInfo;
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

        private void RecoilReducer() {
            if(_localPlayer != null)
                if (Recoil_Reducer)
                {
                    _localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0.5f;
                }
                else if(_localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 1.0f)
                {
                    _localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 1.0f;
                }
        }

        #region - Help Menu -
        private void HelpMenu() {
            if (DisplayHelpInfo) {
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
                            Cons.CalcSizeW(500f),
                            Cons.CalcSizeH(200f + (20f * i)),
                            Cons.boxSize[20],
                            Cons.boxSize[2]
                            ),
                        text[i],
                        WhiteText
                        );
                }
            }
        }
        #endregion

        #region Displaying Usefull data (left, top corner)
        private void DisplayMenu() {
            if (DisplayDebugData)
            {
                if (_localPlayer != null)
                {
                    GUI.color = new Color(1f,1f,1f,.8f);
                    #region Alive Number Display
                    DF.DrawAlive(Players_Alive, new Vector2(1f, 185f));
                    #endregion
                    #region Player Health
                        DF.DrawRecoil(_localPlayer);
                        DF.HealthInfo(_localPlayer);
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
            if (enableCrosshair)
            {
                if (!_localPlayer.ProceduralWeaponAnimation.IsAiming)
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

        #region Update
        private void Update()
        {
            Hotkeys();
            //update diffrent then ongui - so be carefull
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
                        Players_Alive = new int[3] { 0, 0, 0 };
                        #region Players
                        if (ongui_draw_esp)
                        {
                            List<Player> tPlayer = new List<Player>();
                            foreach (Player p in _GameWorld.RegisteredPlayers)
                            {
                                if (p.PointOfView == EPointOfView.FirstPerson)
                                {
                                    _localPlayer = p;
                                }
                                else
                                {
                                    if (p.HealthController.IsAlive)
                                    {
                                        Players_Alive[0]++;
                                        float distance = FMath.FD(Camera.main.transform.position, p.Transform.position);
                                        if (distance <= 100f)
                                        {
                                            Players_Alive[1]++;
                                        }
                                        if (distance <= 250f)
                                        {
                                            Players_Alive[2]++;
                                        }
                                    }
                                    tPlayer.Add(p);
                                }
                            } _players = tPlayer;
                        }
                        #endregion
                        #region Grenades
                        if (ongui_draw_grenades)
                        {
                            List<Throwable> tGrenades = new List<Throwable>();
                            List<Throwable>.Enumerator grenades = _GameWorld.Grenades.GetValuesEnumerator().GetEnumerator();
                            while (grenades.MoveNext())
                            {
                                tGrenades.Add(grenades.Current);
                            } _grenades = tGrenades;
                        }
                        #endregion
                        #region Corpses
                        if (ongui_draw_corpses)
                        {
                            List<LootItem> tCorpses = new List<LootItem>();
                            List<LootItem>.Enumerator temporalCorpsesEnum = _GameWorld.LootItems.GetValuesEnumerator().GetEnumerator();
                            while (temporalCorpsesEnum.MoveNext())
                            {
                                    //"EFT.Interactive.ObservedCorpse" as online corpse
                                    //"EFT.Interactive.Corpse" as offline corpse
                                LootItem temp = temporalCorpsesEnum.Current;
                                if (temp.GetType() == corpses_online || temp.GetType() == corpses_offline)
                                    tCorpses.Add(temp);
                            } _corpses = tCorpses;
                        }
                        #endregion
                        #region AllLoot
                        if (ongui_draw_lootitem)
                        {
                            List<LootItem> tItems = new List<LootItem>();
                            List<LootItem>.Enumerator temporalItemsEnum = _GameWorld.LootItems.GetValuesEnumerator().GetEnumerator();
                            while (temporalItemsEnum.MoveNext())
                            {
                                    //"EFT.Interactive.ObservedLootItem" as online LootItem
                                    //"EFT.Interactive.LootItem" as offline LootItem
                                LootItem temp = temporalItemsEnum.Current;
                                if (temp.GetType() == LootItems_online || temp.GetType() == LootItems_offline)
                                    tItems.Add(temp);
                            } _lootItems = tItems;
                        }
                        #endregion
                        /*
                         - Items Patterns located on maps -
                         if (ongui_draw_lootitem)
                         {
                             List<GClass711> tItems = new List<GClass711>();
                             foreach (GClass711 li in _GameWorld.AllLoot)
                             {
                                 tItems.Add(li);
                             }
                             _lootItems = tItems;
                         }
                         */
                        RecoilReducer();
                    }
                }
            }
            else
            {
                Players_Alive = new int[3] { 0, 0, 0 };
                timestamp = 0f;
                _GameWorld = null;
                _players = null;
                _grenades = null;
                _corpses = null;
                _lootItems = null;
                
            }

        }
        #endregion

        #region OnGui
        private void OnGUI()
        {
            //Updates Each Frame
            this.m_Scen = SceneManager.GetActiveScene();
            this.m_Scen_name = m_Scen.name;
            GUI.color = Color.white;
            if (inMatch())
            {
                EDS.P(new Vector2(1f, 1f), Color.red, 1f);
                string enabled = "";
                if (ongui_draw_esp)
                {
                    enabled = enabled + "P";
                    Drawing_Data.DrawPlayers(_players, _localPlayer, Cons.RenderDistances[0], Switch_Colors, DisplayCorpses);
                }
                if (ongui_draw_grenades)
                {
                    enabled = enabled + "G";
                    Drawing_Data.DrawBombs(_grenades, _localPlayer, Cons.RenderDistances[4]);
                }
                if (ongui_draw_lootitem)
                {
                    enabled = enabled + "L";
                    Drawing_Data.DrawLogs(_lootItems, Cons.RenderDistances[3]);
                }
                if (ongui_draw_corpses)
                {
                    enabled = enabled + "C";
                    Drawing_Data.DrawLI(_corpses, Cons.RenderDistances[3]);
                }
                if (DisplayDebugData)
                {
                    GUI.Label(
                        new Rect(
                            Cons.CalcSizeW(1f),
                            Cons.CalcSizeH(300f),
                            Cons.boxSize[20],
                            Cons.boxSize[2]
                            ),
                        enabled
                    );
                }
            }
            else
            {
                ongui_draw_esp = false;
                ongui_draw_grenades = false;
                ongui_draw_lootitem = false;
                ongui_draw_corpses = false;
                GUI.color = Color.white;
                EDS.P(new Vector2(1f, 1f), Color.white, 1f);
            }
            DisplayMenu();
            HelpMenu();
            #region hide me nygga
            EDS.L(new Vector2(Cons.CalcSizeW(194f), Cons.CalcSizeH(1065f)), new Vector2(Cons.CalcSizeW(285f), Cons.CalcSizeH(1065f)), Color.black, 20f);
            #endregion

        }
        #endregion
    }
    class ErrorHandler
    {
        public static void Catch(string Func_Name, Exception exception)
        {
            string LocalStorage = Cons.MyDocuments + "\\_Maoci_Logs\\";
            if (!Directory.Exists(LocalStorage))
                Directory.CreateDirectory(LocalStorage);
            File.WriteAllText(LocalStorage + Func_Name + ".log", "ErrorStart >>>>>>>>>>>>>" + Environment.NewLine + exception.Message + "|" + Environment.NewLine + exception.Source + "|" + Environment.NewLine + exception.StackTrace + Environment.NewLine + "ErrorEnds <<<<<<<<<<<<<" + Environment.NewLine);
        }
    }
}