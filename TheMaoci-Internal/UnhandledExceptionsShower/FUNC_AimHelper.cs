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
            if (AimAtGuy != Vector3.zero) {
                    AimAtPos(AimAtGuy);
            }
        }

        private static bool IsVisible(GameObject obj, Vector3 Position)
        {
            RaycastHit raycastHit;
            //int mask = 1 << 12 | 1 << 18; // its working for RayCast from game
            return Physics.Linecast(GetShootPos(), Position, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject;
        }

        public static Vector3 GetShootPos()
        {
            //Drawing.P(new Vector2(Camera.main.WorldToScreenPoint(Cons.AimPoint).x - 1f, Screen.height - Camera.main.WorldToScreenPoint(Cons.AimPoint).y - 1f), Color.green, 2f);
            if(Cons.AimPoint != Vector3.zero)
                return Cons.AimPoint + Camera.main.transform.forward * 1f;
            if (Cons.Main._localPlayer == null)
            {
                return Vector3.zero;
            }
            Player.FirearmController firearmController = Cons.Main._localPlayer.HandsController as Player.FirearmController;
            if (firearmController == null)
            {
                return Vector3.zero;
            }
            return firearmController.Fireport.position + Camera.main.transform.forward * 1f;
        }

        public enum ibid
        {
            Head,
            Neck,
            Chest,
            Stomach
        }
        /*
        public enum BodyParts
        {
            Head = 133,
            Chest = 36,
            Stomach = 29,
            Neck = 132,
            LeftArm,
            RightArm,
            LeftLeg,
            RightLeg
        }*/
        /*public enum PlayerBoneType
        {
            Head = 133,
            LeftShoulder,
            RightShoulder,
            Ribcage = 4,
            LeftThigh2,
            RightThigh2,
            WeaponRoot,
            Body,
            Fireport,
            AnimatedTransform,
            Pelvis,
            LeftThigh1,
            RightThigh1,
            Spine
        }*/
        public static int idtobid(ibid bid = ibid.Head)
        {
            switch (bid)
            {
                case ibid.Neck:
                    return 132;

                case ibid.Chest:
                    return 36;

                case ibid.Stomach:
                    return 29;

                default:
                    return 133;
            }
        }

        public static Vector3 getBonePos(Player inP)
        {
            int bid = idtobid();
            return Cons.GetBonePosByID(inP, bid);
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
            Vector3 b = GetShootPos();
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
