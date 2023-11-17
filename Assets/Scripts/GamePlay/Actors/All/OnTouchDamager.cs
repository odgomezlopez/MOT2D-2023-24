using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IActorController))]
public class OnTouchDamager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            float damage = GetComponent<IActorController>().GetStats().attDamage;
            collision.gameObject.GetComponent<IActorController>().OnDamage(damage);
        }
    }
}
