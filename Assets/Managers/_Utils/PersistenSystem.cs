using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenSystem : MonoBehaviourSingletonPersistent<PersistenSystem>
{
    public GameObject originalPrefab; // Assign this in the Unity Editor

    public void ResetGameObject()
    {
        // Destroy the current GameObject
        Destroy(gameObject);

        // Instantiate a new GameObject from the prefab
        Instantiate(originalPrefab, originalPrefab.transform.position, originalPrefab.transform.rotation);
    }
}
