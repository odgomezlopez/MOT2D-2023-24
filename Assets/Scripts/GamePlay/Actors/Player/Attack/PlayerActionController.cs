using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerActionController : MonoBehaviour
{
    //Referencia al controller
    PlayerController playerController;
    PlayerStats stats;

    //Referencia al player input
    InputAction m_action1Action, m_action2Action;

    // Start is called before the first frame update
    void Start()
    {
        //Referencia al playerController
        playerController = GetComponent<PlayerController>();
        stats = (PlayerStats)playerController.GetStats();

        //Acciones de PlayerActionController
        m_action1Action = playerController.playerInput.actions["Action1"];
        m_action2Action = playerController.playerInput.actions["Action2"];
    }


    // Update is called once per frame
    void Update()
    {
        //Acción1
        if (m_action1Action.triggered)
        {
            StartCoroutine(CoolDown(m_action1Action, 0.5f));


            Debug.Log("Action1");
            if (stats.action1Prefab)
            {
                GameObject att=Instantiate(stats.action1Prefab, transform);
                att.layer = gameObject.layer;

                //Inicializo el ataque
                att.GetComponent<OnAttackImpack2>()?.Initialize(2f);
                playerController.TemporalInvulneravility2D(1f);
                //att.GetComponent<Animator>().speed = 10.0f;
                //att.GetComponent<AttackMoveTowards2D>()?.Initialize(10f, new Vector2(1, 0));
            }
            //TODO Invulnerabiliy???
            //stats.action1?.Use(gameObject);
        }

        //Acción2
        if (m_action2Action.triggered)
        {
            StartCoroutine(CoolDown(m_action2Action, 1f));


            Debug.Log("Action2");
            if (stats.action2Prefab)
            {
                GameObject att = Instantiate(stats.action2Prefab, transform.position, Quaternion.identity);
                att.layer = gameObject.layer;

                //Inicializo el ataque a distancia
                att.GetComponent<OnAttackImpack2>()?.Initialize(1f);
                att.GetComponent<AttackMoveTowards2D>()?.Initialize(10f,transform.right);
            }

        }
    }

    //CoolDown
    public IEnumerator CoolDown(InputAction act,float cooldownTime)
    {
        act.Disable();
        yield return new WaitForSeconds(cooldownTime);
        act.Enable();
    }
}
