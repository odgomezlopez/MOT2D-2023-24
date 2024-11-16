using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    CinemachineCamera triggerCamera;

    void Start()
    {
        triggerCamera = GetComponentInChildren<CinemachineCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) triggerCamera.Priority = 100;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) triggerCamera.Priority = 0;
        //Destroy(this);
    }

}
