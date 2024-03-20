using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class PlayerController : MonoBehaviour, IActorController, ISaveable
{
    //Referencia a los Stats
    [Header("Player Current Stats")]

    [SerializeField] PlayerStats stats;

    [HideInInspector] public PlayerInput playerInput;

    //Player default info
    [Header("Player Info")]
    [SerializeField] public PlayerDataSO playerData;
    [SerializeField] private bool statsInitialized = false;

    //Eventos generales
    [Header("Eventos generales")]
    public UnityEvent onHurt;
    public UnityEvent onDie;

    // Start is called before the first frame update
    void Start()
    {
        //1. Cargo la info del SO
        //playerData?.Initialize(this);

        //Sincronizamos la actualización con data
        //Data data = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>();
        //if (data) data.statsInitialized = true;



        //2.Obtengo el PlayerInput
        playerInput = GameObject.FindGameObjectWithTag("PlayerInput").GetComponent<PlayerInput>();

        //Me suscribo a los cambios de HP de los stats
        stats.HP.Restart();
        stats.HP.OnValueUpdate.AddListener(OnHPUpdate);

        stats = (PlayerStats)GetComponent<PlayerController>().GetStats();


        //Comprobaciones de por si acaso
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
    }

    public bool StatsInitialized
    {
        get
        {
            return statsInitialized;
        }
        set
        {
            statsInitialized = value;

            /*if (GameObject.FindGameObjectWithTag("GameData"))
            {
                Data data = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>();
                data.statsInitialized = statsInitialized;
            }*/
        }
    }


    private void OnDestroy()
    {
        //Guardamos los datos
        if(GameDataManager.Instance) SaveData(GameDataManager.Instance.gameData);

        //Desengachamos los eventos
        stats.HP.OnValueUpdate.RemoveListener(OnHPUpdate);

    }


    #region Init Editor
    private void OnRenderObject()
    {
        playerData?.Initialize(this);      
    }
    #endregion

    //Metodos de Stats
    public Stats GetStats()
    {
        return stats;
    }

    private void OnHPUpdate(float val)
    {
        if (val <= 0)        {
            onDie.Invoke();
            //GameManager.Instance.LevelRestart(0.5f);
        }
    }


    public void OnHeal(float heal)
    {
        throw new System.NotImplementedException();
    }

    public void OnDie()
    {
        stats.HP.CurrentValue = 0;
        onDie?.Invoke();
    }

    public void OnDamage(float damage)
    {
        if (stats.invulnerable) return;

        stats.HP.CurrentValue -= damage;
        if (stats.HP.CurrentValue > 0) //Lanzar corutina de invulnerabilidad temporal
        {
            onHurt.Invoke();
        }
    }

    #region Invulnerabilidad
    public void TemporalInvulneravility2D()
    {
        StartCoroutine(TemporalInvulneravilityCoroutine2D());
    }

    private IEnumerator TemporalInvulneravilityCoroutine2D()
    {
        //1. Activo invulnerabilidad
        stats.invulnerable = true;

        //1.1. Activar la invulnarabilidad utilizando las capas (Layer)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"),true);

        //1.2. Hacer cambios al sprite para que el usuario sepa que es invulnerable. Rojo
        SpriteRenderer ren=GetComponentInChildren<SpriteRenderer>();//NO ES EFICIENTE
        Color colorBase = ren.color;

        ren.color = stats.invulnerabilityColor;

        //2. Esperar el tiempo de la invulnerabilidad
        yield return new WaitForSecondsRealtime(stats.invulnerabilitySeconds);

        //3.1. Deshacemos los cambios al sprite
        ren.color = colorBase;

        //3.2. Desactivar la invulnarabilidad utilizando las capas (Layer)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
        
        //3.3. Desactivo la invulnerabilidad en los stats
        stats.invulnerable = false;

        yield return null;
    }

    public void TemporalInvulneravility2D(float seconds)
    {
        StartCoroutine(TemporalInvulneravilityCoroutine2D(seconds));
    }
    private IEnumerator TemporalInvulneravilityCoroutine2D(float seconds)
    {
        stats.invulnerable = true;
        yield return new WaitForSecondsRealtime(seconds);
        stats.invulnerable = false;
    }
    #endregion

    //Other
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void SaveData(GameData g)
    {
        if (g==null) return;
        g.playerStats.Update(stats);
    }

    public void LoadData(GameData g)
    {
        if (g == null) return;
        stats.Update(g.playerStats);
    }
}