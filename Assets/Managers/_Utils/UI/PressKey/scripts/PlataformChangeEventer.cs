using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.XInput;

[ExecuteAlways]
public class PlataformChangeEventer : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] string activeControlScheme="";

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        OnDeviceChange();
    }

    private void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            if (activeControlScheme != playerInput.currentControlScheme)
            {
                activeControlScheme = playerInput.currentControlScheme;
                OnDeviceChange();
            }
        }
    }

    private void OnDeviceChange()
    {
        string device = GetDeviceType();

        switch (device)
        {
            case "keyboard&mouse":
                break;
            case "gamepad":
                break;
            case "playstation":
                break;
            case "xbox":
                break;
            case "switch":
                break;
            default : 
                break;
        }
    }


    private string GetDeviceType()
    {
        string deviceType = "none";
        string controlScheme = playerInput.currentControlScheme.ToLower();

        if (controlScheme == "keyboard&mouse")
            deviceType = "keyboard&mouse";
        else if (controlScheme == "gamepad")
        {
            if (Gamepad.current != null)
            {
                deviceType = Gamepad.current switch
                {
                    DualShockGamepad => "playstation",
                    XInputController => "xbox",
                    SwitchProControllerHID => "switch",
                    _ => "gamepad" // Generic gamepad if type is unknown
                };
            }
        }
        //TODO Añadir else if para XR, etc. 
        return deviceType;
    }
}
