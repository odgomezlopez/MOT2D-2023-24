using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionDisplay : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] InputActionReference inputAction;

    TextMeshProUGUI texto;
    string lastControlScheme = null;

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        texto = GetComponent<TextMeshProUGUI>();

        // Initialize text with current binding
        texto.SetText(DisplayString);

        // Optionally, subscribe to control scheme changes if available
        playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnControlsChanged(PlayerInput pi)
    {
        UpdateDisplay();
    }

    private void Update()
    {
        // Check if the control scheme has changed to update the text
        if (playerInput.currentControlScheme != lastControlScheme)
        {
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        texto.SetText(DisplayString);
        lastControlScheme = playerInput.currentControlScheme;
    }

    private string DisplayString
    {
        get
        {
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
