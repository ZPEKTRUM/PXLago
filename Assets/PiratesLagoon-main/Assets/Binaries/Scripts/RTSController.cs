using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSController : MonoBehaviour
{
    #region Champs
    //INSPECTOR
    [Header("Player_Components")]
    [SerializeField] CharacterController _controller;
    [SerializeField] Grounded _grounded;
    [Header("Player_Informations")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _jumpHeight;
    [SerializeField, Range(0, -11)] float _gravity;
    //PRIVATES
    Managers.InputManager _inputManager;
    Vector3 playerVelocity;
    Vector3 _direction;
    bool _isRunning;
    bool _isJumping;
    bool _groundedPlayer;
    float _currentSpeed;
    //PUBLICS
    public float Gravity { get => _gravity; set => _gravity = value; }
    #endregion
    #region Default Informations
    private void Reset()
    {
        _controller = transform.parent.GetComponentInChildren<CharacterController>();
        _grounded = GetComponentInChildren<Grounded>();

        _gravity = -9.81f;
        _runSpeed = 3f;
        _jumpHeight = 1f;
    }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Start()
    {
        _inputManager = Managers.InputManager.Instance;
    }

    void Update()
    {
        _direction = _inputManager.GetPlayerMovement();
        IsGrounded();
        Moving();
    }
    #endregion
    #region Methods
    // Moving method
    public void Moving()
    {
        // Vector2 moving = PlayerstateMachine Vector2 _dir

        Vector3 move = new Vector3(_direction.x, 0, _direction.y);
        move = _controller.transform.TransformDirection(move);

        if (_direction.magnitude > 0f)
        {
            // Change current speed if player is running
            _currentSpeed = _isRunning ? _runSpeed : _moveSpeed;
            _controller.Move(move * Time.deltaTime * _currentSpeed);
        }

        // Add JumpForce if Jump is pressed
        if (_isJumping && _groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
        }
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    void IsGrounded()
    {
        //groundedPlayer = _controller.isGrounded;
        _groundedPlayer = _grounded.IsGrounded;
        if (_groundedPlayer)
        {
            playerVelocity.y = 0f;
        }
        // Update gravity
        playerVelocity.y += _gravity * Time.deltaTime;
    }
    #endregion
    #region Coroutines
    #endregion 
}
