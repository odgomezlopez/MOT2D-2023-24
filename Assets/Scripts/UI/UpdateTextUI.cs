using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UpdateTextUI : MonoBehaviour
{
    //Referencia a la imagen
    TextMeshProUGUI textUI;

    [SerializeField] Color enable=Color.black;
    [SerializeField] Color disable=Color.gray;

    //Inicializaciones
    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();   
    }

    //Actualización UI
    public void UpdateText(string text)
    {
        textUI.SetText(text);
    }
    public void UpdateText(string text,bool active)
    {
        UpdateText(text);

        if (active)
        {
            textUI.color = Color.black;
        }
        else
        {
            textUI.color = Color.gray;
        }
    }
}
