using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private VolumeSettings volumeSettings;

    private AudioSource backgroundAudioSource;


    private void Start()
    {
        if (!volumeSettings) Debug.LogError("VolumeSettings scriptable objects not attacked");

        volumeSettings.LoadVolumeSettings();
        backgroundAudioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnDestroy()
    {
        volumeSettings?.SaveVolumeSettings();
    }

    #region Metodos de uso simple
    public void PlayBackgroundSound(AudioClip clip, float volume = -1, float pitch = 1)
    {
        if (clip == null) return;
        backgroundAudioSource.clip = clip;
        backgroundAudioSource.outputAudioMixerGroup = volumeSettings.background.Group ?? default;
        backgroundAudioSource.volume = (volume == -1) ? volumeSettings.background.Volume : volume;
        backgroundAudioSource.pitch = pitch;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.Play();
    }


    public void PlayBackgroundSound(AudioClipSO clipSO)
    {
        if (clipSO == null || clipSO.GetRandomClip() == null) return;

        PlayBackgroundSound(clipSO.GetRandomClip(), clipSO.GetAdjustedVolume() * volumeSettings.background.Volume, clipSO.GetAdjustedPitch());
    }


    public void PlaySFX(AudioClip clip, Vector3 position = default)
    {
        if (clip == null) return;

        PlaySoundAtPoint(
            clip,
            position == default ? Camera.main.transform.position : position,
            volumeSettings.sfx.Volume,
            1,
            volumeSettings.sfx.Group
        );
    }


    public void PlaySFX(AudioClipSO clipSO, Vector3 position = default)
    {
        if (clipSO == null || clipSO.GetRandomClip() == null) return;

        PlaySoundAtPoint(
            clipSO.GetRandomClip(),
            position == default ? Camera.main.transform.position : position,
            clipSO.GetAdjustedVolume() * volumeSettings.sfx.Volume,
            clipSO.GetAdjustedPitch(),
            volumeSettings.sfx.Group
        );
    }
    #endregion

    #region Metodo generico para hacer sonar mis sonidos
    public static void PlaySoundAtPoint(AudioClip clip, Vector3 position, float volume = 1, float pitch = 1, AudioMixerGroup mixerGroup = null)
    {
        if (clip == null) return;

        var tempGO = new GameObject("TempAudio") { transform = { position = position } };
        var audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        if(mixerGroup) audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        Destroy(tempGO, clip.length / Mathf.Abs(pitch));
    }
    #endregion



    /*#region Uso del AudioClipManager
    [SerializeField] private AudioClipManager audioClipManager;

    public void PlayBackgroundSound(string key)
    {
        PlayBackgroundSound(audioClipManager.GetBackgroundClip(key));
    }


    public void PlaySFX(string key)
    {
        PlaySFX(audioClipManager.GetSFXClip(key));
    }
    #endregion
    */

}
