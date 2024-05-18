using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class EntityTwinStickLook : MonoBehaviour
{
    #region Champs
    [Header("Player_Components")]
    [SerializeField] CharacterController _controller;
    [Header("Input_Manager_Components")]
    [SerializeField] Managers.InputManager _inputManager;
    [Header("Components_Informations")]
    [SerializeField] float _stickDeadZone;
    [SerializeField] float _gamepadRotateSmoothing;
    //[SerializeField] Inventory _inventory; // Référence au script Inventory
    //[SerializeField] Transform _weaponSpawner;

    //PRIVATES
    Vector2 _direction, _mouseScroll;
    bool _isGamepad;
    //bool _isButtonPressed = _keys.action.IsPressed();

    //PUBLICS

    #endregion
    #region Default Informations
    void Reset()
    {
        _controller = GetComponent<CharacterController>();
        _stickDeadZone = 0.1f;
        _gamepadRotateSmoothing = 1000f;
    }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Awake()
    {
#if UNITY_EDITOR

        //Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
#endif
    }
    // Update is called once per frame
    void Update()
    {
        
        AIM();
    }
    #endregion
    #region Methods
    public void AIM()
    {
        if (_isGamepad)
        {
            if (Mathf.Abs(_direction.x) > _stickDeadZone || Mathf.Abs(_direction.y) > _stickDeadZone)
            {
                Vector3 playerDirection = Vector3.right * _direction.x + Vector3.forward * _direction.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }

        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(_direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 lookDirection = hit.point - transform.position;
                lookDirection.y = 0f; // To ensure the character always looks horizontall
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                _controller.transform.rotation = rotation;
            }
        }
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        _isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
    #endregion
    #region Coroutines
    #endregion
}