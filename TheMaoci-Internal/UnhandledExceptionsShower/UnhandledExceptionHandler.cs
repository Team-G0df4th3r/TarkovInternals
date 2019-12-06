using System;
using System.IO;
using System.Collections.Generic;
//using System.Runtime.InteropServices;
using UnityEngine;
using EFT;
using EFT.Interactive;
using UnityEngine.SceneManagement;
using UnhandledExceptionHandler.Functions;


namespace UnhandledExceptionHandler
{
    class ErrorHandler
    {
        public static void Catch(string Func_Name, Exception exception)
        {
            string LocalStorage = Cons.MyDocuments + "\\_Maoci_Logs\\";
            if(!Directory.Exists(LocalStorage))
                Directory.CreateDirectory(LocalStorage);
            File.WriteAllText(LocalStorage + Func_Name + ".log", "ErrorStart >>>>>>>>>>>>>" + Environment.NewLine + exception.Message + "|" + Environment.NewLine + exception.Source + "|" + Environment.NewLine + exception.StackTrace + Environment.NewLine + "ErrorEnds <<<<<<<<<<<<<" + Environment.NewLine);
        }
    }
    class ErrorLog
    {
        public static void Start()
        {
            new UnhandledException().Load();
        }
    }
    public class Cons {
        public static float[,] HealthPosition = new float[,] {
            { 68f, 32f },   // Head
            { 59f, 70f },   // Chest
            { 106f, 87f },  // Right Arm
            { 19f, 87f },   // Left Arm
            { 83f, 166f },  // Right Leg
            { 41f, 153f },  // Left leg
            { 55f, 103f }   // Stomach
        };
        public static float[] RenderDistances = new float[5] { 1000f, 750f, 500f, 250f, 100f };
        public static float[] UpdateIntervals = new float[6] { 1f, 2f, 5f, 10f, 25f, 50f };
        public static float[] boxSize = new float[20] { 10f, 20f, 30f, 40f, 50f, 60f, 70f, 80f, 90f, 100f, 110f, 120f, 130f, 140f, 150f, 160f, 170f, 180f, 190f, 200f };
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
        private GameObject GameObjectHolder;
        //VisorEffect visor = new VisorEffect();
        //visor.Intensity = 0f; //should remove visior texture - but it dont
        //private PlayerCameraController PCC = new PlayerCameraController();
        
        private Scene m_Scen;
        private string m_Scen_name;
        private IEnumerable<Player> _ply;
        private IEnumerable<Grenade> _g;
        private IEnumerable<LootPoint> _ContainersBody; 
        private int AliveObject_count;
        private bool blatant = false;
        private Player _lP;
        private float _plyR = 0f, _scanSceneR = 0f, _lpR = 0f, _gR = 0f, _lootR;// _secTime = 0f;
        private bool    Main_Loop_Switch,
                        GrenadeLoop = false, 
                        ItemsLoop = false, 
                        Recoil_Reducer = false, 
                        _display_Crosshair,
                        AllowEFunc = true,
                        _aim = false,
                        _aimA = false,
                        DisplayDebugData = false,
                        Switch_Colors = false,
                        DisplayHelpInfo = false;
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
            _ply = null;
            _lP = null;
            _g = null;
            _plyR = 0;
            _gR = 0;
            _scanSceneR = 0;
            _lpR = 0;
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
        private void PreparePlayersPool()
        {
            #region Update Player Objects pool
            if (Time.time >= _plyR)
            {
                if (_ply != FindObjectsOfType<Player>())
                {
                    _ply = null;
                    _ply = FindObjectsOfType<Player>();
                }
                _plyR = Time.time + Cons.UpdateIntervals[2];

                #region AliveCount()
                try
                {
                    Players_Alive = AliveCount();
                }
                catch (Exception e)
                {
                    File.AppendAllText("C:\\AliveCount.log", e.Message + "||||\\n" + e.Source + "||||\\n" + e.StackTrace);
                }
                #endregion
            }
            #endregion
        }
        private void PrepareGrenadesPool()
        {
            if (GrenadeLoop)
            {
                #region Update Grenade Objects pool
                if (Time.time >= _gR)
                {
                    if (_g != FindObjectsOfType<Grenade>())
                    {
                        _g = null;
                        _g = FindObjectsOfType<Grenade>();
                    }
                    _gR = Time.time + Cons.UpdateIntervals[0];
                }
                #endregion
            }
        }
        private void PrepareLootPool()
        {
            if (ItemsLoop)
            {
                #region Update Loot Objects pool
                if (Time.time >= _lootR)
                {
                    if (_ContainersBody != FindObjectsOfType<LootPoint>())
                    {
                        _ContainersBody = null;
                        _ContainersBody = FindObjectsOfType<LootPoint>();
                    }
                    _lootR = Time.time + Cons.UpdateIntervals[2];
                }
                #endregion
            }
        }
        private void CaptureGameScene() {
            #region Capture scene name for checks
            if (Time.time >= _scanSceneR)
            {
                _scanSceneR = Time.time + Cons.UpdateIntervals[2];
                this.m_Scen = SceneManager.GetActiveScene();
                this.m_Scen_name = m_Scen.name;
            }
            #endregion
        }
        private void Hotkeys() {
            #region Hotkeys
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                Debug.unityLogger.logEnabled = !Debug.unityLogger.logEnabled;
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                Recoil_Reducer = !Recoil_Reducer;
            }
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                Main_Loop_Switch = !Main_Loop_Switch;
            }
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                ItemsLoop = !ItemsLoop;
            }
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                GrenadeLoop = !GrenadeLoop;
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                _display_Crosshair = !_display_Crosshair;
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                DisplayDebugData = !DisplayDebugData;
            }
            if (Input.GetKeyDown(KeyCode.Home)) {
                DisplayHelpInfo = !DisplayHelpInfo;
            }
            #endregion
        }
        private void UpdateLocalPlayer() {
            LPBFunc.GLPB(_ply, ref _lP);
            _lpR = Time.time + Cons.UpdateIntervals[5];
        }
        private int[] AliveCount() {
            int All_count = 0, close100_count = 0, close250_count = 0;
            var enumerator = _ply.GetEnumerator();
            while (enumerator.MoveNext())
            {
                try
                {
                    var p = enumerator.Current;
                    if (p != null)
                    {
                        if (p.HealthController.IsAlive)
                        {
                            All_count++;
                            float distance = FMath.FD(Camera.main.transform.position, p.Transform.position);
                            if (distance <= 100f) {
                                close100_count++;
                            }
                            if (distance <= 250f) {
                                close250_count++;
                            }

                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                }
            }
            AliveObject_count = All_count;
            return new int[3] { All_count, close100_count, close250_count };
        }
        private void RecoilReducer() {
            if (Recoil_Reducer)
            {
                _lP.ProceduralWeaponAnimation.Shootingg.Intensity = 0.5f;
            }
            else if(_lP.ProceduralWeaponAnimation.Shootingg.Intensity != 1.0f)
            {
                _lP.ProceduralWeaponAnimation.Shootingg.Intensity = 1.0f;
            }
        }
        #region Update
        private void Update()
        {
            #region CaptureGameScene()
            try
            {
                CaptureGameScene();
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("CaptureGameScene", e);
            }
            #endregion
            #region Hotkeys()
            try
            {
                Hotkeys();
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("Hotkeys", e);
            }
            #endregion
            #region MainLoop
            try
            {
                if (Main_Loop_Switch && !Scene.ReferenceEquals(m_Scen,null))
                {
                    try
                    {
                        if (m_Scen.isLoaded)
                        {
                            try
                            {
                                if (m_Scen_name != "EnvironmentUIScene" && m_Scen_name != "MenuUIScene")
                                {
                                    // Update Main Structs
                                    #region PreparePlayersPool()
                                    try
                                    {
                                        PreparePlayersPool();
                                    }
                                    catch (Exception e)
                                    {
                                        ErrorHandler.Catch("PreparePlayersPool", e);
                                    }
                                    #endregion
                                    #region PrepareGrenadesPool()
                                    try
                                    {
                                        PrepareGrenadesPool();
                                    }
                                    catch (Exception e)
                                    {
                                        ErrorHandler.Catch("PrepareGrenadesPool", e);
                                    }
                                    #endregion
                                    // Update GLPB
                                    if (_ply != null)
                                    {
                                        if (Time.time >= _lpR)
                                        {
                                            //update localplayer and alive counters [total, closer then 100m, closer then 250m
                                            #region UpdateLocalPlayer()
                                            try
                                            {
                                                UpdateLocalPlayer();
                                            }
                                            catch (Exception e)
                                            {
                                                ErrorHandler.Catch("UpdateLocalPlayer", e);
                                            }
                                            #endregion
                                        }
                                        #region RecoilReducer()
                                        try
                                        {
                                            RecoilReducer();
                                        }
                                        catch (Exception e)
                                        {
                                            ErrorHandler.Catch("RecoilReducer", e);
                                        }
                                        #endregion
                                    }
                                }
                                else
                                {
                                    _lP.ProceduralWeaponAnimation.Shootingg.Intensity = 1.0f;
                                    Main_Loop_Switch = false;
                                }
                            }
                            catch (Exception e)
                            {
                                ErrorHandler.Catch("noSceneName", e);
                            }
                        }
                        else
                        {
                            Main_Loop_Switch = false;
                        }
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.Catch("m_Scen", e);
                    }
                }
                else
                {
                    //reset values to off and defaults also clear values
                    Main_Loop_Switch = false;
                    if (_lP.ProceduralWeaponAnimation.Shootingg.Intensity != 1.0f)
                        _lP.ProceduralWeaponAnimation.Shootingg.Intensity = 1.0f;
                    Clear();
                    Players_Alive = new int[3]{ 0, 0, 0 };//reset players count
                }
            }
            catch (Exception e) {
                ErrorHandler.Catch("MainLoop", e);
            }
            #endregion
        }
        #endregion

        private void HelpMenu() {
            if (DisplayHelpInfo) {
                string[] text = new string[10] {
                    "Help Menu: (Turn off/on 'Home' key)",
                    "'Num 0' - Enable + E.S.P.",
                    "'Num 1' - Crosshair",
                    "'Num 2' - Help Data Disp.",
                    "'Num 5' - Recoil Switch",
                    "'Num 7' - Grenade Disp.",
                    "'Num 8' - test Loot",
                    "'Num 9' - Switch GameLogs",
                    "",
                    ""
                };
                GUI.color = Color.white;
                for (int i = 0; i < text.Length; i++) {
                    GUI.Label(
                        new Rect(
                            Cons.CalcSizeW(500f),
                            Cons.CalcSizeH(200f + (20f * i)),
                            Cons.boxSize[19],
                            Cons.boxSize[1]
                            ),
                        text[i]
                        );
                }
            }
        }
        private void DisplayMenu() {
            if (DisplayDebugData)
            {
                #region Alive Number Display
                if(Players_Alive[0] != 0)
                    GUI.Label(new Rect(1f, Cons.CalcSizeH(185f), Cons.boxSize[19], Cons.boxSize[1]), "TotalAlive: " + (Players_Alive[0]).ToString());
                if (Players_Alive[1] != 0)
                    GUI.Label(new Rect(1f, Cons.CalcSizeH(205f), Cons.boxSize[19], Cons.boxSize[1]), "to 100m:" + (Players_Alive[1]).ToString());
                if (Players_Alive[2] != 0)
                    GUI.Label(new Rect(1f, Cons.CalcSizeH(225f), Cons.boxSize[19], Cons.boxSize[1]), "100m - 250m:" + (Players_Alive[2] - Players_Alive[1]).ToString());
                #endregion
                #region Player Health
                if (_lP != null)
                {
                    if(_lP.ProceduralWeaponAnimation.Shootingg.Intensity != 1f)
                        GUI.Label(new Rect(1f, 1f, Cons.boxSize[9], Cons.boxSize[1]),  "Recoil: " + (_lP.ProceduralWeaponAnimation.Shootingg.Intensity * 100).ToString() + "%");
                    GUI.Label(new Rect(1f, 15f, Cons.boxSize[9], Cons.boxSize[1]), "Total: " + Cons.Health(_lP, EFT.HealthSystem.EBodyPart.Common));
                    GUI.Label(new Rect(1f, Cons.CalcSizeH(245f), Cons.boxSize[19], Cons.boxSize[1]), "Energy: " + _lP.HealthController.Energy.Current.ToString() + "/" + _lP.HealthController.Energy.Maximum.ToString());
                    GUI.Label(new Rect(1f, Cons.CalcSizeH(265f), Cons.boxSize[19], Cons.boxSize[1]), "Hydro: " + _lP.HealthController.Hydration.Current.ToString() + "/" + _lP.HealthController.Hydration.Maximum.ToString());
                    GUI.Label(new Rect(HealthPosition[0, 0], HealthPosition[0, 1], Cons.boxSize[5], Cons.boxSize[1]), Cons.Health(_lP, EFT.HealthSystem.EBodyPart.Head));
                    GUI.Label(new Rect(HealthPosition[1, 0], HealthPosition[1, 1], Cons.boxSize[5], Cons.boxSize[1]), Cons.Health(_lP, EFT.HealthSystem.EBodyPart.Chest));
                    GUI.Label(new Rect(HealthPosition[2, 0], HealthPosition[2, 1], Cons.boxSize[5], Cons.boxSize[1]), Cons.Health(_lP, EFT.HealthSystem.EBodyPart.LeftArm));
                    GUI.Label(new Rect(HealthPosition[3, 0], HealthPosition[3, 1], Cons.boxSize[5], Cons.boxSize[1]), Cons.Health(_lP, EFT.HealthSystem.EBodyPart.RightArm));
                    GUI.Label(new Rect(HealthPosition[4, 0], HealthPosition[4, 1], Cons.boxSize[5], Cons.boxSize[1]), Cons.Health(_lP, EFT.HealthSystem.EBodyPart.LeftLeg));
                    GUI.Label(new Rect(HealthPosition[5, 0], HealthPosition[5, 1], Cons.boxSize[5], Cons.boxSize[1]), Cons.Health(_lP, EFT.HealthSystem.EBodyPart.RightLeg));
                    GUI.Label(new Rect(HealthPosition[6, 0], HealthPosition[6, 1], Cons.boxSize[5], Cons.boxSize[1]), Cons.Health(_lP, EFT.HealthSystem.EBodyPart.Stomach));
                }
                #endregion
                #region Error Logs Enabled Display Message
                if (Debug.unityLogger.logEnabled == true)
                    GUI.Label(new Rect(150f, Cons.ScreenHeight - 25f, 100f, 20f), "disable logger! [Numpad 9]");
                #endregion
            }
            #region draw crosshair
            if (_display_Crosshair && _lP != null)
            {
                if (!_lP.ProceduralWeaponAnimation.IsAiming)
                {
                    GUI.color = Color.yellow;
                    EDS.P(new Vector2(Screen.width / 2f - 1f, Screen.height / 2f - 1f), Color.yellow, 2f);
                }
            }
            #endregion
        }

        #region OnGui
        private void OnGUI()
        {
            DisplayMenu();
            HelpMenu();
            #region Main Loop Displayer
            if (Main_Loop_Switch)
            {
                if (m_Scen != null)
                {
                    EDS.P(new Vector2(1f, 1f), Color.red, 1f);
                    if (GrenadeLoop)
                        EDS.P(new Vector2(1f, 2f), Color.blue, 1f);
                    if (ItemsLoop)
                        EDS.P(new Vector2(1f, 3f), Color.yellow, 1f);
                    if (m_Scen.isLoaded)
                    {
                        if (m_Scen_name != "EnvironmentUIScene" && m_Scen_name != "MenuUIScene")
                        {
                            EFunc.DrawError(_ply, _lP, Cons.RenderDistances[0], Switch_Colors);
                            if(GrenadeLoop)
                                EFunc.DrawBombs(_g, _lP, Cons.RenderDistances[4]);
                            if (ItemsLoop)
                                EFunc.DrawLogs(_ContainersBody, Cons.RenderDistances[3]);
                            //EFunc.DrawLogs(_ContainersBody, 200f);
                        }
                    }
                }
            }
            else
            {
                GUI.color = Color.white;
                EDS.P(new Vector2(1f, 1f), Color.white, 1f);
            }
            #endregion
        }
        #endregion
    }
}