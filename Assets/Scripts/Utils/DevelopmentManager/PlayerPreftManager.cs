using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour{}

#if UNITY_EDITOR

[CustomEditor(typeof(PlayerPrefManager))]
public class PlayerPrefManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //https://docs.unity3d.com/ScriptReference/GUILayout.html
        GUILayout.Label("Data Debug");
        if (GUILayout.Button("Delete All Player Pref"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif