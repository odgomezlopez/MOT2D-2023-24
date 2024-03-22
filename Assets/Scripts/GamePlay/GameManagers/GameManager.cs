using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public UnityEvent onInit;
    public UnityEvent onWin;
    public UnityEvent onGameOver;

    private void Start()
    {
        try
        {
            Debug.Log("InitEvent");
            onInit?.Invoke();
        }
        catch { Debug.LogError("A function attached to OnInit event has failed"); }
    }

    public void LevelWin(float seconds = 0)
    {
        //if(ScoreManager.Instance) ScoreManager.Instance.SaveCurrentScore();

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

