using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public abstract class EncriptDecriptStrategy : ScriptableObject
{
    [SerializeField] protected string key = "TwelveTwinsTwirledTwelveTwigs";
    public string Key => key; // Public getter for the key

    // Start is called before the first frame update
    public abstract string EncodeString(string source);
    public abstract string DecodeString(string source);
}

#if UNITY_EDITOR

[CustomEditor(typeof(EncriptDecriptStrategy))]
public class EncriptDecriptStrategyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        EncriptDecriptStrategy t = (EncriptDecriptStrategy)target;

        if (GUILayout.Button("Generate Key"))
        {
            // Using SerializedProperties allows for undo, redo, and prefab overrides
            SerializedProperty keyProp = serializedObject.FindProperty("key");
            keyProp.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();

            Debug.Log($"New Key Generated: {t.Key}");
        }
    }
}
#endif
