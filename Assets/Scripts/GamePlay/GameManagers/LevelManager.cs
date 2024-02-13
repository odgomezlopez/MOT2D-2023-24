using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [SerializeField] private string nextSceneName;
    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StartTheGame()
    {
        GoToLevel("Level1");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
