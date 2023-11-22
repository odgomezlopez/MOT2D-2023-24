using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IActorController
{
    //Referencia a los Stats
    [SerializeField] PlayerDataSO defaultPlayerData;
    [SerializeField] PlayerStats stats;
    [HideInInspector] public PlayerInput playerInput;

    //Eventos generales
    [Header("Eventos generales")]
    public UnityEvent onDie;

    // Start is called before the first frame update
    void Start()
    {
        //Sincronizar los stats locales con lo de GameData si existen 
        if (GameObject.FindGameObjectWithTag("GameData")) {
            stats = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>().stats;
        }
        else
        {
            //throw new System.Exception("Error: GameData gameobject/tag not found");
            Debug.LogError("Error: GameData gameobject/tag not found");
        }

        //Comprobar si hay GameData por defecto
        if (defaultPlayerData) InitializeFromSO(defaultPlayerData);
        
        //Obtengo el PlayerInput
        playerInput = GameObject.FindGameObjectWithTag("PlayerInput").GetComponent<PlayerInput>();


        //Me suscribo a los cambios de HP de los stats
        stats.HP.RestartStats();
        stats.HP.OnIndicatorChange.AddListener(OnHPUpdate);

        //Comprobaciones de por si acaso
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
    }

    private void OnDestroy()
    {
        stats.HP.OnIndicatorChange.RemoveListener(OnHPUpdate);
    }


    #region Init PlayerDataSO
    //Inicializamos desde el Scriptable Object si está
    public void InitializeFromSO(PlayerDataSO newPlayerData)
    {
        //Obtenemos componentes necesarios para la actualización
        SpriteRenderer ren = GetComponentInChildren<SpriteRenderer>();
        Animator ani = GetComponentInChildren<Animator>();
        CapsuleCollider2D capCol= GetComponentInChildren<CapsuleCollider2D>();

        //1. Intercambiamos info
        ren.sprite=newPlayerData.sprite;
        ren.color = newPlayerData.color;
        ani.runtimeAnimatorController = newPlayerData.animator;

        //2. Recalcular el colider (MODIFICAR EN 3D)
        // Adjust the size of the collider to match the sprite size
        capCol.size = new Vector2(ren.bounds.size.x, ren.bounds.size.y);

        // Adjust the position of the collider to match the sprite position
        capCol.offset = new Vector2(ren.bounds.center.x - ren.transform.position.x, ren.bounds.center.y - ren.transform.position.y);
        
        
        //3.Inicializamos los stats
        stats.Update(newPlayerData.playerStats);
    }
    #endregion

    //Metodos de Stats
    public Stats GetStats()
    {
        return stats;
    }

    private void OnHPUpdate(float val)
    {
        if (val <= 0)
        {
            onDie.Invoke();
        }
    }
    public void OnHeal(float heal)
    {
        throw new System.NotImplementedException();
    }

    public void OnDamage(float damage)
    {
        stats.HP.CurrentValue -= damage;

        //Lanzar corutina de invulnerabilidad temporal
        if(stats.HP.CurrentValue > 0 ) StartCoroutine(TemporalInvulneravility());
    }

    public IEnumerator TemporalInvulneravility()
    {
        //1.1. Activo la invulnerabilidad
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), true);

        //1.2. Pongo de rojo el srpite
        SpriteRenderer ren = GetComponentInChildren<SpriteRenderer>(); //TODO Mover a variable global
        Color baseColor= ren.color;
        ren.color = Color.red;
  

        //2. Espero
        yield return new WaitForSecondsRealtime(stats.invulnerabilitySeconds);

        //3.1. Vuelvo a poner el color original
        ren.color = baseColor;

        //3.2. Desactivo la invulnerabilidad
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
    }

    //Other
    public GameObject GetGameObject()
    {
        return gameObject;
    }


}
