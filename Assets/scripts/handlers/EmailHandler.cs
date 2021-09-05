using UnityEngine;
using System.Net;
using System.Net.Mail;
using System;
// using System.DirectoryServices.AccountManagement;


public class EmailHandler : MonoBehaviour
{
    public void Send(string filePath)
    {
        InternetConnection.instance.Check(ErrorManager.TYPE.ERROR, (result) =>
        {
            if (result == true)
            {
                if (Utils.ValidateString(Globals.data.email.da) || Utils.ValidateString(Globals.data.email.soggetto)
                    || Utils.ValidateString(Globals.data.email.da) || Utils.ValidateString(Globals.data.email.SMTP) || Utils.ValidateString(Globals.data.email.password))
                {
                    try
                    {
                        // string from = Globals.data.email.da;
                        string to = "danielesuppo@gmail.com";
                        // string title = Globals.data.email.soggetto;
                        // string body = Globals.data.email.descrizione;
                        // string password = Globals.data.email.password;
                        // string smtpServer = Globals.data.email.SMTP;



                        // /// validate credentials
                        // bool valid = false;
                        // using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                        // {
                        //     valid = context.ValidateCredentials(from, password);
                        // }

                        Debug.Log("Sending email...");

                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient(Globals.data.email.SMTP);
                        mail.From = new MailAddress(Globals.data.email.da);
                        mail.To.Add(to);
                        mail.Subject = Globals.data.email.soggetto;
                        mail.Body = Globals.data.email.descrizione;

                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(filePath);
                        mail.Attachments.Add(attachment);

                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new NetworkCredential(Globals.data.email.da, Globals.data.email.password);
                        SmtpServer.EnableSsl = true;

                        SmtpServer.SendMailAsync(mail);

                        Debug.Log("DONE");
                    }
                    catch (Exception e)
                    {
                        ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, e.ToString());
                    }
                }
                else{
                    ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "Non sono presenti alcuni dati della mail nel file di configurazione\n\n Non è stato possibile inviare la mail");
                }
            }
            else
            {
                ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "Non è stato possibile inviare la mail");
            }
        });
    }
}







