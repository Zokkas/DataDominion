using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject gunHoldster;
    private Rigidbody2D rigidbody;
    private PlayerInput Actions;
    private Action<InputAction.CallbackContext> shotPressedCallback;
    private Action<InputAction.CallbackContext> reloadPressedCallback;
    private GameState gameState;
    private GunBase currentGun;
    private Vector2 aimDirection;

    private bool isFiring = false;
    [SerializeField] private float moveSpeed = 5f;
    void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        Actions = InputActions.Instance.Actions;
        gameState = GameState.Instance;
        shotPressedCallback += ShootSingle;
        reloadPressedCallback += ReloadGun;
    }
    void Start(){
        currentGun = GetComponentInChildren<GunBase>();
        // Debug.Log("currentGun is: " + currentGun);
    }
    void OnEnable(){
        Actions.Player.Shoot.performed += shotPressedCallback;
        Actions.Player.Reload.performed += reloadPressedCallback;
    }
    void OnDisable(){
        Actions.Player.Shoot.performed -= shotPressedCallback;
        Actions.Player.Reload.performed -= reloadPressedCallback;
    }

    void Update(){
        // Input (Shooting auto)
        if(!gameState.IsPaused && currentGun != null){
            if(currentGun.GunData.fireMode == GunData.FireMode.Auto)
                currentGun.Shoot(Actions.Player.Shoot.ReadValue<float>() > 0.1f, false);
        }
        if(!gameState.IsPaused){
            SetGunAim();
        }
    }
    void FixedUpdate(){
        // Input (Movement)
        if(!gameState.IsPaused){
            MovePlayer();
        }
    }
    void MovePlayer(){
        Vector2 moveDirection = Actions.Player.Movement.ReadValue<Vector2>();
        rigidbody.MovePosition(rigidbody.position + moveDirection * moveSpeed * Time.deltaTime);
    }
    void SetGunAim(){
        aimDirection = Vector2.zero;
        Vector2 mousePos = Actions.Player.MousePos.ReadValue<Vector2>();
        Vector2 controllerRSInput = Actions.Player.ControllerRSDirection.ReadValue<Vector2>();
        if (controllerRSInput.magnitude > 0.2f){
            aimDirection = controllerRSInput.normalized;
        } else{
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            aimDirection = (worldMousePos - transform.position).normalized;
            //Debug.Log("aimDirection: "+aimDirection);
        }
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        gunHoldster.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    void ShootSingle(InputAction.CallbackContext context){
        if(currentGun != null)
            currentGun.Shoot(false, true);
    }
    void ReloadGun(InputAction.CallbackContext context){
        if(currentGun != null)
            currentGun.Reload();
    }
}
