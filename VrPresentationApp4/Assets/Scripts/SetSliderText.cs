using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderText : MonoBehaviour
{
    Text sliderText;

    void Start()
    {
        sliderText = GetComponent<Text>();
    }

    // Update is called once per frame
    public void textUpdate(float value)
    {
        sliderText.text = Mathf.RoundToInt(value) + " Minutes";
    }
}
