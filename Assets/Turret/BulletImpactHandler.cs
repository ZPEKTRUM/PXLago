using UnityEngine;

public class BulletImpactHandler : MonoBehaviour
{
    public GameObject explosionPrefab; // Référence au prefab de l'explosion
    public AudioClip explosionSound; // Référence au fichier audio de l'explosion
    public int damage = 20; // Quantité de dégâts infligés à la cible
    public float forceAmount = 500f; // Quantité de force à appliquer
    public bool applyForce = true; // Activer/désactiver l'application de force

    [SerializeField]
    private AudioSource audioSource; // Référence à l'AudioSource pour jouer le son

    void Start()
    {
        // Si l'AudioSource n'est pas assignée via l'inspecteur, essayez de l'ajouter dynamiquement
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Créez l'effet d'explosion à l'endroit de l'impact
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Jouer l'effet sonore d'explosion
        if (explosionSound != null && audioSource != null)
        {
            // Activer l'AudioSource si elle est désactivée
            if (!audioSource.enabled)
            {
                audioSource.enabled = true;
            }
            audioSource.PlayOneShot(explosionSound);
        }

        // Infliger des dégâts si la collision est avec la cible
        if (collision.gameObject.CompareTag("Target"))
        {
            TargetHealth targetHealth = collision.gameObject.GetComponent<TargetHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }

            // Appliquer une force si l'option est activée
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

        // Détruire le projectile
        Destroy(gameObject);
    }
}

