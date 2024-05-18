using UnityEngine;

namespace DialogueManager
{
    [CreateAssetMenu(fileName = "DialoguePlayerData", menuName = "DialogueManager/DialoguePlayerData", order = 2)]
    public class DialoguePlayerData : UnityEngine.ScriptableObject
    {
        [HideInInspector] public int informations = 0;
        [HideInInspector] public int discovery = 0;
        [HideInInspector] public float time = 0f;

        private void OnEnable() {Reset();}
        public void Reset()
        {
            informations = discovery = 0;
            time = 0f;
        }
    }
}