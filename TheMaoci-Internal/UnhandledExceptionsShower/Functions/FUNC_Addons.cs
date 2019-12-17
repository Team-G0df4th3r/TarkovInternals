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
            public static void RecoilReducer()
            {
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
