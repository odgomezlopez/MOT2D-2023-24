using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public string currentPlayableScene;
    public int numberOfDeaths;

    public GameData(string currentPlayableScene, int numberOfDeaths)
    {
        this.currentPlayableScene = currentPlayableScene;
        this.numberOfDeaths = numberOfDeaths;
    }
}


public class GameDataManager : MonoBehaviourSaveableSingleton<GameDataManager>
{
    public GameData gameData;
    public void AddDeath() { gameData.numberOfDeaths++; }
    public void UpdateCurrentScene() { gameData.currentPlayableScene = SceneManager.GetActiveScene().name; }

    public void LossProgresAndAddDeath()
    {
        SaveManager.Instance.LoadData();
        AddDeath();
        SaveManager.Instance.SaveData();
    }

    public void UpdateSceneAndSave() {
        UpdateCurrentScene();    
        SaveManager.Instance.SaveData(); 
    }

}
