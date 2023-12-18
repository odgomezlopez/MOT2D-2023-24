using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackMoveTowards2D : MonoBehaviour
{
    //Propiedades
    [SerializeField] float speed;
    [SerializeField] Vector2 dir;

    //Referencias a componentes
    Rigidbody2D rb;

    void Start()//Se puede cambiar a Awake
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;
    }
    public void Initialize(float newSpeed, Vector2 newDir)
    {
        //Actualizo la variables
        speed = newSpeed;
        dir = newDir;

        //Actualizo la velocidad
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;
    }
}
