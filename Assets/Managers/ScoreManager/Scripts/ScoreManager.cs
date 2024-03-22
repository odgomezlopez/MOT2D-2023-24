using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] private GenericVariable<float> currentSceneScore;
    [SerializeField] private bool resetOnNewScene = false;

    //Inicialización
    private void Start()
    {
        ResetScore();
    }
    //Actualización del valor
    public void AddPoints(float points)
    {
        currentSceneScore.CurrentValue += points;
    }
    public void SetScore(float f)
    {
        currentSceneScore.CurrentValue = f;
    }
    public void ResetScore()
    {
        currentSceneScore.CurrentValue=0;
    }

    #region Detectar cambio de escena
    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeSceneHandle;   
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ChangeSceneHandle;

    }

    public void ChangeSceneHandle(Scene s, LoadSceneMode l)
    {
        if (l == LoadSceneMode.Single)
        {
            //Compruebo si hay que reiniciar
            if (resetOnNewScene) currentSceneScore.Restart();

            //Invoco el evento de actualización de UI
            currentSceneScore.OnValueUpdate.Invoke(currentSceneScore.CurrentValue);
        }
    }

    #endregion

    //Victoria
    /*
    [SerializeField] private float sceneWinningScore = 0;
    public UnityEvent OnWinningScore;

    private void Awake(){currentSceneScore.OnValueUpdate.AddListener(CheckWinningCondition);}

    public void CheckWinningCondition(float f)
    {
        if(currentSceneScore.CurrentValue >= sceneWinningScore)
        {
            OnWinningScore.Invoke();
        }
    }*/

}
