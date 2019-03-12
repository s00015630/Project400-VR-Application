using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordUserAudio : MonoBehaviour
{
    public Toggle toggle;
    public static bool recordAudio;

    public void IsToggleActive()
    {
        toggle = GetComponent<Toggle>();
        if (toggle.isOn)
        {
            recordAudio = true;
            Debug.Log("Active");
            
        }
        else
        {
            recordAudio = false;
            Debug.Log("Not Active");
           
        }
    }
}
