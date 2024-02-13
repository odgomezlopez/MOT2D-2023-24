using UnityEngine;
using UnityEngine.Events;

public class GameEventAudioListener : MonoBehaviour
{
    public GameEventAudio gameEvent;
    public UnityEvent<AudioClip> onEventTriggered;

    void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    public void OnEventTriggered(AudioClip val)
    {
        onEventTriggered.Invoke(val);
    }
}