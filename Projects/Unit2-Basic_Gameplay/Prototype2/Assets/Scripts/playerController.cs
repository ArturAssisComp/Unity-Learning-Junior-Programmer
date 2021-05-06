using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Private attributes:
    private float speed = 45f;
    private float horizontalInput;
    private float mid_point = 0f, range = 20f;

    //Public attributes:
    public GameObject food;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Get the horizontal input [-1, +1]:
        horizontalInput = Input.GetAxis("Horizontal");

        //Move the player within the lateral bounds:
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        //Make the player stay inbound:
        if (transform.position.x < mid_point - range)
            transform.position = new Vector3(mid_point - range, transform.position.y, transform.position.z);
        else if (transform.position.x > mid_point + range)
            transform.position = new Vector3(mid_point + range, transform.position.y, transform.position.z);

        //Get the input from space bar:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Launch the projectile:
            Instantiate(food, transform.position, food.transform.rotation);
        }
    }
}
