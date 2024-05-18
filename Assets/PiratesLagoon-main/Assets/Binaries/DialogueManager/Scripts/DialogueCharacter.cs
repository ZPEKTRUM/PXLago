using UnityEngine;

namespace DialogueManager
{
    /// <summary>
    /// Contains a character's name, name color and illustration
    /// </summary>
    [CreateAssetMenu(fileName = "DialogueCharacter", menuName = "DialogueManager/DialogueCharacter", order = 1)]
    public class DialogueCharacter : UnityEngine.ScriptableObject
    {
        public string _characterName;
        public Color _characterColor;
        public Sprite _characterSprite;

    }
}