using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    //Propiedades
    [SerializeField][Range(0, 1)] float backgroundSoundVolume = 1;
    [SerializeField][Range(0,1)] float SFXVolume = 1;
    [SerializeField][Range(0, 1)] float dialogueSoundVolume = 1;

    //Referencias a subcomponentes
    AudioSource backgroundAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Referencias a fuentes de audio estaticas (solo hijos)
        backgroundAudioSource=gameObject.transform.Find("BackgroundSound").gameObject.GetComponent<AudioSource>();
        backgroundAudioSource.volume = backgroundSoundVolume;
        
    }

    public void PlayBackGroundSound(AudioClip newClip) {
        backgroundAudioSource.clip = newClip;
        backgroundAudioSource.Play();
    }

    public void PlaySFX(AudioClip newClip)
    {
        AudioSource.PlayClipAtPoint(newClip, Camera.main.transform.position, SFXVolume);//Instaciamos uno nuevo en la posición de la camara
    }

    public void PlaySFX(AudioClip newClip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(newClip, pos, SFXVolume);
    }
}
