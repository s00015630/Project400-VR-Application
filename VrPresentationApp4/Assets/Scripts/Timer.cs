using System.Collections;
using UnityEngine;
using System.IO;
using System.Net;
using UnityEngine.UI;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class Timer : MonoBehaviour
{
    //private int timeLeft = GetDurationTime.durationSelected;
    public static bool speechFinished = false;
    private int timeLeft = GetSliderDuration.durationSelected;// duration for testing
    private int speechTime = GetSliderDuration.durationSelected;
    public Text countdownText;
    AudioSource audioData;
    public bool playAudio = true;
    bool isRecording = true;
    AudioClip myAudioClip;
    int count = 0;
    string apiKey;
    
    // Use this for initialization

    void Start()
    {
     
        StartCoroutine("LoseTime");
        if (isRecording)
        {
            Debug.Log("Is recording");
            myAudioClip = GetComponent<AudioClip>();
            //set up recording to last a max of 10 seconds and loop over and over
            myAudioClip = Microphone.Start("", true, speechTime, 44100);
        }

    }


    // Update is called once per frame
    void Update()
    {
        int minutesRemaining;
        int secondsRemaining;
        if (isRecording)
        {
            if (timeLeft > 60)
            {
                minutesRemaining = timeLeft / 60;
                secondsRemaining = timeLeft % 60;
                countdownText.text = (minutesRemaining + ":" + secondsRemaining);
            }
            else
            {
                countdownText.text = (timeLeft+" !");
            }
            if (timeLeft <= 0)
            {
                countdownText.text = "Time Up!";
                if (playAudio)
                {
                    playAudio = false;
                }
                isRecording = false;
                float filenameRand = UnityEngine.Random.Range(0.0f, 10.0f);

                string filename = "testing" + filenameRand;

                Microphone.End(null); //Stop the audio recording

                Debug.Log("Recording Stopped");

                if (!filename.ToLower().EndsWith(".wav"))
                {
                    filename += ".wav";
                }

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

                StopCoroutine("LoseTime");
                

                count++;
                speechFinished = true;
            }
        }


    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
    
}