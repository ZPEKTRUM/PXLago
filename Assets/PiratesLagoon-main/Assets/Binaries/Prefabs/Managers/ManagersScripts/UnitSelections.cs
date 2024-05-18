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
    public class UnitSelections : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [SerializeField] List<GameObject> _unitList = new List<GameObject>();
        [SerializeField] List<GameObject> _unitSelected = new List<GameObject>();
        [SerializeField] EntityPointClick _entityPointClick;
        [SerializeField] int _maxUnits;
        [SerializeField] int _gameObjectChildrange;
        //PRIVATES
        private static UnitSelections _instance;
        //PUBLICS
        public static UnitSelections Instance { get {return _instance; }  }
        public List<GameObject> UnitList { get => _unitList; set => _unitList = value; }
        public List<GameObject> UnitListSelected { get => _unitSelected; set => _unitSelected = value; }
        #endregion
        #region Default Informations
        void Reset()
        {
            _maxUnits = 6;
        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }
         
        void Start()
        {
            foreach (var unit in _unitList)
            {
                //unit.transform.GetChild(7).gameObject.SetActive(false);
                unit.transform.GetChild(_gameObjectChildrange).gameObject.SetActive(false);
            }
        }
        #endregion
        #region Methods
        public void ClickSelect(GameObject unitToadd)
        {
            DeselectAll();
            _unitSelected.Add(unitToadd);
            unitToadd.transform.GetChild(_gameObjectChildrange).gameObject.SetActive(true);
            unitToadd.GetComponentInChildren<EntityPointClick>().enabled = true;
        }

        public void ShiftClickSelect(GameObject unitToadd)
        {
            if (!_unitSelected.Contains(unitToadd ))
            {
                _unitSelected.Add(unitToadd);
                unitToadd.transform.GetChild(_gameObjectChildrange).gameObject.SetActive(true);
                unitToadd.GetComponentInChildren<EntityPointClick>().enabled = true;
            }
            else
            {
                unitToadd.transform.GetChild(_gameObjectChildrange).gameObject.SetActive(false);
                _unitSelected.Remove(unitToadd);
            }
        }

        public void DragSelect(GameObject unitToadd)
        {
            if (!_unitSelected.Contains(unitToadd))
            {
                _unitSelected.Add(unitToadd);
                unitToadd.transform.GetChild(_gameObjectChildrange).gameObject.SetActive(true);
                unitToadd.GetComponentInChildren<EntityPointClick>().enabled = true;
            }
        }

        public void DeselectAll()
        {
            foreach (var unit in _unitSelected)
            {
                unit.transform.GetChild(_gameObjectChildrange).gameObject.SetActive(false);
                unit.GetComponent<EntityPointClick>().enabled = false;
            } 
            _unitSelected.Clear();
        } 

        public void Deselect(GameObject unitToDeselect)
        {

        }
        #endregion
        #region Coroutines
        #endregion
    }
}