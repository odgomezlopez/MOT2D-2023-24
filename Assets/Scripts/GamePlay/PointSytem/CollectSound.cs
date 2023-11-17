using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSound : MonoBehaviour
{
    public AudioClip audioSFX;
    
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (audioSource == null) return;

        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(audioSFX,transform.position,audioSource.volume);     
        }*/
    }
}
