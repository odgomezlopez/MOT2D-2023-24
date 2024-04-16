using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowHideCanvas : MonoBehaviour
{
    Canvas canvas;

    [Header("Timer")]
    [SerializeField] Indicator remainingShowTime;

    [Header("Input")]
    [SerializeField] string actionName = "ShowHUD";
    protected InputAction action;


    void Start()
    {
        canvas = GetComponent<Canvas>();

        //Seleccionar la acción
        var playerInput = GameObject.FindAnyObjectByType<PlayerInput>();
        if (playerInput != null)
        {
            action = playerInput.actions[actionName];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (action.IsPressed())
        {
            remainingShowTime.Restart();
            canvas.enabled = true;
            return;
        }

        if (remainingShowTime.CurrentValue > 0)
        {
            remainingShowTime.CurrentValue -= Time.deltaTime;
            canvas.enabled = true;

        }
        else
        {
            canvas.enabled = false;
        }
    }

    public void AddSeconds(float seconds)
    {
        remainingShowTime.CurrentValue += seconds;
    }
}

