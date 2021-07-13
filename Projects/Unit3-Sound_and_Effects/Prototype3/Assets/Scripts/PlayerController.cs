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
    public float jumpForce = 700f;
    public float gravityModifier = 3f;
    private bool isOnGround = true;
    private bool gameOver = false;
    public bool isGameOver { get { return this.gameOver; } }
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
        if (Input.GetKeyDown(KeyCode.Space) && this.isOnGround && !this.gameOver)
        {
            //---------------------------------------------------------------
            //Apply updwards force:
            this.playerRigidbody.AddForce(Vector3.up * this.jumpForce, ForceMode.Impulse);

            //---------------------------------------------------------------
            //Change state of isOnGround to false:
            this.isOnGround = false;

            //---------------------------------------------------------------
            //Set the jump_trig animator parameter:
            this.playerAnimator.SetTrigger("Jump_trig");

            //---------------------------------------------------------------
            //Stop the dirt effect:
            this.playerDirtEffect.Stop();
            //---------------------------------------------------------------
            //Play the audio effect:
            this.playerAudioSource.PlayOneShot(this.jumpSound);

            //---------------------------------------------------------------
        }
        //---------------------------------------------------------------
    }

    private void OnCollisionEnter(Collision collision)
    {
        //---------------------------------------------------------------
        //Check if the player collided with the ground:
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.isOnGround = true;
            this.playerDirtEffect.Play();

        }
        //Check if the player collided with an obstacle:
        else if (collision.gameObject.CompareTag("Obstacle"))
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
        }
        //---------------------------------------------------------------
    }


}
