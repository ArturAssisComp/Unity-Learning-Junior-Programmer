using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //-----------------------------------------------------
    /*Attributes*/

    private float speed = 10f;

    //-----------------------------------------------------
    /*Methods*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //-----------------------------------------------------
        //Move the game object to the left:
        this.transform.Translate(Vector3.left * Time.deltaTime * this.speed);

        //-----------------------------------------------------
    }

    //Set the speed:
    public void SetSpeed (float speed)
    {
        this.speed = speed;
    }
    //-----------------------------------------------------
}
