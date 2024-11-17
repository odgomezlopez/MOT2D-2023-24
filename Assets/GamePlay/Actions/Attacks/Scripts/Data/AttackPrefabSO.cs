using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "New Attack", menuName = "Actions/Attacks/Prefab Attack")]
public class AttackPrefabSO : ActionSO
{
    [Header("Basic Attack info")]
    public float damage;
    public float speed = 5f;
    public float maxLife = 10;
    public enum AttackType { Melee, Distance };
    public AttackType type;
    public bool onColisionDestroy=true;

    [Header("Prefab Attack")]
    public GameObject prefab;

    public override void Use(GameObject org)
    {
        //Usamos el metodo heredado 
        base.Use(org);

        //Calculo del daño
        float d = damage; //TODO multiplicar por daño del jugador
        /*Si el origen tiene un IActorController (Si se quiere que el daño)if (org.GetComponent<IActorController>()!=null)
        {
            d+=org.GetComponent<IActorController>().GetStats().attDamage;
        }*/

        //Instancio el ataque
        GameObject att;
        if (type == AttackType.Melee)
        {
            att = Instantiate(prefab, org.transform);//Instanciar como hijo
        }
        else //if (type == AttackType.Distance)
        {
            att = Instantiate(prefab, org.transform.position, Quaternion.identity);//Instanciar como elemento independiente en el mundo
        }

        att.layer = org.layer;

        //Inicializo componentes
        att.GetComponent<OnAttackImpact>()?.Initializate(d,onColisionDestroy);
        if (type == AttackType.Melee)
        {
            Animator ani = att.GetComponent<Animator>();

            float currentAnimationClipSeconds = 1f;//TODO
            if (ani != null)
            {
                ani.speed = speed; //Aumento la velocidad del ataque

               currentAnimationClipSeconds = ani.GetCurrentAnimatorClipInfo(0)[0].clip.length / speed;
               //Debug.Log(currentAnimationClipSeconds);
            }

            //Invulnerabilidad al PlayerController
            org.GetComponent<ActorController>()?.TemporalInvulneravility2D(currentAnimationClipSeconds);
        }

        if (type == AttackType.Distance)
        {
            att.GetComponent<AttackMoveTowards2D>()?.Initialize(speed, org.transform.right);
        }
    }


}