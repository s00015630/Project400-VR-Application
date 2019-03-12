using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;


public class SendAudioToEmail : MonoBehaviour
{
    
    public static string SendEmail(string filename)
    {
        try
        {

            MailMessage mail = new MailMessage();
            string name = GlobalVars.smtpName;
            string smtpPass = GlobalVars.smtpPass;
            string sendTo = "";
            mail.From = new MailAddress(GlobalVars.smtpName);
            mail.To.Add(sendTo);
            mail.Subject = "Speech Recording";
            mail.Body = "This is a recording of a speech sent from the 'Class Talk' application. See attachment.";
            Attachment audioMailAttachment;
            audioMailAttachment = new Attachment(filename);
            mail.Attachments.Add(audioMailAttachment);

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(name, smtpPass) as ICredentialsByHost,
                EnableSsl = true
            };
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtpServer.Send(mail);
            Debug.Log("success");
        }
        catch (SmtpException ex)
        {

            string msg = "Mail cannot be sent:";
            msg += ex.Message;
            Debug.Log("Error: Inside catch block of Mail sending");
            Debug.Log("Error msg:" + ex);
            Debug.Log("Stack trace:" + ex.StackTrace);
            //throw new Exception(msg);
        }
        return filename;
    }
}
