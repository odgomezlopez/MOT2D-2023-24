using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventResponses : MonoBehaviour
{
    void OnEndDestroy()
    {
        Destroy(gameObject);
    }
}
