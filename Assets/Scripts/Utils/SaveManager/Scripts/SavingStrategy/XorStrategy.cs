using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Saving Strategies/Xor", fileName = "XorStrategy")]
public class XorStrategy : SavingStrategy
{
    [SerializeField] private string key = "TwelveTwinsTwirledTwelveTwigs";
    public string Key => key; // Public getter for the key

    public override string GetExtension() => ".xor";

    public string EncryptDecrypt(string szPlainText, string szEncryptionKey)
    {
        StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
        StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
        char Textch;
        for (int iCount = 0; iCount < szPlainText.Length; iCount++)
        {
            int stringIndex = iCount % szEncryptionKey.Length;
            Textch = szInputStringBuild[iCount];
            Textch = (char)(Textch ^ szEncryptionKey[stringIndex]);
            szOutStringBuild.Append(Textch);
        }

        return szOutStringBuild.ToString();
    }

    public override void SaveToFile(string saveFile, JObject state)
    {
        var path = GetPathFromSaveFile(saveFile);
        Debug.Log($"Saving to {path} ");
        using (var textWriter = File.CreateText(path))
        {
            var json = state.ToString();
            var encoded = EncryptDecrypt(json, key);
            textWriter.Write(encoded);
        }
    }

    public override JObject LoadFromFile(string saveFile)
    {
        var path = GetPathFromSaveFile(saveFile);
        if (!File.Exists(path)) return new JObject();

        using (var textReader = File.OpenText(path))
        {
            var encoded = textReader.ReadToEnd();
            var json = EncryptDecrypt(encoded, key);
            return (JObject)JToken.Parse(json);
        }
    }

}

#if UNITY_EDITOR

[CustomEditor(typeof(XorStrategy))]
public class XorStrategyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        XorStrategy xorStrategy = (XorStrategy)target;

        if (GUILayout.Button("Generate Key"))
        {
            // Using SerializedProperties allows for undo, redo, and prefab overrides
            SerializedProperty keyProp = serializedObject.FindProperty("key");
            keyProp.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();

            Debug.Log($"New Key Generated: {xorStrategy.Key}");
        }
    }
}
#endif
