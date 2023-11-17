using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLateralMovementController : MonoBehaviour, IActorController
{
    //Referencia a los Stats
    PlayerStats stats;

    //Eventos generales
    [Header("Eventos generales")]
    public UnityEvent onDie = new();


    //Referencia al player input
    PlayerInput playerInput;
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
    void Awake()
    {
        //Referencia a componentes externos
        stats = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>().stats;

        playerInput = GameObject.FindGameObjectWithTag("PlayerInput").GetComponent<PlayerInput>();
        m_moveAction = playerInput.actions["Move"];
        m_jumpAction = playerInput.actions["Jump"];

        //Referencia a componentes propios
        rb = GetComponent<Rigidbody2D>();
        sprite= GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();


        //Me suscribo a los cambios de HP de los stats
        stats.HP.RestartStats();
        stats.HP.OnIndicatorChange.AddListener(OnHPUpdate);
    }

    //Gestión del HP
    private void OnHPUpdate(float val)
    {
        if (val <= 0)
        {
            onDie.Invoke();
        }
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


    public Stats GetStats()
    {
        return stats;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnHeal(float heal)
    {
        throw new System.NotImplementedException();
    }

    public void OnDamage(float damage)
    {
        stats.HP.CurrentValue -= damage;
    }
}
