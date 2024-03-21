using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameDataExtended : GameData
{
    //Utilidades extras de guardado
    [SerializeField] public Flags flags;
    [SerializeField] public SaveableEntityStorage entities;
}

public class GameDataManager : MonoBehaviourSingletonPersistent<GameDataManager>
{
    [Header("Save/Load Info")]
    [SerializeField] string fileName = "Data01.dat";
    private string fullFilePath = "";

    [SerializeField] bool loadOnStart = false;
    [SerializeField] bool saveOnAplicationQuit = false;

    [SerializeField] EncriptDecriptStrategy encriptDecriptStrategy;

    [Header("Data")]
    public GameDataExtended gameData;
    public Flags flags => gameData?.flags;
    public SaveableEntityStorage entities => gameData?.entities;


    #region Save/Load
    private void Start() {
        fullFilePath = Path.Combine(Application.persistentDataPath, fileName);
        if (loadOnStart) LoadData(); 
    }
    private void OnApplicationQuit() { if(saveOnAplicationQuit) SaveData(); }

    //Saving Player Data(Serialization)
    public void SaveData()
    {
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
        gameData = JsonUtility.FromJson<GameDataExtended>(text);
        //gameData = JsonConvert.DeserializeObject<GameDataExtended>(text);
    }

    public void DeleteData()
    {
        System.IO.File.Delete(Path.Combine(Application.persistentDataPath, fileName));
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

        /*if (GUILayout.Button("Delete All Flags (on editor runtime)"))
        {
            t.flags.ClearFlags();
        }

        if (GUILayout.Button("Delete All Objects  (on editor runtime)"))
        {
            t.entities.ClearObjects();
        }*/

        if (GUILayout.Button("Delete Current Save File"))
        {
            t.DeleteData();
        }
    }
}
#endif