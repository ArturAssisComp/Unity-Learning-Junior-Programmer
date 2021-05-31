using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement:
    private float speed = 20f;
    private float horizontalInput, verticalInput;

    //Movement Constraints:
    public float horizontalMidPoint = 0f, verticalMidPoint = 7.5f, horizontalRange = 22f, verticalRange = 9f;

    //Projetile:
    public GameObject food;
    private GameObject clone;

    //Score:
    private float score = 0;
    public float getScore { get{ return score; } }

    //Health:
    private float maxHealth = 10f;
    private float health;
    public float getHealth { get { return health; } } 




    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is dead:
        if (this.health <= 0)
        {
            Debug.Log("Game Over!");
            Destroy(this.gameObject);
        }else
        {
            //Get the horizontal and vertical inputs [-1, +1]:
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
            direction.Normalize();

            //Move the player within the lateral/vertical bounds:
            transform.Translate(direction * Time.deltaTime * speed);

            //Make the player stay inbound:
            if (transform.position.x < horizontalMidPoint - horizontalRange)
                transform.position = new Vector3(horizontalMidPoint - horizontalRange, transform.position.y, transform.position.z);
            else if (transform.position.x > horizontalMidPoint + horizontalRange)
                transform.position = new Vector3(horizontalMidPoint + horizontalRange, transform.position.y, transform.position.z);

            if (transform.position.z < verticalMidPoint - verticalRange)
                transform.position = new Vector3(transform.position.x, transform.position.y, verticalMidPoint - verticalRange);
            else if (transform.position.z > verticalMidPoint + verticalRange)
                transform.position = new Vector3(transform.position.x, transform.position.y, verticalMidPoint + verticalRange);

            //Get the input from space bar:
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Launch the projectile:
                clone = Instantiate(food, transform.position, food.transform.rotation);
                clone.GetComponent<DetectCollision>().setOwner(this.gameObject);
            }
        }
    }

    public void AddToScore(float amount)
    {
        this.score += amount;
    }


    public void AddToHealth(float amount)
    {
        this.health += amount;
    }


}
