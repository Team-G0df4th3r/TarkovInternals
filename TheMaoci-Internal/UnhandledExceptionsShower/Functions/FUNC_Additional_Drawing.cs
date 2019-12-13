﻿using EFT;
using UnityEngine;

namespace UnhandledException
{
    class FUNC_Additional_Drawing
    {
        private static void DrawAlive()
        {
            if (AliveCount.All != 0)
                Drawing.Text(new Rect(Cons.Alive.All.x, Cons.Alive.All.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "Alive:" + (AliveCount.All).ToString(), Statics.Colors.White);
            if (AliveCount.dist_0_100 != 0)
                Drawing.Text(new Rect(Cons.Alive.zero_to_hundred.x, Cons.Alive.zero_to_hundred.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "0 - 100m:" + (AliveCount.dist_0_100).ToString(), Statics.Colors.White);
            if (AliveCount.dist_100_250 != 0)
                Drawing.Text(new Rect(Cons.Alive.hundred_to_2fifty.x, Cons.Alive.hundred_to_2fifty.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "100 - 250m:" + (AliveCount.dist_100_250).ToString(), Statics.Colors.White);
        }

        private static void DrawRecoil() {
            if (Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 1f)
                Drawing.Text(new Rect(Cons.Recoil.position.x, Cons.Recoil.position.y, Cons.boxSize.box_100, Cons.boxSize.box_20), "Recoil: " + (Main._localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity * 100).ToString() + "%", Statics.Colors.White);
        }
        private static void HealthInfo()
        {
            Drawing.Text(new Rect(Cons.Status.CommonHealth.x, Cons.Status.CommonHealth.y, Cons.boxSize.box_100, Cons.boxSize.box_20), "Total: " + Cons.Health(Main._localPlayer, EFT.HealthSystem.EBodyPart.Common), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.Status.Energy.x, Cons.Status.Energy.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "Energy: " + ((int)Main._localPlayer.HealthController.Energy.Current).ToString() + "/" + Main._localPlayer.HealthController.Energy.Maximum.ToString(), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.Status.Hydration.x, Cons.Status.Hydration.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "Hydro: " + ((int)Main._localPlayer.HealthController.Hydration.Current).ToString() + "/" + Main._localPlayer.HealthController.Hydration.Maximum.ToString(), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Head.x, Cons.InitialHealthBox.y + Cons.HealthBox.Head.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(Main._localPlayer, EFT.HealthSystem.EBodyPart.Head), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Chest.x, Cons.InitialHealthBox.y + Cons.HealthBox.Chest.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(Main._localPlayer, EFT.HealthSystem.EBodyPart.Chest), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Left_Arm.x, Cons.InitialHealthBox.y + Cons.HealthBox.Left_Arm.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(Main._localPlayer, EFT.HealthSystem.EBodyPart.LeftArm), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Right_Arm.x, Cons.InitialHealthBox.y + Cons.HealthBox.Right_Arm.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(Main._localPlayer, EFT.HealthSystem.EBodyPart.RightArm), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Left_Leg.x, Cons.InitialHealthBox.y + Cons.HealthBox.Left_Leg.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(Main._localPlayer, EFT.HealthSystem.EBodyPart.LeftLeg), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Right_Leg.x, Cons.InitialHealthBox.y + Cons.HealthBox.Right_Leg.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(Main._localPlayer, EFT.HealthSystem.EBodyPart.RightLeg), Statics.Colors.White);
            Drawing.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Stomach.x, Cons.InitialHealthBox.y + Cons.HealthBox.Stomach.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(Main._localPlayer, EFT.HealthSystem.EBodyPart.Stomach), Statics.Colors.White);
        }

        public static void HelpMenu()
        {
            if (Switches.Display_HelpInfo)
            {
                string[] text = new string[10] {
                    "Help Menu: (Turn off/on 'Home' key)",
                    "'Num 0' - E.S.P - Players",
                    "'Num 1' - E.S.P - Corpses",
                    "'Num 2' - PlayerInfo - Health, Alive Objects, etc.",
                    "'Num 3' - Recoil 50%/100%",
                    "'Num 4' - E.S.P - Grenades",
                    "'Num 5' - Crosshair",
                    "'Num 7' - E.S.P - Loot (very laggy)",
                    "* make sure to not use it all the time",
                    ""
                };
                for (int i = 0; i < text.Length; i++)
                {
                    Drawing.Text(
                        new Rect(
                            500f,
                            200f + (20f * i),
                            Cons.boxSize.box_200,
                            Cons.boxSize.box_20
                            ),
                        text[i],
                        Statics.Colors.White
                        );
                }
            }
        }

        public static void DisplayMenu()
        {
            if (Switches.DisplayDebugData)
            {
                if (Main._localPlayer != null)
                {
                    #region Alive Number Display
                    DrawAlive();
                    #endregion
                    #region Player Health
                    DrawRecoil();
                    HealthInfo();
                    #endregion
                }
                #region Error Logs Enabled Display Message
                if (Debug.unityLogger.logEnabled == true)
                {
                    Drawing.Text(new Rect(250f, Cons.ScreenHeight - 25f, 200f, 20f), "disable logger!",Statics.Colors.White);
                }
                #endregion
            }
            #region draw crosshair
            if (Switches.Draw_Crosshair)
            {
                if (!Main._localPlayer.ProceduralWeaponAnimation.IsAiming)
                {
                    Drawing.P(new Vector2(Screen.width / 2f - 1f, Screen.height / 2f - 1f), Color.yellow, 2f);
                }
            }
            #endregion
        }

        public static void DrawHUDMenu()
        {
            /* First Column: initial.x & initial.y * 2 (next line is 2 + n)
             * Second Column: initial.x + Cons.boxSize.box_100 & initial.y * 2 (next line is 2 + n)
             * 
             * 
             */
            Color guiBackup = GUI.color;
            GUI.color = Color.black;
            GUI.Box(new Rect(10f, 10f, 220f, 200f), "Unknown.Exception.Handler");
            GUI.color = Color.white;
            Vector2 initial = new Vector2(15f, 20f);
            GUI.Label(new Rect(initial.x, initial.y, Cons.boxSize.box_100, Cons.boxSize.box_20), "Unknown.Exception by T.h.e.M.a.o.c.i");
            Switches.Draw_ESP = GUI.Toggle(new Rect(initial.x, initial.y * 2, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.Draw_ESP, "E.S.P");
            Switches.Draw_Grenades = GUI.Toggle(new Rect(initial.x, initial.y * 3, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.Draw_Grenades, "Grenade");
            Switches.Draw_Corpses = GUI.Toggle(new Rect(initial.x, initial.y * 4, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.Draw_Corpses, "Dead.Bodies");
            Switches.Draw_Loot = GUI.Toggle(new Rect(initial.x, initial.y * 5, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.Draw_Loot, "Map.Loot");
            Switches.Draw_Crosshair = GUI.Toggle(new Rect(initial.x, initial.y * 6, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.Draw_Crosshair, "Crosshair");
            Switches.Spawn_FullBright = GUI.Toggle(new Rect(initial.x, initial.y * 7, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.Spawn_FullBright, "Full.Bright");
            Switches.LOD_Controll = GUI.Toggle(new Rect(initial.x, initial.y * 8, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.LOD_Controll, "LOD.Control");
            Switches.DisplayDebugData = GUI.Toggle(new Rect(initial.x, initial.y * 9, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.DisplayDebugData, "Player.Data");

            Switches.AimingAtNikita = GUI.Toggle(new Rect(initial.x + Cons.boxSize.box_100, initial.y * 2, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.AimingAtNikita, "Aim");
            Switches.Aim_Smoothing = GUI.Toggle(new Rect(initial.x + Cons.boxSize.box_100, initial.y * 3, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.DisplayDebugData, "Smoothing");
            //FINSHED
            GUI.color = guiBackup;
        }
    }
}