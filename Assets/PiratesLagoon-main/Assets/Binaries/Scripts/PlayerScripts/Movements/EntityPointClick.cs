using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;


public class EntityPointClick : MonoBehaviour
{
    #region Champs
    //INSPECTOR
    [Header("Player_Components")]
    [SerializeField] CharacterController _controller;
    [SerializeField] Grounded _grounded;
    [Header("Player_Informations")]
    [SerializeField] float _lookRotationSpeed;
    [SerializeField, Range(0, -11)] float _gravity;
    [Header("Click to Move Components")]
    [SerializeField] ParticleSystem _particles;
    [SerializeField] LayerMask _layerMask;
    [Header("Animations Components")]
    [SerializeField] Animator _animator;
    //PRIVATES
    PlayerInputs _input;
    NavMeshAgent _navMesh;
    Vector3 playerVelocity;
    Vector3 _direction;
    bool _groundedPlayer;
    
    //PUBLICS
    public float Gravity { get => _gravity; set => _gravity = value; }
    #endregion
    #region Default Informations
    private void Reset()
    {
        _controller = transform.parent.GetComponentInChildren<CharacterController>();
        _grounded = GetComponentInChildren<Grounded>();
        _animator = GetComponentInChildren<Animator>();
        _gravity = -9.81f;
        _lookRotationSpeed = 8f;
    }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Awake()
    {
        _navMesh = GetComponentInParent<NavMeshAgent>();

        _input = new PlayerInputs();
        AssignInput();

    }

    void AssignInput()
    {
        _input.Player.Fire.performed += ctx => Moving();
    }

    private void Start()
    {

    }

    void Update()
    {
        IsGrounded();
        FaceTarget();
        SetAnimation();     
    }
    #endregion
    #region Methods
    // Moving method
    public void Moving()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, _layerMask))
        {
            _navMesh.destination = hit.point;
            if (_particles)
            {
                Instantiate(_particles, hit.point += new Vector3(0, 0.1f, 0), _particles.transform.rotation);
            }
        }
    }

    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }

    void FaceTarget()
    {
        _direction = (_navMesh.destination - _controller.transform.position).normalized;
        Quaternion lookRtation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z ));
        _controller.transform.rotation = Quaternion.Slerp(transform.rotation, lookRtation, Time.deltaTime * _lookRotationSpeed);
    }

    void SetAnimation()
    {
        if (_navMesh.velocity == Vector3.zero)
        {
            _animator.SetFloat("X", 0);
            _animator.SetFloat("Y", 0);
        }
        else
        {
            _animator.SetFloat("X", 0);
            _animator.SetFloat("Y", 0.7f);
        }
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