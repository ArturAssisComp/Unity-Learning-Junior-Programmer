using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Attributes:
    public float power = 10f;
    private float yLowerBound = -10f;

    public Rigidbody enemyRigidbody;
    private GameObject player;


    // Start is called before the first frame update
    public virtual void Start()
    {
        //Get components:
        this.enemyRigidbody = this.GetComponent<Rigidbody>();
        this.player = GameObject.Find("Player");
    }

    private void Update()
    {
        //Delete enemies out of bound:
        if (this.transform.position.y < this.yLowerBound)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    //FixedUpdate attributes (they are not declared inside the method to avoid overhead):
    private Vector3 currentForceDirection;
    void FixedUpdate()
    {
        //Calculate the direction:
        this.currentForceDirection = (this.player.transform.position - this.transform.position).normalized;

        //Apply the force:
        this.enemyRigidbody.AddForce(this.currentForceDirection * this.power);
    }
}
