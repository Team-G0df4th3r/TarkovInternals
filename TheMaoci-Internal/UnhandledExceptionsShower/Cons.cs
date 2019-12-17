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
            public static float Loot = 1000f;
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
                    //Main._localPlayer.ProceduralWeaponAnimation.Mask = EFT.Animations.EProceduralAnimationMask.ForceReaction;
                }

                public static int CurrentAmmo;
                public static int MaxAmmo;
                public static void UpdateAmmo()
                {
                    CurrentAmmo = Main._localPlayer.Weapon.GetCurrentMagazineCount();
                    MaxAmmo = Main._localPlayer.Weapon.GetMaxMagazineCount();
                }

            }
            #endregion
            #region Status - Health and other
            public class Status {
                public static int Energy;
                public static int EnergyMax;
                public static int Hydration;
                public static int HydrationMax;
                public class Health {
                    public static int Common;
                    public static int CommonMax;
                    public static int Head;
                    public static int HeadMax;
                    public static int Chest;
                    public static int ChestMax;
                    public static int LeftArm;
                    public static int LeftArmMax;
                    public static int RightArm;
                    public static int RightArmMax;
                    public static int LeftLeg;
                    public static int LeftLegMax;
                    public static int RightLeg;
                    public static int RightLegMax;
                    public static int Stomach;
                    public static int StomachMax;
                }
                public static void UpdateStatus()
                {
                    HydrationMax = (int)Main._localPlayer.HealthController.Hydration.Maximum;
                    Hydration = (int)Main._localPlayer.HealthController.Hydration.Current;
                    Health.Chest = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Chest).Current;
                    Health.ChestMax = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Chest).Maximum;
                    Health.Common = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Common).Current;
                    Health.CommonMax = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Common).Maximum;
                    Health.Head = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Head).Current;
                    Health.HeadMax = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Head).Maximum;
                    Health.LeftArm = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.LeftArm).Current;
                    Health.LeftArmMax = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.LeftArm).Maximum;
                    Health.LeftLeg = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.LeftLeg).Current;
                    Health.LeftLegMax = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.LeftLeg).Maximum;
                    Health.RightArm = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.RightArm).Current;
                    Health.RightArmMax = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.RightArm).Maximum;
                    Health.RightLeg = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.RightLeg).Current;
                    Health.RightLegMax = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.RightLeg).Maximum;
                    Health.Stomach = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Stomach).Current;
                    Health.StomachMax = (int)Main._localPlayer.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Stomach).Maximum;
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

        public static string LootSearcher = "";
        public static int ScreenWidth = Screen.width;
        public static int ScreenHeight = Screen.height;
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
        public static bool outOfScreen(Vector3 checkVector) {
            if (checkVector.x > 0.01f && 
                checkVector.z > 0.01f &&
                checkVector.x < ScreenWidth &&
                checkVector.z < ScreenHeight)
                return true;
            return false;
        }

    }
}
