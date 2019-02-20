using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetTextFromWeb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://docs.google.com/document/d/1EXOGdfRlBaEAFeotCT1i2Kfmcddx-KJ9gliOaIHdyB4/edit");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string input = www.downloadHandler.text;
            string search = "<span class=\"kix-wordhtmlgenerator-word-node\"style=\"font-size:14.666666666666666px;font-family:Arial;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;\">";
            
            int p = input.IndexOf(search);
            Debug.Log(www.downloadHandler.text);
            if (p >= 0)
            {
                // move forward to the value
                int start = p + search.Length;
                // now find the end by searching for the next closing tag starting at the start position, 
                // limiting the forward search to the max value length
                int end = input.IndexOf("<span class=\"goog-inline-block\"style=\"width:7.859375px;height:17.599999999999998px\">&nbsp;</span></span>\",start)");
                if (end >= 0)
                {
                    // pull out the substring
                    string value = input.Substring(start, end - start);
                    // finally parse into a float
                    
                    Debug.Log("Value = " + value);
                }
                else
                {
                    Debug.Log("Bad html - closing tag not found");
                }
            }
            else
            {
                Debug.Log("Search tag not found!!");
            }

            //// Show results as text
            //Debug.Log(www.downloadHandler.text);

            //// Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
            
        }
    }
 
}
