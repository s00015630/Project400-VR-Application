using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSliderDuration : MonoBehaviour
{
    public Slider slider;
    public static int durationSelected = 0;


    public void SliderPosition()
    {
        float result = slider.value;
        durationSelected = (int)result * 60;
        Debug.Log("Slider value recorded as " + durationSelected.ToString());

    }
        
}
