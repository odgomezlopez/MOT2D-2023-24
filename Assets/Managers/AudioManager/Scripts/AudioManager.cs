using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private VolumeSettings volumeSettings;
    [SerializeField] private AudioMixerController audioMixerController;

    private AudioSource backgroundAudioSource;

    private void Awake()
    {
        volumeSettings.Initialize(audioMixerController);
        backgroundAudioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        //if (audioClipManager.DefaultBackgroundClip)
        //    PlayBackgroundSound(audioClipManager.DefaultBackgroundClip);
    }

    private void OnDestroy()
    {
        volumeSettings.SaveVolumeSettings();
    }

    #region Metodos de uso simple
    public void PlayBackgroundSound(AudioClip clip, float volume = -1, float pitch = 1)
    {
        if (clip == null) return;
        backgroundAudioSource.clip = clip;
        backgroundAudioSource.outputAudioMixerGroup = audioMixerController.backgroundGroup;
        backgroundAudioSource.volume = (volume == -1) ? volumeSettings.BackgroundVolume : volume;
        backgroundAudioSource.pitch = pitch;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.Play();
    }


    public void PlayBackgroundSound(AudioClipSO clipSO)
    {
        if (clipSO == null || clipSO.GetRandomClip() == null) return;

        PlayBackgroundSound(clipSO.GetRandomClip(), clipSO.GetAdjustedVolume() * volumeSettings.BackgroundVolume, clipSO.GetAdjustedPitch());
    }


    public void PlaySFX(AudioClip clip, Vector3 position = default)
    {
        if (clip == null) return;

        PlaySoundAtPoint(
            clip,
            position == default ? Camera.main.transform.position : position,
            audioMixerController.sfxGroup,
            volumeSettings.SFXVolume,
            1
        );
    }


    public void PlaySFX(AudioClipSO clipSO, Vector3 position = default)
    {
        if (clipSO == null || clipSO.GetRandomClip() == null) return;

        PlaySoundAtPoint(
            clipSO.GetRandomClip(),
            position == default ? Camera.main.transform.position : position,
            audioMixerController.sfxGroup,
            clipSO.GetAdjustedVolume() * volumeSettings.SFXVolume,
            clipSO.GetAdjustedPitch()
        );
    }
    #endregion

    #region Metodo generico para hacer sonar mis sonidos
    private void PlaySoundAtPoint(AudioClip clip, Vector3 position, AudioMixerGroup mixerGroup, float volume, float pitch)
    {
        if (clip == null) return;

        var tempGO = new GameObject("TempAudio") { transform = { position = position } };
        var audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = mixerGroup;
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
