using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //-----------------------------------------------------
    /*Attributes*/

    private float normalSpeed = 25f, fastSpeed = 50f;
    private float speed;
    public float getSpeed { get { return this.speed; } }
    private bool speedUp = false;
    public bool isSpeedUp { get { return this.speedUp; } }
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
        //Initialize the speed:
        this.SetSpeed(this.normalSpeed);

        //-----------------------------------------------------
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.playerControllerScript.isGameOver)
            this.SetSpeed(0);
        else
        {
            //-----------------------------------------------------
            //Check if the player is in speedUp:
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetKey(KeyCode.K) && !this.speedUp))
            {
                this.SetSpeed(this.fastSpeed);
                this.speedUp = true;
                this.playerControllerScript.SetSpeedUpAnimator(true);
            }
            else if (Input.GetKeyUp(KeyCode.K) || (!Input.GetKey(KeyCode.K) && this.speedUp))
            {
                this.SetSpeed(this.normalSpeed);
                this.speedUp = false;
                this.playerControllerScript.SetSpeedUpAnimator(false);
            }

            //-----------------------------------------------------
            //Move the game object to the left:
            this.transform.Translate(Vector3.left * Time.deltaTime * this.speed);

            //-----------------------------------------------------
        }
    }

    //Set the speed:
    public void SetSpeed (float speed)
    {
        this.speed = speed;
    }
    //-----------------------------------------------------
}
