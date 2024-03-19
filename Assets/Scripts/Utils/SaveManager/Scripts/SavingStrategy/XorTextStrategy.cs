using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Saving Strategies/XorText", fileName = "XorTextStrategy")]
public class XorTextStrategy : SavingStrategy
{
    [SerializeField] string key = "TwelveTwinsTwirledTwelveTwigs";
    public string Key => key; // Public getter for the key

    public override string GetExtension() => ".xortext";

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

    string EncodeAsBase64String(string source)
    {
        byte[] sourceArray = new byte[source.Length];

        for (int i = 0; i < source.Length; i++)
            sourceArray[i] = (byte)source[i];

        return Convert.ToBase64String(sourceArray);
    }

    string DecodeFromBase64String(string source)
    {
        byte[] sourceArray = Convert.FromBase64String(source);
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < sourceArray.Length; i++)
            builder.Append((char)sourceArray[i]);

        return builder.ToString();
    }

    public override void SaveToFile(string saveFile, JObject state)
    {
        var path = GetPathFromSaveFile(saveFile);
        Debug.Log($"Saving to {path} ");
        using (var textWriter = File.CreateText(path))
        {
            var json = state.ToString();
            var encoded = EncryptDecrypt(json, key);
            var base64 = EncodeAsBase64String(encoded);
            textWriter.Write(base64);
        }
    }

    public override JObject LoadFromFile(string saveFile)
    {
        var path = GetPathFromSaveFile(saveFile);
        if (!File.Exists(path)) return new JObject();

        using (var textReader = File.OpenText(path))
        {
            var encoded = textReader.ReadToEnd();
            var decoded = DecodeFromBase64String(encoded);
            var json = EncryptDecrypt(decoded, key);
            return (JObject)JToken.Parse(json);
        }
    }
}


#if UNITY_EDITOR

[CustomEditor(typeof(XorTextStrategy))]
public class XorTextStrategyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector


        XorTextStrategy xorStrategy = (XorTextStrategy)target;

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