using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCollision : MonoBehaviour
{
    //Score:
    private float scoreValue = 1f;
    private float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DetectCollision>() == null)
        {
            PlayerController playerComponent;
            playerComponent = this.gameObject.GetComponent<PlayerController>();
            //Update the score:
            playerComponent.AddToScore(-scoreValue);
            playerComponent.AddToHealth(-damage);

            //Destroy the object and the other.
            Destroy(other.gameObject);
        }
    }
}
