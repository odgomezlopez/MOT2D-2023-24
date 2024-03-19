using Newtonsoft.Json.Linq;
using UnityEngine;


[RequireComponent(typeof(SaveableEntity))]
public class GameDataManager : MonoBehaviourSingletonPersistent<GameDataManager>, ISaveable
{
    public GameData gameData;

    #region Save/Load
    string fileName = "Data01.json";

    private void Start() { LoadData(); }
    private void OnDestroy() { SaveData(); }

    //Saving Player Data(Serialization)
    /* public void SaveData()
     {
         string json = JsonUtility.ToJson(this);
         System.IO.File.WriteAllText(Application.persistentDataPath + fileName, json);
     }

     //Loading Player Data (Deserialization)
     public void LoadData()
     {
         string json = System.IO.File.ReadAllText(Application.persistentDataPath + fileName);

         gameData = JsonUtility.FromJson<GameData>(json);
     }*/

    private void LoadData()
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
    #endregion
}
