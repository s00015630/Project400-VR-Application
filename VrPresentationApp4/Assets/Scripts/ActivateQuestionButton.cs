using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The "Ask Question" button becomes active after the speech duration has finished,
// plus 10 seconds for the timer delay
public class ActivateQuestionButton : MonoBehaviour
{
    private readonly float duration = 10f + (float)GetSliderDuration.durationSelected; 

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
