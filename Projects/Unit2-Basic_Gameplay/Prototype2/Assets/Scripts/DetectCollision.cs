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
        if (other.gameObject != owner)
        {
            ownerComponent = owner.GetComponent<PlayerController>();

            //Update the score:
            ownerComponent.AddToScore(scoreValue);

            //Destroy the object and the other.
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void setOwner (GameObject owner)
    {
        this.owner = owner;
    }
}
