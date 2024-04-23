using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverScreen : ScreenControllerV2<GameOverScreen>
{
    public override void HideScreen()
    {
        LevelManager.Instance.RestartLevel();
        //base.HideScreen();
    }


    public override void ShowScreen()
    {
        base.ShowScreen();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
