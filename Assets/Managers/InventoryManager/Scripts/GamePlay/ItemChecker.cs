using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : PlayerContextualActionTriggerer
{
    [Header("Required Item")]
    public Item requiredKey;
    public bool unlocked=false;

    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItemFromInventory(requiredKey);
        unlocked = true;
    }
    protected override bool CheckRequirement()
    {
        if ((InventoryManager.Instance.CheckItemInventory(requiredKey) || unlocked))
        {
            return true;
        }
        else {
            return false;
        }
    }

}
