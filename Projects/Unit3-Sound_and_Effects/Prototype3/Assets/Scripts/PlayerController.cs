using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //---------------------------------------------------------------
    //Player Components:
    private Rigidbody playerRigidbody;
    //---------------------------------------------------------------
    //Player status:
    public float jumpForce = 700f;
    public float gravityModifier = 3f;
    private bool isOnGround = true;
    //---------------------------------------------------------------
    //Methods:


    // Start is called before the first frame update
    void Start()
    {
        //---------------------------------------------------------------
        //Get player components:
        this.playerRigidbody = GetComponent<Rigidbody>();
        //---------------------------------------------------------------
        //Change gravity:
        Physics.gravity *= gravityModifier;
        //---------------------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------------------------------------------
        //Apply upwards force if the player press space bar:
        if (Input.GetKeyDown(KeyCode.Space) && this.isOnGround)
        {
            //Apply updwards force:
            this.playerRigidbody.AddForce(Vector3.up * this.jumpForce, ForceMode.Impulse);

            //Change state of isOnGround to false:
            isOnGround = false;
        }
        //---------------------------------------------------------------
    }

    private void OnCollisionEnter(Collision collision)
    {
        //---------------------------------------------------------------
        //Check if the player collides with the ground:
        if (collision.transform.gameObject.name.Equals("Ground"))
            isOnGround = true;
        //---------------------------------------------------------------
    }


}
