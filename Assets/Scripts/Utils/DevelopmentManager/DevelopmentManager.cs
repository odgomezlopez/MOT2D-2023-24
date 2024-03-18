using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DevelopmentManager : MonoBehaviour{}


[CustomEditor(typeof(DevelopmentManager))]
public class DevelopmentManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //https://docs.unity3d.com/ScriptReference/GUILayout.html
        GUILayout.Label("Data Debug");
        if (GUILayout.Button("Delete All Data (Player Pref)"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
