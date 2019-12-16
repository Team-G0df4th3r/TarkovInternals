using EFT;
using System;
using System.Linq;
using UnityEngine;

namespace UnhandledException
{
    public class Cons
    {
        public class Aim {
            public static float distanceToScan = 200f;
            public static float AAN_FOV = 3f;
            public static Player lastTargeted;
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

    }
}
