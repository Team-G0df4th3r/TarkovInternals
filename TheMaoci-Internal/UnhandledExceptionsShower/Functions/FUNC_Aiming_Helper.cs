using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using EFT;

namespace UnhandledException
{
    class FUNC_Aiming_Helper
    {
        #region vangle_aim

        public static void Aimbot_Method()
        {
            foreach (Player player in Main._players)
            {
                if (!(player == null) && !(player == Main._localPlayer) && player.HealthController != null && player.HealthController.IsAlive)
                {
                    if (player.GroupId != Main._localPlayer.GroupId || Main._localPlayer.GroupId == "" || Main._localPlayer.GroupId == "0" || Main._localPlayer.GroupId == null)
                    {
                        Vector3 vector = getBonePos(player);
                        if (!(vector == Vector3.zero) && CalcInFov(vector) <= Cons.Aim.AAN_FOV && IsVisible(player.gameObject, getBonePos(player)))
                        {
                            AimAtPos(vector);
                        }
                    }
                }
            }
        }

        private static bool IsVisible(GameObject obj, Vector3 Position)
        {
            RaycastHit raycastHit;
            return Physics.Linecast(GetShootPos(), Position, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject;
        }

        public static Vector3 GetShootPos()
        {
            if (Main._localPlayer == null)
            {
                return Vector3.zero;
            }
            Player.FirearmController firearmController = Main._localPlayer.HandsController as Player.FirearmController;
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

        public static int idtobid(ibid bid)
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
            int bid = idtobid(ibid.Neck);
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
            Vector2 rotation = Main._localPlayer.MovementContext.Rotation;
            Vector3 b = GetShootPos();
            Vector3 eulerAngles = Quaternion.LookRotation((pos - b).normalized).eulerAngles;
            if (eulerAngles.x > 180f)
            {
                eulerAngles.x -= 360f;
            }
            Main._localPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
        }
        #endregion
    }
}
