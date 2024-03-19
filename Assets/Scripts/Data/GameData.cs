using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData {

    //Puntos
    [SerializeField] private float points=0;

    GameData() {
        points = 0;//Valor inicial
    }


    #region Puntos

    public void AddPoints(float p)
    {
        points += p;
    }
    #endregion

    #region FLAGS
    #region FlagsGlobables
    private SerializedDictionary<string, bool> flags = new SerializedDictionary<string, bool>();
    public void MarkAsInteracted(string objectId)
    {
        if (!flags.ContainsKey(objectId))
        {
            flags.Add(objectId, true);
        }
    }
    public bool HasBeenInteracted(string objectId)
    {
        return flags.ContainsKey(objectId) && flags[objectId];
    }
    #endregion

    #region FlagsLocales
    private SerializedDictionary<string, SerializedDictionary<string, bool>> sceneFlags = new SerializedDictionary<string, SerializedDictionary<string, bool>>();

    // Método para marcar un objeto como interactuado en una escena.
    public void MarkAsInteractedSceneCurrent(string objectId) => MarkAsInteractedScene(SceneManager.GetActiveScene().name, objectId);
    public bool HasBeenInteractedSceneCurrent(string sceneName, string objectId) => HasBeenInteractedScene(SceneManager.GetActiveScene().name, objectId);

    public void MarkAsInteractedScene(string sceneName, string objectId)
    {
        if (!sceneFlags.ContainsKey(sceneName))
        {
            sceneFlags[sceneName] = new SerializedDictionary<string, bool>();
        }

        sceneFlags[sceneName][objectId] = true;
    }

    // Método para verificar si un objeto ha sido interactuado en una escena.
    public bool HasBeenInteractedScene(string sceneName, string objectId)
    {
        return sceneFlags.ContainsKey(sceneName) && sceneFlags[sceneName].ContainsKey(objectId) && sceneFlags[sceneName][objectId];
    }

    #endregion
    #endregion
}