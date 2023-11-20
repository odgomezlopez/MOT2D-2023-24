using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsometricMovementController : MonoBehaviour
{
    private float CharacterSpeed = 1.0f;
    private Vector2 InputDir;

    private Rigidbody2D PlayerPhysics;

    // collect references
    void Awake()
    {

        PlayerPhysics = GetComponentInChildren<Rigidbody2D>();

    }

    void Update()
    {

        float VertInput = Input.GetAxis("Vertical");
        float HorInput = Input.GetAxis("Horizontal");
        InputDir = UpRightToIsometric(HorInput,VertInput );

    }

    void FixedUpdate()
    {

        Vector2 WhereAmI = PlayerPhysics.position;
        Vector2 WhereTo = WhereAmI + (InputDir * CharacterSpeed) * Time.fixedDeltaTime;

        
        PlayerPhysics.MovePosition(WhereTo);
    }

    private  Vector2 UpRightToIsometric(float horizontal, float vertical)
    {
        return new Vector2 (vertical + horizontal, (vertical - horizontal) / 2).normalized;
    }
}
