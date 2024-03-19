using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameDataManager : MonoBehaviourSingletonPersistent<GameDataManager>
{
    [Header("Save/Load Info")]
    [SerializeField] string fileName = "Data01.json";
    [SerializeField] bool loadOnStart = false;
    [SerializeField] bool saveOnAplicationQuit = false;

    [Header("Data")]
    public GameData gameData;

    #region Save/Load
    private void Start() { if(loadOnStart) LoadData(); }
    private void OnApplicationQuit() { if(saveOnAplicationQuit) SaveData(); }

    //Saving Player Data(Serialization)
    public void SaveData()
    {
        //string json = JsonConvert.SerializeObject(gameData);
        string json = JsonUtility.ToJson(gameData);
        System.IO.File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), json);
     }

     //Loading Player Data (Deserialization)
     public void LoadData()
     {
        string json = System.IO.File.ReadAllText(Path.Combine(Application.persistentDataPath, fileName));
        //gameData = JsonConvert.DeserializeObject<GameData>(json);
        gameData = JsonUtility.FromJson<GameData>(json);

    }

    public void DeleteAllData()
    {
        System.IO.File.Delete(Path.Combine(Application.persistentDataPath, fileName));
    }
    #endregion


    /*private void LoadData()
    {
        FindFirstObjectByType<SavingSystem>().Load(fileName);
    }
    private void SaveData()
    {
        FindFirstObjectByType<SavingSystem>().Save(fileName);
    }

    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(gameData);
    }

    public void RestoreFromJToken(JToken state)
    {
        gameData = state.ToObject<GameData>();
    }
    #endregion*/
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