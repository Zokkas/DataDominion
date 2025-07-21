using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
public class ScriptableObjectLoader : MonoBehaviour
{
    [Header("Global ScriptableObjects")]
    public static Settings settingsSO { get; private set; }
    public static PlayerStats playerOneSO { get; private set; }
    public string settingsKey = "SettingsKey";
    public string playerOneKey = "PlayerOneStatsKey";


    public static bool IsInitialized { get; private set; } = false;

    //public static event System.Action OnInitialized;
    public int LoadingProgress { get; private set; } = 0;
    
    private int totalScriptableObjects = 0;  
    private int loadedObjects = 0;
    private void Awake()
    {
        InitializeAsync();
    }
    private void CountTotalScriptableObjects(){
        // +1 for Settings +1 for PlayerOneStats 
        totalScriptableObjects = 1 + 1; 
    }
    private void UpdateProgress(){
        loadedObjects++;
        LoadingProgress = Mathf.RoundToInt((loadedObjects / (float)totalScriptableObjects) * 100);
        //Debug.Log($"Loading Progress: {LoadingProgress}%");
    }
    private async void InitializeAsync(){
        //Debug.Log("Initializing ScriptableObjectLoader...");

        try{
            CountTotalScriptableObjects();

            await LoadData();

            IsInitialized = true;
            //Debug.Log("ScriptableObjectLoader initialization completed.");

            // Notify all listeners that initialization is complete
            // OnInitialized?.Invoke();
        } catch (System.Exception ex){
            Debug.LogError($"Unexpected error during initialization: {ex.Message}");
        }
    }

    private async Task LoadData() {
        //Debug.Log("Loading global ScriptableObjects...");

        if (settingsSO == null) {
            try {
                settingsSO = await Addressables.LoadAssetAsync<Settings>(settingsKey).Task;
            } catch (Exception ex) {
                Debug.LogError($"Error loading Settings with key '{settingsKey}': {ex.Message}");
            }
        }
        UpdateProgress();
        if (playerOneSO == null) {
            try {
                playerOneSO = await Addressables.LoadAssetAsync<PlayerStats>(playerOneKey).Task;
            } catch (Exception ex) {
                Debug.LogError($"Error loading PlayerOneStats with key '{playerOneKey}': {ex.Message}");
            }
        }
        UpdateProgress();
        
        //Debug.Log("Finished loading all ScriptableObjects.");
    }




    // Getters for specific scriptable objects
    public static Settings GetSettings(){
        if (!IsInitialized){
            Debug.LogError("ScriptableObjectLoader is not yet initialized.");
            return null;
        }

        if (settingsSO != null){
            return settingsSO;
        }

        Debug.LogError($"Settings not found.");
        return null;
    }
    public static PlayerStats GetPlayerOneStats(){
        if (!IsInitialized){
            Debug.LogError("ScriptableObjectLoader is not yet initialized.");
            return null;
        }

        if (settingsSO != null){
            return playerOneSO;
        }

        Debug.LogError($"Settings not found.");
        return null;
    }
    public static void ResetSettingsStats(){
        // Volume
        settingsSO.SoundsVolume = 0.5f;
        settingsSO.MusicVolume = 0.3f;
        settingsSO.UIVolume = 0.3f;
        settingsSO.AmbientVolume = 0.3f;

        // Video
        settingsSO.MaxFPS = 60;
        settingsSO.IsFullscreen = true;
        settingsSO.IsVSyncOn = true;

        // Language
        settingsSO.SelectedLanguage = Language.EN;
    }
    public static void ResetPlayerOneStats(){
        playerOneSO.Level = 1;
        playerOneSO.CurrentExp = 0;
        playerOneSO.Coins = 0;
        playerOneSO.EnemiesKilled = 0;
        playerOneSO.BossesKilled = 0;
        playerOneSO.RunsCompleted = 0;
    }
    
}
