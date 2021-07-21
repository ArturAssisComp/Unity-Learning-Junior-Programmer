using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //---------------------------------------------------------------
    //Player Components and parts:
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private AudioSource playerAudioSource;
    public ParticleSystem playerExplosionEffect;
    public ParticleSystem playerDirtEffect;
    //---------------------------------------------------------------
    //Player status:
    private bool isOnGround = true;
    private bool gameOver = false;
    private bool willJump = false;
    public bool isGameOver { get { return this.gameOver; } }
    private int numOfJumps = 0;
    public float firstJumpForce = 2000f;
    public float secondJumpForce = 1800f;
    public float gravityModifier = 9f;
    //---------------------------------------------------------------
    //Sound effects:
    public AudioClip jumpSound;
    public AudioClip crashSound;

    //---------------------------------------------------------------
    //Methods:


    // Start is called before the first frame update
    void Start()
    {
        //---------------------------------------------------------------
        //Get player components:
        this.playerRigidbody = GetComponent<Rigidbody>();
        this.playerAnimator = GetComponent<Animator>();
        this.playerAudioSource = GetComponent<AudioSource>();
        //---------------------------------------------------------------
        //Change gravity:
        Physics.gravity *= this.gravityModifier;
        //---------------------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------------------------------------------
        //Apply upwards force if the player press space bar:
        if (Input.GetKeyDown(KeyCode.Space) && this.numOfJumps < 2 && !this.gameOver)
        {
            this.willJump = true;
            this.numOfJumps++;
        }
        //---------------------------------------------------------------
    }

    private void FixedUpdate()
    {
        if(this.willJump)
        {
            //---------------------------------------------------------------
            if(this.numOfJumps == 1)
            {
                //---------------------------------------------------------------
                //Set the jump_trig animator parameter:
                this.playerAnimator.SetTrigger("Jump_trig");

                //---------------------------------------------------------------
                //Apply updwards force:
                this.playerRigidbody.AddForce(Vector3.up * this.firstJumpForce, ForceMode.Impulse);

                //---------------------------------------------------------------
                //Change state of isOnGround to false:
                this.isOnGround = false;

                //---------------------------------------------------------------
                //Stop the dirt effect:
                this.playerDirtEffect.Stop();

                //---------------------------------------------------------------
            }
            else if (this.numOfJumps == 2)
            {
                //---------------------------------------------------------------
                //Set the jump_trig animator parameter:
                this.playerAnimator.SetTrigger("Second_Jump_trig");

                //---------------------------------------------------------------
                //Apply updwards force:
                this.playerRigidbody.AddForce(Vector3.up * this.secondJumpForce, ForceMode.Impulse);

                //---------------------------------------------------------------
            }


            //---------------------------------------------------------------
            //Play the audio effect:
            this.playerAudioSource.PlayOneShot(this.jumpSound);

            //---------------------------------------------------------------
            //Reset willJump bool:
            this.willJump = false;

            //---------------------------------------------------------------
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        //---------------------------------------------------------------
        //Check if the player collided with the ground:
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Go back to the ground and reset the number of jumps:
            this.isOnGround = true;
            this.numOfJumps = 0;

            if(!this.gameOver)
                this.playerDirtEffect.Play();

        }
        //Check if the player collided with an obstacle:
        else if (collision.gameObject.CompareTag("Obstacle") && !this.gameOver)
        {
            //---------------------------------------------------------------
            //Set game over:
            this.gameOver = true;
            Debug.Log("Game Over!");

            //---------------------------------------------------------------
            //Set the animation to death:
            this.playerAnimator.SetBool("Death_b", true);
            this.playerAnimator.SetInteger("DeathType_int", 1);

            //---------------------------------------------------------------
            //Set the particles effect:
            this.playerExplosionEffect.Play();

            //---------------------------------------------------------------
            //Stop the dirt effect:
            this.playerDirtEffect.Stop();

            //---------------------------------------------------------------
            //Play the audio effect:
            this.playerAudioSource.PlayOneShot(this.crashSound);

            //---------------------------------------------------------------
            //Stop the music:
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();

            //---------------------------------------------------------------
        }
        //---------------------------------------------------------------
    }

    public void SetSpeedUpAnimator(bool value)
    {
        this.playerAnimator.SetBool("SpeedUp", value);
    }

}
