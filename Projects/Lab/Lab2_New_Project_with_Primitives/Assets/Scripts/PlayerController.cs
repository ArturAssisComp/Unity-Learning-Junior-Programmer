using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //----------------------------------
    //Attributes:
    private float force = 10f;
    private Rigidbody playerRigidbody;

    private float xLeftBound = -21.8f, xRightBound = 22f, zUpperBound = 22f, zLowerBound = -21.9f;

    //----------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //----------------------------------
        //Get the components:
        this.playerRigidbody = GetComponent<Rigidbody>();

        //----------------------------------
    }

    //----------------------------------
    // Update is called once per frame
    void Update()
    {
        //----------------------------------
        //Get the inputs:
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horInput, 0, verInput).normalized;

        //----------------------------------
        //Move the player:
        if (direction.magnitude > 0)
            this.Move(direction, this.force);

        //----------------------------------
        //Check the player position's constraints:
        this.FixPosition();

        //----------------------------------
    }
    //----------------------------------
    //Move the player in the given direction
    //withe the given force:
    private void Move(Vector3 direction, float force)
    {
        this.playerRigidbody.AddForce(direction * force); 
    }
    //----------------------------------
    //Fix the player's position if necessary:
    private void FixPosition()
    {
        //----------------------------------
        //Check the x position:
        if (this.transform.position.x < this.xLeftBound)
        {
            this.transform.position = new Vector3(this.xLeftBound, this.transform.position.y, this.transform.position.z);
            this.playerRigidbody.velocity = new Vector3(0, this.playerRigidbody.velocity.y, this.playerRigidbody.velocity.z);
        }
        else if (this.transform.position.x > this.xRightBound)
        {
            this.transform.position = new Vector3(this.xRightBound, this.transform.position.y, this.transform.position.z);
            this.playerRigidbody.velocity = new Vector3(0, this.playerRigidbody.velocity.y, this.playerRigidbody.velocity.z);
        }

        //----------------------------------
        //Check the z position:
        if (this.transform.position.z < this.zLowerBound)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.zLowerBound);
            this.playerRigidbody.velocity = new Vector3(this.playerRigidbody.velocity.x, this.playerRigidbody.velocity.y, 0);
        }
        else if (this.transform.position.z > this.zUpperBound)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.zUpperBound);
            this.playerRigidbody.velocity = new Vector3(this.playerRigidbody.velocity.x, this.playerRigidbody.velocity.y, 0);
        }

        //----------------------------------
    }

    //----------------------------------
}
