using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerControllerX : MonoBehaviour
{
    private float rotationSpeed = 60f * 360f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);     
    }
}
