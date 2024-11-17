using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceManager : MonoBehaviour
{
    private ActorController player;
    [SerializeField] private GameObject[] managedObjects;
    [SerializeField] private float maxDistance = 20f;

    private Transform playerTransform;
    private float maxDistanceSquared;
    private int frameCount = 0;

    void Start()
    {
        // Attempt to get the PlayerController if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<ActorController>();
        }

        // Ensure player is found and assign transforms
        if (player != null)
        {
            playerTransform = player.transform;
            maxDistanceSquared = maxDistance * maxDistance;
        }
        else
        {
            Debug.LogError("PlayerController not found on any game object!");
        }
    }

    void Update()
    {
        // Increment frame counter and check every 30 frames
        frameCount++;
        if (frameCount >= 30)
        {
            frameCount = 0; // Reset frame count
            CheckDistances();
        }
    }

    void CheckDistances()
    {
        if (playerTransform == null)
            return;

        // Check the distance for each managed object
        foreach (GameObject obj in managedObjects)
        {
            if ((playerTransform.position - obj.transform.position).sqrMagnitude < maxDistanceSquared)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}
