using System.Collections;
using UnityEngine;
using System.IO;
using System.Net;
using UnityEngine.UI;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading;

public class Timer : MonoBehaviour
{
    
    public static bool speechFinished = false;
    private int timeLeft = 10;//GetSliderDuration.durationSelected;// duration for testing
    private int speechTime = 10;// GetSliderDuration.durationSelected;
    public Text countdownText;
    AudioSource audioData;

    bool isRecording = RecordUserAudio.recordAudio;
    AudioClip myAudioClip;
    string apiKey;
    
    // Use this for initialization

    void Start()
    {

        Debug.Log(RecordUserAudio.recordAudio.ToString());
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

        if (timeLeft > 60)
        {
            minutesRemaining = timeLeft / 60;
            secondsRemaining = timeLeft % 60;
            countdownText.text = (minutesRemaining + ":" + secondsRemaining);
        }
        else
        {
            countdownText.text = (timeLeft + " !");
        }

        if (timeLeft <= 0)
        {
            countdownText.text = "Time Up!";
            StopCoroutine("LoseTime");
            speechFinished = true;

            if (isRecording)
            {
                isRecording = false;
                float filenameRand = UnityEngine.Random.Range(0.0f, 10.0f);

                string filename = "Speech" + filenameRand;

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

                SendAudioToEmail.SendEmail(filePath);

                
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