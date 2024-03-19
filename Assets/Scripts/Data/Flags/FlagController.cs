using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagController : MonoBehaviour
{
    //Parametros
    [SerializeField] string baseKey="";
    [SerializeField] bool localFlag = false;

    string key;

    //Getter
    Flags flags => GameDataManager.Instance.gameData.flags;

    private void Start()
    {
        //Construimos la clave completa
        key= baseKey;
        if (localFlag) key = SceneManager.GetActiveScene().name + key;

        //Comprobamos si ya se ha usado
        if (flags.HasBeenMark(key)) Destroy(this);
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
}

#if UNITY_EDITOR

[CustomEditor(typeof(FlagController))]
public class FlagControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        FlagController t = (FlagController)target;

        if (GUILayout.Button("UnMark Flag"))
        {
            t.UnMarkFlag();
        }
    }
}
#endif
