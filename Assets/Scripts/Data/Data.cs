using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviourSingletonPersistent<Data>
{
    //Puntos
    [Header("Sistema de puntos")]
    [SerializeField] private float points;

    public void AddPoints(float p)
    {
        points += p;
    }

    //FLAGS GLOBALES
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

    //FLAGS DE ESCENA

    private SerializedDictionary<string, SerializedDictionary<string, bool>> sceneFlags = new SerializedDictionary<string, SerializedDictionary<string, bool>>();

    // M�todo para marcar un objeto como interactuado en una escena.
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

    // M�todo para verificar si un objeto ha sido interactuado en una escena.
    public bool HasBeenInteractedScene(string sceneName, string objectId)
    {
        return sceneFlags.ContainsKey(sceneName) && sceneFlags[sceneName].ContainsKey(objectId) && sceneFlags[sceneName][objectId];
    }




    /*[Header("Sistema de stats")]
    [SerializeField] public PlayerStats stats;

    //Player default info
    [Header("Player Info")]
    [SerializeField] public PlayerDataSO playerData;
    [SerializeField] public bool statsInitialized = false;  */
}
