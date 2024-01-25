using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] public Indicator score;
    [SerializeField] float winningScore = 10;

    public UnityEvent OnWinningScore;

    //Inicialización
    private void Start()
    {
        ResetScore();
        score.OnValueChange.AddListener(CheckWinningCondition);
    }

    public void ResetScore()
    {
        score.CurrentValue=0;
    }

    //Actualización del valor
    public void AddPoints(float points)
    {
        score.CurrentValue += points;
    }

    public void CheckWinningCondition(float val)
    {
        if(val>= winningScore)
        {
            OnWinningScore.Invoke();
        }
    }
}
