using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpdateFilledImageUI : MonoBehaviour
{
    //Referencia a la imagen
    Image img;

    //Evento al que estoy suscripto
    UnityEvent<float> watchingEvent;

    //Inicializaciones
    private void Start()
    {
        img = GetComponent<Image>();   
    }

    //Actualización UI
    public void UpdateFilledImage(float value)
    {
        img.fillAmount = value;
    }

    //Gestión eventos
    public void WatchEvent(UnityEvent<float> e)
    {
        watchingEvent = e;
        e.AddListener(UpdateFilledImage);
    }

    private void OnDestroy()
    {
        watchingEvent?.RemoveListener(UpdateFilledImage);
    }
}
