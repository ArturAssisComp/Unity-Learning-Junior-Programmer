using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //-------------------------------------------------
    //Components and game objects:
    public GameObject background;
    private MoveLeft MoveLeftScript;

    //-------------------------------------------------
    //Score attributes:
    private float score;
    private float distance;
    private float scoreMultiplier = 1f;
    private float speedUpBonus = 1.5f;


    //-------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //Initialize the score:
        this.score = 0;

        //Get components:
        this.MoveLeftScript = this.background.GetComponent<MoveLeft>();
        
        //Show the score:
        Debug.Log("Score = " + 10 * (int) (this.score / 10));
    }

    //-------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        //Update the score:
        this.distance = this.MoveLeftScript.getSpeed * Time.deltaTime;
        
        if(this.MoveLeftScript.isSpeedUp)
            this.score += this.distance * this.scoreMultiplier * this.speedUpBonus;
        else
            this.score += this.distance * this.scoreMultiplier;

        //Show the score:
        Debug.Log("Score = " + 10 * (int) (this.score / 10));

    }

    //-------------------------------------------------
}
