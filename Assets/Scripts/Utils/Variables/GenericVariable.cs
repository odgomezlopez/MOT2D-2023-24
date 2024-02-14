using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GenericVariable<T>
{
    //Datos
    [Header("Initial value")]
    public T initialValue;

    [Header("Float runtime value")]
    [SerializeField] private T runtimeValue;

    [Header("Float onChange events")]
    public UnityEvent<T> OnValueUpdate;

    //Get y Set publico
    public T CurrentValue
    {
        get => runtimeValue;
        set
        {
            T oldValue = runtimeValue;
            runtimeValue = CheckNewValue(value);

            if (!oldValue.Equals(runtimeValue)) {
                try
                {
                    InvokeEvents();
                }
                catch (Exception e) { Debug.LogError("An event attached to indicator have failed"); Debug.LogError(e); }
            }
        }
    }


    protected virtual T CheckNewValue(T value)
    {
        return value;
    }

    protected virtual void InvokeEvents()
    {
        OnValueUpdate?.Invoke(runtimeValue);
    }

    //Utilidades
    public void Restart()
    {
        CurrentValue = initialValue;
    }

    public void Update(GenericVariable<T> newValue)
    {
        initialValue = newValue.initialValue;
        CurrentValue = newValue.runtimeValue;
    }

    public void Validate()
    {
        CurrentValue = runtimeValue;
    }
}

[System.Serializable]
public class RangedFloatVariable : GenericVariable<float>
{
    [Header("Float Range")]

    public float minValue = 0;
    public float maxValue = 100;

    [Header("Additional Float Events")]
    public UnityEvent<float> OnPercentChange;

    protected override void InvokeEvents()
    {
        base.InvokeEvents();
        OnValueUpdate?.Invoke(GetPercentage());
    }


    //Sobreescrito
    protected override float CheckNewValue(float value)
    {
        return Mathf.Clamp(value, 0.0f, maxValue);
    }

    public void Update(RangedFloatVariable newValue)
    {
        base.Update(newValue);
        minValue = newValue.minValue;
        maxValue = newValue.maxValue;
    }

    public float GetPercentage()
    {
        if ((maxValue - minValue) == 0) return 0;
        return (CurrentValue - minValue) / (maxValue - minValue);
    }
}

//Backwards compatibility
[System.Serializable]
public class Indicator : RangedFloatVariable
{ }