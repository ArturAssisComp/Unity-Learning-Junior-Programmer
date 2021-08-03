using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Attributes:
    public GameObject enemy;
    public GameObject powerUp;
    private int enemiesPerWave = 1;
    private int currentNumberOfEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.SpawnWave(enemiesPerWave);
        Instantiate(this.powerUp, this.GenerateSpawnPosition(), this.powerUp.transform.rotation);
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
            Instantiate(this.powerUp, this.GenerateSpawnPosition(), this.powerUp.transform.rotation);
        }
    }

    private void SpawnWave(int numberOfEnemies)
    {
        for(int i = 0; i < numberOfEnemies; i++)
            Instantiate(this.enemy, GenerateSpawnPosition(), enemy.transform.rotation);
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
