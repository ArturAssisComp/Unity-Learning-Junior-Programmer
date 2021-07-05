using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*In the next time, take all the random variables out from the functions or 
 * turn them into static variables and define them just one time.*/

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    private float positionSpeed;
    private float xRotationSpeed, yRotationSpeed, zRotationSpeed;
    public float currentPosition = 0;
    private float maxPositionSpeed = 30f, maxRotationSpeed = 360f;
    private bool forward = true;
    private float maxPosition = 1e10f;
    
    void Start()
    {
        //Initialize the static variables:
        r1 = Random.Range(8f, 10f);
        r2 = Random.Range(1f, 20f);
        a = Random.Range(0f, 20f); 
        r1_1 = Random.Range(10f, 30f);
        r2_1 = Random.Range(10f, 30f);
        r3_1 = Random.Range(10f, 30f);
        a_1 = Random.Range(4f, 10f);
        b1_1 = Mathf.Clamp(Random.Range(4f, 10f), 4f, a_1);
        b2_1 = Mathf.Clamp(Random.Range(4f, 10f), 4f, a_1);
        b3_1 = Mathf.Clamp(Random.Range(4f, 10f), 4f, a_1);

        //Define the rotation and position speed:
        this.positionSpeed =  10f + Random.value * maxPositionSpeed;
        this.xRotationSpeed = maxRotationSpeed * (Random.value * 2 - 1);
        this.yRotationSpeed = maxRotationSpeed * (Random.value * 2 - 1);
        this.zRotationSpeed = maxRotationSpeed * (Random.value * 2 - 1);
    }
    
    private void FixedUpdate()
    {
        //Update the position:
        if (this.forward)
            this.currentPosition += Time.fixedDeltaTime * this.positionSpeed;
        else
            this.currentPosition -= Time.fixedDeltaTime * this.positionSpeed;

        //Check if position is in bounds:
        if (this.currentPosition < 0)
        {
            this.currentPosition = 0;
            this.forward = true;
        }
        else if (this.currentPosition > this.maxPosition)
        {
            this.currentPosition = this.maxPosition;
            this.forward = false;
        }



        //Move the object:
        this.transform.parent.transform.position = this.translatePosition(this.currentPosition);

        //Change its scale:
        transform.localScale = this.scalePosition(this.currentPosition);

        //Rotate:
        transform.Rotate(xRotationSpeed * Time.fixedDeltaTime, yRotationSpeed * Time.fixedDeltaTime, zRotationSpeed * Time.fixedDeltaTime);

        //Change the color of the cube:
        this.Renderer.material.color = colorPosition(this.currentPosition);

    }

    
    private static float r1, r2, a; 
    private Vector3 translatePosition (float s)
    {
        float x0 = 0, y0 = 0, z0 = 30f;

        return new Vector3(x0 + r1 * Mathf.Cos(s/r1), y0 + r1 * Mathf.Sin(s/r1), z0 + a * Mathf.Cos(s/r2));
    }
    
    private static float r1_1, r2_1, r3_1;
    private static float a_1, b1_1, b2_1, b3_1;
    private Vector3 scalePosition(float s)
    {
        return new Vector3(a_1 + b1_1 * Mathf.Sin(s/r1_1), a_1 + b2_1 * Mathf.Cos(s/r2_1), a + b3_1 * Mathf.Cos(s/r3_1));
    }

    private Color colorPosition(float s)
    {
        Color CurrentColor = this.Renderer.material.color;
        float newRed, newGreen, newBlue, newAlpha, minColor = 0, maxColor = 255, minAlpha = 0, maxAlpha = 1f;
        float range = 0.2f;

        //Generate the new random colors:
        newRed   = Mathf.Clamp(CurrentColor.r + Random.Range(-range, range), minColor, maxColor);
        newGreen = Mathf.Clamp(CurrentColor.g + Random.Range(-range, range), minColor, maxColor);
        newBlue  = Mathf.Clamp(CurrentColor.b + Random.Range(-range, range), minColor, maxColor);
        newAlpha = Mathf.Clamp(CurrentColor.a + Random.Range(-range/20, range/20), minAlpha, maxAlpha);

        return new Color(newRed, newGreen, newBlue, newAlpha);
    }
}
