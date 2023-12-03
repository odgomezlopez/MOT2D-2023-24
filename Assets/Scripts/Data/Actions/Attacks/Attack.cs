using Unity.VisualScripting;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Attack", menuName = "Actions/Attacks/Basic Attack")]
public class Attack : Action
{
    [Header("Basic Attack info")]
    public float damage;
}