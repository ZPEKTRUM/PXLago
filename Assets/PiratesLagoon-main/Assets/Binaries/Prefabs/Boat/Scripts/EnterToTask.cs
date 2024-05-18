using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Tools
{
    public class EnterToTask : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [Header("LayerMask Components")]
        [SerializeField] LayerMask _layerMask; // To verify what sort of GameObject for later
        [Header("This Object TagList Components")]
        [SerializeField] ScriptableObjects.TagsList _tagList;
        [Header("Stamina Objects Components")]
        [SerializeField] GameObject _staminaWheel;
        [SerializeField] ScriptableObjects.StaminaTask _stamina;
        [SerializeField] Managers.CoroutinesStates _coroutines;
        [Header("Events Exit Components")]
        [SerializeField] UnityEvent _event;
        [SerializeField] Animator _animator;
        [Header("Informations Fields Components")]
        [SerializeField] int _xp;
        [SerializeField] string _scene;
        //PRIVATES
        Coroutine _xpCoroutine;
        Collider _tagCollider;
        Player.DragDrop _dragDrop;
        Rigidbody _rb;
        string _name;
        string _tag;
        string _thisTag;
        bool _enemy;
        //PUBLICS

        #endregion
        #region Enumerator
        #endregion
        #region Default Informations
        void Reset()
        {
            _tagList = GetComponent<ScriptableObjects.TagsList>();
            _xp = 10;
            _enemy = false;
            _scene = "CombatScene";
        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
        // Update is called once per frame
        #endregion
        #region Methods
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null) return;

            // Get required Components 
            var tagList = other.attachedRigidbody.gameObject.GetComponent<ScriptableObjects.TagsList>();
            _rb = other.attachedRigidbody.gameObject.GetComponent<Rigidbody>();
            _tagCollider = other.attachedRigidbody.gameObject.GetComponent<CapsuleCollider>();
            _name = other.gameObject.name;
            var go = other.gameObject;

            foreach (var tag in tagList.Tags) { _tag = tag; } // Get gameobject.Tag
            foreach (var thisTag in _tagList.Tags) { _thisTag = thisTag; } // Get This Tag

            if (_enemy == true)
            {
                // Saving Player Stats

                // Start event (musique, effects, ...)
                // LoadScene To Action
                SceneManager.LoadScene(_scene);
            }
            else
            {
                // desactivate GameObject Collider && activate the Wheel
                _tagCollider.enabled = false;
                _staminaWheel.SetActive(true);
                _animator.SetBool("ONTASK", true);

                // test gameObject aqual This
                if (_tag == _thisTag)
                {
                    // Send to Wheel current stamina to start decount
                    _stamina.UsingStamina(go);
                }
                else
                {
                    // Send to Wheel current stamina to start decount
                    _stamina.UsingWrongStamina(go);
                }
            }  
        }

        public void FinishedTask(GameObject go)
        {
            _event.Invoke();
            _animator.SetBool("ONTASK", false);
            var _go = go.GetComponent<Transform>();
            _go.transform.position = new Vector3(0, 0.2f, 0);

            CapsuleCollider collider = go.GetComponent<CapsuleCollider>();
            collider.enabled = true;
            if (_tag != _thisTag)
            {
                _xp = _xp / 3;
                // Send to an other script _xp 
            }
            else
            {
                // Send to an other script _xp 
            }

        }
    #endregion
    #region Coroutines
    #endregion
    }
}