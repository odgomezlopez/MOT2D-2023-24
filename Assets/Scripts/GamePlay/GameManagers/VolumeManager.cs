using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    Volume volume;
    Vignette vignetteFilter;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignetteFilter);
    }
    public void EnableVignette(float second)
    {
        vignetteFilter.active=true;
        StartCoroutine(ActivateFilter(vignetteFilter.intensity,0f,1f,second));
        //StartCoroutine(ActivateVignetteFilter(second));
    }


    private IEnumerator ActivateFilter(ClampedFloatParameter volumeFloat, float initValue, float endValue, float seconds)
    {
        //Inicializaciones
        volumeFloat.Override(initValue);

        //Valores de control
        float elapsedTime = 0f;

        //Bucle
        while (elapsedTime < seconds)
        {
            float t = elapsedTime / seconds;
            
            vignetteFilter.intensity.Override(Mathf.Lerp(initValue, endValue, t));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Comprobación de valor final
        vignetteFilter.intensity.Override(endValue);
    }

    public void DisableVignette()
    {
        vignetteFilter.active = false;
        vignetteFilter.intensity.Override(0f);
    }
}
