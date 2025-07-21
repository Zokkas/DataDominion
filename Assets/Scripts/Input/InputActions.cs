using System;
using UnityEngine;

public class InputActions : MonoBehaviour
{
    public static InputActions Instance;
    public PlayerInput Actions;
    void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Actions = new PlayerInput();
    }
    void OnEnable(){
        Actions.Enable();
        Actions.Player.Enable();
        Actions.UI.Enable();
    }
    void OnDisable(){
        Actions.Disable();
        Actions.Player.Disable();
        Actions.UI.Disable();
    }
}
