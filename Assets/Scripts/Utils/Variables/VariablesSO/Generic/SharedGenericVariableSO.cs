using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[CreateAssetMenu(menuName = "Game Event/Base")]
public abstract class SharedGenericVariableSO<T> : ScriptableObject
{
    //Propiedades
    [Header("Variable value")]
    public T InitialValue;
    [SerializeField] private T RuntimeValue;


    //GetterSetter
    public T CurrentValue
    {
        get => RuntimeValue;
        set
        {
            RuntimeValue = value;
            try
            {
                TriggerEvent(RuntimeValue);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
    //Gestión SO
    protected virtual void OnEnable()
    {
        CurrentValue = InitialValue;
    }

    private void OnValidate()
    {
        CurrentValue = RuntimeValue;
    }

    //Gestión de eventos
    List<SharedGenericVariableSOListener<T>> listeners = new List<SharedGenericVariableSOListener<T>>();

    public void TriggerEvent(T value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventTriggered(value);
    }

    public void AddListener(SharedGenericVariableSOListener<T> listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(SharedGenericVariableSOListener<T> listener)
    {
        listeners.Remove(listener);
    }
}