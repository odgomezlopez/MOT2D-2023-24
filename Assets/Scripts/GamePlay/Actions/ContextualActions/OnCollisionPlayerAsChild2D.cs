using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionPlayerAsChild2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponentInParent<IActorController>().GetGameObject().transform;//TODO simplificar en UD3
            player.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            try { 
                var player = collision.gameObject.GetComponentInParent<IActorController>().GetGameObject().transform;//TODO simplificar en UD3
                player.SetParent(null);
            }
            catch{}
        }
    }
}
