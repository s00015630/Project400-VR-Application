using System.Collections;
using UnityEngine;
using System.IO;
using System.Net;
using UnityEngine.UI;
using Amazon.S3;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading;
using System.Net.Mail;
using Amazon;
using AWSSDK.Examples;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon.CognitoIdentity;


public class Timer : MonoBehaviour
{
    #region VARIABLES

    public static S3Manager Instance { get; set; }

    [Header("AWS Setup")]
    [SerializeField] private string identityPoolId = "eu-west-1:4b6476b1-7eb8-4376-a6f6-5ab2f3bfbff5";
    [SerializeField] private string cognitoIdentityRegion = RegionEndpoint.EUWest1.SystemName;
    [SerializeField] private string s3Region = RegionEndpoint.EUWest1.SystemName;
    private string bucketUrl = "https://s3-eu-west-1.amazonaws.com/classtalknotes/";
    private string _bucketName = "classtalknotes";
    // Variables privates
    private int timeoutGetObject = 5; // seconds
    private string resultTimeout = "";
    //public Action<GetObjectResponse, string> OnResultGetObject;
    private IAmazonS3 s3Client;
    private AWSCredentials credentials;

    // Propertys
    private RegionEndpoint CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(cognitoIdentityRegion); }
    }
    private RegionEndpoint S3Region
    {
        get { return RegionEndpoint.GetBySystemName(s3Region); }
    }
    private AWSCredentials Credentials
    {
        get
        {
            if (credentials == null)
                credentials = new CognitoAWSCredentials(identityPoolId, CognitoIdentityRegion);
            return credentials;
        }
    }
    private IAmazonS3 Client
    {
        get
        {
            if (s3Client == null)
            {
                s3Client = new AmazonS3Client(Credentials, S3Region);
            }
            //test comment
            return s3Client;
        }
    }

    #endregion

    public static bool speechFinished = false;
    public static bool speechSaved = false;
    public static string speechPath;
    private bool startTimer = false;
    private int timeLeft = 10;//duration for testing   GetSliderDuration.durationSelected;
    private int speechTime = 10;// GetSliderDuration.durationSelected;
    private int timerDelay = 10;
    public bool hasStarted;
    private bool emailSent;
    public Text countdownText;
    AudioSource audioData;
    private string fileName = "Speech";
    bool isRecording = BoolRecordAudio.recordAudio;
    AudioClip myAudioClip;
    private bool fileDeleted;

    // Use this for initialization

    void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        Debug.Log("Audio recording = "+ BoolRecordAudio.recordAudio.ToString());
        StartCoroutine("LoseTimeDelay");
        
        if (isRecording)
        {
            Debug.Log("Is recording");
            myAudioClip = GetComponent<AudioClip>();
            //set up recording to last a max of 10 seconds and loop over and over
            myAudioClip = Microphone.Start("", true, speechTime, 44100);
           
        }

    }
    //Start the timer based on duration
    void StartTheTimer()
    {       
        if (!hasStarted)
        {
            StartCoroutine("LoseTime");
            hasStarted = true;
        }      
    }

    #region
    // Update the timer and call the save method when it reaches zero
    void Update()
    {
        if (timeLeft+timerDelay > timeLeft)
        {
            countdownText.text = ("Start in ")+timerDelay.ToString();
            
        }
        if(timerDelay <= 0)
        {
            startTimer = true;
        }
        if (startTimer)
        {
            StartTheTimer();
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
                    SaveAudio();
                }
            }
            if (speechSaved)
            {
                speechSaved = false;
                //Send it to email
                SendAsync();
            }
        }
        

    }
    #endregion

    public void SaveAudio()
    {
        StartCoroutine("SaveTheSpeech");
    }

    IEnumerator SaveTheSpeech()
    {
        isRecording = false;
        
        Microphone.End("");
        Debug.Log("Recording Stopped");
        float filenameRand = UnityEngine.Random.Range(0.0f, 10.0f);
        fileName = "Speech" + filenameRand;
        if (!fileName.ToLower().EndsWith(".wav"))
        {
            fileName += ".wav";
        }

        var filePath = Path.Combine("testing/", fileName);
        filePath = Path.Combine(Application.persistentDataPath, filePath);
        Debug.Log("Created filepath string: " + filePath);
        // Make sure directory exists if user is saving to sub dir.
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        Microphone.End("");

        SavWav.Save(filePath, myAudioClip);

        speechSaved = true;
        Debug.Log(speechSaved.ToString());

        speechPath = filePath;
        Debug.Log(speechPath.ToString());

        yield return null;
    }

    public void SendAsync()
    {
        Debug.Log("in send async");
        StartCoroutine(SendTheSpeech(speechPath));
        StartCoroutine(UploadObjectForBucket(speechPath, _bucketName, fileName));
    }

   
    
    IEnumerator SendTheSpeech(string filePath)
    {
        Debug.Log("in SendTheSpeech");
        try
        {
            MailMessage mail = new MailMessage();
            string name = GlobalVars.smtpName;
            string smtpPass = GlobalVars.smptPass;
            string sendTo = "S00015630@mail.itsligo.ie";
            mail.From = new MailAddress(GlobalVars.smtpName);
            mail.To.Add(sendTo);
            mail.Subject = "Speech Recording";
            mail.Body = "This is a link to a speech sent from the 'Class Talk' application. "+bucketUrl+fileName;
            //Attachment audioMailAttachment;
            //audioMailAttachment = new Attachment(filePath);
            //mail.Attachments.Add(audioMailAttachment);

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(name, smtpPass) as ICredentialsByHost,
                EnableSsl = true
            };
            Debug.Log("exit smtpServer");
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtpServer.SendCompleted += SmtpServer_SendCompleted;
            smtpServer.SendAsync(mail, "test");
            //smtpServer.Send(mail);
            Debug.Log("success");
            emailSent = true;
        }
        catch (SmtpException ex)
        {

            string msg = "Mail cannot be sent:";
            msg += ex.Message;
            Debug.Log("Error: Inside catch block of Mail sending");
            Debug.Log("Error msg:" + ex);
            Debug.Log("Stack trace:" + ex.StackTrace);
            emailSent = false;
            //throw new Exception(msg);
        }
        
         yield return null;
    }
    IEnumerator UploadObjectForBucket(string pathFile, string S3BucketName, string fileNameOnBucket)
    {
        pathFile = speechPath;
        S3BucketName = _bucketName;
        fileNameOnBucket = fileName;
        if (!File.Exists(pathFile))
        {
            Debug.Log("FileNotFoundException: Could not find file " + pathFile);
            
        }

        var stream = new FileStream(pathFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        var request = new PostObjectRequest()
        {
            Bucket = S3BucketName,
            Key = fileNameOnBucket,
            InputStream = stream,
            CannedACL = S3CannedACL.PublicRead,
            Region = S3Region
        };

        Client.PostObjectAsync(request, (responseObj) =>
        {
            if (responseObj.Exception == null)
                Debug.Log("Null responce");
            else
                Debug.Log(responseObj.Exception.ToString());
        });
        yield return null;
    }

    private void DeleteFile()
    {
        
        Debug.Log("Checking if file exists");
        if (File.Exists(speechPath))
        {
            Debug.Log("File found");
            try
            {
                File.Delete(speechPath);
                fileDeleted = true;
            }
            catch(IOException ex)
            {
                Debug.Log("error message " + ex.ToString());
                
            }
            
        }    
    }
    IEnumerator WaitBeforeDeletingFile()
    {
        float time = 5f;
        yield return new WaitForSeconds(time);
        DeleteFile();
    }
    

    private void SmtpServer_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        Debug.Log("Send Mail Complete");
        if (emailSent)
        {
            emailSent = false;
            if (!fileDeleted)
            {
              StartCoroutine(WaitBeforeDeletingFile());

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

    IEnumerator LoseTimeDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timerDelay--;
        }
    }
}