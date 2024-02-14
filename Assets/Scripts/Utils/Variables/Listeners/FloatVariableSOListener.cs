using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatVariableSOListener : MonoBehaviour
{
    [SerializeField] private FloatVariableSO floatVariable;

    public UnityEvent<float> onValueUpdate;

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
    }
}
