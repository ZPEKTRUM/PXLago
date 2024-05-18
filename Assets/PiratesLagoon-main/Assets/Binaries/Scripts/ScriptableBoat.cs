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


namespace Datas
{
    public class ScriptableBoat : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        [Header("Character Informations")]
        [SerializeField] string _name;
        [SerializeField] string _description;
        [Header("Health")]
        [SerializeField] int _currentHealt;
        [SerializeField] int _initialHealth;
        [Header("Caracteristics")]
        [SerializeField] int _damage;
        [SerializeField] float _speed;
        [Header("Screen Name")]
        [SerializeField] string _screenName;
        [SerializeField] int _ammo;
        //PRIVATES


        //PUBLICS
        public int CurrentHealt { get => _currentHealt; set => _currentHealt = value; }
        public int InitialHealth { get => _initialHealth; set => _initialHealth = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public string ScreenName { get => _screenName; set => _screenName = value; }
        public int Ammo { get => _ammo; set => _ammo = value; }
        #endregion
    }
}