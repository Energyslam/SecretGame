using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Transform target;
    public GameObject monster;
    public SpawnManager spawnManager;
    public bool spawnMonster;
    void Start()
    {
        this.target = GameManager.Instance.monsterTarget;   
    }

    private void Update()
    {
        if (spawnMonster == true)
        {
            SpawnMonster();
            spawnMonster = false;
        }
    }

    public void SpawnMonster()
    {
        GameObject demon = Instantiate(monster, this.transform.position, Quaternion.identity);
    }
}
