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

    //Si se hace con JSON, ten en cuenta que tienes que guardar partida antes de salir y no debes cargar antes de iniciar nivel. Sino se pierde
    public void AddDeath() { gameData.numberOfDeaths++; } 
    public void UpdateCurrentScene() { gameData.currentPlayableScene = SceneManager.GetActiveScene().name; }
}
