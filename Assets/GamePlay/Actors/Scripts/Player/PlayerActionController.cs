using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ActorController))]
public class PlayerActionController : MonoBehaviour
{
    //Referencia al controller
    ActorController playerController;

    //Referencia al player input
    [SerializeField] InputActionReference m_action1Action, m_action2Action;

    // Start is called before the first frame update
    void Start()
    {
        //Referencia al playerController
        playerController = GetComponent<ActorController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Acción1
        if (m_action1Action.action.triggered && playerController.stats.action1)
        {
            StartCoroutine(CoolDown(m_action1Action, playerController.stats.action1.cooldown));
            playerController.stats.action1?.Use(gameObject);
        }

        //Acción2
        if (m_action2Action.action.triggered && playerController.stats.action2)
        {
            StartCoroutine(CoolDown(m_action2Action, playerController.stats.action2.cooldown));
            playerController.stats.action2?.Use(gameObject);
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
