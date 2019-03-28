using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateQuestionButton : MonoBehaviour
{
    //Uncomment before final build
    private readonly float duration = 10f; // + (float)GetDurationTime.durationSelected; 

    void Start()
    {
        GetComponent<Button>().interactable = false;
        Debug.Log("Not active");
        StartCoroutine(WaitForSpeechToFinish());
    }

    IEnumerator WaitForSpeechToFinish()
     {
        yield return new WaitForSeconds(duration);
        GetComponent<Button>().interactable = true;       
    }
}
