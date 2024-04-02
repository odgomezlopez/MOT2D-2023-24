using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public string currentPlayableScene;
    public int numberOfDeaths;

    /*public GameData(string currentPlayableScene, int numberOfDeaths)
    {
        this.currentPlayableScene = currentPlayableScene;
        this.numberOfDeaths = numberOfDeaths;
    }*/
}


public class GameDataManager : MonoBehaviourSaveableSingleton<GameDataManager>
{
    public GameData gameData;
    public void AddDeath() { gameData.numberOfDeaths++; }
    public void UpdateCurrentScene() { gameData.currentPlayableScene = SceneManager.GetActiveScene().name; }

    private void Awake()
    {
        gameData.numberOfDeaths = PlayerPrefs.GetInt("playerDeaths",0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("playerDeaths", gameData.numberOfDeaths);
    }

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
