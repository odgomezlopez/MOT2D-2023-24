using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GenericVariable<T>
{
    //TODO Falta investigar como hacer que se autosincronice, de forma bidireccional, con SharedGenericVariableSO

    //Datos
    [Header("Variable value")]
    public T initialValue;
    [SerializeField] private T runtimeValue;

    [Header("OnChange events")]
    //public SharedGenericVariableSO<T> SyncSO;
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
                catch (Exception e) { Debug.LogError("An event attached to indicator have failed"); Debug.LogException(e); }
            }
        }
    }

    protected virtual T CheckNewValue(T value)
    {
        return value;
    }

    protected virtual void InvokeEvents()
    {
        //if (SyncSO) SyncSO.CurrentValue = CurrentValue;
        OnValueUpdate?.Invoke(CurrentValue);
    }

    #region Utils

    public void Restart()
    {
        CurrentValue = initialValue;
    }

    public void Update(GenericVariable<T> newValue)
    {
        initialValue = newValue.initialValue;
        CurrentValue = newValue.runtimeValue;
    }
    #endregion
}

[System.Serializable]
public class RangedFloatVariable : GenericVariable<float>
{
    [Header("Float Range")]

    public float minValue = 0;
    public float maxValue = 100;

    [Header("Float Percent Events")]
    public SharedGenericVariableSO<float> SyncPercentSO;
    public UnityEvent<float> OnPercentUpdate;

    protected override void InvokeEvents()
    {
        base.InvokeEvents();
        OnPercentUpdate?.Invoke(GetPercentage());
        if(SyncPercentSO) SyncPercentSO.CurrentValue=GetPercentage();
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
public class Indicator : RangedFloatVariable { }