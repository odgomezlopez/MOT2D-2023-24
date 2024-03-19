using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class TestComponent : MonoBehaviour, ISaveable
{

    [SerializeField] float numPressSpace = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numPressSpace++;
        }
    }

    #region
    //Si se quieren guardar más datos es necesario 
    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(numPressSpace);
    }

    public void RestoreFromJToken(JToken state)
    {
        numPressSpace = state.ToObject<float>();
    }
    #endregion
}
