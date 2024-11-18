using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




[System.Serializable]
public class Stats
{
    [Header("Basic")]
    public string actorName;

    public RangedFloatVariable HP;

    [Header("Att")]
    public float attDamage;
    
    public ActionSO action1;//MeleeAttack
    public ActionSO action2;//DistanceAttack

    [Header("Dinamic")]
    public bool invulnerable;



    //Metodos
    public virtual void ResetStats()
    {
        HP.Restart();
        invulnerable= false;
    }

    public void Update(Stats newStats)
    {
        actorName = newStats.actorName;
        //HP.Update(newStats.HP);
        attDamage = newStats.attDamage;

        action1 = newStats.action1;
        action2 = newStats.action2;
    }

}
