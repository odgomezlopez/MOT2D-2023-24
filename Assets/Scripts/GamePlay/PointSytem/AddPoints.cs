using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
        if (GameObject.FindGameObjectWithTag("GameData"))
        {
            data = GameObject.FindGameObjectWithTag("GameData").GetComponent<Data>();
        }
        else
        {
            Debug.LogError("Error: GameData gameobject/tag not found");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            data?.AddPoints(points);//Sumo los puntos a data
            pointCollectEvent.Invoke();//Llamo al evento
            Destroy(gameObject);//Me destruyo
        }
    }
}
