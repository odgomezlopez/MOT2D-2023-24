using AYellowpaper.SerializedCollections;
using UnityEngine;

[System.Serializable]
public class AudioClipManager
{
    [SerializeField] private AudioClipSO defaultBackgroundClip;
    [SerializeField] private SerializedDictionary<string, AudioClipSO> backgroundClips, sfxClips;

    public AudioClipSO DefaultBackgroundClip => defaultBackgroundClip;

    public AudioClipSO GetBackgroundClip(string key)
    {
        return backgroundClips.TryGetValue(key, out var clip) ? clip : null;
    }

    public AudioClipSO GetSFXClip(string key)
    {
        return sfxClips.TryGetValue(key, out var clip) ? clip : null;
    }
}