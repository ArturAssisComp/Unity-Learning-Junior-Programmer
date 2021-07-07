using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    //---------------------------------------------------------
    /*Attributes*/
    //Background status:
    private float x0, repeatWidth;

    

    //---------------------------------------------------------
    /*Methods*/

    // Start is called before the first frame update
    void Start()
    {
        this.x0 = this.transform.position.x;
        this. repeatWidth = this.GetComponent<SpriteRenderer>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x <= this.x0 - this.repeatWidth)
        {
            this.transform.position = new Vector3(this.x0, this.transform.position.y, this.transform.position.z);
        }
    }
    //---------------------------------------------------------
}
