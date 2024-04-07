using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageInit : MonoBehaviour
{
    const string languagePrefKey = "lang";

    void Start()
    {
        var selectedLang = PlayerPrefs.GetInt(languagePrefKey, 0);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)selectedLang];
    }
}
