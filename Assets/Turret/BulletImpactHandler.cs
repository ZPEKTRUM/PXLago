using UnityEngine;

public class BulletImpactHandler : MonoBehaviour
{
    public GameObject explosionPrefab; // R�f�rence au prefab de l'explosion
    public AudioClip explosionSound; // R�f�rence au fichier audio de l'explosion
    public int damage = 20; // Quantit� de d�g�ts inflig�s � la cible
    public float forceAmount = 500f; // Quantit� de force � appliquer
    public bool applyForce = true; // Activer/d�sactiver l'application de force

    [SerializeField]
    private AudioSource audioSource; // R�f�rence � l'AudioSource pour jouer le son

    void Start()
    {
        // Si l'AudioSource n'est pas assign�e via l'inspecteur, essayez de l'ajouter dynamiquement
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Cr�ez l'effet d'explosion � l'endroit de l'impact
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Jouer l'effet sonore d'explosion
        if (explosionSound != null && audioSource != null)
        {
            // Activer l'AudioSource si elle est d�sactiv�e
            if (!audioSource.enabled)
            {
                audioSource.enabled = true;
            }
            audioSource.PlayOneShot(explosionSound);
        }

        // Infliger des d�g�ts si la collision est avec la cible
        if (collision.gameObject.CompareTag("Target"))
        {
            TargetHealth targetHealth = collision.gameObject.GetComponent<TargetHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }

            // Appliquer une force si l'option est activ�e
            if (applyForce)
            {
                Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (targetRigidbody != null)
                {
                    Vector3 forceDirection = collision.transform.position - transform.position;
                    targetRigidbody.AddForce(forceDirection.normalized * forceAmount);
                }
            }
        }

        // D�truire le projectile
        Destroy(gameObject);
    }
}

