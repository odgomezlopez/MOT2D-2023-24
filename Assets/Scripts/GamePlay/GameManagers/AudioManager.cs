using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [Header("Volumen")]
    [SerializeField][Range(0, 1)] private float masterVolume = 1;
    [SerializeField][Range(0, 1)] private float backgroundVolume = 1;
    [SerializeField][Range(0, 1)] private float sfxVolume = 1;

    //Audios generales
    [Header("Audios generales")]
    //Default background sound
    [SerializeField] private AudioClip backgroundClip;

    [SerializeField] [SerializedDictionary("Key","Sound")] 
    SerializedDictionary<string, AudioClip> audioBackgroundDictionary, audioSFXDictionary;
    //Referencias a subcomponentes
    AudioSource backgroundAudioSource;

    [Header("Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerGroup backGroup, SFXGroup;



    #region Guardado configuracion en PlayerPrefs
    private void Awake()
    {
        masterVolume = PlayerPrefs.GetFloat("masterVolume", 1);
        backgroundVolume= PlayerPrefs.GetFloat("backgroundVolume", 1);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1);
        OnValidate();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("backgroundVolume", backgroundVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }
    #endregion





    #region background
    void Start()
    {
        //Referencias a fuentes de audio estaticas (solo hijos)
        backgroundAudioSource = GetComponentInChildren<AudioSource>();
        
        //Comprobamos que los sonidos estan bien
        OnValidate();

        //Inicio el sonido por defecto
        PlayBackGroundSound(backgroundClip);
        //AudioManager.Instance.PlayBackGroundSound(backgroundClip);
    }

    public void PlayBackGroundSound(AudioClip newClip) {
        if (backGroup) backgroundAudioSource.outputAudioMixerGroup = backGroup;

        backgroundAudioSource.clip = newClip;
        backgroundAudioSource.Play();
    }

    public void PlayBackGroundSound(string key)
    {
        if (audioBackgroundDictionary.ContainsKey(key))
            PlayBackGroundSound(audioBackgroundDictionary[key]);
        else
            Debug.LogError($"The key {key} does not exist on the audioBackgroundDictionary");
    }
    #endregion

    #region SFX
    public void PlaySFX(AudioClip newClip)
    {
        PlaySFX(newClip, Camera.main.transform.position);
    }

    public void PlaySFX(AudioClip newClip, Vector3 pos)
    {
        //AudioSource.PlayClipAtPoint(newClip, pos, SFXVolume);
        PlaySoundAtPoint(newClip, pos, SFXGroup, sfxVolume);
    }

    public void PlaySFX(string key)
    {
        if (audioSFXDictionary.ContainsKey(key))
            PlaySFX(audioSFXDictionary[key]);
        else
            Debug.LogError($"The key {key} does not exist on the audioSFXDictionary");
    }
    #endregion


    public void PlaySoundAtPoint(AudioClip clip, Vector3 position, AudioMixerGroup mixerGroup, float volume = 1.0f, bool loop = false)
    {
        // Create a new GameObject
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = position;

        // Add an AudioSource
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.volume = volume;
        aSource.loop = loop;

        // Assign the AudioMixerGroup to the AudioSource
        aSource.outputAudioMixerGroup = mixerGroup;

        // Play the clip
        aSource.Play();

        // Destroy the GameObject after the clip has finished
        if(!loop )Destroy(tempGO, clip.length);
    }



    //Utilidades
    private void OnValidate()
    {
        if(backgroundAudioSource) backgroundAudioSource.volume = backgroundVolume;

        if (audioMixer)
        {
            audioMixer?.SetFloat("MasterVolume",LinearToDecibel(masterVolume));
            audioMixer?.SetFloat("BackgroundVolume",LinearToDecibel(backgroundVolume));
            audioMixer?.SetFloat("SFXVolume", LinearToDecibel(sfxVolume));
        }
    }


    #region utilidades audio

    public static float LinearToDecibel(float linear)
    {
        // Avoid taking a log of zero by ensuring the linear value is within a valid range
        if (linear <= 0.0001f)
            return -80f; // Return -80dB when linear is 0, representing silence
        else if (linear >= 0.95f && linear <= 1f)
        {
            // Map the range 0.9-1 linearly to 0-20 dB
            return (linear - 0.95f) * 400f; // (1 - 0.9) -> 0.1 maps to 20 dB, thus 200f is the scaling factor
        }
        else
            return 20.0f * Mathf.Log10(linear); // Convert linear scale to decibels for other values

    }
    #endregion
}
