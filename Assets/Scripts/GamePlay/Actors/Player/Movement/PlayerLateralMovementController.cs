using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLateralMovementController : MonoBehaviour
{
    //Referencias al PlayerController
    PlayerController playerController;
    PlayerStats stats;

    //Eventos de movimiento
    public UnityEvent OnJump;

    //Referencia al player input
    InputAction m_moveAction, m_jumpAction;


    //Parametros privados para gestionar el input
    private float inputX;
    private bool jump = false;

    //Información para salto
    private bool isGrounded = false;
    private int jumpPerformed = 0;


    //Referencias  componentes
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Referencia al playerController
        playerController = GetComponent<PlayerController>();
        stats = (PlayerStats) playerController.GetStats();
        //Acciones de PlayerLateralMovement

        m_moveAction = playerController.playerInput.actions["Move"];
        m_jumpAction = playerController.playerInput.actions["Jump"];

        //Referencia a componentes propios
        rb = GetComponent<Rigidbody2D>();
        sprite= GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        //Capturamos el movimiento en el eje x
        inputX = m_moveAction.ReadValue<Vector2>().x; 

        //Capturamos si hay que saltar
        if (m_jumpAction.triggered && jumpPerformed < stats.jumpMax)
        {
            jump = true;
            jumpPerformed++;
        }
    }

    private void FixedUpdate()
    {
        //Aplicamos movimiento utilizando fisicas
        LateralMove();
        //RunnerMove();

        //Aplicamos el saltos
        Jump();

        //Actualizamos el animator
        UpdateAnimatorParameters();
    }

    private void LateralMove()
    {
        //Calculo si hace falta airMomentum
        float velModified=(isGrounded) ? 1f : stats.airMomemtum;

        //1. Asignando directamente la velocidad
        //rb.velocity = new Vector2(inputX * velModified * stats.movementSpeed, rb.velocity.y);

        //2. Mediante aceleración
        if (inputX != 0)//Aceleración
        {
            rb.AddForce(Vector2.right * inputX * velModified * stats.acceleration);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -stats.movementSpeed, stats.movementSpeed), rb.velocity.y);
        }
        else//Deceleracion
        {
            //A. Directa
            rb.velocity=new Vector2(0, rb.velocity.y);
            
            // B. Gradual
            /*rb.AddForce(new Vector2(-rb.velocity.x * stats.deceleration, 0f));
            if(Mathf.Abs(rb.velocity.x) < 1)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }*/
        }

        //Flip the sprite
        Flip();
    }

    private void RunnerMove()
    {
        rb.velocity = new Vector2(stats.movementSpeed, rb.velocity.y);
        Flip();
    }

    private void Flip()
    {
        Vector2 aux = sprite.transform.localScale;
        if (rb.velocity.x > 0) aux.x = Mathf.Abs(aux.x);
        else if (rb.velocity.x < 0) aux.x = -Mathf.Abs(aux.x);
        sprite.transform.localScale = aux;
    }

    private void Jump()
    {
        if (jump)
        {
            //Invocamos el evento de salto
            OnJump.Invoke();

            //Hacemos el salto
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * stats.jumpSpeed, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", Mathf.Abs(rb.velocity.y));
        animator.SetBool("isGrounded", isGrounded);

    }

    //Metodos control colisiones y triggers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            jumpPerformed = 0;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }


}
