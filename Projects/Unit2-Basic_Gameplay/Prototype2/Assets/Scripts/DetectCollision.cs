using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
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
        //Destroy the object and the other.
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
