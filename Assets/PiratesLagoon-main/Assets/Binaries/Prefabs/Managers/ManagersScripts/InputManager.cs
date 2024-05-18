using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        //PRIVATES
        static InputManager _instance;
        PlayerInputs _playerInputs;
        int _pref;
        //PUBLICS
        // ReadOnly Static Input Manager
        public static InputManager Instance { get => _instance; }
        #endregion
        #region Default Informations
        void Reset()
        {

        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
            _playerInputs = new PlayerInputs();
        }
        void Start()
        {
            //Call PlayerPref to InputAction and Override mouse Look Vector2.y true or false
            _pref = PlayerPrefs.GetInt("Inverted");
            if (_pref == 1)
            {
                _playerInputs.Player.Look.ApplyBindingOverride(new InputBinding { overrideProcessors = "invertVector2(invertX=false,invertY=false)" });
            }
            else
            {
                _playerInputs.Player.Look.ApplyBindingOverride(new InputBinding { overrideProcessors = "invertVector2(invertX=false,invertY=true)" });
            }
        }
        #endregion
        #region PlayerInputs
        private void OnEnable()
        {
            _playerInputs.Enable();
        }

        private void OnDisable()
        {
            _playerInputs.Disable();
        }
        #endregion
        #region MovementInput Methods
        public Vector2 GetPlayerMovement()
        {
            return _playerInputs.Player.Move.ReadValue<Vector2>();
        }
        #endregion
        #region MouseInput Methods
        public Vector2 GetMouseDelta()
        {
            return _playerInputs.Player.Look.ReadValue<Vector2>();
        }
        #endregion
        #region Actions Methods
        public bool GetClickLeft()
        {
            return _playerInputs.Player.RTS.IsPressed();
        }

        public bool GetMultipleSelection()
        {
            return _playerInputs.Player.Multiple.IsPressed();
        }

        public bool DragAndDrop()
        {
            return _playerInputs.Player.DragDrop.IsPressed();
        }

        public bool GetFire()
        {
            return _playerInputs.Player.Fire.IsPressed();
        }
        #endregion
    }
}