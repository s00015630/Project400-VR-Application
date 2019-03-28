using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolRecordAudio : MonoBehaviour
{
    public Toggle toggle;
    public static bool recordAudio;

    public void IsToggleActive() 
    {
        toggle = GetComponent<Toggle>();
        if (toggle.isOn)
        {
            recordAudio = true;
            Debug.Log("Active = " + recordAudio.ToString());

        }
        else
        {
            recordAudio = false;
            Debug.Log("Active = " + recordAudio.ToString());

        }
    }
}
