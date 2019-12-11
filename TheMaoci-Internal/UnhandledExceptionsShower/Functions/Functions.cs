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
                EDS.Text(new Rect(InitialPosition.x, InitialPosition.y, Cons.boxSize[20], Cons.boxSize[2]), "Alive: " + (Players_Alive_all).ToString(), Statics.Colors.White);
            if (Players_Alive_hun != 0)
                EDS.Text(new Rect(InitialPosition.x, InitialPosition.y + 20f, Cons.boxSize[20], Cons.boxSize[2]), "<100m:" + (Players_Alive_hun).ToString(), Statics.Colors.White);
            if (Players_Alive_ht2 != 0)
                EDS.Text(new Rect(InitialPosition.x, InitialPosition.y + 40f, Cons.boxSize[20], Cons.boxSize[2]), "100m - 250m:" + (Players_Alive_ht2).ToString(), Statics.Colors.White);
        }
        public static void DrawRecoil(Player _localPlayer) {
            if (_localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity != 1f)
                EDS.Text(new Rect(1f, 285f, Cons.boxSize[10], Cons.boxSize[2]), "Recoil: " + (_localPlayer.ProceduralWeaponAnimation.Shootingg.Intensity * 100).ToString() + "%", Statics.Colors.White);
        }
        public static void HealthInfo(Player _localPlayer)
        {
            EDS.Text(new Rect(1f, 15f, Cons.boxSize[10], Cons.boxSize[2]), "Total: " + Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.Common), Statics.Colors.White);
            EDS.Text(new Rect(1f, 245f, Cons.boxSize[20], Cons.boxSize[2]), "Energy: " + ((int)_localPlayer.HealthController.Energy.Current).ToString() + "/" + _localPlayer.HealthController.Energy.Maximum.ToString(), Statics.Colors.White);
            EDS.Text(new Rect(1f, 265f, Cons.boxSize[20], Cons.boxSize[2]), "Hydro: " + ((int)_localPlayer.HealthController.Hydration.Current).ToString() + "/" + _localPlayer.HealthController.Hydration.Maximum.ToString(), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthPosition[0, 0], Cons.InitialHealthBox.y + Cons.HealthPosition[0, 1], Cons.boxSize[5], Cons.boxSize[2]), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.Head), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthPosition[1, 0], Cons.InitialHealthBox.y + Cons.HealthPosition[1, 1], Cons.boxSize[5], Cons.boxSize[2]), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.Chest), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthPosition[2, 0], Cons.InitialHealthBox.y + Cons.HealthPosition[2, 1], Cons.boxSize[5], Cons.boxSize[2]), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.LeftArm), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthPosition[3, 0], Cons.InitialHealthBox.y + Cons.HealthPosition[3, 1], Cons.boxSize[5], Cons.boxSize[2]), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.RightArm), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthPosition[4, 0], Cons.InitialHealthBox.y + Cons.HealthPosition[4, 1], Cons.boxSize[5], Cons.boxSize[2]), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.LeftLeg), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthPosition[5, 0], Cons.InitialHealthBox.y + Cons.HealthPosition[5, 1], Cons.boxSize[5], Cons.boxSize[2]), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.RightLeg), Statics.Colors.White);
            EDS.Text(new Rect(Cons.InitialHealthBox.x + Cons.HealthPosition[6, 0], Cons.InitialHealthBox.y + Cons.HealthPosition[6, 1], Cons.boxSize[5], Cons.boxSize[2]), Cons.Health(_localPlayer, EFT.HealthSystem.EBodyPart.Stomach), Statics.Colors.White);
        }
    }
}
