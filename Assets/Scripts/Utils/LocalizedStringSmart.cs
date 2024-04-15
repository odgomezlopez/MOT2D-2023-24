using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class LocalizedStringSmart : MonoBehaviour
{
    public LocalizedString myString;

    public string localizedText;

    public float TimeNow => Time.time;

    /// <summary>
    /// Register a ChangeHandler. This is called whenever we need to update our string.
    /// </summary>
    void OnEnable()
    {
        myString.Arguments = new[] { this };
        myString.StringChanged += UpdateString;
    }

    void OnDisable()
    {
        myString.StringChanged -= UpdateString;
    }

    void UpdateString(string s)
    {
        localizedText = s;
    }

    void OnGUI()
    {
        // This calls UpdateString immediately (if the table is loaded) or when the table is available.
        myString.RefreshString();
        GUILayout.Label(localizedText);
    }
}