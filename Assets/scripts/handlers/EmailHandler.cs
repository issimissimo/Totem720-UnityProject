using UnityEngine;
using System.Net;
using System.Net.Mail;
using System;


public class EmailHandler : MonoBehaviour
{
    public void Send(string filePath)
    {
        InternetConnection.instance.Check(ErrorManager.TYPE.WARNING, (result) =>
        {
            if (result == true)
            {
                Debug.Log("Sending email...");

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.issimissimo.com");
                mail.From = new MailAddress("d.suppo@issimissimo.com");
                mail.To.Add("danielesuppo@gmail.com");
                mail.Subject = "PROVOLONE";
                mail.Body = "mail with attachment";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(filePath);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("d.suppo@issimissimo.com", "cx$eGM#OQuW0");
                SmtpServer.EnableSsl = true;

                SmtpServer.SendMailAsync(mail);

                Debug.Log("DONE");
            }
            else{
                ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Non è possibile inviare la mail");
            }
        });


        // StartCoroutine(InternetConnection.Check((isConnected) =>
        // {
        //     if (isConnected)
        //     {
        //         Debug.Log("Sending email...");

        //         MailMessage mail = new MailMessage();
        //         SmtpClient SmtpServer = new SmtpClient("mail.issimissimo.com");
        //         mail.From = new MailAddress("d.suppo@issimissimo.com");
        //         mail.To.Add("danielesuppo@gmail.com");
        //         mail.Subject = "PROVOLONE";
        //         mail.Body = "mail with attachment";

        //         System.Net.Mail.Attachment attachment;
        //         attachment = new System.Net.Mail.Attachment("c:/test.jpg");
        //         mail.Attachments.Add(attachment);

        //         SmtpServer.Port = 587;
        //         SmtpServer.Credentials = new System.Net.NetworkCredential("d.suppo@issimissimo.com", "yXQfBD!BCBXv");
        //         SmtpServer.EnableSsl = true;

        //         SmtpServer.SendMailAsync(mail);

        //         Debug.Log("DONE");
        //     }

        //     else
        //     {
        //         Debug.LogError("YOU DON'T HAVE INTERNET CONNECTION");
        //     }
    }



}



// public static IEnumerator checkInternetConnection(Action<bool> action)
// {
//     WWW www = new WWW("http://google.com");
//     yield return www;
//     if (www.error != null)
//     {
//         action(false);
//     }
//     else
//     {
//         action(true);
//     }
// }








