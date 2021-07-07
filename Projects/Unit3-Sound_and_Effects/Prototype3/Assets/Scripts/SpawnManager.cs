using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //-----------------------------------------------------
    /*Attributes*/

    //General status:
    private float minDelay = 1f, maxDelay = 2f;
    private float currentDelay = 0f, limitDelay;
    private int nextIndex;

    //Obstacles:
    public GameObject[] obstacles;
    //-----------------------------------------------------
    /*Methods*/

    // Start is called before the first frame update
    void Start()
    {
        //-----------------------------------------------------
        //Initialize the attributes:
        this.currentDelay = 0f;
        this.limitDelay = Mathf.Clamp(Random.value * this.maxDelay, this.minDelay, this.maxDelay);

        //-----------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
        //-----------------------------------------------------
        //Increment the time:
        this.currentDelay += Time.deltaTime;

        //-----------------------------------------------------
        //Check if the time reach the limit to invoke a new obstacle:
        if (this.currentDelay > this.limitDelay)
        {
            //-----------------------------------------------------
            //Reset the current delay and calculate a new limit:
            this.currentDelay = 0;
            this.limitDelay = Mathf.Clamp(Random.value * this.maxDelay, this.minDelay, this.maxDelay);

            //-----------------------------------------------------
            //Create a new obstacle:
            CreateNewObstacle();

            //-----------------------------------------------------
        }

        //-----------------------------------------------------
    }

    //Method for creating new obstacles:
    private void CreateNewObstacle()
    {
        this.nextIndex = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[nextIndex]);
    }
    //-----------------------------------------------------
}
