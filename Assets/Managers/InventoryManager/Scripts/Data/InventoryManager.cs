using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviourSingleton<InventoryManager> //MonoBehaviourSaveableSingleton<InventoryManager> Cambiar para guardado en memoria
{ 
    //Item, Cantidad
    [SerializeField] private SerializedDictionary<Item, int> data = new();
    public int Count => data.Count;

    public void AddItemToInventory(Item itemData, int cant = 1)
    {
        if (!CheckItemInventory(itemData))
            data[itemData] = cant;
        else
            data[itemData] += cant;
    }

    public void RemoveItemFromInventory(Item itemData, int cant = 1)
    {
        if (CheckItemInventory(itemData))
        {
            data[itemData] -= cant;

            if (data[itemData] <= 0) data.Remove(itemData);
        }
    }

    public void Clear()
    {
        data.Clear();
    }

    public bool CheckItemInventory(Item itemData)
    {
        return data.ContainsKey(itemData);
    }

    public List<KeyValuePair<Item, int>> ToList()
    {
        return data.ToList();
    }
}