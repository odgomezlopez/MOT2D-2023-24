
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActorController))]
public class OnTouchDamager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            float damage = GetComponent<ActorController>().Stats.attDamage;
            collision.gameObject.GetComponent<ActorController>().OnDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            float damage = GetComponentInParent<ActorController>().Stats.attDamage;
            collision.GetComponentInParent<ActorController>().OnDamage(damage);
        }
    }
}
