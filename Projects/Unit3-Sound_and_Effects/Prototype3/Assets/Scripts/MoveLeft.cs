using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //-----------------------------------------------------
    /*Attributes*/

    private float speed = 10f;
    private PlayerController playerControllerScript;

    //-----------------------------------------------------
    /*Methods*/

    // Start is called before the first frame update
    void Start()
    {
        //-----------------------------------------------------
        //Get the element playerController:
        this.playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        //-----------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
        //-----------------------------------------------------
        //Move the game object to the left:
        if (this.playerControllerScript.isGameOver == false)
            this.transform.Translate(Vector3.left * Time.deltaTime * this.speed);

        //-----------------------------------------------------
    }

    //Set the speed:
    public void SetSpeed (float speed)
    {
        this.speed = speed;
    }
    //-----------------------------------------------------
}
