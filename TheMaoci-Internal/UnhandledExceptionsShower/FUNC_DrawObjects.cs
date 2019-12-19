using System;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using EFT.Interactive;
#pragma warning disable CS0168
namespace UnhandledException
{
    class FUNC_DrawObjects
    {

        #region - CORPSES -
        public static void DrawPDB(List<LootItem> _lContainer)
        {
            if (_lContainer == null)
                return;
            var e = _lContainer.GetEnumerator();
            var LabelSize = new GUIStyle { fontSize = 12 };
            float deltaDistance = 25f;
            float devLabel = 1f;
            while (e.MoveNext())
            {
                try
                {
                    var item = e.Current;
                    if (item != null)
                    {
                        if (Camera.main.WorldToScreenPoint(item.transform.position).z > 0.01f)
                        { // do not display out of bounds items
                            float distance = FastMath.FD(Camera.main.transform.position, item.transform.position);
                            if (distance < Cons.Distances.Corpses)
                            {
                                Vector3 itemPosition = Camera.main.WorldToScreenPoint(item.transform.position);
                                float[] boxSize = new float[2] { 3f, 1.5f };
                                int FontSize = 12;
                                FastMath.DistSizer(distance, ref FontSize, ref deltaDistance, ref devLabel);
                                LabelSize.fontSize = FontSize;
                                LabelSize.normal.textColor = new Color(.7f, .7f, .7f, .8f);
                                string distanceText = $"{(int)distance}m";
                                Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                                Drawing.P(
                                    new Vector2(
                                        itemPosition.x - boxSize[1],
                                        (float)(Screen.height - itemPosition.y) - boxSize[1]
                                        ),
                                    Constants.Colors.ESP.bodies,
                                    boxSize[0]
                                );
                                Drawing.DrawShadow(
                                    new Rect(
                                        itemPosition.x - sizeOfText.x / 2f,
                                        (float)Screen.height - itemPosition.y - deltaDistance - 1,
                                        sizeOfText.x,
                                        sizeOfText.y
                                        ),
                                    new GUIContent(distanceText),
                                    LabelSize,
                                    Constants.Colors.ESP.bodies,
                                    Constants.Colors.Black,
                                    new Vector2(1f, 1f)
                                );
                            }
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    ErrorHandler.Catch("Corpses", ex);
                }
            }
        }
        #endregion

        #region - LOOT -
        public static void DrawDLI(List<LootItem> _lContainer) {
            if (_lContainer == null)
                return;
            var e = _lContainer.GetEnumerator();
            var LabelSize = new GUIStyle { fontSize = 12 };
            float deltaDistance = 25f;
            float devLabel = 1f;
            while (e.MoveNext())
            {
                try
                {
                    var item = e.Current;
                    if (item != null)
                    {
                        if (Camera.main.WorldToScreenPoint(item.transform.position).z > 0.01f)
                        { // do not display out of bounds items
                            float distance = FastMath.FD(Camera.main.transform.position, item.transform.position);
                            if (distance < Cons.Distances.Loot)
                            {
                                Vector3 itemPosition = Camera.main.WorldToScreenPoint(item.transform.position);
                                float[] boxSize = new float[2] { 3f, 1.5f };
                                int FontSize = 12;
                                FastMath.DistSizer(distance, ref FontSize, ref deltaDistance, ref devLabel);
                                LabelSize.fontSize = FontSize;
                                LabelSize.normal.textColor = Constants.Colors.ESP.items;
                                string distanceText = $"{(int)distance}m";
                                string DebugText = "";
                                try
                                {
                                    DebugText = item.Item.ShortName.Localized();
                                }
                                catch (Exception exp) {
                                    ErrorHandler.Catch("LootTranslation", exp, item.Item.ShortName);
                                    DebugText = ""; }
                                Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                                GUI.color = Constants.Colors.ESP.items;
                                Drawing.P(
                                    new Vector2(
                                        itemPosition.x - boxSize[1],
                                        (float)(Screen.height - itemPosition.y) - boxSize[1]
                                        ),
                                    Constants.Colors.ESP.items,
                                    boxSize[0]
                                );
                                GUI.Label(
                                    new Rect(
                                        itemPosition.x - sizeOfText.x / 2f,
                                        (float)Screen.height - itemPosition.y - deltaDistance - 1,
                                        sizeOfText.x,
                                        sizeOfText.y
                                    ),
                                    distanceText,
                                    LabelSize
                                );
                                GUI.Label(
                                    new Rect(
                                        itemPosition.x - sizeOfText.x / 2f,
                                        (float)Screen.height - itemPosition.y - deltaDistance - FontSize - 1,
                                        sizeOfText.x,
                                        sizeOfText.y
                                    ),
                                    DebugText,
                                    LabelSize
                                );
                            } 
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    ErrorHandler.Catch("LootItems", ex);
                }
            }
        }
        #endregion

        #region - Grenade ESP -
        public static void DrawDTG(List<Throwable> _g, Player _localP) {
            // 100m for grenades is more then enough
            if (_g == null || _localP == null)
                return;
            var e = _g.GetEnumerator();
            var LabelSize = new GUIStyle { fontSize = 12 };
            float deltaDistance = 25f;
            float devLabel = 1f;
            while (e.MoveNext()) {
                try
                {
                    var throwable = e.Current;
                    if (throwable != null) {
                        if (Camera.main.WorldToScreenPoint(throwable.transform.position).z > 0.01f)
                        {
                            float dTO = FastMath.FD(Camera.main.transform.position, throwable.transform.position);
                            if (dTO > Cons.Distances.Grenade)
                                continue;

                            Vector3 pGrenadePosition = Camera.main.WorldToScreenPoint(throwable.transform.position);
                            int FontSize = 10;
                            FastMath.DistSizer(dTO, ref FontSize, ref deltaDistance, ref devLabel);
                            LabelSize.fontSize = FontSize;
                            LabelSize.normal.textColor = Constants.Colors.ESP.grenades;
                            string distanceText = $"{(int)dTO}m";
                            Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                            GUI.color = Constants.Colors.ESP.grenades;
                            Drawing.P(
                                new Vector2(
                                    pGrenadePosition.x - 1.5f,
                                    (float)(Screen.height - pGrenadePosition.y) - 1.5f
                                    ),
                                Constants.Colors.ESP.grenades,
                                3f
                            );
                            Drawing.DrawShadow(
                                new Rect(
                                    pGrenadePosition.x - sizeOfText.x / 2f,
                                    (float)Screen.height - pGrenadePosition.y - deltaDistance - 1,
                                    sizeOfText.x,
                                    sizeOfText.y
                                    ),
                                new GUIContent(distanceText),
                                LabelSize,
                                Constants.Colors.ESP.grenades,
                                Constants.Colors.Black,
                                new Vector2(1f, 1f)
                            );
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    ErrorHandler.Catch("Grenade", ex);
                }
            }

        }
        #endregion

        #region - Player ESP -
        private class ESP {
            public enum PlayerType {
                Scav,
                PlayerScav,
                Player,
                TeamMate,
                Boss
            }
            public static void PlayerBones(float distance, Player player) {
                if (distance < 100f)
                {
                    var pRPVect = Camera.main.WorldToScreenPoint(player.PlayerBones.RightPalm.position);
                    var PLPVect = Camera.main.WorldToScreenPoint(player.PlayerBones.LeftPalm.position);
                    var PLShVect = Camera.main.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position);
                    var PLRShVect = Camera.main.WorldToScreenPoint(player.PlayerBones.RightShoulder.position);
                    var PLNeckVect = Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position);
                    var PLCentrVect = Camera.main.WorldToScreenPoint(player.PlayerBones.Pelvis.position);
                    var PLRTighVect = Camera.main.WorldToScreenPoint(player.PlayerBones.RightThigh2.position);
                    var PLLTighVect = Camera.main.WorldToScreenPoint(player.PlayerBones.LeftThigh2.position);
                    var PLRFootVect = Camera.main.WorldToScreenPoint(player.PlayerBones.KickingFoot.position);
                    var PLLFootVect = Camera.main.WorldToScreenPoint(Cons.GetBonePosByID(player, 18));
                    var PLLBowVect = Camera.main.WorldToScreenPoint(Cons.GetBonePosByID(player, 91));
                    var PLRBowVect = Camera.main.WorldToScreenPoint(Cons.GetBonePosByID(player, 112));
                    var PLLKneeVect = Camera.main.WorldToScreenPoint(Cons.GetBonePosByID(player, 17));
                    var PLRKneeVect = Camera.main.WorldToScreenPoint(Cons.GetBonePosByID(player, 22));
                    Color Backup = GUI.color;
                    GUI.color = Color.white;
                    if (Cons.inScreen(PLNeckVect) && Cons.inScreen(PLCentrVect))
                        Drawing.DrawLine(new Vector2(PLNeckVect.x, (float)Screen.height - PLNeckVect.y), new Vector2(PLCentrVect.x, (float)Screen.height - PLCentrVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLShVect) && Cons.inScreen(PLLBowVect))
                        Drawing.DrawLine(new Vector2(PLShVect.x, (float)Screen.height - PLShVect.y), new Vector2(PLLBowVect.x, (float)Screen.height - PLLBowVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLRShVect) && Cons.inScreen(PLRBowVect))
                        Drawing.DrawLine(new Vector2(PLRShVect.x, (float)Screen.height - PLRShVect.y), new Vector2(PLRBowVect.x, (float)Screen.height - PLRBowVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLLBowVect) && Cons.inScreen(PLPVect))
                        Drawing.DrawLine(new Vector2(PLLBowVect.x, (float)Screen.height - PLLBowVect.y), new Vector2(PLPVect.x, (float)Screen.height - PLPVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLRBowVect) && Cons.inScreen(pRPVect))
                        Drawing.DrawLine(new Vector2(PLRBowVect.x, (float)Screen.height - PLRBowVect.y), new Vector2(pRPVect.x, (float)Screen.height - pRPVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLRShVect) && Cons.inScreen(PLShVect))
                        Drawing.DrawLine(new Vector2(PLRShVect.x, (float)Screen.height - PLRShVect.y), new Vector2(PLShVect.x, (float)Screen.height - PLShVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLLKneeVect) && Cons.inScreen(PLCentrVect))
                        Drawing.DrawLine(new Vector2(PLLKneeVect.x, (float)Screen.height - PLLKneeVect.y), new Vector2(PLCentrVect.x, (float)Screen.height - PLCentrVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLRKneeVect) && Cons.inScreen(PLCentrVect))
                        Drawing.DrawLine(new Vector2(PLRKneeVect.x, (float)Screen.height - PLRKneeVect.y), new Vector2(PLCentrVect.x, (float)Screen.height - PLCentrVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLLKneeVect) && Cons.inScreen(PLLFootVect))
                        Drawing.DrawLine(new Vector2(PLLKneeVect.x, (float)Screen.height - PLLKneeVect.y), new Vector2(PLLFootVect.x, (float)Screen.height - PLLFootVect.y), Color.white, 1f);
                    if (Cons.inScreen(PLRKneeVect) && Cons.inScreen(PLRFootVect))
                        Drawing.DrawLine(new Vector2(PLRKneeVect.x, (float)Screen.height - PLRKneeVect.y), new Vector2(PLRFootVect.x, (float)Screen.height - PLRFootVect.y), Color.white, 1f);
                    GUI.color = Backup;
                }
            }
            public static Color PlayerColor(PlayerType playerType) {
                switch (playerType) {
                    case PlayerType.Scav:
                        return Constants.Colors.ESP.npc;
                    case PlayerType.PlayerScav:
                        return Constants.Colors.ESP.scav_player;
                    case PlayerType.Player:
                        return Constants.Colors.ESP.player[1];
                    case PlayerType.Boss:
                        return Constants.Colors.ESP.npc;
                    case PlayerType.TeamMate:
                        return Constants.Colors.ESP.group;
                    default:
                        return Constants.Colors.ESP.npc;
                }
            }
            public static string PlayerName(Player player, ref PlayerType playerType) {
                if (player.Profile.Info.RegistrationDate <= 0)
                {
                    playerType = PlayerType.Scav;
                    return "";
                }
                else if (Cons.LocalPlayer.isInYourGroup(player))
                {
                    playerType = PlayerType.TeamMate;
                    if (Cons.Switches.StreamerMode)
                    {
                        return "team";
                    }
                    else
                    {
                        return player.Profile.Info.Nickname;
                    }
                }
                else if (player.Profile.Info.Side == EPlayerSide.Savage)
                {
                    playerType = PlayerType.PlayerScav;
                    return "";
                }
                else
                {
                    playerType = PlayerType.Player;
                    if (Cons.Switches.StreamerMode)
                    {
                        return player.Profile.Info.Side.ToString() + " [" + player.Profile.Info.Level.ToString() + "]";
                    }
                    else
                    {
                        return player.Profile.Info.Nickname + " [" + player.Profile.Info.Level.ToString() + "]";
                    }
                }
            }
        }
        public static void DrawPlayers(List<Player> _PlayersList, Player LocalPlayer)
        {
            #region [INITIALS] - to skip data overflow (incase)
            float deltaDistance = 25f;
            string playerDisplayName = "";
            float devLabel = 1f;
            string Status = "";
            var LabelSize = new GUIStyle { fontSize = 12 };
            Color playerColor = Constants.Colors.ESP.npc;
            float distancesAxisY_0 = 0;
            float distancesAxisY_1 = 0;
            float distancesAxisY_2 = 0;
            Color Backup;
            #endregion
            var e = _PlayersList.GetEnumerator();
            while (e.MoveNext())
            {
                var player = e.Current;
                if (Cons.inScreen(Camera.main.WorldToScreenPoint(player.Transform.position)))
                {
                    float dTO = FastMath.FD(Camera.main.transform.position, player.Transform.position);
                    // main head vector 3d (x,y,z)
                    Vector3 pHeadVector = Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position);
                    // setting head size comparing head position and neck position and multiplying by 1.5 (actually its head size)
                    float find_sizebox = Math.Abs(pHeadVector.y - Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position).y) * 1.5f; // size of the head - its not good but its scaling without much maths
                                                                                                                                              // making sure head will not be too big
                    find_sizebox = (find_sizebox > 30f) ? 30f : find_sizebox;
                    float half_sizebox = (find_sizebox > 30f) ? 15f : find_sizebox / 2f;
                    // size of fonts depending on distance
                    int FontSize = 12;
                    FastMath.DistSizer(dTO, ref FontSize, ref deltaDistance, ref devLabel);
                    LabelSize.fontSize = FontSize;
                    //create 3 size table of distances for texts (name, status, weapon)
                    distancesAxisY_0 = deltaDistance + 10f;
                    distancesAxisY_1 = distancesAxisY_0 + FontSize + 1;
                    distancesAxisY_2 = distancesAxisY_1 + FontSize + 1;
                    Status = ((int)(player.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Common).Current)).ToString() + " hp"; // Health here 

                    #region [BONE-ESP]
                    if (Cons.Switches.ShowBones)
                    {
                        ESP.PlayerBones(dTO, player);
                    }
                    #endregion
                    ESP.PlayerType playerType = ESP.PlayerType.Scav;
                    playerDisplayName = ESP.PlayerName(player, ref playerType);
                    playerColor = ESP.PlayerColor(playerType);
                    if (playerType != ESP.PlayerType.TeamMate)
                    {
                        Backup = GUI.color;
                        GUI.color = Color.red;
                        Drawing.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Constants.Colors.Red, find_sizebox);
                        GUI.color = Backup;
                    }
                    #region [VISIBILITY-CHECK]
                    string isVisible = "";
                    if (Raycast.BodyRaycastCheck(player.gameObject, pHeadVector, pHeadVector, pHeadVector, pHeadVector, pHeadVector))
                    {
                        isVisible = "+";
                    }
                    #endregion
                    #region [INIT-Texts]
                    string nameNickname = $"{playerDisplayName}";
                    string playerStatus = $"{isVisible}[{(int)dTO}m] {Status}";
                    string WeaponName = "";
                    #endregion
                    #region [TRY-DecodeWeaponName]
                    try
                    {
                        WeaponName = player.Weapon.ShortName.Localized();
                    }
                    catch (Exception excep)
                    {
                        WeaponName = "No Weapon";
                    }
                    #endregion

                    // set colors now
                    LabelSize.normal.textColor = playerColor;
                    #region Slot 0 - Player Name (vector, size, drawing)
                    if (nameNickname != "")
                    {
                        Vector2 vector_playerName = GUI.skin.GetStyle(nameNickname).CalcSize(new GUIContent(nameNickname));
                        float player_NameText = (devLabel == 1f) ? vector_playerName.x : (vector_playerName.x / devLabel);
                        Drawing.DrawShadow(
                            new Rect(
                                pHeadVector.x - player_NameText / 2f,
                                (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY_0,
                                player_NameText,
                                vector_playerName.y
                                ),
                            new GUIContent(nameNickname),
                            LabelSize,
                            playerColor,
                            Constants.Colors.Black,
                            new Vector2(1f, 1f)
                        );
                    }
                    #endregion
                    #region Slot 1 - Status (distance, health)
                    Vector2 vector_playerStatus = GUI.skin.GetStyle(playerStatus).CalcSize(new GUIContent(playerStatus));
                    float player_TextWidth = (devLabel == 1f) ? vector_playerStatus.x : (vector_playerStatus.x / devLabel);
                    GUIContent content = new GUIContent(playerStatus);
                    Drawing.DrawShadow(
                        new Rect(
                            pHeadVector.x - player_TextWidth / 2f,
                            (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY_1,
                            player_TextWidth,
                            vector_playerStatus.y
                            ),
                        content,
                        LabelSize,
                        playerColor,
                        Constants.Colors.Black,
                        new Vector2(1f, 1f)
                    );
                    #endregion
                    #region Slot 2 - Weapon Name (vector, size, drawing) - if not empty
                    if (WeaponName != "")
                    {
                        Vector2 vector_WeaponName = GUI.skin.GetStyle(WeaponName).CalcSize(new GUIContent(WeaponName));
                        float player_WeaponName = (devLabel == 1f) ? vector_WeaponName.x : (vector_WeaponName.x / devLabel);
                        Drawing.DrawShadow(
                            new Rect(
                                pHeadVector.x - player_WeaponName / 2f,
                                (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY_2,
                                player_WeaponName,
                                vector_WeaponName.y
                                ),
                            new GUIContent(WeaponName),
                            LabelSize,
                            playerColor,
                            Constants.Colors.Black,
                            new Vector2(1f, 1f)
                        );
                    }
                    #endregion
                }
                #region snap lines
                if (Cons.Switches.SnapLines && player != Cons.Main._localPlayer)
                {
                    Vector3 w2s = Camera.main.WorldToScreenPoint(player.PlayerBones.RootJoint.position);
                    if (Cons.inScreen_SnapLines(w2s))
                    {
                        Drawing.DrawLine(
                            new Vector2(
                                (Screen.width / 2), 
                                Screen.height
                                ), 
                            new Vector2(
                                w2s.x, 
                                Screen.height - w2s.y
                                ), 
                            playerColor
                       );
                    }
                }
                #endregion
            }
        }
        #endregion

        #region - Exfils -
        private class Exfiltration {
            public static string TypeOfExfiltration(EExfiltrationStatus status) {
                switch (status)
                {
                    case EExfiltrationStatus.AwaitsManualActivation:
                        return "ManualActivation";
                    case EExfiltrationStatus.Countdown:
                        return "Timer";
                    case EExfiltrationStatus.NotPresent:
                        return "n/a";
                    case EExfiltrationStatus.Pending:
                        return "Pending";
                    case EExfiltrationStatus.RegularMode:
                        return "Default";
                    case EExfiltrationStatus.UncompleteRequirements:
                        return "Requirements";
                    default:
                        return "";
                }
            }
        }
        public static void DrawExfils(List<ExfiltrationPoint> _exfils)
        {
            if (_exfils == null)
                return;
            var e = _exfils.GetEnumerator();
            var LabelSize = new GUIStyle { fontSize = 12 };
            float deltaDistance = 25f;
            float devLabel = 1f;
            while (e.MoveNext())
            {
                try
                {
                    var exfil = e.Current;
                    if (exfil != null)
                    {
                        if (Camera.main.WorldToScreenPoint(exfil.transform.position).z > 0.01f)
                        { // do not display out of bounds items
                            float distance = FastMath.FD(Camera.main.transform.position, exfil.transform.position);
                            if (distance < Cons.Distances.Corpses)
                            {
                                Vector3 itemPosition = Camera.main.WorldToScreenPoint(exfil.transform.position);
                                float[] boxSize = new float[2] { 3f, 1.5f };
                                int FontSize = 12;
                                FastMath.DistSizer(distance, ref FontSize, ref deltaDistance, ref devLabel);
                                LabelSize.fontSize = FontSize;
                                LabelSize.normal.textColor = new Color(.7f, .7f, .7f, .8f);
                                string requirements = (exfil.HasRequirements) ? "req" : "";
                                string exfil_Status = Exfiltration.TypeOfExfiltration(exfil.Status);

                                string distanceText = $"({(int)distance}m){requirements}";
                                Vector2 sizeOfText2 = GUI.skin.GetStyle(exfil_Status).CalcSize(new GUIContent(exfil_Status));
                                Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                                Drawing.P(
                                    new Vector2(
                                        itemPosition.x - boxSize[1],
                                        (float)(Screen.height - itemPosition.y) - boxSize[1]
                                        ),
                                    Constants.Colors.Red,
                                    boxSize[0]
                                );
                                Drawing.DrawShadow(
                                    new Rect(
                                        itemPosition.x - sizeOfText2.x / 2f,
                                        (float)Screen.height - itemPosition.y - deltaDistance - FontSize - 1,
                                        sizeOfText2.x,
                                        sizeOfText2.y
                                        ),
                                    new GUIContent(exfil_Status),
                                    LabelSize,
                                    Constants.Colors.Red,
                                    Constants.Colors.Black,
                                    new Vector2(1f, 1f)
                                );
                                Drawing.DrawShadow(
                                    new Rect(
                                        itemPosition.x - sizeOfText.x / 2f,
                                        (float)Screen.height - itemPosition.y - deltaDistance - 1,
                                        sizeOfText.x,
                                        sizeOfText.y
                                        ),
                                    new GUIContent(distanceText),
                                    LabelSize,
                                    Constants.Colors.Red,
                                    Constants.Colors.Black,
                                    new Vector2(1f, 1f)
                                );
                            }
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    ErrorHandler.Catch("Exfils", ex);
                }
            }
        }
        #endregion

        #region - Containers -
        public static void DrawContainers(Dictionary<GInterface162, EFT.GameWorld.GStruct63>.Enumerator _lootableContainers)
        {
            var e = _lootableContainers;
            var LabelSize = new GUIStyle { fontSize = 12 };
            float deltaDistance = 25f;
            float devLabel = 1f;
            while (e.MoveNext())
            {
                try
                {
                    var Container = e.Current.Key;
                    var Location = e.Current.Value;
                    if (Container.RootItem.IsContainer)
                    {
                        #region Find Item Inside if specified
                        if (Cons.LootSearcher != "")
                        {
                            bool foundItem = false;
                            var inside = Container.Items.GetEnumerator();
                            while (inside.MoveNext())
                            {
                                var item = inside.Current;
                                if (item.ShortName.Localized().ToLower().IndexOf(Cons.LootSearcher) >= 0)
                                {
                                    foundItem = true;
                                    break;
                                }
                            }
                            if (!foundItem) { continue; }
                        }
                        #endregion
                        if (Camera.main.WorldToScreenPoint(Location.Transform.position).z > 0.01f)
                        { // do not display out of bounds items
                            float distance = FastMath.FD(Camera.main.transform.position, Location.Transform.position);
                            if (distance < Cons.Distances.Crates)
                            {
                                Vector3 itemPosition = Camera.main.WorldToScreenPoint(Location.Transform.position);
                                float[] boxSize = new float[2] { 3f, 1.5f };
                                int FontSize = 12;
                                FastMath.DistSizer(distance, ref FontSize, ref deltaDistance, ref devLabel);
                                LabelSize.fontSize = FontSize;
                                LabelSize.normal.textColor = new Color(.7f, .7f, .7f, .8f);
                                string distanceText = $"<{(int)distance}m>";
                                Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                                Drawing.P(
                                    new Vector2(
                                        itemPosition.x - boxSize[1],
                                        (float)(Screen.height - itemPosition.y) - boxSize[1]
                                        ),
                                    Constants.Colors.ESP.items,
                                    boxSize[0]
                                );
                                Drawing.DrawShadow(
                                    new Rect(
                                        itemPosition.x - sizeOfText.x / 2f,
                                        (float)Screen.height - itemPosition.y - deltaDistance - 1,
                                        sizeOfText.x,
                                        sizeOfText.y
                                        ),
                                    new GUIContent(distanceText),
                                    LabelSize,
                                    Constants.Colors.ESP.items,
                                    Constants.Colors.Black,
                                    new Vector2(1f, 1f)
                                );
                            }
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    ErrorHandler.Catch("Containers", ex);
                }
            }
        }
        #endregion
    }
}
#pragma warning disable CS0168
