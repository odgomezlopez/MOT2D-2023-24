using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Indicator
{
    //Info
    public String indicatorName;
    public Image icon;

    //Datos
    [SerializeField] 
    private float currentValue;
    public float initValue;
    public float maxValue;

    //Gestion recuperación/deterioro automatico
    public bool enableAutoUpdate=true;
    public float autoUpdateRate = 0;

    //Eventos para detectar cambios en el indicador. (mas info en https://gamedevbeginner.com/events-and-delegates-in-unity)
    //Opción 1. Unity Event Facil enlazar elementos en el Editor.

    public UnityEvent<float> OnIndicatorChange;


    //Opción 2. Delegate. Facil pasar parámetros asociados al evento.
    //public delegate void OnIndicatorChangeDelegate(float newValue);
    //public event OnIndicatorChangeDelegate OnIndicatorChange;
    //Contrustor
    public Indicator()
    {
        UpdateCaller.OnUpdate += Update;
    }

    ~Indicator()
    { 
        OnIndicatorChange.RemoveAllListeners();
        UpdateCaller.OnUpdate -= Update;
    }

    #region Sistema Auto-Curacion/Deterioro
    //Gestion de restauracion/deterioro
    void Update()
    {
        if (enableAutoUpdate && autoUpdateRate != 0) this.CurrentValue += autoUpdateRate * Time.deltaTime;
    }
    #endregion

    //Inicialización
    public virtual void RestartStats()
    {
        CurrentValue = initValue;
        //autoUpdateRate = 0;
    }

    //Operaciones
    public float GetPercentage()
    {
        return CurrentValue / maxValue;
    }

    //Get y Set publico
    public float CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = Mathf.Clamp(value, 0.0f, maxValue);

            //1. Unity Event
            OnIndicatorChange.Invoke(currentValue);

            //2. Delegate
            //if (OnIndicatorChange != null)
            //    OnIndicatorChange(value);
        }
    }

    public void Update(Indicator newIndicator)
    {
        indicatorName = newIndicator.indicatorName;
        CurrentValue = newIndicator.currentValue;
        initValue = newIndicator.initValue;
        maxValue = newIndicator.maxValue;
        enableAutoUpdate = newIndicator.enableAutoUpdate;
        autoUpdateRate = newIndicator.autoUpdateRate;
    }
}
