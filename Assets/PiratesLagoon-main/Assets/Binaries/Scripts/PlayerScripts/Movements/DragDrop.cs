using UnityEngine;
 
namespace Player
{
    public class DragDrop : MonoBehaviour
    {
        #region Champs
        //INSPECTOR
        //PRIVATES
        Managers.InputManager _inputManager;
        GameObject _selectObject;
        //PUBLICS
        public GameObject SelectObject { get => _selectObject; set => _selectObject = value; }
        #endregion
        #region Default Informations
        #endregion
        #region Unity LifeCycle
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_selectObject == null)
                {
                    RaycastHit hit = CastRay();

                    if (hit.collider != null)
                    {
                        if (!hit.collider.CompareTag("Draggable"))
                        {
                            return;
                        }
                        _selectObject = hit.collider.gameObject;
                        Cursor.visible = false;
                    }
                }
                else // _Selected is not null, move the gameObject with the touch or mouse
                {
                    Vector3 position = new Vector3(Input.mousePosition.x,
                                                   Input.mousePosition.y,
                                                   Camera.main.WorldToScreenPoint(_selectObject.transform.position).z);
                    Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(position);
                    _selectObject.transform.position = new Vector3(WorldPosition.x, WorldPosition.y + 0.5f, WorldPosition.z);

                    _selectObject = null;
                    Cursor.visible = true;
                }
            }

            // Click to stop moving gameObject
            if (_selectObject != null)
            { 
                Vector3 position = new Vector3(Input.mousePosition.x,
                                               Input.mousePosition.y,
                                               Camera.main.WorldToScreenPoint(_selectObject.transform.position).z);
                Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(position);
                _selectObject.transform.position = new Vector3(WorldPosition.x, WorldPosition.y, WorldPosition.z);
            }
        }
        #endregion
        #region Methods
        RaycastHit CastRay()
        {
            Vector3 screenMousePositionFar = new Vector3(Input.mousePosition.x,
                                                         Input.mousePosition.y,
                                                         Camera.main.farClipPlane);
            Vector3 screenMousePositionNear = new Vector3(Input.mousePosition.x,
                                                         Input.mousePosition.y,
                                                         Camera.main.nearClipPlane);
            Vector3 mousePositionFar = Camera.main.ScreenToWorldPoint(screenMousePositionFar); 
            Vector3 mousePositionNear = Camera.main.ScreenToWorldPoint(screenMousePositionNear );

            RaycastHit hit;
            Physics.Raycast(mousePositionNear, mousePositionFar - mousePositionNear, out hit);

            return hit;
        }
        #endregion
        #region Coroutines
        #endregion
    }
}