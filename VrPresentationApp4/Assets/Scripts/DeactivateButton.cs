using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateButton : MonoBehaviour
{
    private float duration = 10f;
    public Button button;

    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(DeactivateOnClick);
    }

    //deactivate button so multiple questions cant be asked at the same time
    void DeactivateOnClick()
    {
        GetComponent<Button>().interactable = false;
        StartCoroutine(WaitForQuestion());
    }
    
    IEnumerator WaitForQuestion()
    {
        yield return new WaitForSeconds(duration);
        GetComponent<Button>().interactable = true;
        Debug.Log("Button is now active again");
    }
}
