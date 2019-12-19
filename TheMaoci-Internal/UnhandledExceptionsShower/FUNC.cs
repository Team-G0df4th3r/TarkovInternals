using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnhandledException
{
    class FUNC
    {
        public class Update {

            #region [HOTKEYS]
            public static void Hotkeys()
            {
                #region Draw non sensitive data
                if (Input.GetKeyUp(KeyCode.Keypad5))
                {
                    Cons.Switches.Draw_Crosshair = !Cons.Switches.Draw_Crosshair;
                }
                if (Input.GetKeyUp(KeyCode.Keypad2))
                {
                    Cons.Switches.DisplayPlayerInfo = !Cons.Switches.DisplayPlayerInfo;
                }
                if (Input.GetKeyUp(KeyCode.Home))
                {
                    Cons.Switches.Display_HelpInfo = !Cons.Switches.Display_HelpInfo;
                }
                if (Input.GetKeyDown(KeyCode.Insert))
                {
                    Cons.Switches.Display_HUDGui = !Cons.Switches.Display_HUDGui;
                }
                #endregion

                #region Draw sensitive data - no errors allowed here
                if (Input.GetKeyUp(KeyCode.Keypad0))
                {
                    Cons.Switches.Draw_ESP = !Cons.Switches.Draw_ESP;
                }
                if (Input.GetKeyUp(KeyCode.Keypad1))
                {
                    Cons.Switches.Draw_Corpses = !Cons.Switches.Draw_Corpses;
                }
                if (Input.GetKeyUp(KeyCode.Keypad7))
                {
                    Cons.Switches.Draw_Loot = !Cons.Switches.Draw_Loot;
                }
                if (Input.GetKeyUp(KeyCode.Keypad4))
                {
                    Cons.Switches.Draw_Grenades = !Cons.Switches.Draw_Grenades;
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Cons.Switches.Recoil_Reducer = !Cons.Switches.Recoil_Reducer;
                }
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Cons.Switches.Spawn_FullBright = !Cons.Switches.Spawn_FullBright;
                }
                if (Input.GetKeyDown(KeyCode.Mouse3)) // You can change it or create a GUI for change it in game
                {
                    Cons.Switches.AimingAtNikita = !Cons.Switches.AimingAtNikita;
                }
                if (Cons.Switches.IKnowWhatImDoing)
                {
                    if (Input.GetKeyDown(KeyCode.F10))
                    {
                        if (Input.GetKeyDown(KeyCode.F10) && Cons.Main._localPlayer != null)
                        {
                            // we can add an prevention to too fast teleporting adding like once per second etc.
                            //if (Time.time >= _secTime) {
                            //_secTime = Time.time + 1f;
                            Cons.Main._localPlayer.Transform.position = Cons.Main._localPlayer.Transform.position + Camera.main.transform.forward * 1f;
                            //}
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.F11))
                    {
                        // simple FOV changer
                        GClass433.SetFov(120f, 1f);
                        GClass433.ApplyFoV(120, 100, 120);
                    }
                }
                #endregion
                /* unused yet
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Debug.unityLogger.logEnabled = !Debug.unityLogger.logEnabled;
                }
                */
            }
            #endregion
            #region [BUTTONS]
            public static void Buttons()
            {
                if (Cons.Buttons.Ma0c1)
                {
                    Cons.Switches.Draw_ESP = true;
                    Cons.Switches.Draw_Corpses = true;
                    Cons.Switches.Draw_Grenades = true;
                    Cons.Switches.Draw_Crosshair = true;
                    Cons.Switches.Spawn_FullBright = true;
                    Cons.Switches.AimingAtNikita = true;
                    Cons.Switches.Aim_Smoothing = true;
                    Cons.Switches.SnapLines = true;
                    Cons.Switches.ShowBones = true;
                    Cons.Buttons.Ma0c1 = false;
                }
                if (Cons.Buttons.Niger)
                {
                    // not used yet :)
                    Cons.Buttons.Niger = false;
                }
            }
            #endregion
            #region [FULL.BRIGHT]
            public static void FullBright()
            {
                if (Cons.Main._localPlayer != null)
                {
                    if (Cons.Switches.Spawn_FullBright)
                    {
                        Vector3 playerPos = Cons.Main._localPlayer.Transform.position;
                        playerPos.y += 1f;

                        Cons.FullBright.lightGameObject.transform.position = playerPos;
                        Cons.FullBright.FullBrightLight.range = 1000f;
                        Cons.FullBright.FullBrightLight.intensity = 0.4f;
                    }
                    else
                    {
                        GameObject.Destroy(Cons.FullBright.FullBrightLight);
                        Cons.FullBright.lightCalled = false;
                    }
                }
            }
            #endregion
            #region [REC.REDUCER]
            public static void RecoilReducer()
            {
                // old method of reducing recoil - still working btw ... not bannable tho
                if (Cons.Main._localPlayer != null)
                    if (Cons.Switches.Recoil_Reducer)
                    {
                        if (Cons.Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 0.5f)
                            Cons.Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0.5f;
                    }
                    else if (Cons.Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 1.0f)
                    {
                        Cons.Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 1.0f;
                    }
            }
            #endregion
            #region [FUNCTION] - LOD Controller // TODO Not working - need to find work around
            /*public LODGroup group;
            private void SetLODToLow()
            {
                if (Switches.LOD_Controll)
                {
                    group.ForceLOD(6);
                }
                else 
                {
                    group.ForceLOD(0);
                }
            }*/
            #endregion
        }
        public class OnGUI {
            public static void FullBright()
            {
                if (!Cons.FullBright.lightCalled)
                {
                    Cons.FullBright.lightGameObject = new GameObject("Fullbright");
                    Cons.FullBright.FullBrightLight = Cons.FullBright.lightGameObject.AddComponent<Light>();
                    Cons.FullBright.FullBrightLight.color = new Color(1f, 0.839f, 0.66f, 1f);
                    Cons.FullBright.lightCalled = true;
                }
            }
        }


    }
}
