using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Attributes:
    public int scoreValue;
    public ParticleSystem explosionEffect;
    private float minForce = 8f,  minTorque = -10f, minHorizontalPosition = -4f;
    private float maxForce = 15f, maxTorque = 10f,  maxHorizontalPosition =  4f;
    private float initialVerticalPosition = -1f;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        //Get the rigid body:
        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();

        //Get gameManager:
        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Apply the initial conditions:
        this.transform.position = new Vector3(this.GetRandomHorizontalPosition(), this.initialVerticalPosition);
        rigidbody.AddForce(Vector3.up * this.GetRandomForce(), ForceMode.Impulse);
        rigidbody.AddTorque(this.GetRandomTorque(), this.GetRandomTorque(), this.GetRandomTorque(), ForceMode.Impulse);
        
        
    }


    private void OnMouseDown()
    {
        if(this.gameManager.IsGameActive && !this.gameManager.isGamePaused)
        {
            Destroy(this.gameObject);
            Instantiate(this.explosionEffect, this.transform.position, this.transform.rotation);
            this.gameManager.AddToScore(this.scoreValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Sensor"))
        {
            if(this.CompareTag("GoodTarget"))
            {
                this.gameManager.LoseLife();
            }
            Destroy(this.gameObject);
        }
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
