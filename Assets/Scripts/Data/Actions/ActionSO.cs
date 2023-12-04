using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Action", menuName = "Actions/Basic Action")]
public class ActionSO : ScriptableObject
{
    [Header("ActionInfo")]
    public string actionName="DefaultName";
    public float cooldown;

    public virtual void Use(GameObject origin)
    {
        Debug.Log($"GameObject {origin.name} has used the action: {actionName}");
    }
}
