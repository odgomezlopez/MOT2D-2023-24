using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SaveableEntity : MonoBehaviour
{
    [SerializeField] string uniqueIdentifier = "";

    private static Dictionary<string, SaveableEntity> _globalLookup = new();
    private List<ISaveable> saveables = new();

    private void Awake()
    {
        if (string.IsNullOrEmpty(uniqueIdentifier))
        {
            GenerateUniqueIdentifier();
        }

        saveables = GetComponents<ISaveable>().ToList();
        SavingSystem.SaveableEntities.Add(this);
    }

    public string GetUniqueIdentifier() => uniqueIdentifier;

    public JToken CaptureAsJToken()
    {
        var state = new JObject();
        IDictionary<string, JToken> stateDict = state;

        foreach (var saveable in saveables)
        {
            var token = saveable.CaptureAsJToken();
            var component = saveable.GetType().ToString();
            stateDict[component] = token;
            print($"{name} Capture {component} = {token}");
        }

        return state;
    }

    public void RestoreFromJToken(JToken s)
    {
        var state = s.ToObject<JObject>();
        IDictionary<string, JToken> stateDict = state;

        foreach (var saveable in saveables)
        {
            var component = saveable.GetType().ToString();
            if (stateDict.ContainsKey(component))
            {
                saveable.RestoreFromJToken(stateDict[component]);
                print($"{name} Restore {component} =>{stateDict[component]}");
            }
        }
    }

    private bool IsValid(string candidate)
    {
        if (string.IsNullOrEmpty(candidate) || !IsUnique(candidate))
            return false;

        return true;
    }

    private bool IsUnique(string candidate)
    {
        if (!_globalLookup.ContainsKey(candidate)) return true;
        if (_globalLookup[candidate] == this) return true;

        if (_globalLookup[candidate] == null)
        {
            _globalLookup.Remove(candidate);
            return true;
        }

        if (_globalLookup[candidate].GetUniqueIdentifier() != candidate)
        {
            _globalLookup.Remove(candidate);
            return true;
        }

        return false;
    }

    public void GenerateUniqueIdentifier()
    {
        if (string.IsNullOrEmpty(uniqueIdentifier))
        {
            uniqueIdentifier = Guid.NewGuid().ToString();

#if UNITY_EDITOR
            if (Application.isEditor)
            {
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.Undo.RecordObject(this, "Generate Unique Identifier");
            }
#endif
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(SaveableEntity))]
public class SaveableEntityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Draws the default inspector UI.

        SaveableEntity saveableEntity = (SaveableEntity)target;

        if (GUILayout.Button("Generate Unique Identifier") && !Application.isPlaying)
        {
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            property.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();
        }

        if (!string.IsNullOrEmpty(saveableEntity.GetUniqueIdentifier()))
        {
            EditorGUILayout.HelpBox($"Unique Identifier: {saveableEntity.GetUniqueIdentifier()}", MessageType.Info);
        }
    }
}
#endif