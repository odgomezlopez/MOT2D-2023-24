using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Actions/Attacks/Prefab Attack")]
public class AttackPrefabSO : AttackSO
{
    [Header("Prefab Attack")]
    public GameObject prefab;

    public enum AttackType { Melee, Distance };
    public AttackType type;

    public float speed = 5f;
    public float maxLife=10;

    [Header("Sprite info")]
    public Sprite sprite;
    public Color color=Color.white;

    public override void Use(GameObject org)
    {
        //Usamos el metodo heredado 
        base.Use(org);

        //Calculo valores
        float damage = this.damage;

        //Si el origen tiene un IActorController (Si se quiere que el da�o)//TODO Pensar como hacer m�s generico
        /*if (org.GetComponent<IActorController>()!=null)
        {
            damage+=org.GetComponent<IActorController>().GetStats().attDamage;
        }*/
        //Actualizo la apariencia del prefab
        GameObject prefabInitialized = Initialize(prefab);

        //Realizo el ataque
        Attack(org, prefabInitialized, damage);
    }

    private void Attack(GameObject org, GameObject newPrefab, float damage)
    {
        //Instanciamos el ataque donde esta el jugador
        GameObject g = Instantiate(newPrefab, org.transform);

        //Inicializo componentes
        bool destroyOnCollision = (type == AttackType.Distance);//Solo se destruye cuando colisiona los ataques de distancia
        g.GetComponent<OnAttackImpact>()?.Initializate(damage, destroyOnCollision, org.tag, 10);

        //Modifico seg�n tipo de ataque
        if (type == AttackType.Melee)
        {
            if (g.GetComponent<Animator>()) g.GetComponent<Animator>().speed = speed;
            //TODO Activar invulnerabilidad cuando se hacen los ataques melee
        }

        if (type == AttackType.Distance)
        {
            //Si es un ataque a distancia hacemos que se desacople del actor
            g.transform.SetParent(null, true);
            //Inicializo la velocidad teniendo en cuenta hacie donde miro
            g.GetComponent<AttackMoveTowards2D>()?.Initialize(speed, g.transform.right);//TODO Adapt for 3D Vector3.forward
        }
    }


    public GameObject Initialize(GameObject g)
    {
        SpriteRenderer ren = g.GetComponentInChildren<SpriteRenderer>();
        //Animator ani = g.GetComponentInChildren<Animator>();
        Collider2D col = g.GetComponentInChildren<Collider2D>();

        //1. Intercambiamos info
        if (ren)
        {
            if(sprite!=null) ren.sprite = sprite;
            ren.color = color;
        }
        /*if (ani)
        {
            ani.runtimeAnimatorController = animator;
        }*/

        //2. Recalcular el colider (MODIFICAR EN 3D)
        // Adjust the size of the collider to match the sprite size
        if (col)
        {
            if (col is BoxCollider2D boxCol)
            {
                boxCol.size = new Vector2(ren.bounds.size.x, ren.bounds.size.y);
                boxCol.offset = new Vector2(ren.bounds.center.x - g.transform.position.x, ren.bounds.center.y - g.transform.position.y);
            }

            if (col is CapsuleCollider2D capCol)
            {
                capCol.size = new Vector2(ren.bounds.size.x, ren.bounds.size.y);
                capCol.offset = new Vector2(ren.bounds.center.x - g.transform.position.x, ren.bounds.center.y - g.transform.position.y);
            }
        }
        return g;
    }
}