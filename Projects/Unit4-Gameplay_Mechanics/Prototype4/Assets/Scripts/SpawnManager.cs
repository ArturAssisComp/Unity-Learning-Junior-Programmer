using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Attributes:
    public GameObject[] enemy;
    public GameObject[] powerUp;
    private int enemiesPerWave = 1;
    private int currentNumberOfEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.SpawnWave(enemiesPerWave);
        this.SpawnPowerUp(1);
    }

    // Update is called once per frame
    void Update()
    {

        //Check if there are still enemies:
        this.currentNumberOfEnemies = FindObjectsOfType<Enemy>().Length;
        
        if(this.currentNumberOfEnemies == 0)
        {
            this.enemiesPerWave++;
            this.SpawnWave(this.enemiesPerWave);
            this.SpawnPowerUp(1);
        }
    }

    private void SpawnWave(int numberOfEnemies)
    {
        int enemyIndex = 0;

        for(int i = 0; i < numberOfEnemies; i++)
        {
            if (numberOfEnemies > 3)
                enemyIndex = Random.Range(0, this.enemy.Length);

            Instantiate(this.enemy[enemyIndex], GenerateSpawnPosition(), this.enemy[enemyIndex].transform.rotation);

        }


    }

    private void SpawnPowerUp(int numberOfPowerUps)
    {
        int powerUpIndex = 0;

        for(int i = 0; i < numberOfPowerUps; i++)
        {
            powerUpIndex = Random.Range(0, this.powerUp.Length);
            Instantiate(this.powerUp[powerUpIndex], GenerateSpawnPosition(), this.powerUp[powerUpIndex].transform.rotation);

        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float rangePosition = 8f;
        float xPosition, zPosition;

        xPosition = Random.Range(-rangePosition, rangePosition);
        zPosition = Random.Range(-rangePosition, rangePosition);

        return new Vector3(xPosition, 0,  zPosition);
    }



}
