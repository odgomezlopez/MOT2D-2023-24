using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteAlways]
public class Flag : MonoBehaviour
{
    //Parametros
    [SerializeField] public string baseKey;
    [SerializeField] bool localFlag = false;
    [SerializeField] bool disableIfMarked = true;

    string key;

    //Getter
    FlagsManager flags => FlagsManager.Instance;

    private void Start()
    {
        //Construimos la clave completa
        key= baseKey;
        if (localFlag) key = SceneManager.GetActiveScene().name + key;

        //Comprobamos si ya se ha usado
        if (flags.HasBeenMark(key) && disableIfMarked) gameObject.SetActive(false);
    }

    //Metodo a ser llamado por otros componentes cuando se quiera marcar la Flag como marcada
    public void MarkFlag()
    {
        flags.MarkFlag(key);
    }

    public void UnMarkFlag()
    {
        flags.UnMarkFlag(key);
    }

    public bool HasBeenMark() { return flags.HasBeenMark(key); }

    //Generar clave base aleatoria
    #region Generador de clave
    private void Awake()
    {
        GenerateUniqueIdentifier();
    }

    public void GenerateUniqueIdentifier()
    {
        if (string.IsNullOrEmpty(baseKey))
        {
            baseKey = Guid.NewGuid().ToString();

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
}

#if UNITY_EDITOR

[CustomEditor(typeof(Flag))]
public class FlagControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        Flag t = (Flag)target;

        if (GUILayout.Button("UnMark Flag"))
        {
            t.UnMarkFlag();
        }

        if (GUILayout.Button("Generate Random ID"))
        {
            // Generate a new GUID and assign it as the entity ID
            t.baseKey = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(t); // Mark the entity as dirty to ensure the new ID is saved
        }
    }
}
#endif
