using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Atributes:
    public float force = 5f;
    public GameObject powerUpIndicator;
    private Vector3 powerUpOffSet = new Vector3(0, -0.18f, 0);
    private float powerUpForce = 20f;
    private float input;
    private float countdownTime = 4f;
    private Rigidbody rigidBody;
    private GameObject CameraCenterPoint;
    private bool hasRepelentPowerUp = false;
    private bool hasMachineGunPowerUp = false;

    //Attributes for machine gun shooting:
    public GameObject projetile;
    private GameObject[] enemies;
    private Vector3 shootingDirection;


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
        if(this.hasMachineGunPowerUp == false && this.hasRepelentPowerUp == false)
        {
            if(other.CompareTag("RepelentPowerUp"))
            {
                this.hasRepelentPowerUp = true;
                this.powerUpIndicator.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(this.PowerUpCountdownRoutine("Repelent"));
            }
            else if (other.CompareTag("MachineGunPowerUp"))
            {
                this.hasMachineGunPowerUp = true;
                this.powerUpIndicator.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(this.PowerUpCountdownRoutine("MachineGun"));

                InvokeRepeating("ShootAllEnemies", 0.01f, 0.2f); 
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

    IEnumerator PowerUpCountdownRoutine(string type)
    {
        yield return new WaitForSeconds(this.countdownTime);
        this.powerUpIndicator.SetActive(false);

        if(type.Equals("Repelent"))
            this.hasRepelentPowerUp = false;
        else if(type.Equals("MachineGun"))
        {
            this.hasMachineGunPowerUp = false;
            CancelInvoke("ShootAllEnemies");

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if the player collides with enemy while with power up:
        if (collision.gameObject.CompareTag("Enemy") && this.hasRepelentPowerUp)
        {
            Vector3 awayDirection = (collision.gameObject.transform.position - this.transform.position).normalized;
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            enemyRigidBody.AddForce(awayDirection * this.powerUpForce, ForceMode.Impulse);
        }
    }


}
