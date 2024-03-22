using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class TestComponent : MonoBehaviour, ISaveable //Alternativamente heredar de MonoBehaviourSaveable
{ 
    [SerializeField] float numPressSpace = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numPressSpace++;
        }
    }

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
