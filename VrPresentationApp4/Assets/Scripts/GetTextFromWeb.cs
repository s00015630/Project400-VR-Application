using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GetTextFromWeb : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        m_Text.text = "Attempting to load text file";
        StartCoroutine(GetText());
    }
    
    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://mailitsligo-my.sharepoint.com/personal/s00015630_mail_itsligo_ie/_layouts/15/onedrive.aspx?id=%2Fpersonal%2Fs00015630_mail_itsligo_ie%2FDocuments%2FAttachments%2FVrNotes%2Etxt&parent=%2Fpersonal%2Fs00015630_mail_itsligo_ie%2FDocuments%2FAttachments");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string input = www.downloadHandler.text;
            m_Text.text = input;
            Debug.Log(input);           
        }
    }
 
}
