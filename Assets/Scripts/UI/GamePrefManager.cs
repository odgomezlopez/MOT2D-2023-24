using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Core.Parsing;
using UnityEngine.UI;

public class GamePrefManager : MonoBehaviour
{
    //Language
    const string languagePrefKey = "lang";
    
    [SerializeField] TMP_Dropdown languageDropDown;
    [SerializeField] int selectedLang;

    private void Start()
    {
        InitLanguageSettings();
    }
    private void InitLanguageSettings()
    {
        //1. Vacio
        languageDropDown.ClearOptions();

        //2. Vuelvo las opciones de LocalizationSettings en el languageDropDown

        // Variables to hold default language settings
        string defaultLocaleName = LocalizationSettings.ProjectLocale.LocaleName;
        int defaultLocaleIndex = 0;

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            languageDropDown.options.Add(new TMP_Dropdown.OptionData(locale.name));

            if (locale.name == defaultLocaleName) //Guardadmos el indes si es el por defecto

            {
                defaultLocaleIndex = i;
            }
        }

        //3. Asocio evento de cambio
        languageDropDown.onValueChanged.AddListener(ChangeLanguage);

        //4. Obento la lengua guardada en memoria
        selectedLang = PlayerPrefs.GetInt(languagePrefKey, defaultLocaleIndex);
        languageDropDown.value = selectedLang;
    }

    public void ChangeLanguage(int option)
    {
        //Guardo la lengua en PlayerPrefs
        selectedLang = option;
        PlayerPrefs.SetInt(languagePrefKey, selectedLang);

        //Cambio la lengua
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)option];
    }
}
