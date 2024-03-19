using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SavingWrapper : MonoBehaviour
{
    [SerializeField] SavingSystem savingSystem;
    [SerializeField] private string saveFile = "save01";
    [SerializeField] private bool loadOnStart = false;
    [SerializeField] private bool saveOnFinish = false;

    //AutoLoad
    private void Start()  { if (loadOnStart) Load(); }
    //SaveOnQuit
    private void OnDestroy() { if (saveOnFinish) Save(); }


    //Metodos
    public void Load() => savingSystem.Load(saveFile);
    public void Save() => savingSystem.Save(saveFile);
    public void DeleteSave() => savingSystem.DeleteSaveFile(saveFile);

    private void Update()
    {
        /*if (Keyboard.current[Key.L].wasPressedThisFrame)
        {
            LoadSave();
            print("Loaded");
        }
        else if (Keyboard.current[Key.S].wasPressedThisFrame)
        {
            WriteSave();
            print("Saved");
        }*/
    }
}
#if UNITY_EDITOR

[CustomEditor(typeof(SavingWrapper))]
public class SavingWrapperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        SavingWrapper wrapper = (SavingWrapper)target;

        if (GUILayout.Button("Load"))
        {
            wrapper.Load();
        }

        if (GUILayout.Button("Save"))
        {
            wrapper.Save();
        }

        if (GUILayout.Button("Delete"))
        {
            wrapper.DeleteSave();
        }
    }
}

#endif