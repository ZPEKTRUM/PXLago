using UnityEngine;

namespace Player
{
    public class Unit : MonoBehaviour
    {
        #region Champs
        //INSPECTOR

        //PRIVATES
        Managers.UnitSelections _unitSelection;

	    //PUBLICS
  
        #endregion
        #region Default Informations
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
        void Start()
        {
                Managers.UnitSelections.Instance.UnitList.Add(gameObject);
        }
        #endregion
        #region Methods
        void OnDestroy()
        {
                Managers.UnitSelections.Instance.UnitList.Remove(gameObject);
        }
        #endregion
    }
}