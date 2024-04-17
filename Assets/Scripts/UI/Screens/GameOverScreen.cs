using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverScreen : ScreenController<GameOverScreen>
{
    public override void HideScreen()
    {
        LevelManager.Instance.RestartLevel();
        //base.HideScreen();
    }

}
