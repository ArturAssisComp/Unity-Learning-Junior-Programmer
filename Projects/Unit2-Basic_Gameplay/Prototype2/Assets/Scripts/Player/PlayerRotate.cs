using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the player to the direction of the mouse:
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouse);
        mouseWorld.y = 0f; 
        Vector3 forward = mouseWorld - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
