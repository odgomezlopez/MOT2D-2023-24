using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[ExecuteAlways]
public class SaveableEntity : MonoBehaviour
{
    //Propiedades
    [SerializeField] private string entityId="";

    //Acceso directo
    private SaveManager g => SaveManager.Instance;
    private SavedComponents e => SaveManager.Instance.entities;


    #region ID
    public string EntityId { get => entityId; set => entityId = value; }

    private void Awake()
    {
        //if (g == null) Debug.LogError("GameDataManager not found. Save/Load entity is not at work.");
        if (string.IsNullOrEmpty(entityId)) GenerateUniqueIdentifier();
    }

    public void GenerateUniqueIdentifier()
    {
        if (string.IsNullOrEmpty(entityId))
        {
            entityId = Guid.NewGuid().ToString();

#if UNITY_EDITOR
            if (Application.isEditor)
            {
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.Undo.RecordObject(this, "Generate Unique Identifier");
            }
#endif
        }
    }
    #endregion


    #region Carga/Guardado en ciclo de vida
    
    private void OnEnable()
    {
        if (g != null) LoadData();
    }
    private void OnDisable()
    {
        if (g != null) SaveData();
    }
    /*private void Start()
    {
         if (g != null) LoadData();
    }
    private void OnDestroy()
    {
        if (g != null) SaveData();
    }*/
    #endregion

    public void SaveData() {
        if (e == null) return;

        ISaveable saveable = GetComponent<ISaveable>();

        if (saveable != null) 
        {
            e.StoreObject(entityId, saveable.SaveData());
            return;
        }
    }
    public void LoadData() {
        if (e == null) return;

        ISaveable saveable = GetComponent<ISaveable>();
        if (saveable != null)
        {
            saveable.LoadData(e.RecoverObject(entityId));
            return;
        }
    }
    public void DeleteFromStorage()
    {
        if (e == null) return;

        e.DeleteObject(entityId);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(SaveableEntity), true)]
public class SaveableEntityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        SaveableEntity t = (SaveableEntity)target;

        if (GUILayout.Button("Generate Random ID"))
        {
            // Generate a new GUID and assign it as the entity ID
            t.EntityId = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(t); // Mark the entity as dirty to ensure the new ID is saved
        }
    }
}
#endif