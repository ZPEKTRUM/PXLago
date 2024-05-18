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


namespace Weapons
{
    public class FollowMouse : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        private CinemachineVirtualCameraBase activeVirtualCamera;

        //PRIVATES

        //PUBLICS

        #endregion
        #region Default Informations
        void Reset()
        {

        }
        #endregion
        #region Unity LifeCycle
        // Start is called before the first frame update
        void Start()
        {
            UpdateActiveVirtualCamera();
        }
        // Trouver la caméra virtuelle active dans la scène
        //virtualCamera = CinemachineCore.Instance.VirtualCameraCount;
        // Update is called once per frame
        void Update()
        {
            // Mettez à jour la caméra virtuelle active à chaque frame
            UpdateActiveVirtualCamera();

            // Si une caméra virtuelle active est trouvée, suivez la souris
            if (activeVirtualCamera != null)
            {
                FollowMouseWithActiveCamera();
            }
        }
        #endregion
        #region Methods
        void UpdateActiveVirtualCamera()
        {
            // Obtenez la caméra virtuelle active actuellement en utilisant CinemachineCore
            activeVirtualCamera = CinemachineCore.Instance.GetVirtualCamera(0);
        }

        void FollowMouseWithActiveCamera()
        {
            // Calcul de la position de la souris dans l'espace du monde
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Ajustez la position de l'objet pour suivre la position de la souris
                transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
        }
        #endregion

    }
}