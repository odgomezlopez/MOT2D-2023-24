using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] private GenericVariable<float> currentSceneScore;
    [SerializeField] private float accumulatedScore = 0;

    [SerializeField] private bool resetOnNewScene = false;
    [SerializeField] private float sceneWinningScore = 0;

    public UnityEvent OnWinningScore;

    //Inicialización
    private void Start()
    {
        if (currentSceneScore == null) Debug.LogError("ScoreManager should have a FloatVariableSO attached");

        ResetScore();
        currentSceneScore.OnValueUpdate.AddListener(CheckWinningCondition);
    }
    //Actualización del valor
    public void AddPoints(float points)
    {
        currentSceneScore.CurrentValue += points;
        //CheckWinningCondition(score.CurrentValue);
    }
    public void SetScore(float f)
    {
        currentSceneScore.CurrentValue = f;
    }
    public void ResetScore()
    {
        currentSceneScore.CurrentValue=0;
    }
    //Victoria
    public void CheckWinningCondition(float f)
    {
        if(currentSceneScore.CurrentValue >= sceneWinningScore)
        {
            OnWinningScore.Invoke();
        }
    }

    #region Score acumulado
    public void ChangeSceneHandle(Scene s, LoadSceneMode l)
    {
        if(l == LoadSceneMode.Single)
        {
            //Guardo los puntos acumulados
            accumulatedScore += currentSceneScore.CurrentValue;

            //Compruebo si hay que reiniciar
            if(resetOnNewScene) currentSceneScore.Restart();

            //Invoco el evento de actualización de UI
            currentSceneScore.OnValueUpdate.Invoke(currentSceneScore.CurrentValue);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeSceneHandle;   
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ChangeSceneHandle;

    }

    #endregion
}
