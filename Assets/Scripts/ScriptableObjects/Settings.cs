using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    [Header("Volume")]
    public float SoundsVolume = 0.5f;
    public float MusicVolume = 0.3f;
    public float UIVolume = 0.3f;
    public float AmbientVolume = 0.3f;
    [Header("Video")]
    public string Resolution = "1920 x 1080";
    public int MaxFPS = 60;
    public bool IsFullscreen = true;
    public bool IsVSyncOn = true;
    [Header("Language")]
    public Language SelectedLanguage = Language.EN;
    
    public bool SkipIntro = false;
}
