using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event/Audio")]
public class GameEventAudio : ScriptableObject
{
    List<GameEventAudioListener> listeners = new List<GameEventAudioListener>();

    public void TriggerEvent(AudioClip val)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventTriggered(val);
    }

    public void AddListener(GameEventAudioListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventAudioListener listener)
    {
        listeners.Remove(listener);
    }
}