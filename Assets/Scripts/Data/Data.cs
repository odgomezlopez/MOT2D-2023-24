using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    //Puntos
    [Header("Sistema de puntos")]
    [SerializeField] private int points;

    [Header("Sistema de stats")]
    [SerializeField] public PlayerStats stats;

    //Player default info
    [Header("Player Info")]
    [SerializeField] public PlayerDataSO playerData;
    [SerializeField] public bool statsInitialized = false;

    //Datos persistentes entre escenas
    private void Awake()
    {
        int numInstance = FindObjectsOfType<Data>().Length;

        if(numInstance != 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    //Metodos de acceso a los puntos
    //public int Points { get => points; set => points = value; }
    public int GetPoints() { return points; }

    public void AddPoints(int newPoints)
    {
        if (newPoints <= 0) return;
        
        points += newPoints;
    }

}
