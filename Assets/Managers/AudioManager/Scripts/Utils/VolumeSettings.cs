using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class VolumeControl
{
    //Variables
    [SerializeField, Range(0, 1)] private float volume = 1f; // Default value of 1
    [SerializeField] private string parameterName = "MasterVolume"; // Default parameter name
    [SerializeField] private AudioMixerGroup group;

    //Getter/Setters
    public AudioMixerGroup Group => group;
    public string ParameterName => parameterName;

    public float Volume
    {
        get => volume;
        set => SetVolume(value);
    }

    private void SetVolume(float value)
    {
        //Set volume
        volume = Mathf.Clamp01(value); // Clamp volume between 0 and 1

        //Apply to the mixer if attached
        if (group == null || group.audioMixer == null || string.IsNullOrEmpty(parameterName))
        {
            Debug.LogWarning("VolumeSet: Invalid AudioMixerGroup or parameter name.");
            return;
        }

        float decibelValue = LinearToDecibel(volume);
        group.audioMixer.SetFloat(parameterName, decibelValue);
    }

    private float LinearToDecibel(float linear, float minDecibels = -80f)
    {
        return linear > 0.0001f ? 20f * Mathf.Log10(linear) : minDecibels; // Prevent log(0) issues
    }
}


[CreateAssetMenu(fileName = "VolumeSettings", menuName = "Settings/VolumeSettings", order = 1)]
public class VolumeSettings : ScriptableObject
{
    [SerializeField] public VolumeControl master;
    [SerializeField] public VolumeControl background;
    [SerializeField] public VolumeControl sfx;

    // Load saved settings from PlayerPrefs
    public void LoadVolumeSettings()
    {
        master.Volume = PlayerPrefs.GetFloat(master.ParameterName, 1f);
        background.Volume = PlayerPrefs.GetFloat(background.ParameterName, 1f);
        sfx.Volume = PlayerPrefs.GetFloat(sfx.ParameterName, 1f);
    }

    // Save settings to PlayerPrefs
    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(master.ParameterName, master.Volume);
        PlayerPrefs.SetFloat(background.ParameterName, background.Volume);
        PlayerPrefs.SetFloat(sfx.ParameterName, sfx.Volume);
    }

    public void ResetVolumeSettings()
    {
        master.Volume = 1f;
        background.Volume = 1f;
        sfx.Volume = 1f;

        SaveVolumeSettings();
    }

    private void OnValidate()
    {
        master.Volume = master.Volume;
        sfx.Volume = sfx.Volume;
        background.Volume = background.Volume;
        SaveVolumeSettings();
    }
}
