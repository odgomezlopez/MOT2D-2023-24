using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(ActorController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLateralMovementController : MonoBehaviour
{
    //Referencias al PlayerController
    ActorController playerController;
    Stats stats;

    //Eventos de movimiento
    public UnityEvent OnJump;

    //Referencia al player input
    [SerializeField] InputActionReference m_moveAction, m_jumpAction;


    //Parametros privados para gestionar el input
    private float inputX;
    private bool jump = false;

    //Informaci�n para salto
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
        playerController = GetComponent<ActorController>();

        //Referencia a componentes
        rb = GetComponent<Rigidbody2D>();
        sprite= GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        //Capturamos el movimiento en el eje x
        inputX = m_moveAction.action.ReadValue<Vector2>().x; 

        //Capturamos si hay que saltar
        if (m_jumpAction.action.triggered && jumpPerformed < playerController.movementConfig.jumpMax)
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
        float velModified=(isGrounded) ? 1f : playerController.movementConfig.airMomentum;

        //1. Asignando directamente la velocidad
        //rb.velocity = new Vector2(inputX * velModified * stats.movementSpeed, rb.velocity.y);

        //2. Mediante aceleraci�n
        if (inputX != 0)//Aceleraci�n
        {
            rb.AddForce(Vector2.right * inputX * velModified * playerController.movementConfig.acceleration);
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x, -playerController.movementConfig.movementSpeed, playerController.movementConfig.movementSpeed), rb.linearVelocity.y);
        }
        else//Deceleracion
        {
            //A. Directa
            rb.linearVelocity=new Vector2(0, rb.linearVelocity.y);
            
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
        rb.linearVelocity = new Vector2(playerController.movementConfig.movementSpeed, rb.linearVelocity.y);
        Flip();
    }

    private void Flip()
    {
        /*Flip utilizando escala
        Vector2 aux = transform.localScale;
        if (rb.velocity.x > 0) aux.x = Mathf.Abs(aux.x);
        else if (rb.velocity.x < 0) aux.x = -Mathf.Abs(aux.x);
        transform.localScale = aux;
        */

        //Flip utilizando la rotaci�n
        if(rb.linearVelocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (rb.linearVelocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    private void Jump()
    {
        if (jump)
        {
            //Invocamos el evento de salto
            OnJump.Invoke();

            //Hacemos el salto
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * playerController.movementConfig.jumpSpeed, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("velocityY", Mathf.Abs(rb.linearVelocity.y));
        animator.SetBool("isGrounded", isGrounded);

    }

    //Metodos control colisiones y triggers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))//&& Utils.Utils.IsGrounded2D(gameObject)
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
