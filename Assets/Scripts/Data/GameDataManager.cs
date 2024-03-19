using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameDataManager : MonoBehaviourSingletonPersistent<GameDataManager>
{
    [Header("Save/Load Info")]
    [SerializeField] string fileName = "Data01.dat";
    private string fullFilePath = "";

    [SerializeField] bool loadOnStart = false;
    [SerializeField] bool saveOnAplicationQuit = false;

    [SerializeField] EncriptDecriptStrategy encriptDecriptStrategy;

    [Header("Data")]
    public GameData gameData;

    #region Save/Load
    private void Start() {
        fullFilePath = Path.Combine(Application.persistentDataPath, fileName);
        if (loadOnStart) LoadData(); 
    }
    private void OnApplicationQuit() { if(saveOnAplicationQuit) SaveData(); }

    //Saving Player Data(Serialization)
    public void SaveData()
    {
        //Cargamos info desde los componentes a GameData
        SaveInfoFromComponentsToGameData();

        //Convertirmos los objetos a JSON
        //string text = JsonConvert.SerializeObject(gameData);
        string text = JsonUtility.ToJson(gameData);

        //Encriptamos
        if(encriptDecriptStrategy) text = encriptDecriptStrategy.EncodeString(text);

        //Guardamos
        System.IO.File.WriteAllText(fullFilePath, text);
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
        gameData = JsonUtility.FromJson<GameData>(text);
        //gameData = JsonConvert.DeserializeObject<GameData>(text);

        //Enviamos la info desde GameData a los componentes
        LoadInfoFromGameDataToComponents();
    }

    public void DeleteAllData()
    {
        System.IO.File.Delete(Path.Combine(Application.persistentDataPath, fileName));
    }
    #endregion

    #region Isavable
    public void SaveInfoFromComponentsToGameData()
    {
        var a_Saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        foreach (var saveable in a_Saveables)
        {
            saveable.SaveInfoToGameData(gameData);
        }
    }

    public void LoadInfoFromGameDataToComponents()
    {
        var a_Saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        foreach (var saveable in a_Saveables)
        {
            saveable.LoadInfoFromGameData(gameData);
        }
    }
    #endregion
}

#if UNITY_EDITOR

[CustomEditor(typeof(GameDataManager))]
public class GameDataManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        GameDataManager t = (GameDataManager)target;

        if (GUILayout.Button("Delete All Flags"))
        {
            t.gameData.flags.ClearFlags();
        }

        if (GUILayout.Button("Delete Current Save File"))
        {
            t.DeleteAllData();
        }
    }
}
#endif