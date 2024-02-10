using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    public UnityEvent onWin;
    public UnityEvent onGameOver;

    public void LevelWin(float seconds = 0)
    {
        onWin.Invoke();

        StartCoroutine(
            DelayedAction(LevelManager.Instance.NextLevel, seconds)
        );
    }

    public void LevelGameOver(float seconds = 0)
    {
        onGameOver.Invoke();

        StartCoroutine(
            DelayedAction(LevelManager.Instance.RestartLevel, seconds)
        );
    }

    private IEnumerator DelayedAction(System.Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
}
