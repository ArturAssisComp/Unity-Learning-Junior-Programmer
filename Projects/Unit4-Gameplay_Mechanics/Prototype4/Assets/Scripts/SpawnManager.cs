using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Attributes:
    public GameObject[] enemy;
    public GameObject boss;
    public GameObject[] powerUp;
    private float waveLevel = 1;
    private float deltaWaveLevel = 0.2f;
    private int enemiesPerWave = 1;
    private int currentNumberOfEnemies = 0;
    private int firstBossLevel = 10;

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
        this.currentNumberOfEnemies = FindObjectsOfType<Enemy>().Length + FindObjectsOfType<Boss>().Length;
        
        if(this.currentNumberOfEnemies == 0)
        {
            //Increase difficult:
            this.enemiesPerWave++;
            this.waveLevel += this.deltaWaveLevel;

            //Spawn enemies and a power up:
            if (this.enemiesPerWave == this.firstBossLevel)
                this.SpawnBoss();
            else
            {
                this.SpawnWave(this.enemiesPerWave);
                this.SpawnPowerUp(1);
            }
        }
    }

    private void SpawnWave(int numberOfEnemies)
    {
        int enemyIndex = 0;

        for(int i = 0; i < numberOfEnemies; i++)
        {
            for (int j = 1; j <= this.enemy.Length; j++)
            {
                if (Random.value <= 1 / Mathf.Pow(this.waveLevel, j))
                {
                    enemyIndex = Random.Range(0, j);
                    break;
                }
            }

            Instantiate(this.enemy[enemyIndex], GenerateSpawnPosition(), this.enemy[enemyIndex].transform.rotation);
        }
    }

    private void SpawnBoss()
    {
            Instantiate(this.boss, this.boss.transform.position, this.boss.transform.rotation);
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
