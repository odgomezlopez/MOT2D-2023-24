using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UpdateTextUI : MonoBehaviour
{
    TextMeshProUGUI textUI;

    [SerializeField] Color enableColor = Color.black;
    [SerializeField] Color disableColor = Color.gray;

    private void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateText(string text)
    {
        textUI.SetText(text);
    }

    public void UpdateText(string text, bool enable)
    {
        UpdateText(text);

        if (enable) textUI.color = enableColor;
        else textUI.color = disableColor;
    }

}
