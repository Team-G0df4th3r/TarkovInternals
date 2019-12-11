using EFT;
using UnityEngine;
using UnhandledExceptionHandler.Functions;

namespace UnhandledExceptionHandler
{
    class DF
    {
        public static void DrawAlive(int Players_Alive_all, int Players_Alive_hun, int Players_Alive_ht2, Vector2 InitialPosition)
        {
            if (Players_Alive_all != 0)
                EDS.Text(new Rect(Cons.Alive.All.x, Cons.Alive.All.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "Alive: " + (Players_Alive_all).ToString(), Statics.Colors.White);
            if (Players_Alive_hun != 0)
                EDS.Text(new Rect(Cons.Alive.zero_to_hundred.x, Cons.Alive.zero_to_hundred.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "<100m:" + (Players_Alive_hun).ToString(), Statics.Colors.White);
            if (Players_Alive_ht2 != 0)
                EDS.Text(new Rect(Cons.Alive.hundred_to_2fifty.x, Cons.Alive.hundred_to_2fifty.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "100m - 250m:" + (Players_Alive_ht2).ToString(), Statics.Colors.White);
        }
        
        public static void DrawRecoil(Player _localPlayer) {
            if (_localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 1f)
                EDS.Text(new Rect(Cons.Recoil.position.x, Cons.Recoil.position.y, Cons.boxSize.box_100, Cons.boxSize.box_20), "Recoil: " + (_localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity * 100).ToString() + "%", Statics.Colors.White);
        }
        public static void HealthInfo(Player _localPlayer)
        {
            EDS.Text(new Rect(Cons.Status.CommonHealth.x, Cons.Status.CommonHealth.y, Cons.boxSize.box_100, Cons.boxSize.box_20), "Total: " + Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.Common), Statics.Colors.White);
            EDS.Text(new Rect(Cons.Status.Energy.x, Cons.Status.Energy.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "Energy: " + ((int)_localPlayer.HealthController.Energy.Current).ToString() + "/" + _localPlayer.HealthController.Energy.Maximum.ToString(), Statics.Colors.White);
            EDS.Text(new Rect(Cons.Status.Hydration.x, Cons.Status.Hydration.y, Cons.boxSize.box_200, Cons.boxSize.box_20), "Hydro: " + ((int)_localPlayer.HealthController.Hydration.Current).ToString() + "/" + _localPlayer.HealthController.Hydration.Maximum.ToString(), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Head.x, Cons.InitialHealthBox.y + Cons.HealthBox.Head.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.Head), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Chest.x, Cons.InitialHealthBox.y + Cons.HealthBox.Chest.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.Chest), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Left_Arm.x, Cons.InitialHealthBox.y + Cons.HealthBox.Left_Arm.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.LeftArm), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Right_Arm.x, Cons.InitialHealthBox.y + Cons.HealthBox.Right_Arm.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.RightArm), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Left_Leg.x, Cons.InitialHealthBox.y + Cons.HealthBox.Left_Leg.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.LeftLeg), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Right_Leg.x, Cons.InitialHealthBox.y + Cons.HealthBox.Right_Leg.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.RightLeg), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthBox.Stomach.x, Cons.InitialHealthBox.y + Cons.HealthBox.Stomach.y, Cons.boxSize.box_50, Cons.boxSize.box_20), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.Stomach), Statics.Colors.White);
        }
    }
}
