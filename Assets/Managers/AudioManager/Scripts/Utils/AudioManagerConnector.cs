using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerConnector : MonoBehaviour
{
    public void PlayBackGroundSound(AudioClipSO newClip) => AudioManager.Instance?.PlayBackgroundSound(newClip);
    public void PlayBackGroundSound(string key) => AudioManager.Instance?.PlayBackgroundSound(key);

    public void PlaySFX(AudioClipSO newClip) => AudioManager.Instance?.PlaySFX(newClip);
    public void PlaySFX(AudioClipSO newClip, Vector3 pos) => AudioManager.Instance?.PlaySFX(newClip, pos);

    public void PlaySFX(string key) => AudioManager.Instance?.PlaySFX(key);
}
