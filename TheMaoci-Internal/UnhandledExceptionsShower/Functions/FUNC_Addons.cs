using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnhandledException
{
    class FUNC_Addons
    {
        public class Update {
            public static void FullBright()
            {
                if (Main._localPlayer != null)
                {
                    if (Switches.Spawn_FullBright)
                    {
                        Vector3 playerPos = Main._localPlayer.Transform.position;
                        playerPos.y += 1f;

                        global::UnhandledException.FullBright.lightGameObject.transform.position = playerPos;
                        global::UnhandledException.FullBright.FullBrightLight.range = 1000;
                    }
                    else
                    {
                        GameObject.Destroy(global::UnhandledException.FullBright.FullBrightLight);
                        global::UnhandledException.FullBright.lightCalled = false;
                    }
                }
            }
            public static void RecoilReducer()
            {
                if (Main._localPlayer != null)
                    if (Switches.Recoil_Reducer)
                    {
                        if (Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 0.5f)
                            Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0.5f;
                    }
                    else if (Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 1.0f)
                    {
                        Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 1.0f;
                    }
            }
            #region [FUNCTION] - LOD Controller // TODO Not working - need to find work around
            public LODGroup group;
            private void SetLODToLow()
            {
                /*if (Switches.LOD_Controll)
                {
                    group.ForceLOD(6);
                }
                else 
                {
                    group.ForceLOD(0);
                }*/
            }
            #endregion
        }
        public class OnGUI {
            public static void FullBright()
            {
                if (!global::UnhandledException.FullBright.lightCalled)
                {
                    global::UnhandledException.FullBright.lightGameObject = new GameObject("Fullbright");
                    global::UnhandledException.FullBright.FullBrightLight = global::UnhandledException.FullBright.lightGameObject.AddComponent<Light>();
                    global::UnhandledException.FullBright.FullBrightLight.color = Color.white;
                    global::UnhandledException.FullBright.lightCalled = true;
                }
            }
        }


    }
}
