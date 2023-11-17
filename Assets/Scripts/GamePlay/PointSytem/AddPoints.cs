using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class AddPoints : MonoBehaviour
{
    //Datos propios
    [SerializeField] [Range(1,10)] private int points=1;

    //Eventos
    public UnityEvent pointCollectEvent;

    //Referencia a componentes internos
    Data data;

    private void Awake()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            data.AddPoints(points);//Sumo los puntos a data
            pointCollectEvent.Invoke();//Llamo al evento
            Destroy(gameObject);//Me destruyo
        }
    }
}
