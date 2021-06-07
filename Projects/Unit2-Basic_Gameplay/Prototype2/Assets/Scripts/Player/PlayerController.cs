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
    public ProgressBar playerHealthBar;




    // Start is called before the first frame update
    void Start()
    {
        this.health = this.maxHealth;
        playerHealthBar.setMaxValue(this.maxHealth);
        playerHealthBar.setValue(this.health);
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
            this.horizontalInput = Input.GetAxis("Horizontal");
            this.verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(this.horizontalInput, 0, this.verticalInput);
            direction.Normalize();

            MovePlayer(direction, this.speed, this.horizontalMidPoint, this.horizontalRange, this.verticalMidPoint, this.verticalRange);

            //Get the input from space bar:
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Launch the projectile:
                this.clone = Instantiate(this.food, this.transform.position, this.food.transform.rotation);
                this.clone.GetComponent<DetectEnemyCollision>().setOwner(this.gameObject);
            }
        }
    }


    void MovePlayer(Vector3 direction, float speed, float horizontalMidPoint, float horizontalRange, float verticalMidPoint, float verticalRange)
    //Move the player in the direction given by 'direction' with speed given by 'speed' with the given constraints.
    {
            //Move the player within the lateral/vertical bounds:
            this.transform.Translate(direction * Time.deltaTime * speed);

            //Make the player stay inbound:
            //Horizontal
            if (this.transform.position.x < horizontalMidPoint - horizontalRange)
                this.transform.position = new Vector3(horizontalMidPoint - horizontalRange, this.transform.position.y, this.transform.position.z);
            else if (this.transform.position.x > horizontalMidPoint + horizontalRange)
                this.transform.position = new Vector3(horizontalMidPoint + horizontalRange, this.transform.position.y, this.transform.position.z);

            //Vertical
            if (this.transform.position.z < verticalMidPoint - verticalRange)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, verticalMidPoint - verticalRange);
            else if (this.transform.position.z > verticalMidPoint + verticalRange)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, verticalMidPoint + verticalRange);
    }


    public void AddToScore(float amount)
    {
        this.score += amount;
    }


    public void AddToHealth(float amount)
    {
        this.health += amount;
        playerHealthBar.setValue(this.health);
    }


}
