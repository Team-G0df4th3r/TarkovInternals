using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using EFT;
using EFT.Interactive;
using System.IO;

namespace UnhandledExceptionHandler.Functions
{

    class EFunc
    {
        #region COLORS
        private static Color[] C_player     = new Color[2] { new Color(1.0f, 0f, 0f, 1.0f), new Color(1.0f, 0f, 1.0f, 1.0f) };
        private static Color[] C_playerRip  = new Color[2] { new Color(0.8f, 0f, 0f, 1.0f), new Color(0.8f, 0f, 0.8f, 1.0f) };
        private static Color C_bots         = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        private static Color C_botsRip      = new Color(0.8f, 0.8f, 0.8f, 1.0f);
        private static Color C_group        = new Color(0f, 0.8f, 1.0f, 1.0f);
        private static Color C_groupRip     = new Color(0f, 0.6f, 0.8f, 1.0f);
        private static Color C_scav         = new Color(1f, 0.75f, 0.0f, 1.0f);
        private static Color C_scavRip      = new Color(0.8f, 0.5f, 0.0f, 1.0f);
        private static Color C_napesy       = new Color(0f, 1.0f, 0f, 1.0f);
        private static Color C_corps        = new Color(0.8f, 0.8f, 0.8f, 1.0f);
        #endregion
        #region LocalVariables
        private static Color OldGuiColor;

        public abstract class Raycasting {
            private float layer = 0f;
            public bool Raycast (Vector3 vector_1, Vector3 vector_2, float distance){
                /*RaycastHit hit;
                Ray ray = new Ray(vector_1, vector_2 * distance);
                if (Physics.Raycast(ray, out hit, layer))
                {
                    if(hit.collider.)
                    return true;
                }
                    */
                return false;
            }
        }
        #endregion

        #region - Corpses -
        public static void DrawLogs(IEnumerable<LootPoint> _lContainer, float _displayDistance = 200f) {
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
                        //if (Camera.main.WorldToScreenPoint(item.transform.position).z > 0.01f)
                        //{ // do not display out of bounds items

                            float distance = FMath.FD(Camera.main.transform.position, item.transform.position);
                            if (distance < _displayDistance) { 
                              

                                Vector3 itemPosition = Camera.main.WorldToScreenPoint(item.transform.position);
                                float[] boxSize = new float[2] { 3f, 1.5f };
                                int FontSize = 12;
                                FMath.DistSizer(distance, ref FontSize, ref deltaDistance, ref devLabel);
                                LabelSize.fontSize = FontSize;
                                LabelSize.normal.textColor = C_corps;
                                //item.TemplateId == "5909e4b686f7747f5b744fa4"; // dead Scav Body
                                string distanceText = $"{distance}m {item.Settings.Enabled}";
                                string DebugText = $"{item.tag} V {item.name}";
                                Vector2 sizeOfText = GUI.skin.GetStyle(distanceText).CalcSize(new GUIContent(distanceText));
                                EDS.P(
                                    new Vector2(
                                        itemPosition.x - boxSize[1],
                                        (float)(Screen.height - itemPosition.y) - boxSize[1]
                                        ),
                                    C_corps,
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
                catch (NullReferenceException ex)
                {
                    ErrorHandler.Catch("Corpses", ex);
                }
            }
        }
        #endregion
        #region - Grenade ESP -
        private static float[] grenadeSize = new float[2] { 3f, 1.5f };
        public static void DrawBombs(IEnumerable<Grenade> _g, Player _localP, float _displayDistance = 100f) {
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
                            EDS.P(
                                new Vector2(
                                    pGrenadePosition.x - grenadeSize[1],
                                    (float)(Screen.height - pGrenadePosition.y) - grenadeSize[1]
                                    ),
                                C_napesy,
                                grenadeSize[0]
                            );
                            GUI.Label(
                                new Rect(
                                    pGrenadePosition.x - sizeOfText.x / 2f,
                                    (float)Screen.height - pGrenadePosition.y - deltaDistance - 1,
                                    sizeOfText.x,
                                    sizeOfText.y
                                ),
                                distanceText,
                                LabelSize
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
        public static void DrawError(IEnumerable<Player> _ply, Player _lP, float _viewdistance, bool switch_colors)
        {
            if (_ply == null || _lP == null)
                return;

                float deltaDistance = 25f;
                string playerDisplayName = "";
                float devLabel = 1f;
                string Status = "";
                
                var LabelSize = new GUIStyle { fontSize = 12 };
                Color playerColor = C_bots;
                var enumerator = _ply.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    try
                    {
                        var p = enumerator.Current;
                        if (p != null)
                        {
                            if (EPointOfView.FirstPerson != p.PointOfView)
                            {
                                if (p.HealthController.IsAlive)
                                {
                                    float dTO = FMath.FD(Camera.main.transform.position, p.Transform.position);
                                    if (dTO > 1f && dTO <= _viewdistance && Camera.main.WorldToScreenPoint(p.Transform.position).z > 0.01f)
                                    {
                                        Vector3 pHeadVector = Camera.main.WorldToScreenPoint(p.PlayerBones.Head.position);
                                        float find_sizebox = Math.Abs(pHeadVector.y - Camera.main.WorldToScreenPoint(p.PlayerBones.Neck.position).y) * 1.5f; // size of the head - its not good but its scaling without much maths
                                        find_sizebox = (find_sizebox > 30f) ? 30f : find_sizebox;
                                        float half_sizebox = (find_sizebox > 30f) ? 15f : find_sizebox / 2f;
                                        int FontSize = 12;
                                        FMath.DistSizer(dTO, ref FontSize, ref deltaDistance, ref devLabel);
                                        LabelSize.fontSize = FontSize;
                                        float[] distancesAxisY = new float[3] { deltaDistance, deltaDistance - (FontSize + 1), deltaDistance - (FontSize + FontSize + 2) };

                                        int health = (int)(p.HealthController.GetBodyPartHealth(EFT.HealthSystem.EBodyPart.Common).Current);
                                        Status = health.ToString() + " HP"; // Health here 
                                        OldGuiColor = GUI.color;
                                        #region detect what we have AI / Teamamate / Scav or Player
                                            if (p.Profile.Info.RegistrationDate <= 0)
                                            {
                                                playerDisplayName = "";
                                                playerColor = C_bots;
                                                GUI.color = Color.white;
                                                EDS.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Color.red, find_sizebox);
                                            }
                                            else if (_lP.Profile.Info.GroupId == p.Profile.Info.GroupId && _lP.Profile.Info.GroupId != "0" && _lP.Profile.Info.GroupId != "" && _lP.Profile.Info.GroupId != null)
                                            {
                                                playerDisplayName = p.Profile.Info.Nickname;
                                                playerColor = C_group;
                                                /* no head is needed for friend just a position
                                                 * EDS.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Color.cyan, find_sizebox);*/
                                            }
                                            else if (p.Profile.Info.Side == EPlayerSide.Savage)
                                            {
                                                playerColor = C_scav;
                                                GUI.color = Color.red;
                                                EDS.P(new Vector2(pHeadVector.x - half_sizebox, (float)(Screen.height - pHeadVector.y) - half_sizebox), Color.red, find_sizebox);
                                            }
                                            else
                                            {
                                                playerDisplayName = p.Profile.Info.Nickname;
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
                                        #region Prepare Label variables
                                            string nameText = $"{playerDisplayName}";
                                            string playerText = $"[{(int)dTO}m] {Status}";
                                            /*string WeaponName = p.Weapon.ShortName;
                                            try
                                            {
                                                WeaponName = WeaponName.Localized();
                                            } catch (Exception e) { UnhandledExceptionHandler.ErrorHandler.Catch("WeaponNames", e); }*/

                                            LabelSize.normal.textColor = playerColor;
                                            GUI.color = playerColor;
                                            #region Player Name
                                                Vector2 vector_playerName = GUI.skin.GetStyle(nameText).CalcSize(new GUIContent(nameText));
                                                float player_NameText = (devLabel == 1f) ? vector_playerName.x : (vector_playerName.x / devLabel);
                                                GUI.Label(new Rect(pHeadVector.x - player_NameText / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(p.PlayerBones.Head.position).y - distancesAxisY[1] - 20f, player_NameText, vector_playerName.y), nameText, LabelSize);
                                            #endregion
                                            #region Weapon Name
                                                /*Vector2 vector_WeaponName = GUI.skin.GetStyle(WeaponName).CalcSize(new GUIContent(WeaponName));
                                                float player_WeaponName = (devLabel == 1f) ? vector_WeaponName.x : (vector_WeaponName.x / devLabel);
                                                GUI.Label(new Rect(pHeadVector.x - player_WeaponName / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(p.PlayerBones.Head.position).y - distancesAxisY[2] - 20f, player_WeaponName, vector_WeaponName.y), WeaponName, LabelSize);*/
                                            #endregion
                                            #region Status distance and health
                                                if (playerText != "")
                                                {
                                                    Vector2 vector_playerStatus = GUI.skin.GetStyle(playerText).CalcSize(new GUIContent(playerText));
                                                    float player_TextWidth = (devLabel == 1f) ? vector_playerStatus.x : (vector_playerStatus.x / devLabel);
                                                    GUI.Label(new Rect(pHeadVector.x - player_TextWidth / 2f, (float)Screen.height - Camera.main.WorldToScreenPoint(p.PlayerBones.Head.position).y - distancesAxisY[0] - 20f, player_TextWidth, vector_playerStatus.y), playerText, LabelSize);
                                                }
                                            #endregion
                                        #endregion
                                }
                            }
                            }
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        ErrorHandler.Catch("Players", ex);
                    }
                }
            
        }
        #endregion
    }
}
