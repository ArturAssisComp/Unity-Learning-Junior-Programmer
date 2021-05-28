using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement:
    private float speed = 20f;
    private float horizontalInput, verticalInput;

    //Movement Constraints:
    private float horizontalMidPoint = 0f, verticalMidPoint = 7.5f, horizontalRange = 20f, verticalRange = 5.6f;

    //Projetile:
    public GameObject food;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Get the horizontal and vertical inputs [-1, +1]:
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction.Normalize();

        //Move the player within the lateral bounds:
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
            Instantiate(food, transform.position, food.transform.rotation);
        }
    }
}
