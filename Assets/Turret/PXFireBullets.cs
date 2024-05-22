using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PXFireBullets : MonoBehaviour
{
    public GameObject bulletObj; // Le prefab de la balle
    public Transform bulletParent; // Le parent des balles dans la hiérarchie
    public float bulletSpeed = 20f; // Vitesse de la balle
    public float fireRate = 2f; // Intervalle de tir

    private Transform _transform;

    void Start()
    {
        _transform = transform;
        StartCoroutine(FireBullet());
    }

    public IEnumerator FireBullet()
    {
        while (true)
        {
            // Créez une nouvelle balle
            GameObject newBullet = Instantiate(bulletObj, _transform.position, _transform.rotation);

            // Assurez-vous que le prefab a le composant BulletImpactHandler
            BulletImpactHandler impactHandler = newBullet.GetComponent<BulletImpactHandler>();
            if (impactHandler == null)
            {
                impactHandler = newBullet.AddComponent<BulletImpactHandler>();
            }

            // Parent pour organiser la hiérarchie
            newBullet.transform.parent = bulletParent;

            // Ajouter une vitesse à la balle
            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = bulletSpeed * _transform.forward;
            }
            else
            {
                Debug.LogWarning("Le prefab de la balle n'a pas de composant Rigidbody.");
            }

            // Attendre avant de tirer la prochaine balle
            yield return new WaitForSeconds(fireRate);
        }
    }
}
