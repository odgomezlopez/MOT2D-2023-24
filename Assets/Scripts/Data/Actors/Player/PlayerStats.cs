using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : Stats
{
    [Header("Aceleración")]
    public float acceleration;
    public float deceleration;

    [Header("Salto")]
    public float jumpSpeed;
    [Range(0, 1)] public float airMomemtum = 0.8f;
    [Range(1, 5)] public int jumpMax = 1;

}
