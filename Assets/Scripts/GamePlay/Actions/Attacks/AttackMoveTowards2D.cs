using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackMoveTowards2D : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    public void Initializate(float newSpeed)
    {
        speed = newSpeed;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speed, 0, 0);
    }
}
