using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public sealed class GetExternalTextFile : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    
    public void ReadFromFile()
    {
        
        m_Text = GetComponent<TextMeshProUGUI>();
        m_Text.text = "Attempting to load text file";
        string fileName = "VrNotes.txt";
        string filePath;
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(filePath))
        {
            m_Text.text = "Text file not found. File must be a text file and named \"VrNotes.txt\"";
            Debug.LogErrorFormat("Error reading {0}\nFile does not exist!", filePath);
        }
        else
        {
           
            StreamReader reader = new StreamReader(filePath);
            m_Text.text = reader.ReadToEnd();
            Debug.Log(reader.ReadToEnd());
            reader.Close();
        }

    }
    public void ButtonClicked()
    {
        Debug.Log("The button is clicked");
    }


}
