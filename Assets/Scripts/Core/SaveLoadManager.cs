using System;
using System.IO;
using UnityEngine;
using System.Collections; 
public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    [SerializeField] private Settings settings;
    [SerializeField] private PlayerStats playerOneStats;
    private string path;
    public bool isSaveFileLoaded = false;

    private void Awake() {
        if (ScriptableObjectLoader.IsInitialized){
            Initialize();
        }
        path = Path.Combine(Directory.GetCurrentDirectory(), "settings.txt");
    }

    void Start() { 
        StartCoroutine(WaitForScriptableObjectsAndLoad());
    }
    private void Initialize(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        
        settings = ScriptableObjectLoader.GetSettings();
        if (settings != null){
            //Debug.Log($"Settings loaded: {settings.name}");
        } else {
            Debug.LogError("Failed to load Setting.");
        }
        playerOneStats= ScriptableObjectLoader.GetPlayerOneStats();
        if (playerOneStats != null){
            //Debug.Log($"PlayerOneStats loaded: {playerOneStats.name}");
        } else {
            Debug.LogError("Failed to load PlayerOneStats.");
        }
    }
    private IEnumerator WaitForScriptableObjectsAndLoad() {
        while (!ScriptableObjectLoader.IsInitialized) {
            //Debug.Log("Waiting for ScriptableObjectLoader to initialize...");
            yield return null;
        }
        Initialize();
         if (!isSaveFileLoaded) {
            if (!File.Exists(path)) {
                NewSettings();
            }
            LoadConfigIntoGameSettings();
            isSaveFileLoaded = true;
        }
    }
    private void LoadConfigIntoGameSettings() {
        if (!File.Exists(path)) {
            Debug.LogWarning($"No save file found at {path}");
            return;
        }
        
        using (StreamReader sr = File.OpenText(path)) {
            string s;
            while ((s = sr.ReadLine()) != null) {
                // Check if it's a settings line
                if (s.Contains("st:")) {
                    string[] parts = s.Split(':');
                    if (parts.Length < 3) continue;

                    string fieldName = parts[1];
                    string value = GetValueFromData(s);

                    var field = settings.GetType().GetField(fieldName);
                    if (field != null) {
                        if(field.FieldType.IsEnum){
                            object converted = Enum.Parse(field.FieldType, value);
                            field.SetValue(settings, converted);
                        } else {
                            object converted = Convert.ChangeType(value, field.FieldType);
                            field.SetValue(settings, converted);
                        }
                    }
                }

                // Add other Scriptable Objects if needed
            }
        }
    }

    // Take the third entry from data because (SO:Variable:Value )
    private string GetValueFromData(string data) {
        return data.Split(":")[2];
    }
    private void ResetSettingsStats(){
        // Volume
        settings.SoundsVolume = 0.5f;
        settings.MusicVolume = 0.3f;
        settings.UIVolume = 0.3f;
        settings.AmbientVolume = 0.3f;

        // Video
        settings.MaxFPS = 60;
        settings.IsFullscreen = true;
        settings.IsVSyncOn = true;

        // Language
        settings.SelectedLanguage = Language.EN;
    }
    public void SaveSettings() {
        if (File.Exists(path)) {
            File.Delete(path);
        }
        if(settings == null)
            return;
        using (StreamWriter sw = File.CreateText(path)) {
            foreach (var field in settings.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                sw.WriteLine($"st:{field.Name}:{field.GetValue(settings)}");
            }
        }
    }
    public void NewSettings() {
        if (File.Exists(path)) {
            File.Delete(path);
        }
        ResetSettingsStats();
        using (StreamWriter sw = File.CreateText(path)) {
            foreach (var field in settings.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                sw.WriteLine($"st:{field.Name}:{field.GetValue(settings)}");
            }
        }
    }

}
