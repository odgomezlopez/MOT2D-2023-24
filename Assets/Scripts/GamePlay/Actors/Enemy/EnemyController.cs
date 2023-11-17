using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour, IActorController
{
    //Propiedades del enemigo
    [SerializeField] EnemyStats stats;

    [Header("Eventos generales")]
    public UnityEvent onDie = new();

    private void Awake()
    {
        //Me suscribo a los cambios de HP de los stats
        stats.HP.OnIndicatorChange.AddListener(OnHPUpdate);
    }

    //Gestión del HP
    private void OnHPUpdate(float val)
    {
        if (val <= 0)
        {
            onDie.Invoke();
            Destroy(gameObject,0.5f);
        }
    }


    //Metodos de la interfaz

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Stats GetStats()
    {
        return stats;
    }

    public void OnDamage(float damage)
    {
        stats.HP.CurrentValue -= damage;
    }

    public void OnHeal(float heal)
    {
        throw new System.NotImplementedException();
    }
}
