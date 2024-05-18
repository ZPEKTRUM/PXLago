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


namespace ScriptableObjects
{
    public class StaminaTask : MonoBehaviour
    {
        #region Champs
        [Header("Images Informations")]
        [SerializeField] Tools.EnterToTask _enterTask;
        [SerializeField] GameObject _staminaWheel;
        [SerializeField] Image _greenWheel;
        [SerializeField] Image _redWheel;
        [Header("Endurance Informations")]
        [SerializeField] float _maxStamina;
        [SerializeField] float _upStamina;
        [SerializeField] float _downStamina;
        [SerializeField] float _decal;
        [Header("Coroutines Informations")]
        [SerializeField] Managers.CoroutinesStates _coroutines;
        [SerializeField] float _coroutineTime;
        //Private
        float _stamina;
        bool _staminaExhausted;
        Coroutine _wheelCoroutine;
        GameObject _go;

        public float Stamina { get => _stamina; set => _stamina = value; }
        public float CoroutineTime { get => _coroutineTime; set => _coroutineTime = value; }
        public bool StaminaExhausted { get => _staminaExhausted; set => _staminaExhausted = value; }
        public GameObject StaminaWheel { get => _staminaWheel; set => _staminaWheel = value; }
        #endregion
        #region Default Informations
        void Reset()
        {
            _maxStamina = 100f;
        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update

        void Awake()
        {

        }
        void Start()
        {
            _stamina = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            UsingStamina(_go);
        }
        #endregion
        #region Methods
        public void UsingStamina(GameObject go)
        {
            _go = go;
            if (_stamina < _maxStamina)
            {
                //_stamina += _upStamina * Time.deltaTime;
                _stamina += _upStamina * Time.deltaTime;
                _redWheel.fillAmount = _stamina / _maxStamina + _decal;

                if (_stamina >= _maxStamina)
                {
                    _wheelCoroutine = _coroutines.StartCoroutine(_coroutines.WheelCoroutine(this, _go));
                    _staminaExhausted = false;
                    _greenWheel.enabled = true;
                }
            }
            _greenWheel.fillAmount = _stamina / _maxStamina;
        }
        public void UsingWrongStamina(GameObject go)
        {
            _go = go;
            _stamina = _stamina / 6;
            if (_stamina < _maxStamina)
            {
                //_stamina += _upStamina * Time.deltaTime;
                _stamina += _upStamina * Time.deltaTime;
                _redWheel.fillAmount = _stamina / _maxStamina + _decal;

                if (_stamina >= _maxStamina)
                {
                    _wheelCoroutine = _coroutines.StartCoroutine(_coroutines.WheelCoroutine(this, _go));
                    _staminaExhausted = false;
                    _greenWheel.enabled = true;
                }
            }
            _greenWheel.fillAmount = _stamina / _maxStamina;
        }
        public void UsedStamina()
        {
            _staminaWheel.SetActive(true);

            if (_stamina > 0)
            {
                //_stamina -= _downStamina * Time.deltaTime;
                _stamina -= _downStamina * Time.deltaTime;
            }
            else
            {
                _staminaExhausted = true;
                _greenWheel.enabled = false;
            }
            _redWheel.fillAmount = _stamina / _maxStamina + _decal;
        }
        #endregion
        #region Coroutines

        #endregion
    }
}