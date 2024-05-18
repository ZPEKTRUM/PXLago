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
    public class LoadingCannon : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [Header("Images Informations")]
        [SerializeField] GameObject _loadingBar;
        [SerializeField] Image _greenBar;
        [SerializeField] Image _redBar;
        [Header("Endurance Informations")]
        [SerializeField] float _maxBar;
        [SerializeField] float _decal;
        [Header("Buttons")]
        [SerializeField] Button _button;
        [Header("Cannons Component")]
        [SerializeField] CannonsSelections _cannon;
        //PRIVATES
        float _upTime;
        float _current;
        bool _timeExhausted;
        Coroutine _wheelCoroutine;
        //PUBLICS
        public float UpTime { get => _upTime; set => _upTime = value; }
        public float Current { get => _current; set => _current = value; }
        public bool TimeExhausted { get => _timeExhausted; set => _timeExhausted = value; }
        #endregion
        #region Default Informations
        void Reset()
        {
            _maxBar = 100f;
            _decal = 0.07f;
        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
        void Start()
        {
            _upTime = 10f;
            _current = 0f;
            _button.enabled = false;
            _timeExhausted = false;
        }

        // Update is called once per frame
        void Update()
        {
            LoadingTheBar();
        }
        #endregion
        #region Methods
        public void LoadingTheBar()
        {
            if (_current < _maxBar)
            {
                _cannon.DisabledSpawner();
                _current += _upTime * Time.deltaTime;
                _redBar.fillAmount = _current / _maxBar + _decal;

                if (_current >= _maxBar)
                {
                    _greenBar.enabled = true;
                    _timeExhausted = true;
                    _button.enabled = true;
                }
            }
            _greenBar.fillAmount = _current / _maxBar;
        }
        #endregion
    }
}