using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackMoveTowards2D : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 dir;

    Rigidbody2D rb;
    public void Initializate(float newSpeed,Vector2 newDir)
    {
        speed = newSpeed;
        dir = newDir;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = dir* speed;
    }
}
