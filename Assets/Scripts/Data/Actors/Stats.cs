using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [Header("Basic")]
    public Indicator HP;

    [Header("Att")]
    public float attDamage;

    [Header("Movimiento")]
    public float movementSpeed;

    //Metodos
    public void ResetStats()
    {
        HP.RestartStats();
    }
}
