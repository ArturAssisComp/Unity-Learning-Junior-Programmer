using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public Image fill;
    public Gradient gradient;
    

    private void Start()
    {
        //Get the necessary component:
        this.slider = this.GetComponent<Slider>();
    }

    public void setMaxHealth(float amount)
    {
        this.slider.maxValue = amount;
        this.slider.value = amount;
        this.fill.color = gradient.Evaluate(this.slider.normalizedValue);
    }

    public void setHealth(float amount)
    {
        this.slider.value = amount;
        this.fill.color = gradient.Evaluate(this.slider.normalizedValue);
    }

}
