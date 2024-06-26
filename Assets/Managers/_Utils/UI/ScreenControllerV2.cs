using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UI;

public class ScreenControllerV2<T> : MonoBehaviourSingleton<T> where T : MonoBehaviourSingleton<T>
{
    //Internal references
    Canvas canvas;
    PlayerInput playerInput;

    [Header("Action map")]
    [SerializeField] string newActionMap;
    string oldActionMap;


    [Header("Actions")]
    [SerializeField] InputActionReference showAction;
    [SerializeField] InputActionReference hideAction;

    [Header("UI references")]
    [SerializeField] Selectable firstSelectObject;

    //Guardamo el fixedDetalTime para poder pausar bien
    private float fixedDeltaTime;
    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    void Start()
    {
        canvas = GetComponent<Canvas>();
        playerInput = FindObjectOfType<PlayerInput>();
        canvas.enabled = false;
    }

    protected virtual void Update()
    {
        if (showAction && showAction.action.triggered)
        {
            ShowScreen();
        }
        if (hideAction && hideAction.action.triggered)
        {
            HideScreen();
        }
    }

    public virtual void ShowScreen()
    {
        canvas.enabled = true;
        oldActionMap = playerInput.currentActionMap.name;
        playerInput.SwitchCurrentActionMap(newActionMap);

        //Pausar
        ChangeTimeScale(0f);
        firstSelectObject?.Select();
    }
    public virtual void HideScreen()
    {
        canvas.enabled = false;
        playerInput.SwitchCurrentActionMap(oldActionMap);

        //Despausar
        ChangeTimeScale(1f);

    }
    private void OnDestroy()
    {
        ChangeTimeScale(1f);
    }

    private void ChangeTimeScale(float newScale)
    {
        Time.timeScale = newScale;
        //Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }
}
