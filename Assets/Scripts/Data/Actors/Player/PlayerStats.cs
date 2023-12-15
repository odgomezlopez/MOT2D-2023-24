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

    [Header("Invulnerability")]
    public bool invulnerable = false;
    public Color invulnerabilityColor = Color.red;
    [Range(0, 3)] public float invulnerabilitySeconds = 1f;

    public override void ResetStats()
    {
        base.ResetStats();
        invulnerable = false;
    }

    public void Update(PlayerStats newStats)
    {
        actorName = newStats.actorName;
        HP.Update(newStats.HP);
        attDamage = newStats.attDamage;
        movementSpeed = newStats.movementSpeed;

        acceleration = newStats.acceleration;
        deceleration = newStats.deceleration;
        jumpSpeed = newStats.jumpSpeed;
        airMomemtum = newStats.airMomemtum;
        jumpMax = newStats.jumpMax;

        invulnerabilityColor=newStats.invulnerabilityColor;
        invulnerabilitySeconds = newStats.invulnerabilitySeconds;

        action1 = newStats.action1;
        action2 = newStats.action2;
        action1Prefab = newStats.action1Prefab;
        action2Prefab = newStats.action2Prefab;
    }
}
