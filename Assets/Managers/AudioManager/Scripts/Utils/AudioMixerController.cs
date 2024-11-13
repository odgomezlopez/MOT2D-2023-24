using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioMixerController
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private const string MasterVolumeKey = "MasterVolume";
    [SerializeField] private const string BackgroundVolumeKey = "BackgroundVolume";
    [SerializeField] private const string SFXVolumeKey = "SFXVolume";

    [SerializeField] public AudioMixerGroup backgroundGroup, sfxGroup;
    public void SetMasterVolume(float value)
    {
        SetAudioMixerLevel(MasterVolumeKey, value);
    }

    public void SetBackgroundVolume(float value)
    {
        SetAudioMixerLevel(BackgroundVolumeKey, value);
    }

    public void SetSFXVolume(float value)
    {
        SetAudioMixerLevel(SFXVolumeKey, value);
    }

    private void SetAudioMixerLevel(string parameter, float value)
    {
        audioMixer?.SetFloat(parameter, LinearToDecibel(value));
    }

    private float LinearToDecibel(float linear)
    {
        return linear > 0.0001f ? 20f * Mathf.Log10(linear) : -80f;
    }
}