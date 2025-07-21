using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
   private static AudioManager _instance;

    public static AudioManager Instance {
        get {
            // Make sure that the instance is from the active scene
            if (_instance == null || !_instance.gameObject.scene.isLoaded) {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }

    public enum SoundGroup { Music, SoundEffects, Dialogue, Ambient, UISounds }

    [System.Serializable]
    public class Sound{
        public string name;
        public SoundGroup group;
        public AudioClip clip;
        [Range(0f, 2f)] public float volumeMultiplier = 1f;
    }

    [Header("Sounds")]
    public List<Sound> sounds = new List<Sound>();
    public List<Sound> gunSounds = new List<Sound>();
    public List<Sound> eventSounds = new List<Sound>();
    private Dictionary<SoundGroup, float> groupVolumes = new Dictionary<SoundGroup, float>();
    private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();
    private Dictionary<SoundGroup, List<AudioSource>> allSounds = new Dictionary<SoundGroup, List<AudioSource>>();
    private Dictionary<AudioSource,Sound> allSoundData = new Dictionary<AudioSource,Sound>();

    

    void Awake()
    {
        // Assign instance if it's null or from the current scene
        if (_instance == null || !_instance.gameObject.scene.isLoaded) {
            _instance = this; 
        } else if (_instance != this) {
            // Do NOT destroy, just log a warning about multiple instances
            // Not destroying because we could use scenes inside of scenes
            Debug.LogWarning($"Multiple AudioManagers detected in scene: {gameObject.scene.name}. Ensure correct instance is used.");
        }

        // Set group volumes (default to 0.5)
        if(_instance != null){
            foreach (SoundGroup group in System.Enum.GetValues(typeof(SoundGroup))){
                groupVolumes[group] = 0.5f;
            }
        }

        // Populate sound dictionary
        // Add each sound group like that
        foreach (var sound in sounds){
            if (!soundDictionary.ContainsKey(sound.name))
                soundDictionary.Add(sound.name, sound);
        }
        foreach (var sound in gunSounds){
            if (!soundDictionary.ContainsKey(sound.name))
                soundDictionary.Add(sound.name, sound);
        }
        foreach (var sound in eventSounds){
            if (!soundDictionary.ContainsKey(sound.name))
                soundDictionary.Add(sound.name, sound);
        }
    }
    
    public void PlaySound(string name, GameObject gO = null,  bool isLooping = false, bool needsPitch = false){
        if (soundDictionary.TryGetValue(name, out Sound sound)){
            GameObject parentObject = gameObject;
            if(gO != null)
                parentObject = gO;
            
            AudioSource source = parentObject.AddComponent<AudioSource>();
            allSoundData.Add(source, sound); // Add the sound and the source to find its multiplier later
            
            if(gO != null)
                source.spatialBlend = 1f;
                
            source.clip = sound.clip;
            source.volume = groupVolumes[sound.group] * sound.volumeMultiplier;
            if(needsPitch)
                source.pitch = Random.Range(0.8f, 1f);
            source.Play();
            if(isLooping)
                source.loop = true;
            // Sounds like Button Clicks(UI)
            if(!allSounds.ContainsKey(sound.group) && sound.group != SoundGroup.UISounds){
                allSounds.Add(sound.group, new List<AudioSource>());
            }
            if(sound.group != SoundGroup.UISounds)
                allSounds[sound.group].Add(source);
            if(!isLooping)
                Destroy(source, sound.clip.length); // Destroy AudioSource after playback
            
        } else{
            Debug.LogWarning($"Sound '{name}' not found!");
        }
    }
    public void PlaySoundAt(string name, bool isLooping = false, GameObject gO = null, bool needsPitch = false, float startTime = 0){
        if (soundDictionary.TryGetValue(name, out Sound sound)){
            GameObject parentObject = gameObject;
            if(gO != null)
                parentObject = gO;
            
            AudioSource source = parentObject.AddComponent<AudioSource>();
            allSoundData.Add(source, sound); // Add the sound and the source to find its multiplier later
            
            if(gO != null)
                source.spatialBlend = 1f;
                
            source.clip = sound.clip;
            source.volume = groupVolumes[sound.group] * sound.volumeMultiplier;
            if(needsPitch)
                source.pitch = Random.Range(0.8f, 1f);
            source.time = startTime;
            source.Play();
            if(isLooping)
                source.loop = true;
            // Sounds like Button Clicks(UI)
            if(!allSounds.ContainsKey(sound.group) && sound.group != SoundGroup.UISounds){
                allSounds.Add(sound.group, new List<AudioSource>());
            }
            if(sound.group != SoundGroup.UISounds)
                allSounds[sound.group].Add(source);
            if(!isLooping)
                Destroy(source, sound.clip.length); // Destroy AudioSource after playback
            
        } else{
            Debug.LogWarning($"Sound '{name}' not found!");
        }
    }

    public void SetVolume(SoundGroup group, float volume, float multiplier = 0){
        if (groupVolumes.ContainsKey(group)){
            groupVolumes[group] = Mathf.Clamp01(volume);
        }
            if(allSounds.Count != 0){
                if(allSounds.ContainsKey(group)){
                    foreach(AudioSource source in allSounds[group]){
                        if(source == null)
                            continue;
                        multiplier = 0;
                        if(allSoundData.ContainsKey(source))
                            multiplier = allSoundData[source].volumeMultiplier;
                        if(multiplier != 0)
                            source.volume = volume * multiplier;
                        else 
                            source.volume = volume;
                    }
                }
            return;
        }
        
        // if(allSounds.Count != 0){
        //     if(allSounds.ContainsKey(group)){
        //         foreach(AudioSource source in allSounds[group]){
        //             source.volume = volume;
        //         }
        //     }
        // }
    }
    public void RemoveAllSoundEffects(){
        if(allSounds.Count != 0){
            if(allSounds.ContainsKey(SoundGroup.SoundEffects)){
                foreach(AudioSource source in allSounds[SoundGroup.SoundEffects]){
                    Destroy(source);
                }
            }
        }
    }

    public float GetVolume(SoundGroup group){
        return groupVolumes.ContainsKey(group) ? groupVolumes[group] : 1f;
    }
    public AudioClip GetAudioClip(string name) {
        if (soundDictionary.TryGetValue(name, out Sound sound)) {
            return sound.clip;
        } else {
            Debug.LogWarning($"Sound '{name}' not found in AudioManager!");
            return null;
        }
    }
}
