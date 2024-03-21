using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveableEntityStorage
{
    [SerializeField]
    [SerializedDictionary("ID", "JSON")]
    private SerializedDictionary<string, string> storage = new SerializedDictionary<string, string>();

    public void StoreObject(string newKey, object newObject)
    {
        string json=JsonUtility.ToJson(newObject);
        storage[newKey] = json;
    }

    public bool HasObject(string newKey)
    {
        return storage.ContainsKey(newKey);
    }

    public T RecoverObject<T>(string newKey)
    {
        if (storage.TryGetValue(newKey, out string json))
            return JsonUtility.FromJson<T>(json);
        else
            return default(T);
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
