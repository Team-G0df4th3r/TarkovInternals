using System;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using EFT.Interactive;

namespace UnhandledException
{
    class Raycast
    {
        /* I added overloaded function to make sure we do not overcalc this shit :)
         *
         * */
        private static RaycastHit raycastHit;

        private static Vector3 GetHandsPos()
        {
            if (Cons.Main._localPlayer == null)
            {
                return Vector3.zero;
            }
            Player.FirearmController firearmController = Cons.Main._localPlayer.HandsController as Player.FirearmController;
            if (firearmController == null)
            {
                return Vector3.zero;
            }
            return firearmController.Fireport.position + Camera.main.transform.forward * 1f; //fireport 
        }
        public static bool BodyRaycastCheck(GameObject obj, Vector3 Vector_1, Vector3 Vector_2, Vector3 Vector_3, Vector3 Vector_4, Vector3 Vector_5)
        {
            var HandsPos = GetHandsPos();
            if(Vector_1 != null)
                if (Physics.Linecast(HandsPos, Vector_1, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_2 != null)
                if (Physics.Linecast(HandsPos, Vector_2, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_3 != null)
                if (Physics.Linecast(HandsPos, Vector_3, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_4 != null)
                if (Physics.Linecast(HandsPos, Vector_4, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_5 != null)
                if (Physics.Linecast(HandsPos, Vector_5, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            return false;
        }
        public static bool BodyRaycastCheck(GameObject obj, Vector3 Vector_1, Vector3 Vector_2, Vector3 Vector_3, Vector3 Vector_4)
        {
            var HandsPos = GetHandsPos();
            if (Vector_1 != null)
                if (Physics.Linecast(HandsPos, Vector_1, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_2 != null)
                if (Physics.Linecast(HandsPos, Vector_2, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_3 != null)
                if (Physics.Linecast(HandsPos, Vector_3, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_4 != null)
                if (Physics.Linecast(HandsPos, Vector_4, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            return false;
        }
        public static bool BodyRaycastCheck(GameObject obj, Vector3 Vector_1, Vector3 Vector_2, Vector3 Vector_3)
        {
            var HandsPos = GetHandsPos();
            if (Vector_1 != null)
                if (Physics.Linecast(HandsPos, Vector_1, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_2 != null)
                if (Physics.Linecast(HandsPos, Vector_2, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_3 != null)
                if (Physics.Linecast(HandsPos, Vector_3, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            return false;
        }
        public static bool BodyRaycastCheck(GameObject obj, Vector3 Vector_1, Vector3 Vector_2)
        {
            var HandsPos = GetHandsPos();
            if (Vector_1 != null)
                if (Physics.Linecast(HandsPos, Vector_1, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            if (Vector_2 != null)
                if (Physics.Linecast(HandsPos, Vector_2, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            return false;
        }
        public static bool BodyRaycastCheck(GameObject obj, Vector3 Vector_1)
        {
            var HandsPos = GetHandsPos();
            if (Vector_1 != null)
                if (Physics.Linecast(HandsPos, Vector_1, out raycastHit) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject)
                {
                    return true;
                }
            return false;
        }

    }
}
