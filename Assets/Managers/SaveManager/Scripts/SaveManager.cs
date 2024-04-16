using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviourSingletonPersistent<SaveManager> //Si no es persistant tiene algunos problemas de guardado. Investigar
{
    [Header("Save/Load Info")]
    [SerializeField] string fileName = "Data01.dat";
    private string fullFilePath = "";

    [SerializeField] bool loadOnStart = false;
    [SerializeField] bool saveOnAplicationQuit = false;

    [Header("Strategies")]
    [SerializeField] EncriptDecriptStrategy encriptDecriptStrategy;

    //Save Data
    [SerializeField] public SavedComponents entities;


    #region Save/Load
    private void OnEnable() {
        fullFilePath = Path.Combine(Application.persistentDataPath, fileName);
        if (loadOnStart) LoadData(); 
    }

    private void OnApplicationQuit() { if(saveOnAplicationQuit) SaveData(); }

    //Saving Player Data(Serialization)

    public bool HasSaveData()
    {
        return System.IO.File.Exists(fullFilePath);
    }
    public void SaveData()
    {
        //Recopilamos la info de los componentes
        SaveInfoFromComponentsToGameData();

        //Convertirmos los objetos a JSON
        //return JsonConvert.SerializeObject(o);
        string text = JsonUtility.ToJson(entities);

        //Encriptamos
        if (encriptDecriptStrategy) text = encriptDecriptStrategy.EncodeString(text);

        //Guardamos. Se puede extender y aqui utilizar alguna API REST. https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.html
        System.IO.File.WriteAllText(fullFilePath, text);
        Debug.Log($"Save data to {fileName}");
     }

     //Loading Player Data (Deserialization)
     public void LoadData()
     {
        //Comprobamos si el archivo de guardado existe
        if (!System.IO.File.Exists(fullFilePath)) return;

        //Leemos
        string text = System.IO.File.ReadAllText(fullFilePath);

        //Desencriptamos
        if (encriptDecriptStrategy) text = encriptDecriptStrategy.DecodeString(text);

        //Convertimos el JSON a Objetos
        entities = JsonUtility.FromJson<SavedComponents>(text);
        //entities = JsonConvert.DeserializeObject<SavedComponents>(text);

        //Enviamos la info a los componentes
        LoadInfoFromGameDataToComponents();
        Debug.Log($"Load data from {fileName}");

    }

    public void DeleteData()
    {
        System.IO.File.Delete(Path.Combine(Application.persistentDataPath, fileName));
    }
    #endregion

    #region recogida de ISaveables
    public void SaveInfoFromComponentsToGameData()
    {
        var a_Saveables = FindObjectsOfType<SaveableEntity>(true);//.OfType<ISaveable>(); Plantearse poner true dentro para que pille a desactivados?

        foreach (var saveable in a_Saveables)
        {
            saveable.SaveData();
        }
    }

    public void LoadInfoFromGameDataToComponents()
    {
        var a_Saveables = FindObjectsOfType<SaveableEntity>(false);

        foreach (var saveable in a_Saveables)
        {
            saveable.LoadData();
        }
    }
    #endregion
}

#if UNITY_EDITOR

[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        SaveManager t = (SaveManager)target;

        if (GUILayout.Button("Load Current Save File (running)"))
        {
            t.LoadData();
        }
        if (GUILayout.Button("Save Current Save File (running)"))
        {
            t.SaveData();
        }
        if (GUILayout.Button("Delete Current Save File"))
        {
            t.DeleteData();
        }
    }
}
#endif