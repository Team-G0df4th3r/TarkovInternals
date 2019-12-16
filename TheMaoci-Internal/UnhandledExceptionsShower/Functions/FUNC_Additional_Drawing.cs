using EFT;
using UnityEngine;

namespace UnhandledException
{
    class FUNC_Additional_Drawing
    {
        private static void DrawAlive()
        {
            if (AliveCount.All != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.All.x, Constants.Locations.Alive.All.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "Alive:" + (AliveCount.All).ToString(), Constants.Colors.White);
            if (AliveCount.dist_0_100 != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.zero_to_hundred.x, Constants.Locations.Alive.zero_to_hundred.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "0 - 100m:" + (AliveCount.dist_0_100).ToString(), Constants.Colors.White);
            if (AliveCount.dist_100_250 != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.hundred_to_2fifty.x, Constants.Locations.Alive.hundred_to_2fifty.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "100 - 250m:" + (AliveCount.dist_100_250).ToString(), Constants.Colors.White);
        }

        private static void DrawRecoil() {
            if (Cons.LocalPlayer.Weapon.RecoilIntensity != 100)
            {
                Drawing.Text(
                    new Rect(
                        Constants.Locations.Recoil.position.x,
                        Constants.Locations.Recoil.position.y,
                        Constants.Locations.boxSize.box_100,
                        Constants.Locations.boxSize.box_20
                    ),
                    "Recoil: " + Cons.LocalPlayer.Weapon.RecoilIntensity.ToString() + "%",
                    Constants.Colors.White
                );
            }
        }
        private static void HealthInfo()
        {
            Drawing.Text(
                new Rect(
                    Constants.Locations.Status.CommonHealth.x, 
                    Constants.Locations.Status.CommonHealth.y, 
                    Constants.Locations.boxSize.box_100, 
                    Constants.Locations.boxSize.box_20
                    ), 
                "Total: " + Cons.LocalPlayer.Status.Health.Common + "/" + Cons.LocalPlayer.Status.Health.CommonMax, 
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.Status.Energy.x, 
                    Constants.Locations.Status.Energy.y, 
                    Constants.Locations.boxSize.box_200, 
                    Constants.Locations.boxSize.box_20
                    ), 
                "Energy: " + Cons.LocalPlayer.Status.Energy + "/" + Cons.LocalPlayer.Status.EnergyMax, 
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.Status.Hydration.x, 
                    Constants.Locations.Status.Hydration.y, 
                    Constants.Locations.boxSize.box_200, 
                    Constants.Locations.boxSize.box_20
                    ), 
                "Hydro: " + Cons.LocalPlayer.Status.Hydration + "/" + Cons.LocalPlayer.Status.HydrationMax,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Head.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Head.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.Head + "/" + Cons.LocalPlayer.Status.Health.HeadMax,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Chest.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Chest.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.Chest + "/" + Cons.LocalPlayer.Status.Health.ChestMax,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Left_Arm.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Left_Arm.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.LeftArm + "/" + Cons.LocalPlayer.Status.Health.LeftArmMax,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Right_Arm.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Right_Arm.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.RightArm + "/" + Cons.LocalPlayer.Status.Health.RightArmMax,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Left_Leg.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Left_Leg.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.LeftLeg + "/" + Cons.LocalPlayer.Status.Health.LeftLegMax,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Right_Leg.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Right_Leg.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.RightLeg + "/" + Cons.LocalPlayer.Status.Health.RightLegMax,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Stomach.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Stomach.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.Stomach + "/" + Cons.LocalPlayer.Status.Health.StomachMax,
                Constants.Colors.White
            );
        }

        public static void HelpMenu()
        {
            for (int i = 0; i < Cons.HelpMenuTexts.Length; i++)
            {
                Drawing.Text(
                    new Rect(
                        500f,
                        200f + (20f * i),
                        Constants.Locations.boxSize.box_200,
                        Constants.Locations.boxSize.box_20
                        ),
                    Cons.HelpMenuTexts[i],
                    Constants.Colors.White
                    );
            }
        }

        public static void DisplayMenu()
        {
            if (Switches.DisplayHelpPlayerInfo)
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
                    Drawing.Text(new Rect(250f, Cons.ScreenHeight - 25f, 200f, 20f), "disable logger!",Constants.Colors.White);
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
            /* 
             * First Column: initial.x & initial.y * 2 (next line is 2 + n)
             * Second Column: initial.x + Cons.boxSize.box_100 & initial.y * 2 (next line is 2 + n)
             */
            Color guiBackup = GUI.color;
            GUI.color = Color.black;
            GUI.Box(new Rect(10f, 10f, 220f, 300f), "");
            GUI.color = Color.white;
            Vector2 initial = new Vector2(15f, 20f);
            GUI.Label(new Rect(1f, 1f, 200f, 20f), "Unknown.Exception.Handler");
            Switches.Draw_ESP = GUI.Toggle(new Rect(initial.x, initial.y * 2, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.Draw_ESP, "E.S.P");
            Switches.Draw_Grenades = GUI.Toggle(new Rect(initial.x, initial.y * 3, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.Draw_Grenades, "Grenade");
            Switches.Draw_Corpses = GUI.Toggle(new Rect(initial.x, initial.y * 4, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.Draw_Corpses, "Dead.Bodies");
            Switches.Draw_Loot = GUI.Toggle(new Rect(initial.x, initial.y * 5, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.Draw_Loot, "Map.Loot");
            Switches.Draw_Crosshair = GUI.Toggle(new Rect(initial.x, initial.y * 6, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.Draw_Crosshair, "Crosshair");
            Switches.Spawn_FullBright = GUI.Toggle(new Rect(initial.x, initial.y * 7, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.Spawn_FullBright, "Full.Bright");
            //Switches.LOD_Controll = GUI.Toggle(new Rect(initial.x, initial.y * 8, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.LOD_Controll, "LOD.Control");
            Switches.DisplayHelpPlayerInfo = GUI.Toggle(new Rect(initial.x, initial.y * 9, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.DisplayHelpPlayerInfo, "Player.Data");
            Switches.StreamerMode = GUI.Toggle(new Rect(initial.x, initial.y * 10, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.StreamerMode, "Streamer.Mode");
            Switches.SnapLines = GUI.Toggle(new Rect(initial.x, initial.y * 11, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.SnapLines, "Snap.Lines");
            Switches.AimingAtNikita = GUI.Toggle(new Rect(initial.x + Constants.Locations.boxSize.box_100, initial.y * 2, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), Switches.AimingAtNikita, "Aim");
            //Switches.Aim_Smoothing = GUI.Toggle(new Rect(initial.x + Cons.boxSize.box_100, initial.y * 3, Cons.boxSize.box_100, Cons.boxSize.box_20), Switches.Aim_Smoothing, "Smoothing");
            //GUI.Label(new Rect(initial.x + Cons.boxSize.box_150, initial.y * 4, Cons.boxSize.box_50, Cons.boxSize.box_20), "Speed:" + Cons.Aim.aimSpeed.ToString());
            //Cons.Aim.aimSpeed = (int)GUI.HorizontalSlider(new Rect(initial.x + Cons.boxSize.box_100, initial.y * 4, 50f, 20f), (float)Cons.Aim.aimSpeed, 1, 25);
            GUI.Label(new Rect(initial.x, initial.y * 1, Constants.Locations.boxSize.box_100, Constants.Locations.boxSize.box_20), "FOV:" + Cons.Aim.AAN_FOV.ToString());
            Cons.Aim.AAN_FOV = (int)GUI.HorizontalSlider(new Rect(initial.x + Constants.Locations.boxSize.box_100, initial.y * 2, 100f, 20f), (float)Cons.Aim.AAN_FOV, 1f, 25f);
            Cons.LootSearcher = GUI.TextField(new Rect(initial.x + Constants.Locations.boxSize.box_100, initial.y * 5, 100f, 20f), Cons.LootSearcher);
            //GUI.Label(new Rect(initial.x + Cons.boxSize.box_150, initial.y * 4, Cons.boxSize.box_100, Cons.boxSize.box_20), "Dis3d:" + Cons.Aim.distanceToScan.ToString());
            //Cons.Aim.distanceToScan = (int)GUI.HorizontalSlider(new Rect(initial.x + Cons.boxSize.box_100, initial.y * 4, 50f, 20f), (float)Cons.Aim.distanceToScan, 10f, 800f);
            //FINSHED
            GUI.color = guiBackup;
        }
    }
}
