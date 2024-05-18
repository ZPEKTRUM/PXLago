
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity {

    [SerializeField] private float moveSpeed;

    private Vector3 velocityVector;
    private Rigidbody _rigidbody;
    private Character_Base characterBase;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        characterBase = GetComponent<Character_Base>();
    }

    public void SetVelocity(Vector3 velocityVector) {
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate() {
        _rigidbody.velocity = velocityVector * moveSpeed;

        //characterBase.PlayMoveAnim(velocityVector);
    }

    public void Disable() {
        this.enabled = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void Enable() {
        this.enabled = true;
    }

}
