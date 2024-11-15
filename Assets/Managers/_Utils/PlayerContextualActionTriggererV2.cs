using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerContextualActionTriggererV2 : MonoBehaviour //MonoBehaviourSaveable Cambiar para guardado en memoria
{ 
    [Header("Configuraci�n general")]
    [SerializeField] InputActionReference m_contextualAction;
    [SerializeField] protected string UITextEnable="Activar";
    [SerializeField] protected string UITextDisable ="Necesitas X";

    [SerializeField] bool actionEnabled = true;
    [SerializeField] bool playerInArea = false;

    [Header("Configuraci�n Trigger (req. collider trigger)")]
    [SerializeField] bool detectTrigger = true;

    [Header("Configuraci�n Trigger (req. collider trigger)")]
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
                if (value)
                {
                    if(CheckRequirement()) PlayerEnter?.Invoke(UITextEnable, true);
                    else PlayerEnter?.Invoke(UITextDisable, false);
                }
                else PlayerExit?.Invoke();
                playerInArea = value;
            }
        }
    }

    void Start()
    {
        //Referencias
       // m_contextualAction = GameObject.FindGameObjectWithTag("PlayerInput").GetComponent<PlayerInput>().actions["ContextualAction"];//TODO Hacer que la acci�n concreta se elija en el editor
        player = GameObject.FindGameObjectWithTag("Player");

        //Inicializaciones
        actionEnabled = true;
        playerInArea = false;
        //PlayerExit?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos si se cumplen los requisitos de la funci�n
        if(ActionEnabled)
        {
            CheckDistance2D();
            if(PlayerInArea && CheckRequirement() && m_contextualAction &&  m_contextualAction.action.triggered)
            {
                //se desactiva el componente
                ActionEnabled = false;
                PlayerInArea = false;
                //Se ejecuta la acci�n
                ActionTriggered.Invoke();
            }
        }
    }
    protected virtual bool CheckRequirement()
    {
        return true;
    }

    //Detectar si el jugador est� dentro
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
