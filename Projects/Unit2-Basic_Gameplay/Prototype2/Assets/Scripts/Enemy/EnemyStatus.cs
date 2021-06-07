using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{

    //Hungry status:
    public float maxHungry = 3f;
    private float currentHungry;
    public GameObject hungryBarUI;
    private ProgressBar progressBarComponent;
    public float getHungry{ get { return currentHungry; } }

    // Start is called before the first frame update
    void Start()
    {
        progressBarComponent = this.hungryBarUI.GetComponentInChildren<ProgressBar>();
        this.currentHungry = 0f;
        this.progressBarComponent.setMaxValue(this.maxHungry);
        this.progressBarComponent.setValue(this.currentHungry);
        hungryBarUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Activate the hungry bar:
        if (this.currentHungry > 0) this.hungryBarUI.SetActive(true);

        //Check if the enemy is fed:
        if (this.currentHungry >= this.maxHungry)
            Destroy(this.gameObject);

    }


    public void AddToHungry(float amount)
    {
        this.currentHungry += amount;
        this.progressBarComponent.setValue(this.currentHungry);
    }





}
