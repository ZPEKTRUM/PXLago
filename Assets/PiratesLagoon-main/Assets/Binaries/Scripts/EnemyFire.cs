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
using Managers;


namespace Weapons
{
    public class EnemyFire : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [Header("Bullet")]
        [SerializeField] BulletDirection _bulletPrefab;
        [SerializeField] Transform _aim;
        [Header("Fire Informations")]
        [SerializeField] LayerMask _layers;
        [SerializeField] float _fireRate;
        [SerializeField] int _numberOfBullets;
        [Header("Coroutines")]
        [SerializeField] CoroutinesStates _coroutines;
        //PRIVATES

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
        
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

	    // Fi
	    void FixedUpdate()
        {
        
        }
        void LateUpdate()
        {
        
        }
        #endregion
        #region Methods
        void MyMethod()
        {
        
        }
        #endregion
        #region Coroutines
	    IEnumerator EndCoroutine()
        {
            throw new NotImplementedException();   
        }
        #endregion
    }
}