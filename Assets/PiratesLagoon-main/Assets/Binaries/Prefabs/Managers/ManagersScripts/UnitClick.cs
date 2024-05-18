using UnityEngine;
using UnityEngine.Events;


namespace Managers
{
    public class UnitClick : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [SerializeField] LayerMask _layerMaskClickable;
        [SerializeField] LayerMask _layerMaskGround;
        [SerializeField] UnityEvent _groundMarker;
        //PRIVATES
        Camera _camera;
        Managers.InputManager _inputManager;
        bool _click;
        bool _multiple;
	    //PUBLICS
  
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

            _camera = Camera.main;
        }
        void Start()
        {
            _inputManager = Managers.InputManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            //_click = _inputManager.GetClickLeft();
            //_multiple = _inputManager.GetMultipleSelection();
            OnClick();
        }

        #endregion
        #region Methods
        void OnClick()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMaskClickable))
                {
                    Debug.Log($"hit = {hit.collider.gameObject}");
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                    }
                    else
                    {
                        UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                    }
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        UnitSelections.Instance.DeselectAll();
                    }  
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMaskGround))
                {
                    _groundMarker.Invoke();

                }
            }  
        }
        #endregion
        #region Interfaces
        #endregion
    }
}