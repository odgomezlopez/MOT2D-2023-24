using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionItemRequiered : PlayerContextualActionTriggererV2
{
    [Header("Required Item")]
    public Item requiredItem;
    public bool unlocked=false;

    private void Start()
    {
        if(unlocked) gameObject.SetActive(false);
        //UITextDisable+= requiredItem.itemName;
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
