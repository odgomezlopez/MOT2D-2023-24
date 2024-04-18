﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.XInput;

public class InputActionDisplay : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] InputActionReference inputAction;
    [SerializeField] string activeControlScheme;

    TextMeshProUGUI texto;

    #region keyToSymbol
    //Font https://shinmera.github.io/promptfont/
    private Dictionary<(string, string), string> symbols = new Dictionary<(string, string), string>
    {
        {("keyboard&mouse", "space"), "␺"},
        {("keyboard&mouse", "leftarrow"), "←"},
        {("keyboard&mouse", "rightarrow"), "→"},
        {("keyboard&mouse", "uparrow"), "↑"},
        {("keyboard&mouse", "downarrow"), "↓"},
        //{("keyboard", "tab"), "␫"},

        {("keyboard&mouse", "w"), "␣"}, //Move

        {("keyboard&mouse", "delta"), "␾"},  //Mouse Left click
        {("keyboard&mouse", "lmb"), "⟵"},  //Mouse Left click
        {("keyboard&mouse", "rmb"), "⟶"}, //Mouse Right click

        {("xbox", "rt"), "↗"},
        {("xbox", "rb"), "↝"},
        {("xbox", "rs"), "⇌"},

        {("playstation", "a"), "⇣"},//X
        {("playstation", "y"), "⇡"},//Triangle
        {("playstation", "b"), "⇢"},//Circle
        {("playstation", "x"), "⇠"},//Square

        {("playstation", "lt"), "↖"},
        {("playstation", "lb"), "↜"},
        {("playstation", "ls"), "⇱"},

        {("playstation", "rt"), "↗"},
        {("playstation", "rb"), "↝"},
        {("playstation", "rs"), "⇲"},

        {("switch", "a"), "B"},
        {("switch", "y"), "X"},
        {("switch", "b"), "A"},
        {("switch", "x"), "Y"},

        {("gamepad", "rt"), "↗"},
        {("gamepad", "rb"), "↝"},
        {("gamepad", "rs"), "⇌"}

    };
    #endregion


    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        texto = GetComponent<TextMeshProUGUI>();
        activeControlScheme = playerInput.currentControlScheme;

        // Initialize text with current binding
        UpdateDisplay();
    }

    private void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            if (activeControlScheme != playerInput.currentControlScheme)
            {
                activeControlScheme = playerInput.currentControlScheme;
                UpdateDisplay();
            }
        }
        //UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        string key = DisplayString();
        texto.SetText(key);
    }

    private string DisplayString() {
        //0. Defino las variables
        string key= "No active binding";
        string controlScheme = playerInput.currentControlScheme.ToLower();
    
        //2. Obtengo el binding
        InputBinding activeBinding = inputAction.action.bindings
             .FirstOrDefault(binding =>
                 binding.groups
                 .Split(";")
                 .Any(scheme => scheme.ToLower() == controlScheme.ToLower())
             );

        key = activeBinding != default
            ? activeBinding.ToDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions) 
            //activeBinding.effectivePath.Split("/")[1]
            : "No active binding";


        //3. Comrpuebo si estoy en consola;
        if (controlScheme == "gamepad") controlScheme = GetDeviceType();

        // 4. Comrpuebo si hay simbolo especial asociado
        if (symbols.TryGetValue((controlScheme, key.ToLower()), out string symbol))
        {
            return symbol;
        }

        return key; 
    }
  
    private string GetDeviceType()
    {
        string deviceType = "keyboard&mouse";

        //playerInput.currentControlScheme;
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
        return deviceType;
    }
}
