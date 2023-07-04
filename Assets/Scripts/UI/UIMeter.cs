using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMeter : MonoBehaviour
{
    public float minimum;
    public float maximum;
    public float current;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maximum;
        slider.minValue = minimum;
        Mathf.Clamp(current, minimum, maximum);
        slider.value = current;
    }

    public void SetValue(float value)
    {
        current = value;
        Mathf.Clamp(current, minimum, maximum);
        slider.value = current;
    }
}
