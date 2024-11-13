using UnityEngine;

[System.Serializable]
public class VolumeSettings
{
    private AudioMixerController audioMixerController;

    [SerializeField][Range(0, 1)]
    private float masterVolume = 1;

    [SerializeField] [Range(0, 1)]
    private float backgroundVolume = 1;

    [SerializeField] [Range(0, 1)]
    private float sfxVolume = 1;

    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = value;
            audioMixerController?.SetMasterVolume(masterVolume);
        }
    }

    public float BackgroundVolume
    {
        get => backgroundVolume;
        set
        {
            backgroundVolume = value;
            audioMixerController?.SetBackgroundVolume(backgroundVolume);
        }
    }

    public float SFXVolume
    {
        get => sfxVolume;
        set
        {
            sfxVolume = value;
            audioMixerController?.SetSFXVolume(sfxVolume);
        }
    }

    public void Initialize(AudioMixerController mixerController)
    {
        audioMixerController = mixerController;
        LoadVolumeSettings();
        ApplyToMixer();
    }

    public void LoadVolumeSettings()
    {
        MasterVolume = PlayerPrefs.GetFloat("masterVolume", 1);
        BackgroundVolume = PlayerPrefs.GetFloat("backgroundVolume", 1);
        SFXVolume = PlayerPrefs.GetFloat("sfxVolume", 1);
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("masterVolume", MasterVolume);
        PlayerPrefs.SetFloat("backgroundVolume", BackgroundVolume);
        PlayerPrefs.SetFloat("sfxVolume", SFXVolume);
    }

    private void ApplyToMixer()
    {
        audioMixerController?.SetMasterVolume(MasterVolume);
        audioMixerController?.SetBackgroundVolume(BackgroundVolume);
        audioMixerController?.SetSFXVolume(SFXVolume);
    }
}