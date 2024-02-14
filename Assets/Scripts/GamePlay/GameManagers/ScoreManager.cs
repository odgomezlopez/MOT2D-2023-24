using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] private FloatVariableSO score;
    [SerializeField] private float winningScore = 10;

    public UnityEvent OnWinningScore;


    //Inicialización
    private void Start()
    {
        if (score == null) Debug.LogError("ScoreManager should have a FloatVariableSO attached");

        ResetScore();
        score.OnValueUpdate.AddListener(CheckWinningCondition);//Alternativa con eventos para comprobar si se ha ganado, si se usa comentar la linea 30
    }

    public void ResetScore()
    {
        score.CurrentValue=0;
    }

    //Actualización del valor
    public void AddPoints(float points)
    {
        score.CurrentValue += points;
        //CheckWinningCondition(score.CurrentValue);
    }

    public void CheckWinningCondition(float f) => CheckWinningCondition();
    public void CheckWinningCondition()
    {
        if(score.CurrentValue >= winningScore)
        {
            OnWinningScore.Invoke();
        }
    }
}
