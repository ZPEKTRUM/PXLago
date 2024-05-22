using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject destructionEffectPrefab; // R�f�rence au prefab de l'effet de destruction
    public AudioClip destructionSound; // R�f�rence au son de destruction

    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;

        // Assurez-vous qu'il y a un AudioSource attach� � l'objet
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Jouez l'effet de destruction
        if (destructionEffectPrefab != null)
        {
            Instantiate(destructionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Jouez le son de destruction
        if (destructionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destructionSound);
        }

        // D�truire l'objet apr�s avoir jou� les effets
        Destroy(gameObject);
    }
}
