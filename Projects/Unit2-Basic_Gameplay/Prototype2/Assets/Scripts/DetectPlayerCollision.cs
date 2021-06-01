using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCollision : MonoBehaviour
{
    //Score:
    private float deltaScore = 1f;
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
            playerComponent.AddToScore(-this.deltaScore);
            playerComponent.AddToHealth(-this.damage);

            //Destroy the other.
            Destroy(other.gameObject);
        }
    }
}
