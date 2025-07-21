using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public static event System.Action OnLanguageChanged;
    private TMP_Dropdown dropdown;
    void Start(){
        dropdown = GetComponent<TMP_Dropdown>();
        StartCoroutine(SetDropdownValue());
    }
    void OnEnable(){
        OnLanguageChanged += ChangeLanguage;
    }
    void OnDisable(){
        OnLanguageChanged -= ChangeLanguage;
    }
    private IEnumerator SetDropdownValue(){
        while(!ScriptableObjectLoader.IsInitialized)
            yield return null;
        
        switch (ScriptableObjectLoader.settingsSO.SelectedLanguage){
            case Language.EN: dropdown.value = 0; break;
            case Language.DE: dropdown.value = 1; break;
            default: dropdown.value = 0; break;
        }
    }
    public void ChangeLanguage(){
        StartCoroutine(WaitForLocalizationAndChange());
    }
    private IEnumerator WaitForLocalizationAndChange(){
        while (LocalizationLoader.Instance == null)
            yield return null;
        ScriptableObjectLoader.settingsSO.SelectedLanguage = Enum.Parse<Language>(dropdown.value.ToString());
        SaveLoadManager.Instance.SaveSettings();
        LocalizationLoader.TriggerLanguageChangedDropdown(Enum.Parse<Language>(dropdown.value.ToString()));
    }
}
