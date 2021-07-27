using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Atributes:
    public float force = 5;
    private float input;
    private Rigidbody rigidBody;
    private GameObject CameraCenterPoint;


    // Start is called before the first frame update
    void Start()
    {
        //Get the player's components:
        this.rigidBody = this.gameObject.GetComponent<Rigidbody>();

        //Get the references to GameObject:
        this.CameraCenterPoint = GameObject.Find("Camera Center Point");
    }

    // Update is called once per frame
    void Update()
    {
        //Get inputs:
        this.input = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if(this.input != 0)
        {
            //Move the player:
            this.rigidBody.AddForce(this.CameraCenterPoint.transform.forward * this.input * this.force, ForceMode.Force);
        }
    }



}
