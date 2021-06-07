using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Gradient gradient;
    

    private void Start()
    {
    }

    public void setMaxValue(float amount)
    {
        //Add the maximum value:
        this.slider.maxValue = amount;
    }

    public void setValue(float amount)
    {
        this.slider.value = amount;
        this.fill.color = gradient.Evaluate(this.slider.normalizedValue);
    }

}
