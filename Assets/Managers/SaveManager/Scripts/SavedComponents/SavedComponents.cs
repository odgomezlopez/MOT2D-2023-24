using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedComponents
{
    [SerializeField]
    [SerializedDictionary("ID", "JSON")]
    private SerializedDictionary<string, string> storage = new SerializedDictionary<string, string>();

    //[SerializeField]
    //private Dictionary<string, string> storage = new Dictionary<string, string>();

    public void StoreObject(string newKey, string json)
    {
        storage[newKey] = json;
    }

    public bool HasObject(string newKey)
    {
        return storage.ContainsKey(newKey);
    }

    public string RecoverObject(string newKey)
    {
        if (storage.TryGetValue(newKey, out string json))
            return json;
        else
            return null;
    }

    public void DeleteObject(string newKey)
    {
        if(storage.ContainsKey(newKey)) storage.Remove(newKey);
    }

    public void ClearObjects()
    {
        storage.Clear();
    }

}
