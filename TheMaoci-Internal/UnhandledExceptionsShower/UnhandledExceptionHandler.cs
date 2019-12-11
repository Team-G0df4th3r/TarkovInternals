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
            float timestamp = 0;
            float prtscrn_timestamp = 0;
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
                DisplayDebugData = false,
                PrintScreened = false,
                FullBright_Enabled = false;
        #region Full bright
        public GameObject lightGameObject;
        public Light FullBrightLight;
        public bool _LightEnabled = true;
        public bool _LightCreated;
        public bool lightCalled;
        #endregion

        private bool[] saved_onGui = new bool[4] { false, false, false, false };
            private bool Recoil_Reducer = false;
            private int[] Players_Alive = new int[3];
        #endregion

        #region Awake
        private void Awake()
        {
            UnityEngine.Debug.unityLogger.logEnabled = false;
            // recalculate HealthPositions for diffrent ecreen sizes
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

            /*if (Input.GetKeyDown(KeyCode.Print)) {
                saved_onGui = new bool[4] { ongui_draw_esp, ongui_draw_grenades, ongui_draw_lootitem, ongui_draw_corpses };
                ongui_draw_esp = false;
                ongui_draw_grenades = false;
                ongui_draw_lootitem = false;
                ongui_draw_corpses = false;
                PrintScreened = true;
                prtscrn_timestamp = Time.time + 2f;
            }*/
 
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

        private void setFullBright_update()
        {
            if (_localPlayer != null)
            {
                if (FullBright_Enabled)
                {
                    Vector3 playerPos = _localPlayer.Transform.position;
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

        #region Update
        private void Update()
        {
            Hotkeys();
            //update diffrent then ongui - so be carefull
            /*if (PrintScreened)
            {
                if (prtscrn_timestamp < Time.time)
                {
                    ongui_draw_esp = saved_onGui[0];
                    ongui_draw_grenades = saved_onGui[1];
                    ongui_draw_lootitem = saved_onGui[2];
                    ongui_draw_corpses = saved_onGui[3];
                    PrintScreened = false;
                }
            }*/
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
                                if (temp.GetType() == Types.Corpse || temp.GetType() == Types.ObserverCorpse)
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
                                if (temp.GetType() == Types.LootItem || temp.GetType() == Types.LootItem)
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
                        setFullBright_update();
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
                    Drawing_Data.DrawDTG(_grenades, _localPlayer, Cons.RenderDistances[4]);
                }
                if (ongui_draw_lootitem)
                {
                    enabled = enabled + "L";
                    Drawing_Data.DrawDLI(_lootItems, Cons.RenderDistances[3]);
                }
                if (ongui_draw_corpses)
                {
                    enabled = enabled + "C";
                    Drawing_Data.DrawPDB(_corpses, Cons.RenderDistances[3]);
                }
                if (DisplayDebugData)
                {
                    EDS.Text(
                        new Rect(
                            Cons.CalcSizeW(1f),
                            Cons.CalcSizeH(300f),
                            Cons.boxSize[20],
                            Cons.boxSize[2]
                            ),
                        enabled,
                        Statics.Colors.White
                    );
                }
                setFullBright_onGui();
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
                EDS.L(new Vector2(Cons.CalcSizeW(184f), Cons.CalcSizeH(1065f)), new Vector2(Cons.CalcSizeW(245f), Cons.CalcSizeH(1065f)), Color.black, 20f);
            #endregion

        }
        #endregion
    }
}