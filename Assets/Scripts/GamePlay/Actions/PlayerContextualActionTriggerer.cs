using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerContextualActionTriggerer : MonoBehaviour
{
    [Header("Configuración general")]
    InputAction m_contextualAction;
    [SerializeField] string UIText = "Activar";
    [SerializeField] bool actionEnabled = true;
    [SerializeField] bool playerInArea = false;

    [Header("Configuración Trigger (req. collider trigger)")]
    [SerializeField] bool detectTrigger = true;

    [Header("Configuración Trigger (req. collider trigger)")]
    [SerializeField] bool detectDistance = false;
    GameObject player;
    [SerializeField] float activateDistance=5f;

    [Header("Eventos")]
    public UnityEvent<string,bool> PlayerEnter;
    public UnityEvent PlayerExit;

    public UnityEvent ActionTriggered;

    //Getter/Setters
    public bool ActionEnabled { get => actionEnabled; set => actionEnabled = value; }
    public bool PlayerInArea { 
        get { 
            return playerInArea; 
        } 
        set {
            if (value!=playerInArea)
            {
                if(value) PlayerEnter?.Invoke(UIText,CheckRequirement());
                else PlayerExit?.Invoke();
                playerInArea = value;
            }
        }
    }

    void Start()
    {
        //Referencias
        m_contextualAction = GameObject.FindGameObjectWithTag("PlayerInput").GetComponent<PlayerInput>().actions["ContextualAction"];
        player = GameObject.FindGameObjectWithTag("Player");

        //Inicializaciones
        actionEnabled = true;
        playerInArea = false;
        //PlayerExit?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos si se cumplen los requisitos de la función
        if(ActionEnabled)
        {
            CheckDistance2D();
            if(PlayerInArea && CheckRequirement() && m_contextualAction.triggered)
            {
                //se desactiva el componente
                ActionEnabled = false;
                PlayerInArea = false;
                //Se ejecuta la acción
                ActionTriggered.Invoke();

            }
        }
    }
    protected virtual bool CheckRequirement()
    {
        return true;
    }

    //Detectar si el jugador está dentro
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!detectTrigger) return;
        if (ActionEnabled && collision.CompareTag("Player"))
        {
            PlayerInArea = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!detectTrigger) return;
        if (ActionEnabled && collision.CompareTag("Player"))
        {
            PlayerInArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!detectTrigger) return;
        if (ActionEnabled && collision.CompareTag("Player"))
        {
            PlayerInArea = false;
        }
    }

    //Comprobar distancia
    private void CheckDistance2D()
    {
        if (!detectDistance) return;

        float distance = Vector2.Distance(transform.position,player.transform.position);
        if(distance < activateDistance)
        {
            PlayerInArea = true;
        }
        else
        {
            PlayerInArea = false;
        }
    }
}
