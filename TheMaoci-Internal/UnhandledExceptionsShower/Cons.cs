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
            public static int distanceFromCenterOfScreen = 100;
            public static float aimSpeed = 10;
            public static Player lastTargeted;
        }
        public static Vector2 InitialHealthBox = new Vector2(19f, 32f);
        public class HealthBox {
            public static Vector2 Head = new Vector2(49f, 0f);
            public static Vector2 Chest = new Vector2(40f, 38f);
            public static Vector2 Right_Arm = new Vector2(87f, 55f);
            public static Vector2 Left_Arm = new Vector2(0f, 55f);
            public static Vector2 Right_Leg = new Vector2(64f, 134f);
            public static Vector2 Left_Leg = new Vector2(22f, 121f);
            public static Vector2 Stomach = new Vector2(36f, 71f);
        }
        public class Alive {
            public static Vector2 All = new Vector2(19f, 32f);
            public static Vector2 zero_to_hundred = new Vector2(19f, 52f);
            public static Vector2 hundred_to_2fifty = new Vector2(19f, 72f);
        }
        public class Recoil {
            public static Vector2 position = new Vector2(1f, 285f);
        }
        public class Status {
            public static Vector2 CommonHealth = new Vector2(1f, 15f);
            public static Vector2 Hydration = new Vector2(1f, 265f);
            public static Vector2 Energy = new Vector2(1f, 245f);
        }
        public class boxSize {
            public static float box_10 = 10f;
            public static float box_20 = 20f;
            public static float box_30 = 30f;
            public static float box_40 = 40f;
            public static float box_50 = 50f;
            public static float box_60 = 60f;
            public static float box_70 = 70f;
            public static float box_80 = 80f;
            public static float box_90 = 90f;
            public static float box_100 = 100f;
            public static float box_110 = 110f;
            public static float box_120 = 120f;
            public static float box_130 = 130f;
            public static float box_140 = 140f;
            public static float box_150 = 150f;
            public static float box_160 = 160f;
            public static float box_170 = 170f;
            public static float box_180 = 180f;
            public static float box_190 = 190f;
            public static float box_200 = 200f;
        }
        public class RenderDistance {
            public static float d_1000 = 1000f;
            public static float d_750 = 750f;
            public static float d_500 = 500f;
            public static float d_250 = 250f;
            public static float d_100 = 100f;
        }
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

        public static void RedalculateDistances() {
            if (ScreenWidth != 1920 && ScreenHeight != 1080)
            {
                // recalculate all variables for diffrent screensizes
                HealthBox.Chest.x = CalcSizeW(HealthBox.Chest.x);
                HealthBox.Chest.y = CalcSizeH(HealthBox.Chest.y);
                HealthBox.Head.x = CalcSizeW(HealthBox.Head.x);
                HealthBox.Head.y = CalcSizeH(HealthBox.Head.y);
                HealthBox.Left_Arm.x = CalcSizeW(HealthBox.Left_Arm.x);
                HealthBox.Left_Arm.y = CalcSizeH(HealthBox.Left_Arm.y);
                HealthBox.Left_Leg.x = CalcSizeW(HealthBox.Left_Leg.x);
                HealthBox.Left_Leg.y = CalcSizeH(HealthBox.Left_Leg.y);
                HealthBox.Right_Arm.x = CalcSizeW(HealthBox.Right_Arm.x);
                HealthBox.Right_Arm.y = CalcSizeH(HealthBox.Right_Arm.y);
                HealthBox.Right_Leg.x = CalcSizeW(HealthBox.Right_Leg.x);
                HealthBox.Right_Leg.y = CalcSizeH(HealthBox.Right_Leg.y);
                HealthBox.Stomach.x = CalcSizeW(HealthBox.Stomach.x);
                HealthBox.Stomach.y = CalcSizeH(HealthBox.Stomach.y);
                Alive.All.x = CalcSizeW(Alive.All.x);
                Alive.All.y = CalcSizeH(Alive.All.y);
                Alive.hundred_to_2fifty.x = CalcSizeW(Alive.hundred_to_2fifty.x);
                Alive.hundred_to_2fifty.y = CalcSizeH(Alive.hundred_to_2fifty.y);
                Alive.zero_to_hundred.x = CalcSizeW(Alive.zero_to_hundred.x);
                Alive.zero_to_hundred.y = CalcSizeH(Alive.zero_to_hundred.y);
                Recoil.position.x = CalcSizeW(Recoil.position.x);
                Recoil.position.y = CalcSizeH(Recoil.position.y);
                Status.CommonHealth.x = CalcSizeW(Status.CommonHealth.x);
                Status.CommonHealth.y = CalcSizeH(Status.CommonHealth.y);
                Status.Energy.x = CalcSizeW(Status.Energy.x);
                Status.Energy.y = CalcSizeH(Status.Energy.y);
                Status.Hydration.x = CalcSizeW(Status.Hydration.x);
                Status.Hydration.y = CalcSizeH(Status.Hydration.y);
                InitialHealthBox.x = CalcSizeW(InitialHealthBox.x);
                InitialHealthBox.y = CalcSizeH(InitialHealthBox.y);
            }
        }
        public static string Health(Player LocalPlayer, EFT.HealthSystem.EBodyPart bodypart)
        {
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
        private static float CalcSizeW(float size) { return ScreenWidth * size / 1920; }
        private static float CalcSizeH(float size) { return ScreenHeight * size / 1080; }

    }
}
