using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackImpack2 : MonoBehaviour
{
    [SerializeField] float damage = 1;
    [SerializeField] bool destroyOnImpact = true;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,10f);
    }

    public void Initialize(float newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Realizar daño si el ataque colisiona con algún actor
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<ActorController>()?.OnDamage(damage);
            if(destroyOnImpact)  Destroy(gameObject);
        }
        //Destruir también si el disparo toca el suelo o las paredes
        else if(collision.CompareTag("Floor") || collision.CompareTag("Wall"))
        {
            if (destroyOnImpact) Destroy(gameObject);
        }
    }
}
