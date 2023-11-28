using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Utils
    {
        public static bool IsGrounded2D(GameObject g, float dist = 1f, string tag = "Floor")
        {
            Collider2D col = g.GetComponentInChildren<Collider2D>();

            Vector3 centro = col.bounds.center;//g.transform.position

            //Comprueba si toca el centro
            float[] positionsX = { 0 };//, col.bounds.extents.x , -col.bounds.extents.x };
            foreach (float x in positionsX)
            {
                Vector3 downPoint = new Vector3(x, -(col.bounds.extents.y + 0.0001f));
                RaycastHit2D isTouchingTheGround = Physics2D.Raycast(centro + downPoint, Vector2.down, dist);

                Debug.DrawRay(centro + downPoint, Vector2.down * dist, Color.red);

                if (isTouchingTheGround.collider?.CompareTag(tag) ?? false)
                {
                    Debug.DrawRay(centro + downPoint, Vector2.down * dist, Color.red);
                    return true;
                }
                else
                {
                    Debug.DrawRay(centro + downPoint, Vector2.down * dist, Color.yellow);
                }
            }

            return false;
        }
    }
}
