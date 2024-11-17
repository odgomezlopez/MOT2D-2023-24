using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/ItemDataScriptableObject/HealingPotion", order = 1)]
public class Potion : Item
{
    [Header("Potion info")]
    [SerializeField] private int HP = 1;
    public override void Use()
    {
        ActorController target = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<ActorController>();
        target.Stats.HP.CurrentValue += HP;
        base.Use();
    }
}
