using EFT;
using UnityEngine;

namespace UnhandledException
{
    class FUNC_Additional_Drawing
    {
        private static void DrawAlive()
        {
            if (Cons.AliveCount.All != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.All.x, Constants.Locations.Alive.All.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "Alive:" + (Cons.AliveCount.All).ToString(), Constants.Colors.White);
            if (Cons.AliveCount.dist_0_25 != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.d0_25.x, Constants.Locations.Alive.d0_25.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "0-25m:" + (Cons.AliveCount.dist_0_25).ToString(), Constants.Colors.White);
            if (Cons.AliveCount.dist_25_50 != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.d25_50.x, Constants.Locations.Alive.d25_50.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "25-50m:" + (Cons.AliveCount.dist_25_50).ToString(), Constants.Colors.White);
            if (Cons.AliveCount.dist_50_100 != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.d50_100.x, Constants.Locations.Alive.d50_100.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "50-100m:" + (Cons.AliveCount.dist_50_100).ToString(), Constants.Colors.White);
            if (Cons.AliveCount.dist_100_250 != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.d100_250.x, Constants.Locations.Alive.d100_250.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "100-250m:" + (Cons.AliveCount.dist_100_250).ToString(), Constants.Colors.White);
            if (Cons.AliveCount.dist_250_1000 != 0)
                Drawing.Text(new Rect(Constants.Locations.Alive.d250_1000.x, Constants.Locations.Alive.d250_1000.y, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20), "250-1000m:" + (Cons.AliveCount.dist_250_1000).ToString(), Constants.Colors.White);
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
                "Total: " + Cons.LocalPlayer.Status.Health.Common, 
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.Status.Energy.x, 
                    Constants.Locations.Status.Energy.y, 
                    Constants.Locations.boxSize.box_200, 
                    Constants.Locations.boxSize.box_20
                    ), 
                "Energy: " + Cons.LocalPlayer.Status.Energy, 
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.Status.Hydration.x, 
                    Constants.Locations.Status.Hydration.y, 
                    Constants.Locations.boxSize.box_200, 
                    Constants.Locations.boxSize.box_20
                    ), 
                "Hydro: " + Cons.LocalPlayer.Status.Hydration,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Head.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Head.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.Head,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Chest.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Chest.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.Chest,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Left_Arm.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Left_Arm.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.LeftArm,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Right_Arm.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Right_Arm.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.RightArm,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Left_Leg.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Left_Leg.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.LeftLeg,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Right_Leg.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Right_Leg.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.RightLeg,
                Constants.Colors.White
            );
            Drawing.Text(
                new Rect(
                    Constants.Locations.InitialHealthBox.x + Constants.Locations.HealthBox.Stomach.x, 
                    Constants.Locations.InitialHealthBox.y + Constants.Locations.HealthBox.Stomach.y, 
                    Constants.Locations.boxSize.box_50, 
                    Constants.Locations.boxSize.box_20
                    ),
                Cons.LocalPlayer.Status.Health.Stomach,
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
            if (Cons.Switches.DisplayHelpPlayerInfo)
            {
                if (Cons.Main._localPlayer != null)
                {
                    #region Alive Number Display
                    DrawAlive();
                    #endregion
                    DrawRecoil();
                    #region Player Health
                    HealthInfo();
                    #endregion
                }
                #region Error Logs Enabled Display Message
                if (Debug.unityLogger.logEnabled == true)
                {
                    Drawing.Text(new Rect(250f, Cons.ScreenHeight.Full - 25f, 200f, 20f), "disable logger!",Constants.Colors.White);
                }
                #endregion
            }
            #region draw crosshair
            if (Cons.Switches.Draw_Crosshair)
            {
                if (!Cons.Main._localPlayer.ProceduralWeaponAnimation.IsAiming)
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
            GUI.Box(new Rect(10f, 10f, 220f, 450f), "");
            GUI.color = Color.white;
            Vector2 initial = new Vector2(15f, 20f);
            Drawing.Label("Unknown.Exception.Handler", 0, 0, Constants.Locations.boxSize.box_200, Constants.Locations.boxSize.box_20);
            // First column
            Drawing.CheckBox(ref Cons.Switches.Draw_ESP, "E.S.P", 2);
            Drawing.CheckBox(ref Cons.Switches.Draw_Grenades, "Grenade", 3);
            Drawing.CheckBox(ref Cons.Switches.Draw_Corpses, "Dead.Bodies", 4);
            Drawing.CheckBox(ref Cons.Switches.Draw_Loot, "Map.Loot", 5);
            Drawing.CheckBox(ref Cons.Switches.Draw_Crosshair, "Crosshair", 6); 
            Drawing.CheckBox(ref Cons.Switches.Spawn_FullBright, "Full.Bright", 7);
            Drawing.CheckBox(ref Cons.Switches.DisplayHelpPlayerInfo, "Player.Data", 8);
            Drawing.CheckBox(ref Cons.Switches.StreamerMode, "Streamer.Mode", 9);
            Drawing.CheckBox(ref Cons.Switches.SnapLines, "Snap.Lines", 10);
            Drawing.CheckBox(ref Cons.Switches.ShowBones, "Draw.Bones", 10, 1);
            Drawing.CheckBox(ref Cons.Switches.AimingAtNikita, "Aim", 11);
            Drawing.Label("FOV:" + Cons.Aim.AAN_FOV.ToString(), 12);
            Drawing.HorizontalSlider(ref Cons.Aim.AAN_FOV, 1f, 25f, 13);
            Drawing.Label("AimDist:" + Cons.Distances.Aim.ToString(), 14);
            Drawing.HorizontalSlider(ref Cons.Distances.Aim, 100f, 1000f, 15);
            Drawing.Label("LootDist:" + Cons.Distances.Loot.ToString(), 16);
            Drawing.HorizontalSlider(ref Cons.Distances.Loot, 100f, 1000f, 17);
            Drawing.Label("GrenadeDist:" + Cons.Distances.Grenade.ToString(), 18);
            Drawing.HorizontalSlider(ref Cons.Distances.Grenade, 100f, 1000f, 19);
            Drawing.Label("CorpseDist:" + Cons.Distances.Corpses.ToString(), 20);
            Drawing.HorizontalSlider(ref Cons.Distances.Corpses, 100f, 1000f, 21);
            //i know what im doing
            Drawing.CheckBox(ref Cons.Switches.IKnowWhatImDoing, "IKWID", 1, 2);
            Drawing.Button(ref Cons.Buttons.Ma0c1, "Maoci", 2, 2);
            Drawing.Button(ref Cons.Buttons.Niger, "Niger", 3, 2);
            // Second column indicates with column = 1
            if (!Cons.Switches.Draw_Loot)
                Drawing.TextField(ref Cons.LootSearcher, 5, 1);
            //FINSHED
            GUI.color = guiBackup;
        }
    }
}
