using UnityEngine;
using System.IO;
using System;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public sealed class GetExternalTextFile : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    
    public void ReadFromFile()
    {        
        m_Text = GetComponent<TextMeshProUGUI>();
        string fileName = "VrNots.txt";
        string filePathTesting;
        filePathTesting = Path.Combine(Application.persistentDataPath, fileName);
        
        if (!File.Exists(filePathTesting))
        {
            GetTextWeb();
            Debug.Log("Attempting to get from website");
        }
        else
        {
           
            StreamReader reader = new StreamReader(filePathTesting);
            m_Text.text = reader.ReadToEnd();
            Debug.Log(reader.ReadToEnd());
            reader.Close();
        }
    }
    public void GetTextWeb()
    {
        StartCoroutine(GetText());
        
    }
    

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://s3-eu-west-1.amazonaws.com/s00015630bucket1/folder1/vrnotes.txt");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            m_Text.text = "There was a problem retrieving your notes.\nThis could be caused by no internet connection or the server could be down";
            Debug.Log(www.error);
        }
        else
        {
            string origin = "NOTES FROM WEB\n";
            string input = www.downloadHandler.text;
            m_Text.text = origin+input;
            Debug.Log(input);
        }
    }



}
