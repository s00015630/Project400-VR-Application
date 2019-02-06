using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerQuestion : MonoBehaviour
{
    private float sec = 10f; //GetDurationTime.durationSelected;
    //private float sec = (float)GetDurationTime.durationSelected
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().interactable = false;
        Debug.Log("Not active");
        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
     {
 
         yield return new WaitForSeconds(sec);

        GetComponent<Button>().interactable = true;
        Debug.Log("Button is now active");
        //Do Function here...
    }

    
}
