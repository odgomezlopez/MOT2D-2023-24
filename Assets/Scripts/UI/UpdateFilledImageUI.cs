using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class UpdateFilledImageUI : MonoBehaviour
{
    Image img;
    private void Start()
    {
        img = GetComponent<Image>();   
    }
    public void UpdateFilledImage(float value)
    {
        img.fillAmount = value;
    }
}
