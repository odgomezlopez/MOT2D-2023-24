using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    public void OnButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }
}
