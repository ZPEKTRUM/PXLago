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
using MyNameSpace;
using Health;

namespace Weapons
{
public class BulletDirection : MonoBehaviour
{
        #region Champs
        //INSPECTOR
        [Header("Bullet Informations")]
        [SerializeField] Rigidbody _rb;
        [SerializeField] float _speed;
        [SerializeField] int _damage;
        [Header("Tags informations")]
        [SerializeField] string _gameObjectTag;
        [Header("Events")]
        [SerializeField] UnityEvent _event;
        //PRIVATES
        Vector3 _direction;
        //Health _health;
        //PUBLICS
        #endregion
        #region Default Informations
        void Reset()
        {
            _speed = 100f;
            _damage = 10;
        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
        // Update is called once per frame
        void Update()
        {
            _rb.AddForce(_direction * _speed);
        }

        #endregion
        #region Methods
        internal void SetDirection(Vector3 aimCursor)
        {
            _direction = (aimCursor - transform.position).normalized;

        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Qui je touche = {collision.gameObject.tag}");
            bool tagCompare = collision.gameObject.CompareTag(_gameObjectTag);
            if (tagCompare == true)
            {
                _event.Invoke();
                var effect = collision.gameObject.GetComponentInChildren<ParticleSystem>();
                effect.Play();
                var sound = collision.gameObject.GetComponentInChildren<AudioSource>();
                sound.Play();
                collision.gameObject.GetComponent<DamageZoneHealth>().TakeDamage(_damage);
            }
            else
            {
                _event.Invoke();
            }   
        }
        #endregion
    }
}