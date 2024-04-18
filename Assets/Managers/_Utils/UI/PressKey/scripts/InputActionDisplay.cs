using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.XInput;

public class InputActionDisplay : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] InputActionReference inputAction;

    TextMeshProUGUI texto;

    #region keyToSymbol
    //Font https://shinmera.github.io/promptfont/
    private Dictionary<(string, string), string> symbols = new Dictionary<(string, string), string>
    {
        {("keyboard", "space"), "␺"},
        {("keyboard", "leftarrow"), "←"},
        {("keyboard", "rightarrow"), "→"},
        {("keyboard", "uparrow"), "↑"},
        {("keyboard", "downarrow"), "↓"},
        //{("keyboard", "tab"), "␫"},

         {("keyboard", "w"), "␣"}, //Move


        {("keyboard", "lmb"), "⟵"},  //Mouse Left click
        {("keyboard", "rmb"), "⟶"}, //Mouse Right click

        {("xbox", "buttonsouth"), "A"},
        {("xbox", "buttonnorth"), "Y"},
        {("xbox", "buttoneast"), "B"},
        {("xbox", "buttonwest"), "X"},

        {("playstation", "buttonsouth"), "⇣"},//X
        {("playstation", "buttonnorth"), "⇡"},//Triangle
        {("playstation", "buttoneast"), "⇢"},//Circle
        {("playstation", "buttonwest"), "⇠"},//Square

        {("switch", "buttonsouth"), "B"},
        {("switch", "buttonnorth"), "X"},
        {("switch", "buttoneast"), "A"},
        {("switch", "buttonwest"), "Y"},

        {("gamepad", "buttonsouth"), "↧"},
        {("gamepad", "buttonnorth"), "↥"},
        {("gamepad", "buttoneast"), "↦"},
        {("gamepad", "buttonwest"), "↤"}
    };
    #endregion


    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        texto = GetComponent<TextMeshProUGUI>();

        // Initialize text with current binding
        UpdateDisplay();

        // Optionally, subscribe to control scheme changes if available
        playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnControlsChanged(PlayerInput pi)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        string key = DisplayString();
        string symbol = KeyToSymbol(key);
        texto.SetText(symbol);
    }

    private string DisplayString() { 
        InputBinding activeBinding = inputAction.action.bindings
            .FirstOrDefault(binding =>
                binding.groups
                .Split(";")
                .Any(scheme => scheme == playerInput.currentControlScheme)
            );
        string cadena= activeBinding != default
            ? activeBinding.ToDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions)
            : "No active binding";

        //TODO Parsear al simbolo del mando concreto.

        return cadena;
    }
    private string KeyToSymbol(string key)
    {
        // Determine the device type
        string deviceType = "keyboard"; // Default device type
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

        // Lookup symbol in the unified dictionary
        if (symbols.TryGetValue((deviceType, key.ToLower()), out string symbol))
        {
            return symbol;
        }

        return key; // Return the original key if no symbol is found
    }


    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (playerInput != null)
        {
            playerInput.onControlsChanged -= OnControlsChanged;
        }
    }
}
