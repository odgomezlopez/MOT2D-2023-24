using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionSelector : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] public string actionName = "ShowHUD";
    protected InputAction action;
    
    
    void Awake()
    {
        //Seleccionar la acción
        var playerInput = GameObject.FindAnyObjectByType<PlayerInput>();
        if (playerInput != null)
        {
            action = playerInput.actions[actionName];
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ActionSelector), true )]
[CanEditMultipleObjects] // This attribute allows the editor to handle multiple selections.
public class ActionSelectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update(); // Handle the serialized object

        // Display dropdown for each selected ActionSelector object
        EditorGUI.BeginChangeCheck(); // Track changes to handle undo/redo

        // Using SerializedProperty to handle multiple objects uniformly
        SerializedProperty actionNameProp = serializedObject.FindProperty("actionName");

        // Assuming all instances have the same PlayerInput component in the scene for simplicity
        var playerInput = GameObject.FindGameObjectWithTag("PlayerInput")?.GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            EditorGUILayout.HelpBox("No PlayerInput component found with tag 'PlayerInput'.", MessageType.Warning);
            return;
        }

        var actions = playerInput.actions.ToList();
        string[] actionNames = actions.Select(a => a.name).ToArray();
        int currentIndex = actions.FindIndex(a => a.name == actionNameProp.stringValue);
        currentIndex = Mathf.Max(0, currentIndex); // Ensure valid index

        int selectedIndex = EditorGUILayout.Popup("Input Action", currentIndex, actionNames);
        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < targets.Length; i++)
            {
                var actionSelector = (ActionSelector)targets[i];
                Undo.RecordObject(actionSelector, "Change Input Action");
                actionSelector.actionName = actionNames[selectedIndex];
                EditorUtility.SetDirty(actionSelector);
            }
        }

        serializedObject.ApplyModifiedProperties(); // Apply changes to all selected objects
    }
}
#endif