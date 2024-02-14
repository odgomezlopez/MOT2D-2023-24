using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class GenericVariableSO<T, V> : ScriptableObject where V : GenericVariable<T>, new()
{
    [SerializeField] protected V genericVariable = new V();

    protected virtual void OnEnable()
    {
        genericVariable.Restart();
        if (genericVariable.OnValueUpdate == null)
            genericVariable.OnValueUpdate = new UnityEvent<T>();
    }

    protected virtual void OnValidate()
    {
        genericVariable.Validate();
    }

    public T CurrentValue
    {
        get => genericVariable.CurrentValue;
        set => genericVariable.CurrentValue = value;
    }

    public UnityEvent<T> OnValueUpdate
    {
        get => genericVariable.OnValueUpdate;
        set => genericVariable.OnValueUpdate = value;
    }

    public void Restart() => genericVariable.Restart();
    public void UpdateVariable(V newValue) => genericVariable.Update(newValue);
}