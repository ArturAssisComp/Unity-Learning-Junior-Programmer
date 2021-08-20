using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Attributes:
    private float minForce = 8f,  minTorque = -10f, minHorizontalPosition = -4f;
    private float maxForce = 15f, maxTorque = 10f,  maxHorizontalPosition =  4f;
    private float initialVerticalPosition = -1f;


    // Start is called before the first frame update
    void Start()
    {
        //Get the rigid body:
        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();

        //Apply the initial conditions:
        this.transform.position = new Vector3(this.GetRandomHorizontalPosition(), this.initialVerticalPosition);
        rigidbody.AddForce(Vector3.up * this.GetRandomForce(), ForceMode.Impulse);
        rigidbody.AddTorque(this.GetRandomTorque(), this.GetRandomTorque(), this.GetRandomTorque(), ForceMode.Impulse);
        
        
    }


    private float GetRandomHorizontalPosition()
    {
        return Random.Range(this.minHorizontalPosition, this.maxHorizontalPosition);
    }

    private float GetRandomForce()
    {
        return Random.Range(this.minForce, this.maxForce);
    }

    private float GetRandomTorque()
    {
        return Random.Range(this.minTorque, this.maxTorque);
    }


}
