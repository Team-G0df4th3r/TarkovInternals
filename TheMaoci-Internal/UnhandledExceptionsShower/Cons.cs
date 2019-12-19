using EFT;
using EFT.Interactive;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnhandledException
{
    public class Cons
    {
        public class Aim {
            public static float distanceToScan = 200f;
            public static float AAN_FOV = 3f;
            public static Player lastTargeted;
        }
        public class Distances
        {
            public static float Aim = 200f;
            public static float Players = 1000f;
            public static float Loot = 1000f;
            public static float Crates = 1000f;
            public static float Exfils = 1000f;
            public static float Corpses = 200f;
            public static float Grenade = 100f;
        }
        public class LocalPlayer {
            #region Group
            private static string Group = "";
            public static void SetGroup(string localPlayerGroup)
            {
                Group = localPlayerGroup;
            }
            public static void SetGroup(Player localPlayer)
            {
                Group = localPlayer.Profile.Info.GroupId;
            }
            public static bool isInYourGroup(Player p)
            {
                return Group == p.Profile.Info.GroupId && Group != "0" && Group != "" && Group != null;
            }
            public static bool isInYourGroup(string groupId)
            {
                return Group == groupId && Group != "0" && Group != "" && Group != null;
            }
            #endregion
            #region Weapon
            public class Weapon {
                public static int RecoilIntensity; // described in %
                public static void SetRecoil() {
                    RecoilIntensity = (int)(Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity * 100);
                }
                public static void NoRecoil() {
                    if (Main._localPlayer != null)
                    {
                        Main._localPlayer.ProceduralWeaponAnimation.Mask = EFT.Animations.EProceduralAnimationMask.DrawDown;
                        Main._localPlayer.ProceduralWeaponAnimation.AimSwayMax = new Vector3(0f, 0f, 0f);
                        Main._localPlayer.ProceduralWeaponAnimation.AimSwayMin = new Vector3(0f, 0f, 0f);
                        Main._localPlayer.ProceduralWeaponAnimation.AimSwayStartsThreshold = 0f; 
                        Main._localPlayer.ProceduralWeaponAnimation.AimSwayMaxThreshold = 0f;
                    }
                    // Main._localPlayer.ProceduralWeaponAnimation.
                }

                public static string CurrentAmmo;
                public static string MaxAmmo;
                public static void UpdateAmmo()
                {
                    try
                    {
                        if (Main._localPlayer.Weapon != null)
                        {
                            CurrentAmmo = Main._localPlayer.Weapon.GetCurrentMagazineCount().ToString();
                            MaxAmmo = Main._localPlayer.Weapon.GetMaxMagazineCount().ToString();
                        }
                        else 
                        {
                            CurrentAmmo = "Melee";
                            MaxAmmo = "";
                        }
                    }
                    catch (Exception e) 
                    {
                        CurrentAmmo = "Melee";
                        MaxAmmo = "";
                    }
                    
                }
            }
            #endregion
            #region Status - Health and other
            public class Status {
                public static string Energy;
                public static string Hydration;
                public class Health {
                    public static string Common;
                    public static string Head;
                    public static string Chest;
                    public static string LeftArm;
                    public static string RightArm;
                    public static string LeftLeg;
                    public static string RightLeg;
                    public static string Stomach;
                }
                private static string GetHealthEndStatus(EFT.HealthSystem.EBodyPart Part) {
                    int curr = (int)Main._localPlayer.HealthController.GetBodyPartHealth(Part).Current;
                    if (curr == 0)
                        return "n/a";
                    return curr.ToString() + "/" + ((int)Main._localPlayer.HealthController.GetBodyPartHealth(Part).Maximum).ToString();
                }
                private static string GetVitalEndStatus(string type) {
                    switch (type) {
                        case "Energy":
                            int curr_e = (int)Main._localPlayer.HealthController.Energy.Current;
                            if (curr_e == 0) {
                                return "No Energy!!";
                            }
                            return curr_e.ToString() + "/" + ((int)Main._localPlayer.HealthController.Energy.Maximum).ToString();
                        case "Hydration":
                            int curr_h = (int)Main._localPlayer.HealthController.Hydration.Current;
                            if (curr_h == 0)
                            {
                                return "Dehydration!!";
                            }
                            return curr_h.ToString() + "/" + ((int)Main._localPlayer.HealthController.Hydration.Maximum).ToString();
                        default:
                            return "";
                    }
                }
                public static void UpdateStatus()
                {
                    Energy = GetVitalEndStatus("Energy");
                    Hydration = GetVitalEndStatus("Hydration");
                    Health.Chest = GetHealthEndStatus(EFT.HealthSystem.EBodyPart.Chest);
                    Health.Common = GetHealthEndStatus(EFT.HealthSystem.EBodyPart.Common);
                    Health.Head = GetHealthEndStatus(EFT.HealthSystem.EBodyPart.Head);
                    Health.LeftArm = GetHealthEndStatus(EFT.HealthSystem.EBodyPart.LeftArm);
                    Health.LeftLeg = GetHealthEndStatus(EFT.HealthSystem.EBodyPart.LeftLeg);
                    Health.RightArm = GetHealthEndStatus(EFT.HealthSystem.EBodyPart.RightArm);
                    Health.RightLeg = GetHealthEndStatus(EFT.HealthSystem.EBodyPart.RightLeg);
                    Health.Stomach = GetHealthEndStatus(EFT.HealthSystem.EBodyPart.Stomach);
                }
            }
            #endregion
        }
        public class Main
        {
            public class G_Scene
            {
                public static Scene Game_Scene;
                private static string GetSceneName()
                {
                    return Game_Scene.name;
                }
                public static bool isActiveAndLoaded()
                {
                    return Game_Scene.isLoaded;
                }
                public static bool isInMatch()
                {
                    return GetSceneName() != "EnvironmentUIScene" &&
                            GetSceneName() != "MenuUIScene" &&
                            GetSceneName() != "CommonUIScene" &&
                            GetSceneName() != "MainScene" &&
                            GetSceneName() != "";
                }
                public static void SaveScene()
                {
                    Game_Scene = SceneManager.GetActiveScene();
                }
            }
            public static List<Player> _players;
            public static List<Throwable> _grenades;
            public static List<LootItem> _corpses;
            public static List<LootItem> _lootItems;
            public static List<ExfiltrationPoint> _exfils;
            public static Dictionary<GInterface162, EFT.GameWorld.GStruct63>.Enumerator _containers;

            public static Player _localPlayer;
            public static List<Player> tPlayer;
            public static List<Throwable> tGrenades;
            public static List<LootItem> tCorpses;
            public static List<LootItem> tItems;
            public static List<ExfiltrationPoint> tExfils;
            //public static List<LootItem> tContainers;
            public static void Clear()
            {
                _players = null;
                _grenades = null;
                _corpses = null;
                _lootItems = null;
                _exfils = null;
                //_containers = null;
                _localPlayer = null;
                tPlayer = null;
                tGrenades = null;
                tCorpses = null;
                tItems = null;
                tExfils = null;
                //tContainers = null;
            }
        }
        public class Buttons {
            public static bool Ma0c1 = false;
            public static bool Niger = false;
        }
        public class Switches
        {
            public static bool Draw_ESP = false;
            public static bool Draw_Corpses = false;
            public static bool Draw_Grenades = false;
            public static bool Draw_Loot = false;
            public static bool Draw_Exfil = false;
            public static bool Draw_Containers = false;
            public static bool Draw_Crosshair = false;
            public static bool Display_HelpInfo = false;
            public static bool Switch_Colors = false;
            public static bool DisplayPlayerInfo = false;
            public static bool Spawn_FullBright = false;
            public static bool LOD_Controll = false;
            public static bool AimingAtNikita = false;
            public static bool Display_HUDGui = false;
            public static bool Recoil_Reducer = false;
            public static bool Aim_Smoothing = true;
            public static bool StreamerMode = false;
            public static bool SnapLines = false;
            public static bool ShowBones = false;
            public static bool IKnowWhatImDoing = false;
            public static bool ChangeSessionID = false;
            public static bool IamStrumer = false;
            public static void SetToOff()
            {
                Draw_ESP = false;
                Draw_Corpses = false;
                Draw_Grenades = false;
                Draw_Loot = false;
                Draw_Crosshair = false;
                Display_HelpInfo = false;
                Switch_Colors = false;
                DisplayPlayerInfo = false;
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
            public static int dist_50_100 = 0;
            public static int dist_100_250 = 0;
            public static int dist_250_1000 = 0;
            public static void Reset()
            {
                All = 0;
                dist_0_25 = 0;
                dist_25_50 = 0;
                dist_50_100 = 0;
                dist_100_250 = 0;
                dist_250_1000 = 0;
            }
        }
        
        public static string LootSearcher = ""; // variable used for searches loop
        public static Vector3 AimPoint = Vector3.zero;
        public class ScreenWidth {
            public static int Full = Screen.width;
            public static int Half = (int)(Full / 2);
        }
        public class ScreenHeight {
            public static int Full = Screen.height;
            public static int Half = (int)(Full / 2);
        }
        //public static int ScreenWidth = Screen.width;
        //public static int ScreenHeight = Screen.height;
        public static string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string[] HelpMenuTexts = new string[10] {
                "Help Menu: (Turn off/on 'Home' key)",
                "'Num 0' - E.S.P - Players",
                "'Num 1' - E.S.P - Corpses",
                "'Num 2' - PlayerInfo - Health, Alive Objects, etc.",
                "'Num 3' - Recoil 50%/100%",
                "'Num 4' - E.S.P - Grenades",
                "'Num 5' - Crosshair",
                "'Num 7' - E.S.P - Loot (kinda laggy) * dont use it all the time",
                "'Num 9' - Full Bright",
                "'Insert' - GUI Menu"
            };
        public static string Health(Player LocalPlayer, EFT.HealthSystem.EBodyPart bodypart)
        { // not used now but will use that later maybe
            int health_curr = (int)LocalPlayer.HealthController.GetBodyPartHealth(bodypart).Current;
            if (health_curr > 0)
            {
                return health_curr.ToString() + "/" + ((int)LocalPlayer.HealthController.GetBodyPartHealth(bodypart).Maximum).ToString();
            }
            return "n/a";
        }

        public static Vector3 SkeletonBonePos(Diz.Skinning.Skeleton sko, int id)
        {
            return sko.Bones.ElementAt(id).Value.position;
        }
        public static Vector3 GetBonePosByID(Player p, int id)
        {
            Vector3 result;
            try
            {
                result = SkeletonBonePos(p.PlayerBones.AnimatedTransform.Original.gameObject.GetComponent<PlayerBody>().SkeletonRootJoint, id);
            }
            catch (Exception)
            {
                result = Vector3.zero;
            }
            return result;
        }
        public static bool inScreen(Vector3 V) {
            if (V.x > 0.01f && 
                V.y > 0.01f &&
                V.x < ScreenWidth.Full &&
                V.y < ScreenHeight.Full && 
                V.z > 0.01f)
                return true;
            return false;
        }
        public static bool inScreen_SnapLines(Vector3 V) {
            // properly display snap lines :)
            if (V.y > 0.01f &&
                V.y < (ScreenHeight.Full - 5f) &&
                V.z > 0.01f)
                return true;
            return false;
        }

    }
}
