using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Collections;
public class LocalizationLoader : MonoBehaviour
{
     public static LocalizationLoader Instance { get; private set; }

    public static event System.Action OnLanguageChanged;

    private Dictionary<string, string> currentLanguageData = new Dictionary<string, string>();
    public Language CurrentLanguage { get; private set; } = Language.EN;

    void Start(){
        StartCoroutine(WaitForScriptableObjectsAndLoad());
    }
    private void Initialize(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);

            CurrentLanguage = ScriptableObjectLoader.settingsSO.SelectedLanguage;
            LoadLanguage();
        } else{
            Destroy(gameObject);
        }
    }
    private IEnumerator WaitForScriptableObjectsAndLoad() {
        while (!ScriptableObjectLoader.IsInitialized) {
            //Debug.Log("Waiting for ScriptableObjectLoader to initialize...");
            yield return null;
        }
        Initialize();
    }
    public void LoadLanguage(){
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), CurrentLanguage.ToString() + ".json");

        if (File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            LocalizationData data = JsonUtility.FromJson<LocalizationData>(json);
            // Clear out the old data to replace it with the new one
            currentLanguageData.Clear();
            foreach (var item in data.items){
                currentLanguageData[item.key] = item.value;
            }

            OnLanguageChanged?.Invoke();
        }
        else{
            Debug.LogError($"Localization file not found: {filePath}");
        }
    }

    public string GetLocalizedValue(string key){
        if (currentLanguageData.TryGetValue(key, out string value)){
            return value;
        }

        return $"Language key {key} not found!";
    }
    public static void TriggerLanguageChangedDropdown(Language selectedLanguage){
        Instance.CurrentLanguage = selectedLanguage;
        Instance.LoadLanguage();
        //OnLanguageChanged?.Invoke();
    }

}
