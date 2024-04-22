using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UseItem : PlayerContextualActionTriggerer
{
    [Header("Required Item")]
    public Item requiredItem;
    public bool unlocked=false;

    private void Awake()
    {
        UITextDisable+= requiredItem.itemName;
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItemFromInventory(requiredItem);
        unlocked = true;
    }
    protected override bool CheckRequirement()
    {
        if ((InventoryManager.Instance.CheckItemInventory(requiredItem) || unlocked))
        {
            return true;
        }
        else {
            return false;
        }
    }

}
