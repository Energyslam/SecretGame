using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints;
    public GameObject demon;
    // Start is called before the first frame update
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
        int totalSpawnPoints = spawnPoints.Count;
        Debug.Log("total" + totalSpawnPoints);
        int randomNumber = Random.Range(0, totalSpawnPoints);
        Debug.Log("random" + randomNumber);
        spawnPoints[randomNumber].SpawnMonster();

        Invoke("SpawnMonsterAtRandomLocation", 2f);
    }
}
