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


public class GameDataManager : MonoBehaviourSingleton<GameDataManager>
{
    public GameData gameData;

    public void AddDeath() { gameData.numberOfDeaths++; }
    public void UpdateCurrentScene() { gameData.currentPlayableScene = SceneManager.GetActiveScene().name; }
}
