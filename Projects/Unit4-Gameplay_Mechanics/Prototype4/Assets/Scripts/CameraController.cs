using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Attributes:
    public float rotationSpeed = 50f;
    private float input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the user's input:
        this.input = Input.GetAxis("Horizontal");

        //Move the camera:
        this.transform.Rotate(Vector3.up * this.rotationSpeed * Time.deltaTime * this.input);
    }
}
