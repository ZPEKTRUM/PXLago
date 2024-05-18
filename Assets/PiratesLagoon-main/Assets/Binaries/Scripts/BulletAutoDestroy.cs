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


namespace Weapons
{
    public class BulletAutoDestroy : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [Header("Bullet Informations")]
        [SerializeField] float _timelife;
        //PRIVATES
        float _startLife;
        //PUBLICS
        #endregion
        #region Default Informations
        void Reset()
        {
            _timelife = 2;
        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
        void Start()
        {
            _startLife = Time.time;
        }

        // Update is called once per frame
        void Update()
        {

            if (Time.time > _startLife + _timelife)
            {
                Destroy(gameObject);
            }   
        }
        #endregion
        #region Methods
        #endregion
    }
}