using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviourSingletonPersistent<Data>
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
}
