using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCollision : MonoBehaviour
{

    //Enemy:
    private EnemyStatus enemy;


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
        this.enemy = other.GetComponent<EnemyStatus>();
        if (this.enemy != null) //If true, enemy hit the player.
        {
            PlayerController playerComponent;
            playerComponent = this.gameObject.GetComponent<PlayerController>();
            //Update the score:
            playerComponent.AddToScore(-this.enemy.score);
            playerComponent.AddToHealth(-this.enemy.damage);

            //Destroy the other.
            Destroy(other.gameObject);
        }
    }
}
