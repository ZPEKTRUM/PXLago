using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class WayPointNavigator : MonoBehaviour
{

    [Header("NPC character")]

    public CharacterNavigator character;
    public WayPoint currentWaypoint;

    int direction;

    private void Awake()
    {
        character = GetComponent<CharacterNavigator>();
    }

    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        character.LocateDestination(currentWaypoint.GetPosition());
    }

    private void Update()
    {
        if (character.destinationReached)
        {
            bool shouldBranch = false;

            if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)

            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
            }

            if (shouldBranch)

            {
                currentWaypoint = currentWaypoint.branches[ Random.Range(0, currentWaypoint.branches.Count - 1)];
            }
            else
            {
                if (direction == 0)
                {

                    if (currentWaypoint.nextWaypoint != null) 
                    
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                            
                            }
                    else 
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        direction = 1;

                    }
                     

                }
                else if (direction == 1)
                {

                    if (currentWaypoint.previousWaypoint != null)

                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;

                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        direction = 0;

                    }

                }

            }

            character.LocateDestination(currentWaypoint.GetPosition());
        }

    }

}




