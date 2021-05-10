using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float minDelay = 2f, timeElapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        //Update timeElapsed:
        timeElapsed += Time.deltaTime;

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && timeElapsed >= minDelay)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            //Reset timeElapsed:
            timeElapsed = 0;
        }
    }
}
