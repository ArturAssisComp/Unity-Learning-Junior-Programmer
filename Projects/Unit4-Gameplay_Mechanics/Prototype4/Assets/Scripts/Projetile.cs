using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetile : MonoBehaviour
{
    //Projetile attributes:
    private float speed = 50f;
    public float hitPower = 3f;
    private float totalDistance = 0f;
    private float deltaDistance;
    private float maxDistance = 30f;
    private float bossHealthDamage = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move the projetile:
        this.deltaDistance = this.speed * Time.deltaTime;
        this.transform.Translate(Vector3.up * this.deltaDistance);

        //Check if it reached its distance limits:
        this.totalDistance += this.deltaDistance;

        if (this.totalDistance > this.maxDistance)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Apply away force to the enemy:
            Vector3 awayDirection = other.gameObject.transform.position - this.transform.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(awayDirection * this.hitPower, ForceMode.Impulse);

            //Destroy the projetile:
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            //Apply away force to the enemy:
            Vector3 awayDirection = other.gameObject.transform.position - this.transform.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(awayDirection * this.hitPower, ForceMode.Impulse);

            //Cause damage to the boss:
            other.gameObject.GetComponent<Boss>().AddToHealth(-this.bossHealthDamage);

            //Destroy the projetile:
            Destroy(this.gameObject);
        }
    }
}
