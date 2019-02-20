using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateQuestionButton : MonoBehaviour
{
    //Uncomment before final build
    //private float duration = (float)GetDurationTime.durationSelected;
    private float duration = 10f; 
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().interactable = false;
        Debug.Log("Not active");
        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
     {
 
         yield return new WaitForSeconds(duration);

        GetComponent<Button>().interactable = true;
        Debug.Log("Button is now active");
        
    }

    
}
