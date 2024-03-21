using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SaveableEntity : MonoBehaviour
{
    //ID
    [SerializeField]
    private string entityId="";
    public string EntityId{get => entityId; set => entityId = value;}

    //Acceso directo
    private GameData g => GameDataManager.Instance.gameData;
    private SaveableEntityStorage e => GameDataManager.Instance.entities;


    #region Sincronizarse con GameData automáticamente siguiendo el ciclo de vida de componente
    /*
    private void OnEnable()
    {
        try
        {
            if (g != null)
            {
                LoadData(g);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading data for {entityId}: {ex.Message}");
        }
    }
    private void OnDisable()
    {
        if (g != null)
        {
            SaveData(g);
        }
    }*/
    private void Start()
    {
        if (g != null)
        {
            LoadData(g);
        }
    }
    private void OnDestroy()
    {
        if (g != null)
        {
            SaveData(g);
        }
    }
    #endregion

    #region Guardar/Cargar del storage generico utilizando la clave
    protected T RecoverFromStorage<T>()
    {
        return e.RecoverObject<T>(entityId);
    }

    protected void SaveToStorage(object newObject)
    {
        e.StoreObject(entityId, newObject);
    }
    protected void DeleteFromStorage()
    {
        e.DeleteObject(entityId);
    }

    #endregion

    //Estos metodos son a ser sobreescritos por quien use esta clase
    protected virtual void SaveData(GameData g) { Debug.Log("SaveData method is not implemented"); }
    protected virtual void LoadData(GameData g) { Debug.Log("LoadData method is not implemented"); }
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