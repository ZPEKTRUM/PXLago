using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Manager
{
    public class UiManager : MonoBehaviour
    {
        [Serializable] public class GameObjectDictionary : SerializableDictionary<string, GameObject>{}
        [SerializeField] private GameObjectDictionary _UIObjects;

        [Serializable] public class AnimatorDictionary : SerializableDictionary<string, Animator>{}
        [SerializeField] private AnimatorDictionary _Animators;

        [Header("Animation clips")]
        [SerializeField] private AnimationClip _button0_hide;
        [SerializeField] private AnimationClip _button1_hide;
        [SerializeField] private AnimationClip _button2_hide;
        [SerializeField] private AnimationClip _button3_hide;


        public GameObject GetGameObject(string goName)
        {
            return _UIObjects[goName];
        }

        public T GetComponentInGameObject<T>(string goName)
        {
            if(_UIObjects[goName] == null)
                throw new Exception("UiManagerExecption : The UiManager doesn't contain a GameObject called "+goName+"");

            if (_UIObjects[goName].GetComponent<T>() == null)
                throw new Exception("UiManagerExecption : No TMP_Text found in _UIObjects["+goName+"]");
            
            return _UIObjects[goName].GetComponent<T>();
        }
        public void Play(string animatorName, AnimationClip clip)
        {
            _Animators[animatorName].Play(clip.name);
        }

        public Animator GetAnimator(string animName)
        {
            return _Animators[animName];
        }

        public void HideButtonsAnim(string animatorName)
        {
            _Animators[animatorName].SetBool("CanShowButtons", false);
        }
        public void ShowButtonsAnim(string animatorName, int nbOptions)
        {
            _Animators[animatorName].SetInteger("nbOptions", nbOptions);
            _Animators[animatorName].SetBool("CanShowButtons", true);
        }

        public void AddaptButton(int nbOpt)
        {
            
        }
    }
}
