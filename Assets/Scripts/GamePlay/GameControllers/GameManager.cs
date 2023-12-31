using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public void LevelRestart(float seconds=0)
    {
        StartCoroutine(LevelRestartCorutine(seconds));
    }

    private IEnumerator LevelRestartCorutine(float seconds = 0)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
