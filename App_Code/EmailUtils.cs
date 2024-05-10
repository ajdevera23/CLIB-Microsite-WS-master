using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for EmailUtils
/// </summary>
public class EmailUtils
{
    public EmailUtils()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void SendEmail(
        string toAddress,
        string toCcAdress,
        string toBccAdress,
        string subject,
        string body,
        bool boolIsBodyHTML)
    {
        using (MailMessage email = new MailMessage())
        {
            //email.From = new MailAddress(ConfigurationManager.AppSettings["mailFrom"]);
            email.To.Add(toAddress);
            if (toCcAdress != "")
            {
                email.CC.Add(toCcAdress);
            }
            if (toBccAdress != "")
            {
                email.Bcc.Add(toBccAdress);
            }
            email.Subject = subject;
            email.Body = body;
            email.IsBodyHtml = boolIsBodyHTML;

            SmtpClient smtp = new SmtpClient();
            smtp.Send(email);
        }
    }

}