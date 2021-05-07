using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Public attributes:
    public GameObject[] enemies;

    //Private attribute:
    private float offset = 25f, range = 20f, midPoint = 0f;
    private float startTime = 2, delay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnRandomEnemy", startTime, delay);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawnRandomEnemy()
    {
        int enemyIndex;
        float randomPositionX;
        Vector3 spawnPosition;
        //Get the random position and the random enemy:
        randomPositionX = midPoint + (2 * Random.value - 1) * range;
        enemyIndex     = Random.Range(0, enemies.Length);
        spawnPosition  = new Vector3(randomPositionX, 0, offset);

        //Instantiate the enemy:
        Instantiate(enemies[enemyIndex], spawnPosition, enemies[enemyIndex].transform.rotation);
    }
}
