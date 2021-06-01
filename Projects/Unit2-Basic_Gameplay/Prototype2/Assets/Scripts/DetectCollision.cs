using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    //Score:
    public float scoreValue = 1f;
    private GameObject owner;
    private PlayerController ownerComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Override the method OnTrigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != this.owner && this.owner != null)
        {
            this.ownerComponent = this.owner.GetComponent<PlayerController>();

            //Update the score:
            this.ownerComponent.AddToScore(scoreValue);

            //Destroy the object and the other.
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void setOwner (GameObject owner)
    /*This method is called when this object is created and by its future owner.*/
    {
        this.owner = owner;
    }
}
