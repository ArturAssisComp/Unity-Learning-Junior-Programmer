using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Attributes:
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(this.enemy, GenerateSpawnPosition(), enemy.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
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
