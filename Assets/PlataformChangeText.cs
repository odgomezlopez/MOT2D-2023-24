using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

[ExecuteAlways]
public class PlataformChangeText : MonoBehaviour
{
    TextMeshProUGUI mensaje;
    [SerializeField] string textoKeyBoard;
    [SerializeField] string textoGamePad;


    private void Start()
    {
        mensaje = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Keyboard.current != null)
        {
            mensaje.SetText(textoKeyBoard);
        }
        else if (Gamepad.current != null)
        {
            mensaje.SetText(textoGamePad);
        }
        //También se puede mirar la consola: https://docs.unity3d.com/ScriptReference/Application-platform.html y https://docs.unity3d.com/ScriptReference/RuntimePlatform.html
    }
}
