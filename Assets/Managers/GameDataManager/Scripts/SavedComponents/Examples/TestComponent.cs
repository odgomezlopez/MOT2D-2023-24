using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class TestComponent : MonoBehaviour, ISaveable 
//Alternativamente, al ser un caso donde se guarda todo el componente, se puede heredar de la clase de ayuda MonoBehaviourSaveable.
//En este caso hay que borrar los metodos de SaveData() y LoadData(string json). 
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
