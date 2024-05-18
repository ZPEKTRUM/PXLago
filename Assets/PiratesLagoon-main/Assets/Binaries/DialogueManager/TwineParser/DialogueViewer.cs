using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TwineParser;

namespace DialogueManager
{
    public class DialogueViewer : MonoBehaviour
    {
        [Header("DialogueObjects")]
        [SerializeField] private DialoguePlayerData _playerData;
        [Serializable] public class CharacterDictionary : SerializableDictionary<string, DialogueCharacter>{}
        [SerializeField] private CharacterDictionary _characters;
        
        [Header("UiManager")]
        [SerializeField] private UI_Manager.UiManager _uiManager;
        [SerializeField] private string _mainDialogueText;
        [SerializeField] private string _characterName;
        [SerializeField] private string _characterArtIllu;
        [SerializeField] private string _optionButton0;
        [SerializeField] private string _optionButton1;
        [SerializeField] private string _optionButton2;
        [SerializeField] private string _optionButton3;
        [SerializeField] private string _gradeText;
        [SerializeField] private string _endButton;
        
        [Header("Animation")]
        [SerializeField] private string _dialogueAnimator;
        [SerializeField] private AnimationClip _endClip;

        #region private fields
        private TweeParser _tweeParser;
        private Button[] _optionButtons = new Button[4];
        private TweeParser.TweePassage _currentPassage = null;
        private TweeParser.TweePassage _nextPassage;
        private List<string> _seenPassages;
        #endregion

        private void Start() 
        {
            _optionButtons[0] = _uiManager.GetComponentInGameObject<Button>(_optionButton0);
            _optionButtons[1] = _uiManager.GetComponentInGameObject<Button>(_optionButton1);
            _optionButtons[2] = _uiManager.GetComponentInGameObject<Button>(_optionButton2);
            _optionButtons[3] = _uiManager.GetComponentInGameObject<Button>(_optionButton3);
            _uiManager.GetComponentInGameObject<Button>(_endButton).onClick.AddListener(OnEndButton); 

            _tweeParser = GetComponent<TweeParser>();
              
            _tweeParser.Parse();
                
            StartDialogue();
        }

        private void Update() 
        {
            if (_currentPassage != null && !_currentPassage._isEnd)
                _playerData.time += Time.deltaTime;
        }

        private void StartDialogue()
        {
            _seenPassages = new List<string>();
            _currentPassage = _tweeParser._passages[_tweeParser._firstPassage];
            _seenPassages.Add(_currentPassage._title);

            UpdateDialogue();
            UpdateButtons(_currentPassage._links.Count);

            _playerData.time = 0f;
        }

        private void UpdateDialogue()
        {
            // Check if we see this passage for the first time
            if (!_seenPassages.Contains(_currentPassage._title))
            {
                _seenPassages.Add(_currentPassage._title);
                _playerData.discovery++;
            }

            // Update Main Text
            _uiManager.GetComponentInGameObject<TMP_Text>(_mainDialogueText).text = _currentPassage._body;

            // Update Character
            DialogueCharacter character = _characters[_currentPassage._character];
            _uiManager.GetComponentInGameObject<TMP_Text>(_characterName).text = character._characterName;
            _uiManager.GetComponentInGameObject<TMP_Text>(_characterName).color = character._characterColor;
            _uiManager.GetComponentInGameObject<Image>(_characterArtIllu).sprite = character._characterSprite;

            //Update Buttons
            string[] buttonText = new string[_currentPassage._links.Count];
            for (int i = 0; i < _currentPassage._links.Count; i++)
            {
                buttonText[i] = _currentPassage._links[i]._linkText;
                string nextPassage = _currentPassage._links[i]._passageName;
                if (nextPassage == "END")
                {
                    _optionButtons[i].onClick.AddListener(()=>
                    {
                        StartCoroutine("GoToEnd");
                    });
                }
                else {
                    _optionButtons[i].onClick.AddListener(()=>
                    {
                        GoToPassage(nextPassage);
                    });
                }
            }
            IEnumerator coroutine = UpdateOptionButtonsText(buttonText);
            StartCoroutine(coroutine);
        }

        private void OnEndButton()
        {
            SceneManager.LoadScene("SampleScene");
        }

        private void GoToPassage(string nextPassage)
        {
            _optionButtons[0].onClick.RemoveAllListeners();
            _optionButtons[1].onClick.RemoveAllListeners();
            _optionButtons[2].onClick.RemoveAllListeners();
            _optionButtons[3].onClick.RemoveAllListeners();

            // TODO : Hide button here and start coroutine to update and show buttons again
            _nextPassage = _tweeParser._passages[nextPassage];
            StartCoroutine("WaitForButtonsToHide");
        }

        private void UpdateButtons(int buttonNumber)
        {
            _uiManager.GetAnimator(_dialogueAnimator).SetBool("ButtonsNeedUpdate", true);
            _uiManager.GetAnimator(_dialogueAnimator).SetInteger("ButtonNumber", buttonNumber);
        }

        private void SetEndingPage()
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append("Durée de la discussion : " + _playerData.time.ToString("0.##") + "s");
            strBuilder.Append("<br><br>");
            strBuilder.Append("Textes découverts : " + _playerData.discovery);
            strBuilder.Append("<br><br>");

            _uiManager.GetComponentInGameObject<TMP_Text>(_mainDialogueText).text = strBuilder.ToString();
        }

        private IEnumerator WaitForButtonsToHide()
        {
            UpdateButtons(-_currentPassage._links.Count);
            _currentPassage = _nextPassage;
            
            UpdateDialogue();

            yield return new WaitForSeconds(_uiManager.GetAnimator(_dialogueAnimator).GetCurrentAnimatorClipInfo(0).Length);

            UpdateButtons(_currentPassage._links.Count);
        }

        private IEnumerator GoToEnd()
        {
            UpdateButtons(-_currentPassage._links.Count);

            yield return new WaitForSeconds(_uiManager.GetAnimator(_dialogueAnimator).GetCurrentAnimatorClipInfo(0).Length);

            _uiManager.GetAnimator(_dialogueAnimator).Play(_endClip.name);

            SetEndingPage();
        }

        private IEnumerator UpdateOptionButtonsText(string[] buttonsTexts)
        {
            // Wait for hide animation to finish before updating the texts
            int currentClipLength = _uiManager.GetAnimator(_dialogueAnimator).GetCurrentAnimatorClipInfo(0).Length;
            if(currentClipLength > 0 && _uiManager.GetAnimator(_dialogueAnimator).GetInteger("ButtonNumber") < 0)
            {
                yield return new WaitForSeconds(currentClipLength);
            }

            for (int i = 0; i < buttonsTexts.Length; i++)
            {
                _optionButtons[i].GetComponentInChildren<TMP_Text>().text = buttonsTexts[i];
            }
        }
    }
}
