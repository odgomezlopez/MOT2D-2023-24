using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Actions/Attacks/RayCast2D Attack")]
public class AttackRayCastSO2D : AttackSO
{
    [Header("RayCast Attack")]
    public float range;

    public override void Use(GameObject org)
    {
        //Usamos el metodo heredado 
        base.Use(org);
        
        //Obtenemos la info que necesitamos del actor
        Transform transform = org.transform;

        //TODO lanzar Animacion/SFX/VFX

        //Lanzamos el ray
        Vector3 dir = transform.right * transform.localScale.x;//En 3D hacer que sea la dirección hacia donde mira

        var hit = Physics2D.Raycast(transform.position, dir, this.range);

        if (hit.collider && hit.collider.CompareTag("Enemy"))
        {
            Debug.DrawRay(transform.position, dir * hit.distance, Color.red, 1f);

            float damage = this.damage;
            hit.collider.GetComponentInParent<IActorController>()?.OnDamage(damage);
        }
        else
        {
            Debug.DrawRay(transform.position, dir * this.range, Color.blue, 1f);
        }
    }
}
