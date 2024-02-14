using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedFloatVariableSOListener : MonoBehaviour
{
    [SerializeField] private RangedFloatVariableSO floatVariable;

    public UnityEvent<float> onValueUpdate;
    public UnityEvent<float> onPercentUpdate;

    private void Start()
    {
        OnValue(floatVariable.CurrentValue);
    }

    private void OnEnable()
    {
        floatVariable?.OnValueUpdate.AddListener(OnValue);
    }
    private void OnDisable()
    {
        floatVariable?.OnValueUpdate.RemoveListener(OnValue);
    }

    private void OnValue(float value)
    {
        onValueUpdate.Invoke(value);
        onPercentUpdate.Invoke(floatVariable.GetPercentage());
    }
}
