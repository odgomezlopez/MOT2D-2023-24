using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackImpack2 : MonoBehaviour
{
    [SerializeField] float damage = 1;
    [SerializeField] string originTag="";


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,10);
    }

    public void Initialize(string newTag)
    {
        originTag = newTag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(originTag)) return;
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<IActorController>()?.OnDamage(damage);
            Destroy(gameObject);
        }
    }
}
