using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    [SerializeField] private SavingStrategy strategy;
    public static List<SaveableEntity> SaveableEntities = new();

    public void Save(string saveFile)
    {
        JObject state = LoadJsonFromFile(saveFile);
        CaptureAsToken(state);
        SaveFileAsJSon(saveFile, state);
    }

    public void Load(string saveFile)
    {
        RestoreFromToken(LoadJsonFromFile(saveFile));
    }

    public void DeleteSaveFile(string saveFile)
    {
        var path = GetPathFromSaveFile(saveFile);
        if (!File.Exists(path)) return;

        print($"Save file: {saveFile} deleted");
        File.Delete(path);
    }

    private void SaveFileAsJSon(string saveFile, JObject state)
    {
        strategy.SaveToFile(saveFile, state);
    }

    private JObject LoadJsonFromFile(string saveFile)
    {
        return strategy.LoadFromFile(saveFile);
    }

    private void CaptureAsToken(JObject state)
    {
        IDictionary<string, JToken> stateDict = state;
        foreach (var saveable in SaveableEntities)
        {
            stateDict[saveable.GetUniqueIdentifier()] = saveable.CaptureAsJToken();
        }
    }

    private void RestoreFromToken(JObject state)
    {
        IDictionary<string, JToken> stateDict = state;
        foreach (var saveable in SaveableEntities)
        {
            var id = saveable.GetUniqueIdentifier();
            if (stateDict.ContainsKey(id))
                saveable.RestoreFromJToken(stateDict[id]);
        }
    }

    private string GetPathFromSaveFile(string saveFile)
    {
        return strategy.GetPathFromSaveFile(saveFile);
    }
}