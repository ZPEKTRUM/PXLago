using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Champs
        //INSPECTOR]
        [Header("Player_Animations")]
        [SerializeField] Animator _animator;
        [Header("Player_Audios")]
        [SerializeField] AudioSource _source;
        [Header("Coroutines_Scripts")]
        //PRIVATES
        bool _death;
        bool _hit;
        bool _isJumping;
        bool _isRunning;
        Vector3 _dir;
        PlayerState _currentState;
        Managers.InputManager _inputManager;

        //PUBLICS
        public bool Death { get => _death; set => _death = value; }
        public bool Hit { get => _hit; set => _hit = value; }
        public PlayerState CurrentState { get => _currentState; set => _currentState = value; }
        #endregion
        #region Enumerator
        public enum PlayerState
        {
            IDLE,
            WALK,
            RUN,
            JUMP,
            HIT,
            DEATH
        }
        #endregion
        #region Default Informations
        void Reset()
        {
            // Call components when LevelDesigner take it to the Hierarchy
            _animator = transform.parent.GetComponentInChildren<Animator>();
            _source = transform.parent.GetComponentInParent<AudioSource>();
        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
        void Start()
        {
            _inputManager = Managers.InputManager.Instance;
            _currentState = PlayerState.IDLE;
        }

        // Update is called once per frame
        void Update()
        {
            OnStateUpdate();
        }
        #endregion
        #region StatesMachine
        void OnStateEnter()
        {
            switch (_currentState)
            {
                case PlayerState.IDLE:
                    _animator.SetFloat("X", 0);
                    _animator.SetFloat("Y", 0);
                    _dir = new Vector3(0, 0, 0);
                    break;
                case PlayerState.WALK:
                    //_animator.SetFloat("X", _dir.x);
                    //_animator.SetFloat("Y", _dir.y);
                    break;
                case PlayerState.RUN:
                    _animator.SetBool("ISRUNNING", true);
                    break;
                case PlayerState.JUMP:
                    _animator.SetFloat("X", 0);
                    _animator.SetFloat("Y", 0);
                    _animator.SetTrigger("WASJUMPED");
                    break;
                case PlayerState.HIT:
                    _animator.SetTrigger("HIT");
                    break;
                case PlayerState.DEATH:
                    _death = true;
                    StopAllCoroutines();
                    _animator.SetFloat("X", 0);
                    _animator.SetFloat("Y", 0);
                    _animator.SetBool("ISDEAD", true);
                    break;
                default:
                    break;
            }
        }
        void OnStateUpdate()
        {
            _dir = _inputManager.GetPlayerMovement();
            switch (_currentState)
            {
                case PlayerState.IDLE: //Base statement
                    if (_dir.magnitude > 0)
                    {
                        //Debug.Log("Is Moving");
                        TransitionToState(PlayerState.WALK);
                    }
                    else if (_isJumping)
                    {
                        //Debug.Log("Is Jumping");
                        TransitionToState(PlayerState.JUMP);
                    }
                    else if (_hit == true)
                    {
                        //Debug.Log("Is crouching");
                        TransitionToState(PlayerState.HIT);
                    }
                    //_entityMove.Movements(_dir);
                    break;
                case PlayerState.WALK: // State Start to move and make interactions
                    _animator.SetFloat("X", _dir.x);
                    _animator.SetFloat("Y", _dir.y);
                    if (_dir.magnitude <= 0f)
                    {
                        TransitionToState(PlayerState.IDLE);
                    }

                    if (_isRunning)
                    {
                        TransitionToState(PlayerState.RUN);
                    }
                    else if (_isJumping)
                    {
                        TransitionToState(PlayerState.JUMP);
                    }
                    else if (_hit == true)
                    {
                        TransitionToState(PlayerState.HIT);
                    }

                    break;
                case PlayerState.RUN:
                    if (_isRunning == false && _dir.magnitude < 0.1f || _dir.magnitude == 0f)
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                    else if (_isRunning == false && _dir.magnitude > 0.1f)
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                    else if (_isJumping)
                    {
                        TransitionToState(PlayerState.JUMP);
                    }

                    //_entityRun.Run();
                    break;
                case PlayerState.JUMP:
                    if (_dir.magnitude == 0f)
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                    else if (_dir.magnitude > 0.1f)
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                    break;
                case PlayerState.HIT:
                    break;
                case PlayerState.DEATH:
                    break;
                default:
                    break;
            }
        }

        void OnStateExit()
        {
            switch (_currentState)
            {
                case PlayerState.IDLE:
                    break;
                case PlayerState.WALK:
                    _animator.SetFloat("X", 0);
                    _animator.SetFloat("Y", 0);
                    _dir = new Vector3(0, 0, 0);
                    break;
                case PlayerState.RUN:
                    _animator.SetBool("ISRUNNING", false);
                    break;
                case PlayerState.JUMP:
                    break;
                case PlayerState.HIT:
                    break;
                case PlayerState.DEATH:
                    break;
                default:
                    break;
            }
        }
        public void TransitionToState(PlayerState nextState)
        {
            OnStateExit();
            _currentState = nextState;
            OnStateEnter();
        }
        #endregion
    }
}