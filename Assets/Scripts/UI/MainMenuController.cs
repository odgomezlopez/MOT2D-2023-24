using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //Configuración
    [Header("Config")]
    [SerializeField] string newGameScene = "Level1";

    //Referencias menu inicial
    [Header("Menu")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] Button menuInitButton;
    [SerializeField] Button continueButton;

    //Referencias menu creditos
    [Header("Credits")]
    [SerializeField] GameObject creditPanel;
    [SerializeField] Button creditInitButton;

    [Header("Prefences")]
    [SerializeField] GameObject preferencePanel;
    [SerializeField] Selectable preferenceInit;

    private void Start()
    {
        if (SaveManager.Instance.HasSaveData())
        {
            continueButton.interactable = true;
            continueButton.Select();
        }
    }
    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameScene);
    }
    public void ContinueButton()
    {
        //1. Cargo partida
        SaveManager.Instance.LoadData();

        //2.Cambio de escena
        SceneManager.LoadScene(GameDataManager.Instance.gameData.currentPlayableScene);
    }
    public void CreditButton()
    {
        //Cambiamos los paneles
        menuPanel.SetActive(false);
        creditPanel.SetActive(true);
        //Seleccionar el botón activo del panel
        creditInitButton.Select();
    }
    public void PreferenceButton()
    {
        //Cambiamos los paneles
        menuPanel.SetActive(false);
        preferencePanel.SetActive(true);

        //Seleccionar el botón activo del panel
        preferenceInit.Select();
    }


    public void Back()
    {
        //Cambiamos los paneles
        creditPanel.SetActive(false);
        preferencePanel.SetActive(false);

        menuPanel.SetActive(true);

        //Seleccionar el botón activo del panel
        if(continueButton.interactable) continueButton.Select();
        else menuInitButton.Select();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
