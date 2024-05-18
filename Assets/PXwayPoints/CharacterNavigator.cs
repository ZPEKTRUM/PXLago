using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigator : MonoBehaviour
{
    [Header("Character Info")]
    public float movingSpeed;
    public float turningSpeed = 300f;
    public float stopSpeed = 1f;
    public float characterHealth = 100f;
    public float presentHealth; 

    [Header("Destination Var")]

    public Vector3 destination;
    public bool destinationReached;
    public Animator animator;
    public Playerx player;


    private void Update()
    {
        Walk(); // Able fonction to lookat destintion for IA no code to tell IA destination
        player = GameObject.FindAnyObjectByType<Playerx>();
    }
    /// <summary>
    /// Rotating moving the Ai Character destination 
    /// </summary>
    public void Walk()
    {
        if(transform.position != destination) 
        
        { 
        Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;  // found destination
            
            if(destinationDistance >= stopSpeed ) 
            
            {
            
            //turning
            destinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime); //Any waypoint

                //Move Ai

                transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime); 



            }
            else 
            {
                destinationReached = true;  // Save to true
            }
        }
    }

    /// ****<summary>
    /// Lookat in the destination way the to false walk 
    /// </summary>
    /// <param name="destination"></param>
    /// 

    public void LocateDestination(Vector3 destination) 
    
    { 
    
        this.destination = destination;
        destinationReached = true; 
    
    }

    public void CharacterHitDamage(float takeDamage)
    {

        presentHealth -= takeDamage;

        if (presentHealth <= 0)
        {
            animator.SetBool("Die", true);
        }

        {

            CharacterDie();

        }

    }

    private void CharacterDie() 
    
    {
        movingSpeed = 0f;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Object.Destroy(gameObject, 4.0f);
        player.currentkills += 1;
        player.playerMoney -= 5;
    }

}


