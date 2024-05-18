using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Grounded : MonoBehaviour
{
    #region Fields
    //INSPECTOR
    [Header("Character_Components")]
    [SerializeField] CharacterController _controller;
    [Header("Character_Fields")]
    [SerializeField] bool _isGrounded;
    [SerializeField] float _rayDistance;
    [SerializeField, Range(30, 60)] float _rotationXLimit;
    [Header("Layers Informations")]
    [SerializeField] LayerMask _layers;
    //PRIVATES
    Vector3 _rayStart;
    //PUBLICS
    public bool IsGrounded { get => _isGrounded; }
    public Vector3 RayStart { get => _rayStart; set => _rayStart = value; }
    #endregion
    #region Default Informations
    // Start is called before the first frame update
    private void Reset()
    {
        _controller = GetComponentInParent<CharacterController>();
        _isGrounded = false;
        _rayDistance = 0.3f;
    }
    #endregion
    #region Unity LifeCycle
    private void Update()
    {
        _rayStart = transform.position;

        //Make Ray DownWard
        if (Physics.Raycast(_rayStart, Vector3.down, out RaycastHit hit, _rayDistance, _layers))
        {
            // Ray is Green
            _isGrounded = true;
            Debug.DrawRay(_rayStart, Vector3.down * _rayDistance, Color.green);
        }
        else
        {
            // Ray is Red
            _isGrounded = false;
            Debug.DrawRay(_rayStart, Vector3.down * _rayDistance, Color.red);
        }
    }

    #endregion
    #region Methods
    void IsGroundedGravity()
    {
        // take info grounded's characterController
        _isGrounded = _controller.isGrounded;
    }
    #endregion
}