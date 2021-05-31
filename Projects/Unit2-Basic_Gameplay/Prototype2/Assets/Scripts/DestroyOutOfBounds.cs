using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float upperBound = 35f, lowerBound = -15f, leftBound = -32f, rightBound = 32f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy the object if it goes out of bound:
        if(transform.position.z > upperBound || transform.position.z < lowerBound || transform.position.x < leftBound || transform.position.x > rightBound)
            Destroy(gameObject);
        
    }
}
