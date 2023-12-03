using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
        //Acciones de PlayerLateralMovement

        m_action1Action = playerController.playerInput.actions["Action1"];
        m_action2Action = playerController.playerInput.actions["Action2"];
    }

    // Update is called once per frame
    void Update()
    {
        //Acción1
        if (m_action1Action.triggered)
        {
            Debug.Log("Action1");
            CoolDown(m_action1Action, stats.action1.cooldown);
            stats.action1?.Use(playerController);
        }
        //Acción2
        if (m_action2Action.triggered)
        {
            Debug.Log("Action2");
            CoolDown(m_action2Action, stats.action2.cooldown);
            stats.action2?.Use(playerController);
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
