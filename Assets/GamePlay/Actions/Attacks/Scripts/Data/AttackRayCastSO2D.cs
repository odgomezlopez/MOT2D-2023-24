using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Actions/Attacks/RayCast2D Attack")]
public class AttackRayCastSO2D : ActionSO
{
    [Header("Basic Attack info")]
    public float damage;
    [Header("RayCast Attack")]
    public float range;

    public override void Use(GameObject org)
    {
        //Usamos el metodo heredado 
        base.Use(org);
        
        //Obtenemos la info que necesitamos del actor

        var hit = Physics2D.Raycast(org.transform.position, org.transform.right, this.range);

        if (hit.collider && (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Player")))
        {
            Debug.DrawRay(org.transform.position, org.transform.right * hit.distance, Color.red, 1f);
            hit.collider.GetComponentInParent<ActorController>()?.OnDamage(damage);
        }
        else
        {
            Debug.DrawRay(org.transform.position, org.transform.right * this.range, Color.blue, 1f);
        }
    }
}
