using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public GameObject[] Aiprefab;
    public int AIToSpawn;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < AIToSpawn)
        {
            int randomIndex = Random.Range(0, Aiprefab.Length);
            GameObject obj = Instantiate(Aiprefab[randomIndex]);

            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));

            obj.GetComponent<WayPointNavigator>().currentWaypoint = child.GetComponent<WayPoint>();

            obj.transform.position = child.position;

            yield return new WaitForSeconds(1f);

            count ++;
        }
    }
}