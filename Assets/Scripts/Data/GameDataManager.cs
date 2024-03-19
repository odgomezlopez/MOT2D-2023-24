using UnityEngine;

public class GameDataManager : MonoBehaviourSingletonPersistent<GameDataManager>
{
    public GameData gameData;

    #region Save/Load
    string fileName = "Data01.json";

    //Saving Player Data(Serialization)
    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Application.persistentDataPath + fileName, json);
    }

    //Loading Player Data (Deserialization)
    public void LoadData()
    {
        string json = System.IO.File.ReadAllText(Application.persistentDataPath + fileName);

        gameData = JsonUtility.FromJson<GameData>(json);
    }
    #endregion
}
