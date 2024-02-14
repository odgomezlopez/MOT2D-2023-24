using UnityEngine;

[CreateAssetMenu(menuName = "Shared Variable/RangedFloat")]
public class RangedFloatVariableSO : GenericVariableSO<float, RangedFloatVariable>
{
    public float GetPercentage() => genericVariable.GetPercentage();
    // UpdateVariable method is inherited, can be used for RangedFloatVariable specific updates.
}