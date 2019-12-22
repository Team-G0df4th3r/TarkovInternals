using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using EFT;

namespace UnhandledException
{
    class FUNC_AimHelper
    {
        #region vangle_aim
        public static void Aimbot_Method()
        {
            try
            {
                Vector3 AimAtGuy = Vector3.zero;
                float distanceOfTarget = 9999f;
                foreach (Player player in Cons.Main._players)
                {
                    if (!(player == null) && !(player == Cons.Main._localPlayer) && player.HealthController != null && player.HealthController.IsAlive)
                    {
                        if (player.GroupId != Cons.Main._localPlayer.GroupId || Cons.Main._localPlayer.GroupId == "" || Cons.Main._localPlayer.GroupId == "0" || Cons.Main._localPlayer.GroupId == null)
                        {
                            Vector3 vector = getBonePos(player);
                            float dist = FastMath.FD(Camera.main.transform.position, player.Transform.position);
                            if (dist > Cons.Distances.Aim)
                                continue;
                            if (!(vector == Vector3.zero) && CalcInFov(vector) <= Cons.Aim.AAN_FOV/* && IsVisible(player.gameObject, getBonePos(player))*/)
                            {
                                if (distanceOfTarget > dist)
                                {
                                    distanceOfTarget = dist;
                                    AimAtGuy = vector;
                                }
                            }
                        }
                    }
                }
                if (AimAtGuy != Vector3.zero)
                {
                    AimAtPos(AimAtGuy);
                }
            }
            catch (Exception e)
            {
                ErrorHandler.Catch("Draw_AiBo", e);
            }
        }

        public enum BodyPart
        {
            Head = 133,
            Neck = 132,
            Chest = 36,
            Stomach = 29,
            Ribcage, // propably 4
            Palm = 94,
            RightPalm,
            LeftPalm,
            LeftShoulder,
            RightShoulder,
            Pelvis = 14,
            LeftThigh1 = 15,
            LeftThigh2 = 16,
            RightThigh1 = 20,
            RightThigh2 = 21,
            KickingFoot, // who fucking cares ...
            LeftFoot = 18,
            LeftToes = 19,
            LeftKnee = 17,
            RightFoot = 23,
            RightToes = 24,
            RightKnee = 22,
            LeftElbow = 91,
            RightElBow = 112,
        }

        public static int idtobid(BodyPart bid = BodyPart.Head)
        {
            switch (bid)
            {
                case BodyPart.Neck:
                    return 132;

                case BodyPart.Chest:
                    return 36;

                case BodyPart.Stomach:
                    return 29;

                default:
                    return 133;
            }
        }

        public static Vector3 getBonePos(Player inP)
        {
            int bid = idtobid();
            return FUNC.Bones.GetBonePosByID(inP, bid);
        }

        public static float CalcInFov(Vector3 Position)
        {
            Vector3 position = Camera.main.transform.position;
            Vector3 forward = Camera.main.transform.forward;
            Vector3 normalized = (Position - position).normalized;
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, normalized), -1f, 1f)) * 57.29578f;
        }

        public static void AimAtPos(Vector3 pos)
        {
            Vector2 rotation = Cons.Main._localPlayer.MovementContext.Rotation;
            Vector3 b = Raycast.GetHandsPos();
            Vector3 eulerAngles = Quaternion.LookRotation((pos - b).normalized).eulerAngles;
            if (eulerAngles.x > 180f)
            {
                eulerAngles.x -= 360f;
            }
            Cons.Main._localPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
        }
        #endregion
    }
}
