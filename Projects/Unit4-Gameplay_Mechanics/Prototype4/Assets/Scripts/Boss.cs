using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss :  Enemy
{

    //Definition of types:
        enum AttackType
    {
        NOTHING = 0,
        SMASH_ATTACK,
        MACHINE_GUN,
        SPAWN_ENEMY 
    }


    //Boss' attributes:
    private float maxHealth = 100f;
    private float maxScale  = 10f;
    private float minScale  = 2f;
    private float maxMass   = 500f;
    private float minMass   = 50f;
    private float maxPower  = 3000f;
    private float minPower  = 500f;
    private float collisionDamageTaken = 1f;
    private float repelentForce = 25f;
    private float currentHealth;
    private float attackingPeriod = 10f;
    public Renderer bossRenderer;

    //Attributes for special attacks:
    //SMASH_ATTACK:
    private float smashAttackMaxForce         = 1000f;
    private float smashAttackMinForce         = 500f;
    private float smashAttackJumpAcceleration = 10f;
    private float smashAttackGravityFactor = 1f;
    private bool isSmashAttackActive = false;
    private Vector3 awayDirection;

    //MACHINE_GUN:
    public GameObject bossProjetile;
    private Vector3   shootingDirection;
    private float machineGunCountdownTime = 5f;
    private float shootingPeriodMax = 0.5f;
    private float shootingPeriodMin = 0.1f;

    //SPAWN_ENEMY:
    public GameObject[] enemy;
    public GameObject[] powerUp;
    private float waveLevel = 1;
    private float deltaWaveLevel = 1f;
    private int enemiesPerWave = 1;
    private int currentNumberOfEnemies = 0;
         

    //Get functions:
    public float GetHealth { get { return this.currentHealth; } }

    public override void Start()
    {
        base.Start();


        //Start the health to max:
        this.SetHealth(this.maxHealth);

        //Start invoking the attacking method from the boss:
        InvokeRepeating("GenericSpecialAttack", 2 * attackingPeriod, attackingPeriod);


    }

    public void AddToHealth(float amount)
    {
        this.currentHealth += amount;

        //Check if the boss has died:
        if(this.currentHealth <= 0)
        {
            //The boss died.
            StopAllCoroutines();
            Destroy(this.gameObject);
        }

        //Update other attributes:
        this.UpdateAttributes();
    }

    private void SetHealth(float amount)
    {
        this.currentHealth = amount;

        //Update other attributes:
        this.UpdateAttributes();
    }

    /// <summary>
    /// This function updates the attributes scale, weight, and power of the boss.
    /// It does not update the health. This function must be called from the method
    /// that updates the health.
    /// </summary>
    private void UpdateAttributes()
    {
        const float maxColorValue = 1f;
        float currentProportion = this.currentHealth / this.maxHealth;
        float newRedAndGreen;

        //Update scale:
        this.transform.localScale = Vector3.one * ((this.maxScale - this.minScale) * currentProportion + this.minScale);

        //Update weight:
        this.enemyRigidbody.mass = (this.maxMass - this.minMass) * currentProportion + this.minMass;

        //Update the power:
        this.power = (this.maxPower - this.minPower) * currentProportion + this.minPower;

        //Update the color:
        newRedAndGreen = maxColorValue * currentProportion;
        this.bossRenderer.material.color = new Color(maxColorValue, newRedAndGreen, newRedAndGreen, maxColorValue);

    }

    /// <summary>
    /// This method randomly calls a generic method of a special attack each time it is called.
    /// </summary>
    private void GenericSpecialAttack()
    {
        float currentProportion = this.currentHealth / this.maxHealth;

        if(currentProportion > 0)
        {
            switch (Random.Range((int) AttackType.NOTHING, (int) AttackType.SPAWN_ENEMY + 1))
            {
                case (int) AttackType.NOTHING:
                    break;
                case (int) AttackType.SMASH_ATTACK:
                    //Add vertical impulse and change the gravity:
                    this.enemyRigidbody.AddForce(Vector3.up * this.enemyRigidbody.mass * this.smashAttackJumpAcceleration, ForceMode.Impulse);
                    Physics.gravity *= this.smashAttackGravityFactor;

                    //Activate the smash attack:
                    this.isSmashAttackActive = true;
                    break;
                case (int) AttackType.MACHINE_GUN:
                    StartCoroutine(this.MachineGunCountdownRoutine());
                    float repeatPeriod = currentProportion * (this.shootingPeriodMax - this.shootingPeriodMin) + this.shootingPeriodMin;
                    InvokeRepeating("ShootPlayer", 0.01f, repeatPeriod); 
                    break;
                case (int) AttackType.SPAWN_ENEMY:
                    //Check if there are still enemies:
                    this.currentNumberOfEnemies = FindObjectsOfType<Enemy>().Length;
                    
                    if(this.currentNumberOfEnemies < this.enemiesPerWave + 1)
                    {
                        //Increase difficult:
                        this.enemiesPerWave++;
                        this.waveLevel += this.deltaWaveLevel;

                        //Spawn enemies and a power up:
                        this.SpawnWave(this.enemiesPerWave - this.currentNumberOfEnemies);
                    }
                    break;
                default:
                    break;
            }

            //Spawn 1 power up for the enemy:
            this.SpawnPowerUp(1);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.AddToHealth(-this.collisionDamageTaken);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((collision.gameObject.transform.position - this.transform.position).normalized * this.repelentForce, ForceMode.Impulse);

        }
        else if (collision.gameObject.CompareTag("Floor") && this.isSmashAttackActive)
        {
            float currentProportion = this.currentHealth / this.maxHealth;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            this.awayDirection = player.transform.position - this.transform.position;
            player.GetComponent<Rigidbody>().AddForce(this.awayDirection.normalized * ((this.smashAttackMaxForce - this.smashAttackMinForce) * currentProportion + this.smashAttackMinForce) / Mathf.Pow(this.awayDirection.magnitude, 2), ForceMode.Impulse);
            
            //Reset the gravity:
            Physics.gravity /= this.smashAttackGravityFactor;

            //Deactivate the smash attack:
            this.isSmashAttackActive = false;
        }

    }


    private void SpawnWave(int numberOfEnemies)
    {
        int enemyIndex = 0;

        for(int i = 0; i < numberOfEnemies; i++)
        {
            for (int j = 1; j <= this.enemy.Length; j++)
            {
                if (Random.value <= 1 / Mathf.Pow(this.waveLevel, j))
                {
                    enemyIndex = Random.Range(0, j);
                    break;
                }
            }

            Instantiate(this.enemy[enemyIndex], GenerateSpawnPosition(), this.enemy[enemyIndex].transform.rotation);
        }
    }

    private void SpawnPowerUp(int numberOfPowerUps)
    {
        int powerUpIndex = 0;

        for(int i = 0; i < numberOfPowerUps; i++)
        {
            powerUpIndex = Random.Range(0, this.powerUp.Length);
            Instantiate(this.powerUp[powerUpIndex], GenerateSpawnPosition(), this.powerUp[powerUpIndex].transform.rotation);

        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float rangePosition = 8f;
        float xPosition, zPosition;

        xPosition = Random.Range(-rangePosition, rangePosition);
        zPosition = Random.Range(-rangePosition, rangePosition);

        return new Vector3(xPosition, 0,  zPosition);
    }

    IEnumerator MachineGunCountdownRoutine()
    {
        yield return new WaitForSeconds(this.machineGunCountdownTime);
        CancelInvoke("ShootPlayer");
    }
    private void ShootPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            this.shootingDirection = player.transform.position - this.transform.position;
            Instantiate(this.bossProjetile, this.transform.position, Quaternion.FromToRotation(this.bossProjetile.transform.forward, this.shootingDirection));
        }
    }

}
