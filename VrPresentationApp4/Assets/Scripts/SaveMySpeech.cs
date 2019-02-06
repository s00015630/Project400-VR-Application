using System.Collections;
using UnityEngine;
using System.IO;
using System.Net;
using UnityEngine.UI;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class SaveMySpeech : MonoBehaviour
{
    public Button stopSpeech;
    private int speechTime = GetDurationTime.durationSelected;
    
    public AudioSource audioData;
    public bool playAudio = true;
    bool isRecording = true;
    public AudioClip myAudioClip;
    int count = 0;
    public string apiKey;
    // Start is called before the first frame update
    void Start()
    {
        if (isRecording)
        {
            Debug.Log("Speech is recording.");
            myAudioClip = GetComponent<AudioClip>();
            // Set duration from user input selected in the start menu
            
            myAudioClip = Microphone.Start("", true, speechTime, 44100);


        }
        //stopSpeech.onClick.AddListener(TaskOnClick);
    }

    // to deal with a recording stoped before timer runs out
   //void EndRecording()
   // {
        
        
   //     //Capture the current clip data
   //     AudioClip recordedClip = myAudioClip;
   //     var position = Microphone.GetPosition("");
   //     var soundData = new float[recordedClip.samples * recordedClip.channels];
   //     recordedClip.GetData(soundData, 0);

   //     //Create shortened array for the data that was used for recording
   //     var newData = new float[position * recordedClip.channels];

   //     //Microphone.End (null);
   //     //Copy the used samples to a new array
   //     for (int i = 0; i < newData.Length; i++)
   //     {
   //         newData[i] = soundData[i];
   //     }

   //     //One does not simply shorten an AudioClip,
   //     //    so we make a new one with the appropriate length
   //     var newClip = AudioClip.Create(recordedClip.name, position, recordedClip.channels, recordedClip.frequency, false);
   //     newClip.SetData(newData, 0);        //Give it the data from the old clip

   //     //Replace the old clip
   //     AudioClip.Destroy(recordedClip);
   //     audioData.clip = newClip;
   //     Debug.Log("stopped recordein form Endrecording method");
   // }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TaskOnClick()
    {
        
        //Output this to console when Button is clicked
        Debug.Log("You have clicked the button!");
        if (playAudio)
        {
            playAudio = false;
        }
        isRecording = false;
        Microphone.End(null); //Stop the audio recording
        
        Debug.Log("Recording has Stopped");

        SaveTheSpeech();
    }

    private void SaveTheSpeech()
    {
        //float filenameRand = UnityEngine.Random.Range(0.0f, 10.0f);
        string filename = "testing";// + filenameRand;
        //if (!filename.ToLower().EndsWith(".wav"))
        //{
        //    filename += ".wav";
        //}

        var filePath = Path.Combine("testing/", filename);
        filePath = Path.Combine(Application.persistentDataPath, filePath);
        Debug.Log("Created filepath string: " + filePath);

        // Make sure directory exists if user is saving to sub dir.
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        Microphone.End("");
        SavWav.Save(filePath, myAudioClip);

                //apiKey = "0dd1709ed323b7ac7aba870501496ef12f9cb0fc";

                //string apiURL = "http://www.google.com/speech-api/v2/recognize?output=json&lang=IE-&key=" + apiKey;
                //string Response;

                //Debug.Log("Uploading " + filePath);
                //Response = HttpUploadFile(apiURL, filePath, "file", "audio/wav; rate=44100");
                //Debug.Log("Response String: " + Response);

                //var jsonresponse = SimpleJSON.JSON.Parse(Response);

                //if (jsonresponse != null)
                //{
                //    string resultString = jsonresponse["result"][0].ToString();
                //    var jsonResults = SimpleJSON.JSON.Parse(resultString);

                //    string transcripts = jsonResults["alternative"][0]["transcript"].ToString();

                //    Debug.Log("transcript string: " + transcripts);
                //    //TextBox.text = transcripts;

                //}
    }

    public string HttpUploadFile(string url, string file, string paramName, string contentType)
    {
        Debug.Log(string.Format("Uploading {0} to {1}", file, url));
        HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
        wr.ContentType = "audio/l16; rate=44100";
        wr.Method = "POST";
        wr.KeepAlive = true;
        wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

        Stream rs = wr.GetRequestStream();
        Debug.Log("Get request stream");
        FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        Debug.Log("Get request stream");
        byte[] buffer = new byte[4096];
        int bytesRead = 0;
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            rs.Write(buffer, 0, bytesRead);
        }
        fileStream.Close();
        rs.Close();
        Debug.Log("Closed file stream");
        WebResponse wresp = null;
        try
        {
            wresp = wr.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);

            string responseString = string.Format("{0}", reader2.ReadToEnd());
            Debug.Log("HTTP RESPONSE" + responseString);
            return responseString;

        }
        catch (Exception ex)
        {
            Debug.Log(string.Format("Error uploading file error: {0}", ex));
            if (wresp != null)
            {
                wresp.Close();
                wresp = null;
                return "Error";
            }
        }
        finally
        {
            wr = null;

        }

        return "empty";
    }
}
