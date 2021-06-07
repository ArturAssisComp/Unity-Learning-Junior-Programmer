using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //Player:
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PrintStatus(this.player);
    }

    void PrintStatus(GameObject player)
    {
        //Get player component:
        PlayerController playerComponent;
        if (player != null)
            playerComponent = player.GetComponent<PlayerController>();
        else playerComponent = null;

        //Print the status:
        if (playerComponent != null)
        {
            Debug.Log("Health: " + playerComponent.getHealth);
            Debug.Log("Score: " + playerComponent.getScore);
        }
    }
}
