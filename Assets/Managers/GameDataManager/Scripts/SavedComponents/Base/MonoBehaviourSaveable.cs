using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class MonoBehaviourSaveable : MonoBehaviour, ISaveable
{
    public string SaveData()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadData(string json)
    {
        if (json == null) return;

        JsonUtility.FromJsonOverwrite(json, this);//Mandatory with Monobehaviour objects
    }
}

[RequireComponent(typeof(SaveableEntity))]
public class MonoBehaviourSaveableSingleton<T> : MonoBehaviourSingleton<T>, ISaveable
        where T : Component

{
    public string SaveData()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadData(string json)
    {
        if (json == null) return;

        JsonUtility.FromJsonOverwrite(json, this);//Mandatory with Monobehaviour objects
    }
}


[RequireComponent(typeof(SaveableEntity))]
public class MonoBehaviourSaveableSingletonPersistent<T> : MonoBehaviourSingletonPersistent<T>, ISaveable
        where T : Component

{
    public string SaveData()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadData(string json)
    {
        if (json == null) return;

        JsonUtility.FromJsonOverwrite(json, this);//Mandatory with Monobehaviour objects
    }
}
