using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using EFT;

namespace UnhandledException
{
    class FUNC_Aiming_Helper
    {
        #region DLLImporting + mouse events
        //[DllImport("user32.dll")]
        //static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }
        // _lP.ProceduralWeaponAnimation.HandsContainer.CameraTransform.

        /*public struct POINT
        {
            public int X;
            public int Y;
        }*/
        #endregion
        public static void TargetLock(IEnumerable<Player> _ply, Player _lP, float _AimSpeed, float _checkDistance = 200f, double _distance2d = 100)
        {
            
            if (_ply != null && _lP != null)
            {
                float aimPosX = 0;
                float aimPosY = 0;
                Vector3 velocity = new Vector3(0, 0, 0);
                var enumerator = _ply.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    try
                    {
                        var p = enumerator.Current;
                        if (p != null)
                        {
                            /*if (Cons.Aim.lastTargeted != null)
                            {
                                if (@Cons.Aim.lastTargeted.Profile.AccountId == p.Profile.AccountId)
                                {
                                    Vector3 playerHeadVector = Camera.main.WorldToScreenPoint(p.PlayerBones.Head.position);
                                    double screenDist = FastMath.FDv2(new Vector2(Screen.width, Screen.height) / 2, new Vector2(playerHeadVector.x, playerHeadVector.y));
                                    if (p.HealthController.IsAlive || screenDist < _distance2d)
                                    {
                                        velocity = p.Velocity;
                                        aimPosX = playerHeadVector.x;
                                        aimPosY = playerHeadVector.y;
                                        AimAtPos(aimPosX, aimPosY, velocity, _AimSpeed);
                                        continue;
                                    }
                                    else
                                    {
                                        Cons.Aim.lastTargeted = null;
                                    }
                                }
                            }*/
                            if (_lP.Profile.Info.GroupId == p.Profile.Info.GroupId && _lP.Profile.Info.GroupId != "0" && _lP.Profile.Info.GroupId != "" && _lP.Profile.Info.GroupId != null) continue;
                            //if (!p.IsVisible) continue;
                            float distanceToObject = FastMath.FD(Camera.main.transform.position, p.Transform.position);
                            if (distanceToObject < _checkDistance)
                            {
                                if (p.HealthController.IsAlive && p.IsVisible && EPointOfView.FirstPerson != p.PointOfView)
                                {
                                    Vector3 playerHeadVector = Camera.main.WorldToScreenPoint(p.PlayerBones.Head.position);
                                    double screenDist = FastMath.FDv2(new Vector2(Screen.width, Screen.height) / 2, new Vector2(playerHeadVector.x, playerHeadVector.y));
                                    if (screenDist < _distance2d)
                                    {
                                        Cons.Aim.lastTargeted = p;
                                        velocity = p.Velocity;
                                        aimPosX = playerHeadVector.x;
                                        aimPosY = playerHeadVector.y;
                                    }
                                    else
                                        continue;
                                }
                                else
                                    continue;
                            }
                            else
                                continue;
                        }
                        else
                            continue;

                    }
                    catch (NullReferenceException ex)
                    {
                        ErrorHandler.Catch("AimError", ex);
                    }
                }
                AimAtPos(aimPosX, aimPosY, velocity, _AimSpeed);

            }

        }
        private static void AimAtPos(float x, float y, Vector3 velocity, float _AimSpeed)
        {
            int ScreenCenterX = (Screen.width / 2);
            int ScreenCenterY = (Screen.height / 2);
            float TargetX = 0;
            float TargetY = 0;


            float diffrenceX = Math.Abs(ScreenCenterX - x);
            float diffrenceY = Math.Abs(ScreenCenterY - y);


            if (x != 0)
            {
                if (x > ScreenCenterX)
                {
                    TargetX = -(ScreenCenterX - x);
                    TargetX /= _AimSpeed;
                    if (TargetX + ScreenCenterX > Screen.width) TargetX = 0;
                }
                if (x < ScreenCenterX)
                {
                    TargetX = x - ScreenCenterX;
                    TargetX /= _AimSpeed;
                    if (TargetX + ScreenCenterX < 0) TargetX = 0;
                }
            }

            if (y != 0)
            {
                if (y > ScreenCenterY)
                {
                    TargetY = ScreenCenterY - y;
                    TargetY /= _AimSpeed;
                    if (TargetY + ScreenCenterY > Screen.height) TargetY = 0;
                }
                if (y < ScreenCenterY)
                {
                    TargetY = -(y - ScreenCenterY);
                    TargetY /= _AimSpeed;
                    if (TargetY + ScreenCenterY < 0) TargetY = 0;
                }
            }
            #region No Smoothing
            if (!Switches.Aim_Smoothing)
            {
                Move((int)TargetX, (int)TargetY);
                return;
            }
            #endregion
            TargetX = TargetX + TargetX;
            TargetY = TargetY + TargetY;
            //TargetX /= 10;
            //TargetY /= 10;
            if (Math.Abs(TargetX) >= 1 || Math.Abs(TargetY) >= 1)
            {
                Move((int)TargetX, (int)TargetY);
                /*if (TargetX > 0)
                {
                    TargetX = 1f;
                }
                if (TargetX < 0)
                {
                    TargetX = -1f;
                }
            }
            if (Math.Abs(TargetY) < 1f)
            {
                if (TargetY > 0)
                {
                    TargetY = 1f;
                }
                if (TargetY < 0)
                {
                    TargetY = -1f;
                }*/
            }
            
        }
    }
}
