using System;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using EFT.Interactive;

namespace UnhandledExceptionHandler.Functions
{

    class Drawing_Data
    {
        #region - Drawing COLORS -
            private static Color[] C_player     = new Color[2] { new Color(1.0f, 0f, 0f, 1.0f), new Color(1.0f, 0f, 1.0f, 1.0f) };
            private static Color C_bots         = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            private static Color C_group        = new Color(0f, 0.8f, 1.0f, 1.0f);
            private static Color C_scav         = new Color(1f, 0.75f, 0.0f, 1.0f);
            private static Color C_napesy       = new Color(0f, 1.0f, 0f, 1.0f);
            private static Color C_corps        = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            private static Color C_items        = new Color(1f, 1f, 1f, .7f);
            private static Color C_overlay      = new Color(0f, 0f, 0f, 1f);
        #endregion

        #region - CORPSES -
        public static void DrawLI(List<LootItem> _lContainer, float _displayDistance = 300f)
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
                            float distance = FMath.FD(Camera.main.transform.position, item.transform.position);
                            if (distance < _displayDistance)
                            {
                                Vector3 itemPosition = Camera.main.WorldToScreenPoint(item.transform.position);
                                float[] boxSize = new float[2] { 3f, 1.5f };
                                int FontSize = 12;
                                FMath.DistSizer(distance, ref FontSize, ref deltaDistance, ref devLabel);
                                LabelSize.fontSize = FontSize;
                                LabelSize.normal.textColor = new Color(.7f, .7f, .7f, .8f);
                                string distanceText = $"{(int)distance}m - {item.Name.Localized()}";
                                Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                                GUI.color = new Color(.7f, .7f, .7f, .8f);
                                EDS.P(
                                    new Vector2(
                                        itemPosition.x - boxSize[1],
                                        (float)(Screen.height - itemPosition.y) - boxSize[1]
                                        ),
                                    C_corps,
                                    boxSize[0]
                                );
                                EDS.DrawShadow(
                                    new Rect(
                                        itemPosition.x - sizeOfText.x / 2f,
                                        (float)Screen.height - itemPosition.y - deltaDistance - 1,
                                        sizeOfText.x,
                                        sizeOfText.y
                                        ),
                                    new GUIContent(distanceText),
                                    LabelSize,
                                    C_corps,
                                    C_overlay,
                                    new Vector2(1f, 1f)
                                );
                            }
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    ErrorHandler.Catch("LoogItems", ex);
                }
            }
        }
        #endregion

        #region - LOOT -
        public static void DrawLogs(List<LootItem> _lContainer, float _displayDistance = 300f) {
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
                        //if (!item.isActiveAndEnabled && !item.IsVisibilityEnabled) // check if item stil exists ??
                            //continue;
                        if (Camera.main.WorldToScreenPoint(item.transform.position).z > 0.01f)
                        { // do not display out of bounds items
                            float distance = FMath.FD(Camera.main.transform.position, item.transform.position);
                            if (distance < _displayDistance)
                            {
                                Vector3 itemPosition = Camera.main.WorldToScreenPoint(item.transform.position);
                                float[] boxSize = new float[2] { 3f, 1.5f };
                                int FontSize = 12;
                                FMath.DistSizer(distance, ref FontSize, ref deltaDistance, ref devLabel);
                                LabelSize.fontSize = FontSize;
                                LabelSize.normal.textColor = new Color(.7f, .7f, .7f, .8f);
                                //item.TemplateId == "5909e4b686f7747f5b744fa4"; // dead Scav Body
                                string distanceText = $"{(int)distance}m";
                                string DebugText = "";
                                try
                                {
                                    DebugText = item.Item.ShortName.Localized();
                                }
                                catch (Exception exp) { DebugText = ""; }
                                Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                                GUI.color = C_items;
                                EDS.P(
                                    new Vector2(
                                        itemPosition.x - boxSize[1],
                                        (float)(Screen.height - itemPosition.y) - boxSize[1]
                                        ),
                                    C_items,
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
                    ErrorHandler.Catch("LoogItems", ex);
                }
            }
        }
        #endregion

        #region - Grenade ESP -
        private static float[] grenadeSize = new float[2] { 3f, 1.5f };
        public static void DrawBombs(List<Throwable> _g, Player _localP, float _displayDistance = 100f) {
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
                            float dTO = FMath.FD(Camera.main.transform.position, throwable.transform.position);
                            if (dTO > _displayDistance)
                                continue;

                            Vector3 pGrenadePosition = Camera.main.WorldToScreenPoint(throwable.transform.position);
                            int FontSize = 10;
                            FMath.DistSizer(dTO, ref FontSize, ref deltaDistance, ref devLabel);
                            LabelSize.fontSize = FontSize;
                            LabelSize.normal.textColor = C_napesy;
                            string distanceText = $"{(int)dTO}m";
                            Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                            GUI.color = C_napesy;
                            EDS.P(
                                new Vector2(
                                    pGrenadePosition.x - grenadeSize[1],
                                    (float)(Screen.height - pGrenadePosition.y) - grenadeSize[1]
                                    ),
                                C_napesy,
                                grenadeSize[0]
                            );
                            EDS.DrawShadow(
                                new Rect(
                                    pGrenadePosition.x - sizeOfText.x / 2f,
                                    (float)Screen.height - pGrenadePosition.y - deltaDistance - 1,
                                    sizeOfText.x,
                                    sizeOfText.y
                                    ),
                                new GUIContent(distanceText),
                                LabelSize,
                                C_napesy,
                                C_overlay,
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
        public static void DrawPlayers(List<Player> _PlayersList, Player LocalPlayer, float _viewdistance, bool switch_colors, bool displayCorpses)
        {
            float deltaDistance = 25f;
            string playerDisplayName = "";
            float devLabel = 1f;
            string Status = "";
            var LabelSize = new GUIStyle { fontSize = 12 };
            Color playerColor = C_bots;
            foreach (Player player in _PlayersList)
            {
                if (player.HealthController.IsAlive)
                {
                    float dTO = FMath.FD(Camera.main.transform.position, player.Transform.position);
                    if (dTO > 1f && dTO <= _viewdistance && Camera.main.WorldToScreenPoint(player.Transform.position).z > 0.01f)
                    {
                        // main head vector 3d (x,y,z)
                        Vector3 pHeadVector = Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position);
                        // setting head size comparing head position and neck position and multiplying by 1.5 (actually its head size)
                        float find_sizebox = Math.Abs(pHeadVector.y - Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position).y) * 1.5f; // size of the head - its not good but its scaling without much maths
                        // making sure head will not be too big
                        find_sizebox = (find_sizebox > 30f) ? 30f : find_sizebox;
                        float half_sizebox = (find_sizebox > 30f) ? 15f : find_sizebox / 2f;
                        // size of fonts depending on distance
                        int FontSize = 12;
                        FMath.DistSizer(dTO, ref FontSize, ref deltaDistance, ref devLabel);
                        LabelSize.fontSize = FontSize;
                        deltaDistance = deltaDistance + 20f;
                        //create 3 size table of distances for texts (name, status, weapon)
                        float[] distancesAxisY = new float[3] {
                            deltaDistance,
                            deltaDistance - (FontSize + 1),
                            deltaDistance - (FontSize + FontSize + 2) };

                        int health = (int)(player.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Common).Current);
                        Status = health.ToString() + " HP"; // Health here 
                        #region Set: PlayerName / Color / Head Pixel
                        if (player.Profile.Info.RegistrationDate <= 0)
                        {
                            playerDisplayName = "";
                            playerColor = C_bots;
                            GUI.color = Color.white;
                            EDS.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Color.red, find_sizebox);
                        }
                        else if (LocalPlayer.Profile.Info.GroupId == player.Profile.Info.GroupId && LocalPlayer.Profile.Info.GroupId != "0" && LocalPlayer.Profile.Info.GroupId != "" && LocalPlayer.Profile.Info.GroupId != null)
                        {
                            playerDisplayName = player.Profile.Info.Nickname;
                            playerColor = C_group;
                        }
                        else if (player.Profile.Info.Side == EPlayerSide.Savage)
                        {
                            playerDisplayName = "";
                            playerColor = C_scav;
                            GUI.color = Color.red;
                            EDS.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Color.red, find_sizebox);
                        }
                        else
                        {
                            playerDisplayName = player.Profile.Info.Nickname;
                            if (switch_colors)
                            {
                                playerColor = C_player[0];
                            }
                            else
                            {
                                playerColor = C_player[1];
                            }
                            GUI.color = Color.red;
                            EDS.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Color.red, find_sizebox);
                        }
                        #endregion
                        #region Prepare Main Texts
                        string nameNickname = $"{playerDisplayName}";
                        string playerStatus = $"[{(int)dTO}m] {Status}";
                        string WeaponName = "";
                        #endregion
                        #region Try to decode weapon name
                        try
                        {
                            WeaponName = player.Weapon.ShortName.Localized();
                        }
                        catch (Exception e)
                        {
                            UnhandledExceptionHandler.ErrorHandler.Catch("WeaponNames", e);
                        }
                        #endregion

                        // set colors now
                        LabelSize.normal.textColor = playerColor;
                        GUI.color = playerColor;
                        #region Slot 0 - Player Name (vector, size, drawing)
                        if (nameNickname != "")
                        {
                            Vector2 vector_playerName = GUI.skin.GetStyle(nameNickname).CalcSize(new GUIContent(nameNickname));
                            float player_NameText = (devLabel == 1f) ? vector_playerName.x : (vector_playerName.x / devLabel);
                            //GUI.Label(new Rect(pHeadVector.x - player_NameText / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY[0], player_NameText, vector_playerName.y), nameNickname, LabelSize);
                            EDS.DrawShadow(
                                new Rect(
                                    pHeadVector.x - player_NameText / 2f,
                                    (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY[0],
                                    player_NameText,
                                    vector_playerName.y
                                    ),
                                new GUIContent(nameNickname),
                                LabelSize,
                                playerColor,
                                C_overlay,
                                new Vector2(1f, 1f)
                            );
                        }
                        #endregion
                        #region Slot 1 - Status (distance, health)
                        Vector2 vector_playerStatus = GUI.skin.GetStyle(playerStatus).CalcSize(new GUIContent(playerStatus));
                        float player_TextWidth = (devLabel == 1f) ? vector_playerStatus.x : (vector_playerStatus.x / devLabel);
                        //GUI.Label(new Rect(pHeadVector.x - player_TextWidth / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY[1], player_TextWidth, vector_playerStatus.y), playerStatus, LabelSize);
                        GUIContent content = new GUIContent(playerStatus);
                        EDS.DrawShadow(
                            new Rect(
                                pHeadVector.x - player_TextWidth / 2f, 
                                (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY[1], 
                                player_TextWidth, 
                                vector_playerStatus.y
                                ), 
                            content, 
                            LabelSize, 
                            playerColor, 
                            C_overlay, 
                            new Vector2(1f, 1f)
                        );
                        #endregion
                        #region Slot 2 - Weapon Name (vector, size, drawing) - if not empty
                        if (WeaponName != "")
                        {
                            Vector2 vector_WeaponName = GUI.skin.GetStyle(WeaponName).CalcSize(new GUIContent(WeaponName));
                            float player_WeaponName = (devLabel == 1f) ? vector_WeaponName.x : (vector_WeaponName.x / devLabel);
                            //GUI.Label(new Rect(pHeadVector.x - player_WeaponName / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY[2] - 20f, player_WeaponName, vector_WeaponName.y), WeaponName, LabelSize);
                            EDS.DrawShadow(
                                new Rect(
                                    pHeadVector.x - player_WeaponName / 2f, 
                                    (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY[2], 
                                    player_WeaponName, 
                                    vector_WeaponName.y
                                    ),
                                new GUIContent(WeaponName), 
                                LabelSize, 
                                playerColor, 
                                C_overlay, 
                                new Vector2(1f, 1f)
                            );
                        }
                        #endregion
                    }
                }
                /*else
                {
                    #region Set Corpse Color
                    if (player.Profile.Info.RegistrationDate <= 0)
                    {
                        playerColor = C_botsRip;
                    }
                    else if (LocalPlayer.Profile.Info.GroupId == player.Profile.Info.GroupId && LocalPlayer.Profile.Info.GroupId != "0" && LocalPlayer.Profile.Info.GroupId != "" && LocalPlayer.Profile.Info.GroupId != null)
                    {
                        playerColor = C_groupRip;
                    }
                    else if (player.Profile.Info.Side == EPlayerSide.Savage)
                    {
                        playerColor = C_scavRip;
                    }
                    else
                    {
                        if (switch_colors)
                        {
                            playerColor = C_playerRip[0];
                        }
                        else
                        {
                            playerColor = C_playerRip[1];
                        }
                    }
                    LabelSize.normal.textColor = playerColor;
                    GUI.color = playerColor;
                    #endregion
                    #region Draw Corpse
                    float dTO = FMath.FD(Camera.main.transform.position, player.Transform.position);
                    string playerStatus = $"{(int)dTO}m";
                    Vector3 pHeadVector = Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position);
                    Vector2 vector_playerStatus = GUI.skin.GetStyle(playerStatus).CalcSize(new GUIContent(playerStatus));
                    float player_TextWidth = (devLabel == 1f) ? vector_playerStatus.x : (vector_playerStatus.x / devLabel);
                    GUI.Label(new Rect(pHeadVector.x - player_TextWidth / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y, player_TextWidth, vector_playerStatus.y), playerStatus, LabelSize);
                    #endregion
                }*/
            }
        }
        #endregion
    }
}
