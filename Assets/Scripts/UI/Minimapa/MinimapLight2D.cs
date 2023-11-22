using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEditor.Experimental.GraphView;

public class MinimapLight2D : MonoBehaviour
{
    //Luminosidad
    private Light2D globalLight;
    [SerializeField] private float miniMapLight=1f;
    private float lightIntesity;

    void Start()
    {
        //Busco la luz global de la escena
        Light2D[] luces= GameObject.FindObjectsOfType<Light2D>();
        for(int i = 0; i < luces.Length; i++)
        {
            if (luces[i].lightType == Light2D.LightType.Global)
            {
                globalLight= luces[i];
                break;
            }
        }

        if (globalLight)
        {
            RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
            RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
        }
        else { 
            Debug.LogError("MiniMap: Global Light not assigned!");
        }
    }

    void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera.name == gameObject.GetComponent<Camera>().name)
        {

            lightIntesity = globalLight.intensity;
            globalLight.intensity = miniMapLight;
        }
    }

    void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera.name == gameObject.GetComponent<Camera>().name)
        {
            globalLight.intensity = lightIntesity;
        }
    }

    void OnDestroy()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }
}
