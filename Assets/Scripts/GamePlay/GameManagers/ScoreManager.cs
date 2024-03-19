using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] private GenericVariable<float> score;
    [SerializeField] private float winningScore = 10;
    public UnityEvent OnWinningScore;

    //Inicialización
    private void Start()
    {
        if (score == null) Debug.LogError("ScoreManager should have a FloatVariableSO attached");

        ResetScore();
        score.OnValueUpdate.AddListener(CheckWinningCondition);
    }
    //Actualización del valor
    public void AddPoints(float points)
    {
        score.CurrentValue += points;
        //CheckWinningCondition(score.CurrentValue);
    }
    public void SetScore(float f)
    {
        score.CurrentValue = f;
    }
    public void ResetScore()
    {
        score.CurrentValue=0;
    }
    //Victoria
    public void CheckWinningCondition(float f)
    {
        if(score.CurrentValue >= winningScore)
        {
            OnWinningScore.Invoke();
        }
    }

    public void SaveCurrentScore()
    {
        GameDataManager.Instance?.gameData.AddPoints(score.CurrentValue);
    }
}
