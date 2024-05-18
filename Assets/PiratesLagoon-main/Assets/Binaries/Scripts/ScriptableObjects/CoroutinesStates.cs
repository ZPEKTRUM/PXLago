using System.Collections;
using UnityEngine;


namespace Managers
{
    public class CoroutinesStates : MonoBehaviour
    {
        #region Fields
        [Header("Tools")]
        [SerializeField] Tools.EnterToTask _enterTask;
        #endregion
        #region Coroutines
        public IEnumerator WheelCoroutine(ScriptableObjects.StaminaTask stamina, GameObject go)
        {
            GameObject _go = go;
            yield return new WaitForSeconds(stamina.CoroutineTime);
            stamina.Stamina = 0f;
            stamina.StaminaWheel.SetActive(false);
            _enterTask.FinishedTask(_go);
        }
        #endregion
        #region Boat
        public IEnumerator ShootCoroutine(FireZoneSelection fire)
        {
            var waiter = new WaitForSeconds(fire.FireRate);
            while (true)
            {
                for (int i = 0; i < fire.NumberOfBullets; i++)
                {
                    // Calcul de l'offset aléatoire basé sur la hauteur et la largeur de l'objet détecté
                    // Calculation of random offset based on height and width of detected object
                    float randomY = Random.Range(-(fire.TargetHeight + 1), fire.TargetHeight + 2);
                    float randomZ = Random.Range(-(fire.TargetWidth + 1), fire.TargetWidth + 2);
                    float randomX = Random.Range(-(fire.TargetDepth), fire.TargetDepth);
                    // Crée un vecteur 3 avec le décalage aléatoire
                    // Creates a 3 vector with random offset
                    Vector3 offset = new Vector3(randomX, randomY, randomZ);
                    Instantiate(fire.BulletPrefab, fire.Spawner.position + offset,
                                 Quaternion.LookRotation(fire.AimCursor.position - fire.Spawner.position)).SetDirection(fire.AimCursor.position);
                }
                fire.UseAmmo(); // Déduire une munition
                yield return waiter;
            }
        }
        #endregion
    }
}