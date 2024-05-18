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


namespace Managers
{
    public class CannonsSelections : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [Header("Spawers Components")]
        [SerializeField] GameObject _pivotSpawner;
        [SerializeField] GameObject _sideSpawner;
        [SerializeField] GameObject _bowSpawner;

        public GameObject PivotSpawner { get => _pivotSpawner; set => _pivotSpawner = value; }
        public GameObject SideSpawner { get => _sideSpawner; set => _sideSpawner = value; }
        public GameObject BowSpawner { get => _bowSpawner; set => _bowSpawner = value; }

        //PRIVATES
        //PUBLICS
        #endregion
        #region Unity LifeCycle
        // FixUpdate Is called for Physic
        private void Start()
        {
            DisabledSpawner();
        }
        #endregion
        #region Methods
        public void DisabledSpawner()
        {
            _bowSpawner.SetActive(false);
            _sideSpawner.SetActive(false);
            _pivotSpawner.SetActive(false);
        }

        public void Bow()
        {
            _bowSpawner.SetActive(true);
            _sideSpawner.SetActive(false);
            _pivotSpawner.SetActive(false);
        }

        public void Side()
        {
            _bowSpawner.SetActive(false);
            _sideSpawner.SetActive(true);
            _pivotSpawner.SetActive(false);
        }

        public void Pivot()
        {
            _bowSpawner.SetActive(false);
            _sideSpawner.SetActive(false);
            _pivotSpawner.SetActive(true);

        }
        #endregion
    }
}