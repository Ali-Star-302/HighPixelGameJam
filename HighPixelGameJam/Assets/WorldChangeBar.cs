using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldChangeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image worldImage;
    [HideInInspector]
    public bool stationary;

    public void SetMaxCharge(int charge)
    {
        slider.maxValue = charge;
        slider.value = charge;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetCharge(int charge)
    {
        slider.value = charge;
        if (stationary)
            fill.color = gradient.Evaluate(slider.normalizedValue);
        else
            fill.color = Color.gray;
    }
}
