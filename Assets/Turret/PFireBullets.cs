using System.Collections;
using UnityEngine;

public class PFireBullets : MonoBehaviour
{
    public GameObject bulletObj; // The bullet prefab to instantiate
    public Transform bulletParent; // The parent object to organize bullets in the hierarchy
    public float bulletSpeed = 20f; // Speed at which the bullet will travel
    public float fireRate = 2f; // Time interval between each bullet shot

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
            // Create a new bullet
            GameObject newBullet = Instantiate(bulletObj, _transform.position, _transform.rotation);

            // Parent it to get a less messy workspace
            newBullet.transform.parent = bulletParent;

            // Get the Rigidbody component
            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                // Add velocity to the bullet
                bulletRb.velocity = bulletSpeed * _transform.forward;
            }
            else
            {
                Debug.LogWarning("Bullet object is missing a Rigidbody component.");
            }

            // Wait for the specified time before firing the next bullet
            yield return new WaitForSeconds(fireRate);
        }
    }
}
