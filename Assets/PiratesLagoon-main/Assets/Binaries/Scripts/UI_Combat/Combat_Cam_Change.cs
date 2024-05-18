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
    public class Combat_Cam_Change : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [Header("Cannons Buttons")]
        [SerializeField] GameObject _cannons;
        [Header("Cameras GameObjects")]
        [SerializeField] GameObject _pirateCamera;
        [SerializeField] GameObject _enemyCamera;
        [SerializeField] GameObject _aim;
        [Header("Change Camera Button")]
        [SerializeField] Button _fightButton;
        [SerializeField] TextMeshProUGUI _buttonText;
        [SerializeField] Color _normalButton;
        [SerializeField] Color _pressedButton;
        [SerializeField] string _managementText;
        [SerializeField] string _attackText;
        //PRIVATES
        Fight _currentState;
        Button _btn;
        ColorBlock _thisColor;
        CinemachineVirtualCamera _vCam;
        bool _click = true;
	    //PUBLICS
        #endregion
        #region Enumerator
        public enum Fight
        {
            MANAGEMENT,
            ATTACK
        }
        #endregion
        #region Unity LifeCycle
        private void Start()
        {
            _vCam = _enemyCamera.GetComponentInChildren<CinemachineVirtualCamera>();
            _cannons.SetActive(false);
            _aim.SetActive(false);
            _thisColor = _fightButton.colors;
            _thisColor.normalColor = _normalButton;
            _fightButton.colors = _thisColor;
        }
        // FixUpdate Is called for Physic
        void Update()
        {
            OnStateUpdate();
        }
        #endregion
        #region Methods
        public void OnClick()
        {
            if (_click)
            {
                _click = false;
                TransitionToState(Fight.ATTACK);
                
            }
            else
            {
                _click = true;
                TransitionToState(Fight.MANAGEMENT);
                _thisColor.normalColor = _normalButton;
            }
        }
        #endregion
        #region StatesMachine
        void OnStateEnter()
        {
            switch (_currentState)
            {
                case Fight.MANAGEMENT:
                    _pirateCamera.SetActive(true);
                    _aim.SetActive(false);
                    _thisColor.pressedColor = _normalButton;
                    _thisColor.selectedColor = _normalButton;
                    _fightButton.colors = _thisColor;
                    _buttonText.text = _attackText;
                    _cannons.SetActive(false);
                    break;
                case Fight.ATTACK:
                    _vCam.enabled = true;
                    _aim.SetActive(true);
                    _thisColor.pressedColor = _pressedButton;
                    _thisColor.selectedColor = _pressedButton;
                    _fightButton.colors = _thisColor;
                    _buttonText.text = _managementText;
                    _cannons.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        void OnStateUpdate()
        {
            switch (_currentState)
            {
                case Fight.MANAGEMENT: //Base statement 
                    break;
                case Fight.ATTACK: // State Start to move and make interactions
                    break;
                default:
                    break;
            }
        }

        void OnStateExit()
        {
            switch (_currentState)
            {
                case Fight.MANAGEMENT:
                    _pirateCamera.SetActive(false);
                    break;
                case Fight.ATTACK:
                    _vCam.enabled = false;
                    break;
                default:
                    break;
            }
        }
        public void TransitionToState(Fight nextState)
        {
            OnStateExit();
            _currentState = nextState;
            OnStateEnter();
        }
        #endregion
    }
}