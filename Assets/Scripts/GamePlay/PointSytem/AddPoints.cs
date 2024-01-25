using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

using UnityEngine.Events;

public class AddPoints : MonoBehaviour
{
    //Datos propios
    [SerializeField] [Range(1,10)] private int points=1;
    public UnityEvent<float> addPointEvent;
    public UnityEvent pointCollectEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Opción 1. Comunicación por Singleton
            //ScoreManager.Instance.AddPoints(points);
            //Opcion 2. Comunicación con eventos
            addPointEvent?.Invoke(points);

            //Evento para efectos
            pointCollectEvent?.Invoke();//Llamo al evento
            Destroy(gameObject);//Me destruyo
        }
    }
}
