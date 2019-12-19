using System;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using EFT.UI;
using EFT.Interactive;
using UnityEngine.SceneManagement;
using System.Reflection;
#pragma warning disable CS0168
namespace UnhandledException
{

    /*public class DrawRadar : MonoBehaviour
    {
        public float ThetaScale = 0.01f;
        public float radius = 0f;
        private int Size;
        private LineRenderer LineDrawer;
        private float Theta = 0f;
        //DrawRadar(float size) { radius = size; }
        void Start()
        {
            LineDrawer = GetComponent<LineRenderer>();
        }

        void Update()
        {
            if (radius != 0f)
            {
                Theta = 0f;
                Size = (int)((1f / ThetaScale) + 1f);
                LineDrawer.positionCount = Size;
                for (int i = 0; i < Size; i++)
                {
                    Theta += (2.0f * Mathf.PI * ThetaScale);
                    float x = radius * Mathf.Cos(Theta);
                    float y = radius * Mathf.Sin(Theta);
                    LineDrawer.SetPosition(i, new Vector3(x, y, 0));
                }
            }
        }
    }*/

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
            GameObjectHolder = new GameObject();
            GameObjectHolder.AddComponent<UnhandledException>();
            DontDestroyOnLoad(GameObjectHolder);

        }
        #endregion

        #region [FUNCTION] - Clear
        private void Clear()
        {
            Cons.Main.Clear();
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
                if (Cons.Main.G_Scene.isInMatch() && Cons.Main.G_Scene.isActiveAndLoaded())
                {
                if (Cons.Switches.ChangeSessionID) 
                {
                    if (LWIAY_timer < Time.time)
                    {
                        MonoBehaviourSingleton<PreloaderUI>.Instance.SetSessionId("NotForSale"); // use this for memes only
                        LWIAY_timer = Time.time + 1f;
                    }
                }
                // delay start of script for 20 seconds on start of match cause match is starting way before deploy is displaying - it will cause less errors displaying
                // lower time from 20f if its holdup too long
                    if (timestamp == 0f)
                        timestamp = Time.time + 20f;

                    if (Time.time > timestamp)
                    {
                        if (_GameWorld == null)
                        {
                            //done only once on start of each map
                            try
                            {
                                _GameWorld = FindObjectOfType<GameWorld>();
                                if (Cons.Switches.StreamerMode)
                                {
                                    MonoBehaviourSingleton<PreloaderUI>.Instance.SetStreamMode(true); // this should disable this stupid text ;)
                                }
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
                            try 
                            {
                                Cons.AliveCount.Reset();
                                Cons.Main.tPlayer = new List<Player>();
                                foreach (Player p in _GameWorld.RegisteredPlayers)
                                {
                                    if (p.PointOfView == EPointOfView.FirstPerson)
                                    {
                                        Cons.Main._localPlayer = p;
                                        Cons.LocalPlayer.Weapon.SetRecoil();
                                        Cons.LocalPlayer.Weapon.UpdateAmmo();
                                        Cons.LocalPlayer.Status.UpdateStatus();
                                    }
                                    else
                                    {
                                        if (p.HealthController.IsAlive)
                                        {

                                            Cons.AliveCount.All++;
                                            float distance = FastMath.FD(Camera.main.transform.position, p.Transform.position);
                                            if (distance > 0f && distance <= 25f)
                                            {
                                                Cons.AliveCount.dist_0_25++;
                                            }

                                            if (distance > 25f && distance <= 50f)
                                            {
                                                Cons.AliveCount.dist_25_50++;
                                            }
                                            if (distance > 50f && distance <= 100f)
                                            {
                                                Cons.AliveCount.dist_50_100++;
                                            }
                                            if (distance > 100f && distance <= 250f)
                                            {
                                                Cons.AliveCount.dist_100_250++;
                                            }
                                            if (distance > 250f && distance <= 1000f)
                                            {
                                                Cons.AliveCount.dist_250_1000++;
                                            }
                                            if (distance > 1f && distance <= Cons.Distances.Players && Cons.inScreen(Camera.main.WorldToScreenPoint(p.Transform.position)))
                                            {
                                                Cons.Main.tPlayer.Add(p);
                                            }
                                        }
                                    }
                                }
                                Cons.Main._players = Cons.Main.tPlayer;
                            }
                            catch (Exception e)
                            {
                                ErrorHandler.Catch("Get_Players", e);
                            }
                            }
                            #endregion
                            #region Grenades
                            if (Cons.Switches.Draw_Grenades)
                            {
                            /* Grenade scanner - scans for grenades if this function is enabled
                             * also added as much RAM free functions as possible
                             */
                                try 
                                { 
                                    Cons.Main.tGrenades = new List<Throwable>();
                                    List<Throwable>.Enumerator grenades = _GameWorld.Grenades.GetValuesEnumerator().GetEnumerator();
                                    while (grenades.MoveNext())
                                    {
                                        Cons.Main.tGrenades.Add(grenades.Current);
                                    }
                                    Cons.Main._grenades = Cons.Main.tGrenades;
                                    grenades.Dispose();
                                }
                                catch (Exception e)
                                {
                                    ErrorHandler.Catch("Get_GrenadesThrown", e);
                                }
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
                                try 
                                {
                                    Cons.Main.tCorpses = new List<LootItem>();
                                    List<LootItem>.Enumerator temporalCorpsesEnum = _GameWorld.LootItems.GetValuesEnumerator().GetEnumerator();
                                    while (temporalCorpsesEnum.MoveNext())
                                    {
                                        LootItem temp = temporalCorpsesEnum.Current;
                                        if (temp.GetType() == Types.Corpse || temp.GetType() == Types.ObserverCorpse)
                                            Cons.Main.tCorpses.Add(temp);
                                    }
                                    Cons.Main._corpses = Cons.Main.tCorpses;
                                    temporalCorpsesEnum.Dispose();
                                }
                                catch (Exception e)
                                {
                                    ErrorHandler.Catch("Get_Corpses", e);
                                }
                            }
                            #endregion
                            #region AllLoot
                            if (Cons.Switches.Draw_Loot)
                            {
                            /* Map Loot Scanner - scans and creates a list of loot on map - RAM free as always
                             * EFT.Interactive.ObservedLootItem - as online LootItem
                             * EFT.Interactive.LootItem - as offline LootItem
                             */
                            try { 
                                Cons.Main.tItems = new List<LootItem>();
                                //Cons.Main.tContainers = new List<LootPoint>();
                                #region LootItems
                                List<LootItem>.Enumerator temporalItemsEnum = _GameWorld.LootItems.GetValuesEnumerator().GetEnumerator();
                                while (temporalItemsEnum.MoveNext())
                                {
                                    LootItem temp = temporalItemsEnum.Current;
                                    if (temp.GetType() == Types.LootItem || temp.GetType() == Types.ObservedLootItem)
                                    {
                                        if(Cons.LootSearcher == "")
                                            Cons.Main.tItems.Add(temp);
                                        try
                                        {
                                            if (temp.Item.ShortName.Localized().ToLower().IndexOf(Cons.LootSearcher) >= 0)
                                            {
                                                Cons.Main.tItems.Add(temp);
                                            }
                                        }
                                        catch (Exception e) {}
                                    }
                                }
                                // temporalContainersEnum
                                Cons.Main._lootItems = Cons.Main.tItems;
                                temporalItemsEnum.Dispose();
                                #endregion

                            }
                            catch (Exception e)
                                {
                                    ErrorHandler.Catch("Get_LootItems", e);
                                }
                            }
                            if (Cons.Switches.Draw_Containers)
                            {
                                Cons.Main._containers = _GameWorld.ItemOwners.GetEnumerator();
                            }

                        #endregion
                        #region Exfiltrations
                        if (Cons.Switches.Draw_Exfil)
                            {
                                /* Corspes scanner - scans for corpses in the map and creates a list of them
                                 * also contains RAM free things to not cause out of memory violations 0xc0...05
                                 * also online Corpses are diffrent then Offline Corpses and i check for both of them
                                 * EFT.Interactive.ObservedCorpse - as online corpse
                                 * EFT.Interactive.Corpse - as offline corpse
                                 */
                                try
                                {
                                    if (Cons.Main._exfils == null)
                                    {
                                        Cons.Main.tExfils = new List<ExfiltrationPoint>();
                                        for (int i = 0; i < _GameWorld.ExfiltrationController.ExfiltrationPoints.Length; i++)
                                        {
                                            Cons.Main.tExfils.Add(_GameWorld.ExfiltrationController.ExfiltrationPoints[i]);
                                        }
                                        Cons.Main._exfils = Cons.Main.tExfils;
                                    }
                                }
                                catch (Exception e)
                                {
                                    ErrorHandler.Catch("Get_Exfils", e);
                                }
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
                    _GameWorld = null;
                }
        }
        #endregion

        #region [MAIN] - OnGui
        private void OnGUI()
        {
            try { 
                FUNC_Additional_Drawing.DisplayMenu();
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("Draw_DisplayMenu", e);
            }
            if (Cons.Switches.Display_HelpInfo)
            {
                try { 
                    FUNC_Additional_Drawing.HelpMenu();
                }
                catch (Exception e)
                {
                    ErrorHandler.Catch("Draw_HelpMenu", e);
                }
            }
            if (Cons.Switches.Display_HUDGui)
            {
                try { 
                    FUNC_Additional_Drawing.DrawHUDMenu();
                }
                catch (Exception e)
                {
                    ErrorHandler.Catch("Draw_HUDMenu", e);
                }
            }
            //Updates Each Frame
            if (Cons.Main.G_Scene.isInMatch() && Cons.Main.G_Scene.isActiveAndLoaded())
            {
                Drawing.P(new Vector2(1f, 1f), Color.red, 1f);
                string enabled = "";
                if (Cons.Switches.Draw_ESP)
                {
                    enabled = enabled + "P";
                    try {
                        FUNC_DrawObjects.DrawPlayers(Cons.Main._players, Cons.Main._localPlayer);
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.Catch("Draw_Players", e);
                    }
                }
                if (Cons.Switches.Draw_Grenades)
                {
                    enabled = enabled + "G";
                    try
                    {
                        FUNC_DrawObjects.DrawDTG(Cons.Main._grenades, Cons.Main._localPlayer);
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.Catch("Draw_Grenades", e);
                    }
                }
                if (Cons.Switches.Draw_Loot)
                {
                    enabled = enabled + "L";
                    try
                    {
                        FUNC_DrawObjects.DrawDLI(Cons.Main._lootItems);
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.Catch("Draw_Loot", e);
                    }
                }
                if (Cons.Switches.Draw_Corpses)
                {
                    enabled = enabled + "C";
                    try
                    {
                        FUNC_DrawObjects.DrawPDB(Cons.Main._corpses);
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.Catch("Draw_Corpses", e);
                    }
                }
                if (Cons.Switches.Draw_Exfil)
                {
                    enabled = enabled + "E";
                    try
                    {
                        FUNC_DrawObjects.DrawExfils(Cons.Main._exfils);
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.Catch("Draw_Exfils", e);
                    }
                }
                if (Cons.Switches.Draw_Containers)
                {
                    enabled = enabled + "K";
                    try
                    {
                       FUNC_DrawObjects.DrawContainers(Cons.Main._containers);
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.Catch("Draw_Containers", e);
                    }
                }

                if (Cons.Switches.AimingAtNikita) 
                {
                    enabled = enabled + "A";
                    try
                    {
                        FUNC_AimHelper.Aimbot_Method();
                    }
                    catch (Exception e)
                    {
                        ErrorHandler.Catch("Draw_AiBo", e);
                    }
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