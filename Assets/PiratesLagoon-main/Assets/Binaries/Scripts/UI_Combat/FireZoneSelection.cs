using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Weapons;
using Managers;


namespace Managers
{
    public class FireZoneSelection : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [Header("Action Map")]
        [SerializeField] InputActionReference _action;
        [Header("spawners Zones")]
        [SerializeField] List<GameObject> _spawners;
        [Header("Bullet")]
        [SerializeField] BulletDirection _bulletPrefab;
        [SerializeField] UnityEvent _event;
        [Header("Fire Informations")]
        [SerializeField] List<LoadingCannon> _loadingCannons;
        [SerializeField] LayerMask _layers;
        [SerializeField] float _fireRate;
        [SerializeField] int _numberOfBullets;
        [Header("Coroutines")]
        [SerializeField] CoroutinesStates _coroutines;
        [Header("Camera")]
        //PRIVATES
        Vector3 playerVelocity;
        Vector3 _direction;
        Coroutine _shootRoutine;
        Transform _spawner;
        Transform _aimCursor; // AIM CURSOR POINT
        int _currentAmmo;
        int _maxAmmo; //after use scriptableObject with serializeField
        bool _isFire;
        float _targetHeight;
        float _targetWidth;
        float _targetDepth;
        //PUBLICS
        public BulletDirection BulletPrefab { get => _bulletPrefab; set => _bulletPrefab = value; }
        public Transform Spawner { get => _spawner; set => _spawner = value; }
        public Transform AimCursor { get => _aimCursor; set => _aimCursor = value; }
        public float FireRate { get => _fireRate; set => _fireRate = value; }
        public int NumberOfBullets { get => _numberOfBullets; set => _numberOfBullets = value; }
        public float TargetHeight { get => _targetHeight; set => _targetHeight = value; }
        public float TargetWidth { get => _targetWidth; set => _targetWidth = value; }
        public float TargetDepth { get => _targetDepth; set => _targetDepth = value; }
        #endregion
        #region Default Informations
        void Reset()
        {
            _maxAmmo = 10; // To test start at 10 ==> after use scriptableObject with serializeField
        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
    
        void Awake()
        {
        
        }
        void Start()
        {
            _maxAmmo = 10; // next time get them to Scriptable player
            _currentAmmo = _maxAmmo;
        }

        // Update is called once per frame
        void Update()
        {
            _isFire = _action.action.WasPerformedThisFrame();
            if(_isFire == true)
            {
                Aim();   
            }
            else
            {
                ShootStop();
            }
        }
        #endregion
        #region Methods
        public void UseAmmo()
        {
            _currentAmmo--;
        }

        public void ReloadAmmo(int addAmmo)
        {
            // Cannon Charging current to 0f
        }

        public void ShootStart()
        {
            if (_shootRoutine != null) return;

            if (_currentAmmo > 0)
            {
                _event.Invoke();
                _shootRoutine = StartCoroutine(_coroutines.ShootCoroutine(this));
            }
        }

        public void ShootStop()
        {
            if (_shootRoutine == null) return;
            StopCoroutine(_shootRoutine);
            _shootRoutine = null;
        }

        public void Aim()
        {
            // Obtenir la position de la souris ou du toucher
            // Get touch ou mouse position
            Vector3 screenPosition = Input.mousePosition;
            if (Input.touchCount > 0)
            {
                screenPosition = Input.GetTouch(0).position;
            }

            // Convertir la position de l'écran en rayon dans le monde 3D
            // Convert positoin to ray in the 3D world
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            // Effectuer le raycast
            // do the RayCast
            if (Physics.Raycast(ray, out hit, _layers))
            {
                // Vérifier si le raycast a touché un collider
                // test if raycast hit collider
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;
                    Collider collider = hit.collider;
                    for (int i = 0; i < _spawners.Count; i++)
                    {
                        bool isActive = _spawners[i].activeSelf;
                        if (isActive == true)
                        {
                            if (collider is BoxCollider boxCollider)
                            {
                                // Si le collider est un BoxCollider, obtenir les dimensions
                                Vector3 size = boxCollider.size;
                                _targetHeight = size.y; // height (axe Y)
                                _targetWidth = size.z; // width (axe Z)
                                _targetDepth = size.x; // width (axe X)
                            }
                            _aimCursor = hit.transform;
                            _spawner = _spawners[i].transform;
                            ShootStart();

                            foreach (var cannonLoadBar in _loadingCannons)
                            {
                                if (cannonLoadBar.tag == _spawners[i].tag)
                                {
                                    cannonLoadBar.Current = 0f;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region Coroutines
        #endregion
    }
}