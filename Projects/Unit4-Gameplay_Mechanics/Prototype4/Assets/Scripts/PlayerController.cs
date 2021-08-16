using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum powerUp
{
    NONE,
    SMASH_ATTACK,
    REPELENT,
    MACHINE_GUN 
}

public class PlayerController : MonoBehaviour
{
    //Atributes:
    public float force = 5f;
    public GameObject powerUpIndicator;

    private Vector3 powerUpOffSet = new Vector3(0, -0.18f, 0);
    private float input;
    private float countdownTime = 4f;
    private Rigidbody rigidBody;
    private GameObject CameraCenterPoint;

    //Attributes for general power ups:
    private powerUp powerUpStatus = powerUp.NONE;
    private GameObject[] enemies;

    //Attributes for smash atack power up:
    private float smashAttackForce         = 1000f;
    private float smashAttackJumpForce     = 30f;
    private float smashAttackGravityFactor = 10f;
    private Vector3 awayDirection;

    //Attributes for repelent power up:
    private float repelentForce = 25f;

    //Attributes for machine gun shooting:
    public GameObject projetile;
    private Vector3   shootingDirection;


    // Start is called before the first frame update
    void Start()
    {
        //Get the player's components:
        this.rigidBody = this.gameObject.GetComponent<Rigidbody>();

        //Get the references to GameObject:
        this.CameraCenterPoint = GameObject.Find("Camera Center Point");
    }

    // Update is called once per frame
    void Update()
    {
        //Get inputs:
        this.input = Input.GetAxis("Vertical");

        //Update the position of the power up:
        this.powerUpIndicator.transform.position = this.transform.position + this.powerUpOffSet;

    }

    private void FixedUpdate()
    {
        if(this.input != 0)
        {
            //Move the player:
            this.rigidBody.AddForce(this.CameraCenterPoint.transform.forward * this.input * this.force, ForceMode.Force);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Check if it is a power up:
        if(this.powerUpStatus == powerUp.NONE)
        {
            if(other.CompareTag("RepelentPowerUp"))
            {
                this.powerUpStatus = powerUp.REPELENT;
                this.powerUpIndicator.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(this.PowerUpCountdownRoutine("Repelent"));
            }
            else if (other.CompareTag("MachineGunPowerUp"))
            {
                this.powerUpStatus = powerUp.MACHINE_GUN;
                this.powerUpIndicator.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(this.PowerUpCountdownRoutine("MachineGun"));

                InvokeRepeating("ShootAllEnemies", 0.01f, 0.2f); 
            }
            else if (other.CompareTag("SmashAttackPowerUp"))
            {
                this.powerUpStatus = powerUp.SMASH_ATTACK;
                this.powerUpIndicator.SetActive(true);
                Destroy(other.gameObject);

                //Add vertical impulse and change the gravity:
                this.rigidBody.AddForce(Vector3.up * this.smashAttackJumpForce, ForceMode.Impulse);
                Physics.gravity *= this.smashAttackGravityFactor;
            }
        }
    }

    private void ShootAllEnemies()
    {
        this.enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in this.enemies)
        {
            this.shootingDirection = item.transform.position - this.transform.position;
            Instantiate(this.projetile, this.transform.position, Quaternion.FromToRotation(this.projetile.transform.forward, this.shootingDirection));
        }
    }
    private void SmashAttackAllEnemies()
    {
        this.enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in this.enemies)
        {
            this.awayDirection = item.transform.position - this.transform.position;
            item.GetComponent<Rigidbody>().AddForce(this.awayDirection.normalized * this.smashAttackForce / Mathf.Pow(this.awayDirection.magnitude, 2), ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdownRoutine(string type)
    {
        yield return new WaitForSeconds(this.countdownTime);
        this.powerUpIndicator.SetActive(false);

        if(type.Equals("MachineGun"))
            CancelInvoke("ShootAllEnemies");

        //Reset the power up status:
        this.powerUpStatus = powerUp.NONE;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if the player collides with enemy while with power up:
        if (collision.gameObject.CompareTag("Enemy") && this.powerUpStatus == powerUp.REPELENT)
        {
            Vector3 awayDirection = (collision.gameObject.transform.position - this.transform.position).normalized;
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            enemyRigidBody.AddForce(awayDirection * this.repelentForce, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Floor") && this.powerUpStatus == powerUp.SMASH_ATTACK)
        {
            this.SmashAttackAllEnemies();

            //Reset the gravity:
            Physics.gravity /= this.smashAttackGravityFactor;

            this.powerUpIndicator.SetActive(false);

            //Reset the power up status:
            this.powerUpStatus = powerUp.NONE;
        }
    }


}
