using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class PlayerController : MonoBehaviour, IActorController
{
    //Player default info
    [Header("Player Info")]
    [SerializeField] public PlayerDataSO playerData;
    [SerializeField] public bool statsInitialized = false;

    //Referencia a los Stats
    [Header("Player Current Stats")]

    [SerializeField] PlayerStats stats;

    [HideInInspector] public PlayerInput playerInput;

    //Eventos generales
    [Header("Eventos generales")]
    public UnityEvent onDie;

    // Start is called before the first frame update
    void Start()
    {
        //1. Sincronizar la info local con GameData si existe 
        Data data = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>();
        if (data) {
            stats = data.stats;
            playerData = data.playerData;
            statsInitialized = data.statsInitialized;
        }
        else
        {
            //throw new System.Exception("Error: GameData gameobject/tag not found");
            Debug.LogError("Error: GameData gameobject/tag not found");
        }

        //2. Cargo la info del SO
        if (playerData)
        {
            //2.1. Actualizo los stats
            if (!statsInitialized)
            {
                stats.Update(data.playerData.playerStats);
                statsInitialized = true;
                if (data) data.statsInitialized = true;
            }

            //2.2. Actualizo el avatar
            LoadAvatar(data.playerData);
        }

        //3.Obtengo el PlayerInput
        playerInput = GameObject.FindGameObjectWithTag("PlayerInput").GetComponent<PlayerInput>();

        //Me suscribo a los cambios de HP de los stats
        stats.HP.RestartStats();
        stats.HP.OnIndicatorChange.AddListener(OnHPUpdate);

        stats = (PlayerStats)GetComponent<PlayerController>().GetStats();


        //Comprobaciones de por si acaso
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
    }

    private void OnDestroy()
    {
        stats.HP.OnIndicatorChange.RemoveListener(OnHPUpdate);
    }


    #region Init PlayerDataSO
    private void OnRenderObject()
    {
        Data data = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>();
        if (data)
        {
            LoadAvatar(data.playerData);
        }
        else
        {
            LoadAvatar(playerData);
        }
    }

    //Inicializamos desde el Scriptable Object si está
    public void LoadAvatar(PlayerDataSO newPlayerData)
    {
        if (!newPlayerData) return;
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