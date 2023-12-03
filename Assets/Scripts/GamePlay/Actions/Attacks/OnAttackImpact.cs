using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackImpact : MonoBehaviour
{
    [SerializeField] float damage = 0;
    [SerializeField] bool onColisionDestroy=false;

    [SerializeField] float maxLife = 10f;
    [SerializeField] string attackerTag;

    public void Initializate(float newDamage, bool newOnColisionDestroy=false, string newAttackerTag="Player", float newMaxLife = 10f)
    {
        damage = newDamage;
        onColisionDestroy = newOnColisionDestroy;
        attackerTag = newAttackerTag;
        maxLife = newMaxLife;
    }
    private void Start()
    {
        Destroy(gameObject, maxLife);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag(attackerTag)) return;//Evitar fuego amigo
            
            collision.gameObject.GetComponent<IActorController>().OnDamage(damage);
        }

        if(onColisionDestroy) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag(attackerTag)) return;//Evitar fuego amigo

            collision.GetComponentInParent<IActorController>().OnDamage(damage);
        }

        if (onColisionDestroy) Destroy(gameObject);
    }
}
