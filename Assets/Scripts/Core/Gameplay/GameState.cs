using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    // Replace with an enum
    public bool IsPaused = false;
    void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
