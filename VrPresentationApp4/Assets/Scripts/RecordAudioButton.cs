using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordAudioButton : MonoBehaviour
{
    public static bool recordAudio;
    public static Toggle m_Toggle;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("in start");
        IsToggleActive();
    }

    // Update is called once per frame
    public void IsToggleActive()
    {
        m_Toggle = GetComponent<Toggle>();
        if (m_Toggle.isOn)
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
