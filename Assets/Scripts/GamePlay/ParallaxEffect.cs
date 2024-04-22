using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxEffectMultiplier = 0.5f;
    private float startPosition;
    private float length;

    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        var cam = Camera.main;
        float temp = (cam.transform.position.x * (1 - parallaxEffectMultiplier));
        float distance = (cam.transform.position.x * parallaxEffectMultiplier);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (temp > startPosition + length) startPosition += length;
        else if (temp < startPosition - length) startPosition -= length;
    }
}
