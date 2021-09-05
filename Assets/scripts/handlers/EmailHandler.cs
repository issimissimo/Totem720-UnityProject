using UnityEngine;
using System.Net;
using System.Net.Mail;
using System;
// using System.DirectoryServices.AccountManagement;


public class EmailHandler : MonoBehaviour
{
    public void Send(string filePath)
    {
        InternetConnection.instance.Check(ErrorManager.TYPE.WARNING, (result) =>
        {
            if (result == true)
            {
                try
                {
                    string from = Globals.data.email.da;
                    string to = "danielesuppo@gmail.com";
                    string title = Globals.data.email.soggetto;
                    string body = Globals.data.email.descrizione;
                    string password = Globals.data.email.password;
                    string smtpServer = Globals.data.email.SMTP;
                    

                    // /// validate credentials
                    // bool valid = false;
                    // using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                    // {
                    //     valid = context.ValidateCredentials(from, password);
                    // }

                    Debug.Log("Sending email...");

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(smtpServer);
                    mail.From = new MailAddress(from);
                    mail.To.Add(to);
                    mail.Subject = title;
                    mail.Body = body;

                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(filePath);
                    mail.Attachments.Add(attachment);

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new NetworkCredential(from, password);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.SendMailAsync(mail);

                    Debug.Log("DONE");
                }
                catch (Exception e)
                {
                    Debug.Log("Error!!!: " + e.ToString());
                    ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, e.ToString());
                }
            }
            else
            {
                ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Non è possibile inviare la mail");
            }
        });
    }
}







