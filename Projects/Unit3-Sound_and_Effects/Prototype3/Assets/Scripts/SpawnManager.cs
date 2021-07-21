using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //-----------------------------------------------------
    /*Attributes*/

    //General status:
    private float minDelay = 0.8f, maxDelay = 2.5f;
    private float currentDelay = 0f, limitDelay;
    private int nextIndex;
    private PlayerController playerControllerScript;

    //Obstacles:
    public GameObject[] obstacles;
    private Vector3 initialPosition = new Vector3(35.5f, 0, 0);
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
        //Get the element playerController:
        this.playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        //-----------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerControllerScript.isGameOver == false)
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
                CreateNewObstacle(this.initialPosition);

                //-----------------------------------------------------
            }

            //-----------------------------------------------------
        }
    }

    //Method for creating new obstacles:
    private void CreateNewObstacle(Vector3 initialPosition)
    {
        this.nextIndex = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[nextIndex], initialPosition, obstacles[nextIndex].transform.rotation);
    }
    //-----------------------------------------------------
}
