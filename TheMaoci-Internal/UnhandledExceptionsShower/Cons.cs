using EFT;
using System;
using System.Linq;
using UnityEngine;

namespace UnhandledExceptionHandler
{
    public class Cons
    {
        public static Vector2 InitialHealthBox = new Vector2(19f, 32f);
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
        public static float CalcSizeW(float size) { return ScreenWidth * size / 1920; }
        public static float CalcSizeH(float size) { return ScreenHeight * size / 1080; }
        public static string Health(Player LocalPlayer, EFT.HealthSystem.EBodyPart bodypart)
        {
            int health_curr = (int)LocalPlayer.HealthController.GetBodyPartHealth(bodypart).Current;
            if (health_curr > 0)
            {
                return health_curr.ToString() + "/" + ((int)LocalPlayer.HealthController.GetBodyPartHealth(bodypart).Maximum).ToString();
            }
            return "[OFF]";
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
