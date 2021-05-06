using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //Private attributes:
    private float speed = 35f;
    private float horizontalInput;
    private float leftBound = -20f, rightBound = 20f;


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
        if (transform.position.x < leftBound)
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        else if (transform.position.x > rightBound)
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
    }
}
