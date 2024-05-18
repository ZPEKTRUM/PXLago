using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerCoroutines : MonoBehaviour
{
    #region Coroutines
    public IEnumerator HeadCoroutine()
    {
        //throw new NotImplementedException();
        yield return new WaitForSeconds(1);
    }
    #endregion
}