﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Enemies:
    public GameObject[] enemies;

    //Constraints:
    private float leftX = -24.5f, rightX = 24.5f, upZ = 17.1f, bottomZ = -2.1f;
    private float offset = 3f;
    private float startTime = 2, delay = 0.2f;

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
        float randomPositionX = 0f, randomPositionZ = 0f;
        Vector3 spawnPosition;
        

        //Choose the enemy:
        enemyIndex     = Random.Range(0, enemies.Length);
        //enemies[enemyIndex].transform.rotation

        switch (Random.Range(0, 4))
        {
            case 0: //x == leftX - offset
                randomPositionX = leftX - offset;
                randomPositionZ = bottomZ + (upZ - bottomZ) * Random.value;
                enemies[enemyIndex].transform.Rotate(Vector3.up, 90);
                break;
            case 1: //x == rightX + offset
                randomPositionX = rightX + offset;
                randomPositionZ = bottomZ + (upZ - bottomZ) * Random.value;
                enemies[enemyIndex].transform.Rotate(Vector3.up, -90);
                break;
            case 2: //z == upZ + offset
                randomPositionZ = upZ + offset;
                randomPositionX = leftX + (rightX - leftX) * Random.value;
                //Not necessary to rotate.
                break;
            case 3: //z == bottomZ - offset
                randomPositionZ = bottomZ - offset;
                randomPositionX = leftX + (rightX - leftX) * Random.value;
                enemies[enemyIndex].transform.Rotate(Vector3.up, 180);
                break;
            default:
                break;
        }

        //Calculate the position:
        spawnPosition  = new Vector3(randomPositionX, 0, randomPositionZ);

        //Instantiate the enemy:
        Instantiate(enemies[enemyIndex], spawnPosition, enemies[enemyIndex].transform.rotation);
    }
}
