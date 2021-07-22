using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    //----------------------------------------------------------
    //External objects:
    public GameObject playerGameObject;
    public ParticleSystem dirtParticleEffect;
    public GameObject background;
    public GameObject spawnManager;
    public GameObject scoreManager;
    public GameObject mainCamera;

    //----------------------------------------------------------
    //CutScene states:
    private enum cutSceneState
    {
        WALKING,
        IDLE,
        START_GAME
    }
    cutSceneState currentState = cutSceneState.WALKING;

    //----------------------------------------------------------
    //CutScene status:
    public float playerSpeed = 3f;
    private float finalXPosition = 0f;
    private float idleTimer = 1.5f;
    private float startGameTimer = 0.6f;

    //----------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        //Stop particle effects from player:
        this.dirtParticleEffect.Stop();
    }

    //----------------------------------------------------------
    // Update is called once per frame
    void Update()
    {

        //----------------------------------------------------------
        switch (this.currentState)
        {
            case cutSceneState.WALKING:
                {
                    //----------------------------------------------------------
                    //Stop particle effects from player:
                    if(!this.dirtParticleEffect.isStopped)
                        this.dirtParticleEffect.Stop();

                    //----------------------------------------------------------
                    //Move the player to the right:
                    this.playerGameObject.transform.Translate(Vector3.forward * this.playerSpeed * Time.deltaTime);

                    //----------------------------------------------------------
                    //Check if the player reached the final position:
                    if(this.playerGameObject.transform.position.x >= this.finalXPosition)
                    {
                        //----------------------------------------------------------
                        //Update the current state:
                        this.currentState = cutSceneState.IDLE;

                        //----------------------------------------------------------
                        //Start the music:
                        this.mainCamera.GetComponent<AudioSource>().enabled = true;

                        //----------------------------------------------------------
                        //update the final value of player x position and freezes its x coordinate:
                        this.playerGameObject.transform.position = new Vector3(0, this.playerGameObject.transform.position.y, 
                                                                               this.playerGameObject.transform.position.z);
                        this.playerGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation  | 
                                                                                      RigidbodyConstraints.FreezePositionX | 
                                                                                      RigidbodyConstraints.FreezePositionZ;
                        //----------------------------------------------------------
                        //Update the player's animation mode:
                        this.playerGameObject.GetComponent<Animator>().SetFloat("Speed_f", 0.2f);

                        //----------------------------------------------------------
                    }

                    //----------------------------------------------------------
                }
                break;
            case cutSceneState.IDLE:
                {
                    if(this.idleTimer > 0)
                    {
                        //----------------------------------------------------------
                        //Update the timer:
                        this.idleTimer -= Time.deltaTime;
                        //----------------------------------------------------------
                    }
                    else
                    {
                        //----------------------------------------------------------
                        //Update the state:
                        this.currentState = cutSceneState.START_GAME;
                        //----------------------------------------------------------
                    }
                }
                break;
            case cutSceneState.START_GAME:
                {
                    //----------------------------------------------------------
                    //Update the player's animation mode:
                    this.playerGameObject.GetComponent<Animator>().SetFloat("Speed_f", 0.6f);

                    if(this.startGameTimer > 0)
                    {
                        //----------------------------------------------------------
                        //Update the timer:
                        this.startGameTimer -= Time.deltaTime;
                        //----------------------------------------------------------
                    }
                    else
                    {
                        //----------------------------------------------------------
                        //Activate the scripts to start the game:
                        this.playerGameObject.GetComponent<PlayerController>().enabled = true;
                        this.background.GetComponent<MoveLeft>().enabled = true;
                        this.spawnManager.GetComponent<SpawnManager>().enabled = true;
                        this.scoreManager.GetComponent<ScoreManager>().enabled = true;

                        //----------------------------------------------------------
                        //Deactivate this script:
                        this.enabled = false;

                        //----------------------------------------------------------
                    }

                }
                break;
            default:
                break;
        }

        //----------------------------------------------------------
    }

    //----------------------------------------------------------
}
