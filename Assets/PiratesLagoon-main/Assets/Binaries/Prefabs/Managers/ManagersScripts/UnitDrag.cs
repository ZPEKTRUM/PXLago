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
    public class UnitDrag : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [SerializeField] RectTransform _boxVisual;
        //PRIVATES
        Camera _camera;
        Rect _selectionBox;
        Vector2 _startPosition;
        Vector2 _endPosition;
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
            _camera = Camera.main;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            DrawVisual();
        }

        // Update is called once per frame
        void Update()
        {
            // When click
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
                _selectionBox = new Rect();
            }
            // When Dragging
            if (Input.GetMouseButton(0))
            {
                _endPosition = Input.mousePosition;
                DrawVisual();
                DrawSelection();
            }
            // When Release click
            if (Input.GetMouseButtonUp(0))
            {
                SelectUnits();
                _startPosition = Vector2.zero;
                _endPosition = Vector2.zero;
                DrawVisual();
            }
        }
 
        #endregion
        #region Methods
        void DrawVisual()
        {
            Vector2 boxStart = _startPosition;
            Vector2 boxEnd = _endPosition;
            Vector2 boxCenter = (boxStart + boxEnd) /2;
            _boxVisual.position = boxCenter;
            Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x),
                                          Mathf.Abs(boxStart.y - boxEnd.y));

            _boxVisual.sizeDelta = boxSize;
        }

        void DrawSelection()
        {
            // Calculation X
            if (Input.mousePosition.x < _startPosition.x)
            {
                // Dragging left
                _selectionBox.xMin = Input.mousePosition.x;
                _selectionBox.xMax = _startPosition.x;
            }
            else
            {
                // Dragging right
                _selectionBox.xMin = _startPosition.x;
                _selectionBox.xMax = Input.mousePosition.x;
            }
            // Calculation Y
            if (Input.mousePosition.y < _startPosition.y )
            {
                // Dragging down
                _selectionBox.yMin = Input.mousePosition.y;
                _selectionBox.yMax = _startPosition.y;
            }
            else
            {
                // Dragging up
                _selectionBox.yMin = _startPosition.y;
                _selectionBox.yMax = Input.mousePosition.y;
            }
        } 

        void SelectUnits()
        {
            // Lop thru all the units
            foreach (var unit in UnitSelections.Instance.UnitList)
            {
                // if unit is within bounds of the selection rect
                if (_selectionBox.Contains(_camera.WorldToScreenPoint(unit.transform.position)))
                {
                    // if any unit is within the selection add them to selection
                    UnitSelections.Instance.DragSelect(unit);
                }
            }
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