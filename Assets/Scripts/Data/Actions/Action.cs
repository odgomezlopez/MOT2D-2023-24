using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Action", menuName = "Actions/Basic Action")]
public class Action : ScriptableObject
{
    [Header("ActionInfo")]
    public string actionName="DefaultName";
    public float cooldown;

    public virtual void Use(IActorController actor)
    {
        Debug.Log($"Actor {actor.GetStats().actorName} has used the action: {actionName}");
    }
}
