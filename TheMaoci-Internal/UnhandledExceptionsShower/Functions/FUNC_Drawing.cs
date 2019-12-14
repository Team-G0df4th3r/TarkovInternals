using System;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using EFT.Interactive;

namespace UnhandledException
{
    class FUNC_Drawing
    {
        #region - CORPSES -
        public static void DrawPDB(List<LootItem> _lContainer, float _displayDistance = 300f)
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
                            if (distance < _displayDistance)
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
                                    Statics.Colors.ESP.bodies,
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
                                    Statics.Colors.ESP.bodies,
                                    Statics.Colors.Black,
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
        public static void DrawDLI(List<LootItem> _lContainer, float _displayDistance = 300f) {
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
                        //if (!item.isActiveAndEnabled && !item.IsVisibilityEnabled) // this is some BSG bullshit
                            //continue;
                        if (Camera.main.WorldToScreenPoint(item.transform.position).z > 0.01f)
                        { // do not display out of bounds items
                            float distance = FastMath.FD(Camera.main.transform.position, item.transform.position);
                            if (distance < _displayDistance)
                            {
                                Vector3 itemPosition = Camera.main.WorldToScreenPoint(item.transform.position);
                                float[] boxSize = new float[2] { 3f, 1.5f };
                                int FontSize = 12;
                                FastMath.DistSizer(distance, ref FontSize, ref deltaDistance, ref devLabel);
                                LabelSize.fontSize = FontSize;
                                LabelSize.normal.textColor = Statics.Colors.ESP.items;
                                //item.TemplateId == "5909e4b686f7747f5b744fa4"; // dead Scav Body
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
                                GUI.color = Statics.Colors.ESP.items;
                                Drawing.P(
                                    new Vector2(
                                        itemPosition.x - boxSize[1],
                                        (float)(Screen.height - itemPosition.y) - boxSize[1]
                                        ),
                                    Statics.Colors.ESP.items,
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
        public static void DrawDTG(List<Throwable> _g, Player _localP, float _displayDistance = 100f) {
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
                            if (dTO > _displayDistance)
                                continue;

                            Vector3 pGrenadePosition = Camera.main.WorldToScreenPoint(throwable.transform.position);
                            int FontSize = 10;
                            FastMath.DistSizer(dTO, ref FontSize, ref deltaDistance, ref devLabel);
                            LabelSize.fontSize = FontSize;
                            LabelSize.normal.textColor = Statics.Colors.ESP.grenades;
                            string distanceText = $"{(int)dTO}m";
                            Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                            GUI.color = Statics.Colors.ESP.grenades;
                            Drawing.P(
                                new Vector2(
                                    pGrenadePosition.x - 1.5f,
                                    (float)(Screen.height - pGrenadePosition.y) - 1.5f
                                    ),
                                Statics.Colors.ESP.grenades,
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
                                Statics.Colors.ESP.grenades,
                                Statics.Colors.Black,
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
        public static void DrawPlayers(List<Player> _PlayersList, Player LocalPlayer, float _viewdistance, bool switch_colors)
        {
            float deltaDistance = 25f;
            string playerDisplayName = "";
            float devLabel = 1f;
            string Status = "";
            var LabelSize = new GUIStyle { fontSize = 12 };
            Color playerColor = Statics.Colors.ESP.npc;
            float distancesAxisY_0 = 0;
            float distancesAxisY_1 = 0;
            float distancesAxisY_2 = 0;
            foreach (Player player in _PlayersList)
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
                    #region BONE ESP
                    if (dTO < 100f)
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
                        if (PLNeckVect.z > 0.01f)
                        {
                            Drawing.DrawLine(new Vector2(PLNeckVect.x, (float)Screen.height - PLNeckVect.y), new Vector2(PLCentrVect.x, (float)Screen.height - PLCentrVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLShVect.x, (float)Screen.height - PLShVect.y), new Vector2(PLLBowVect.x, (float)Screen.height - PLLBowVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLRShVect.x, (float)Screen.height - PLRShVect.y), new Vector2(PLRBowVect.x, (float)Screen.height - PLRBowVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLLBowVect.x, (float)Screen.height - PLLBowVect.y), new Vector2(PLPVect.x, (float)Screen.height - PLPVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLRBowVect.x, (float)Screen.height - PLRBowVect.y), new Vector2(pRPVect.x, (float)Screen.height - pRPVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLRShVect.x, (float)Screen.height - PLRShVect.y), new Vector2(PLShVect.x, (float)Screen.height - PLShVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLLKneeVect.x, (float)Screen.height - PLLKneeVect.y), new Vector2(PLCentrVect.x, (float)Screen.height - PLCentrVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLRKneeVect.x, (float)Screen.height - PLRKneeVect.y), new Vector2(PLCentrVect.x, (float)Screen.height - PLCentrVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLLKneeVect.x, (float)Screen.height - PLLKneeVect.y), new Vector2(PLLFootVect.x, (float)Screen.height - PLLFootVect.y), playerColor, 1f);
                            Drawing.DrawLine(new Vector2(PLRKneeVect.x, (float)Screen.height - PLRKneeVect.y), new Vector2(PLRFootVect.x, (float)Screen.height - PLRFootVect.y), playerColor, 1f);
                        }
                    }
                    #endregion
                    #region Set: PlayerName / Color / Head Pixel
                    if (player.Profile.Info.RegistrationDate <= 0)
                    {
                        playerDisplayName = "";
                        playerColor = Statics.Colors.ESP.npc;
                        Drawing.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Statics.Colors.Red, find_sizebox);
                    }
                    else if (LocalPlayer.Profile.Info.GroupId == player.Profile.Info.GroupId && LocalPlayer.Profile.Info.GroupId != "0" && LocalPlayer.Profile.Info.GroupId != "" && LocalPlayer.Profile.Info.GroupId != null)
                    {
                        playerDisplayName = player.Profile.Info.Nickname;
                        playerColor = Statics.Colors.ESP.group;
                    }
                    else if (player.Profile.Info.Side == EPlayerSide.Savage)
                    {
                        playerDisplayName = "";
                        playerColor = Statics.Colors.ESP.scav_player;
                        GUI.color = Color.red;
                        Drawing.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Statics.Colors.Red, find_sizebox);
                    }
                    else
                    {
                        playerDisplayName = player.Profile.Info.Nickname + " [" + player.Profile.Info.Level.ToString() + "]";
                        playerColor = Statics.Colors.ESP.player[0];
                        Drawing.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Statics.Colors.Red, find_sizebox);
                    }
                    #endregion
                    #region Prepare Main Texts
                    string nameNickname = $"{playerDisplayName}";
                    if (Switches.StreamerMode)
                        nameNickname = "";
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
                        global::UnhandledException.ErrorHandler.Catch("WeaponNames", e, player.Weapon.ShortName);
                        WeaponName = ".";
                    }
                    #endregion
                        
                    // set colors now
                    LabelSize.normal.textColor = playerColor;
                    #region Slot 0 - Player Name (vector, size, drawing)
                    if (nameNickname != "")
                    {
                        Vector2 vector_playerName = GUI.skin.GetStyle(nameNickname).CalcSize(new GUIContent(nameNickname));
                        float player_NameText = (devLabel == 1f) ? vector_playerName.x : (vector_playerName.x / devLabel);
                        //GUI.Label(new Rect(pHeadVector.x - player_NameText / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY[0], player_NameText, vector_playerName.y), nameNickname, LabelSize);
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
                            Statics.Colors.Black,
                            new Vector2(1f, 1f)
                        );
                    }
                    #endregion
                    #region Slot 1 - Status (distance, health)
                    Vector2 vector_playerStatus = GUI.skin.GetStyle(playerStatus).CalcSize(new GUIContent(playerStatus));
                    float player_TextWidth = (devLabel == 1f) ? vector_playerStatus.x : (vector_playerStatus.x / devLabel);
                    //GUI.Label(new Rect(pHeadVector.x - player_TextWidth / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(player.PlayerBones.Head.position).y - distancesAxisY[1], player_TextWidth, vector_playerStatus.y), playerStatus, LabelSize);
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
                        Statics.Colors.Black, 
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
                            Statics.Colors.Black, 
                            new Vector2(1f, 1f)
                        );
                    }
                #endregion

                    #region snap lines
                    if (Switches.SnapLines && player != Main._localPlayer)
                    {
                        Vector3 w2s = Camera.main.WorldToScreenPoint(player.PlayerBones.RootJoint.position);
                        if (!(w2s.z < 0.01f))
                        {
                            Drawing.DrawLine(new Vector2((Screen.width / 2), Screen.height), new Vector2(w2s.x, Screen.height - w2s.y), playerColor);
                        }

                    }
                    #endregion
            }
        }
        #endregion
    }
}
