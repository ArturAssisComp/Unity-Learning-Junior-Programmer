using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    //Projetile:
    public GameObject food;
    private GameObject clone;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the input from space bar or mouse click:
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Launch the projectile:
            this.clone = Instantiate(this.food, this.transform.position, this.transform.rotation);
            this.clone.GetComponent<DetectEnemyCollision>().setOwner(this.transform.parent.gameObject);
        }
    }
}
