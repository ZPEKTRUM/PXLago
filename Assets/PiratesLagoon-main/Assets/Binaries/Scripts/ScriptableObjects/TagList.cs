using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    public class TagsList : MonoBehaviour
    {
        #region Champs
        [Header("Tags List")]
        [SerializeField] List<string> _tags = new List<string>();
        [Header("Screen Name")]
        [SerializeField] string _screenName;

        public List<string> Tags { get => _tags; set => _tags = value; }
        public string ScreenName { get => _screenName; set => _screenName = value; }
        #endregion
    }
}