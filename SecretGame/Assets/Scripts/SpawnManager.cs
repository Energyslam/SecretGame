using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints;
    public GameObject demon;

    int spawnedMonsters = 0;
    float delayBetweenSpawn = 2f;
    void Start()
    {
        foreach(Transform t in transform)
        {
            if (t.GetComponent<SpawnPoint>() != null)
            {
                t.GetComponent<SpawnPoint>().monster = demon;
                spawnPoints.Add(t.GetComponent<SpawnPoint>());
            }
        }

        SpawnMonsterAtRandomLocation();
    }

    public void SpawnMonsterAtRandomLocation()
    {
        spawnedMonsters++;
        int totalSpawnPoints = spawnPoints.Count;
        Debug.Log("total" + totalSpawnPoints);
        int randomNumber = Random.Range(0, totalSpawnPoints);
        Debug.Log("random" + randomNumber);
        spawnPoints[randomNumber].SpawnMonster();

        if (spawnedMonsters > 10)
        {
            spawnedMonsters = 0;
            delayBetweenSpawn -= 0.1f;
            delayBetweenSpawn = Mathf.Clamp(delayBetweenSpawn, 0.3f, 2f);
        }

        Invoke("SpawnMonsterAtRandomLocation", delayBetweenSpawn);
    }
}
