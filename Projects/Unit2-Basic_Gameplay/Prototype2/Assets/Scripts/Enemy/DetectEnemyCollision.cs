using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 * Description: This script will be added as component to projetiles that are 
 * shot by the player. Those projetiles can hit the enemies and satisfy their 
 * hungry.
 */

public class DetectEnemyCollision : MonoBehaviour
{
    //Score:
    public float scoreValue = 1f;
    private GameObject owner;
    private PlayerController ownerComponent;

    //Enemy:
    private EnemyStatus enemyStatus;

    //Hungry:
    public float hungryValue = 1f;


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
        this.enemyStatus = other.GetComponent<EnemyStatus>();
        this.ownerComponent = this.owner.GetComponent<PlayerController>();
        if(this.enemyStatus != null && this.ownerComponent != null)
        {
            //Update the score:
            this.ownerComponent.AddToScore(this.scoreValue);

            //Update the hungry of the enemy:
            this.enemyStatus.AddToHungry(this.hungryValue);

            //Destroy the object
            Destroy(this.gameObject);
        }
    }

    public void setOwner (GameObject owner)
    /*This method is called when this object is created and by its future owner.*/
    {
        this.owner = owner;
    }
}
