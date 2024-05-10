using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for RefCodeIsUsed
/// </summary>
public class RefCodeIsUsed
{
    GetList getList = new GetList();
    public RefCodeIsUsed()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public BaseResult TagRefCodeIsUsed(TokenRequest token)
    {
        BaseResult result = new BaseResult();
        try
        {
            if (!string.IsNullOrEmpty(token.ReferenceCode))
            {
                var connString = ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringWriter"].ConnectionString;
                var selectSQL = "Updater.usp_TagRefCodeIsUsed";
                if (token.ReferenceCode != ConfigurationManager.AppSettings["CLIBrefCode"])
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open(); 
                        using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@refCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            result = SendCOCToClient(token.Email, token.ContactNumber, token.FirstName, token.MiddleName, token.LastName, token.ProductName, token.COCNumber, token.EffectiveDateTime,token.TerminationDate);
            
        }
        catch (Exception ex)
        {

            result.ResultStatus = ResultType.Error;
            result.Message = ex.ToString();
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
        
        return result;
    }

    public BaseResult SendCOCToClient(string email,string contactNumber, string firstName,string middleName, string lastName, string productCode, string cocNumber, string effectiveDate, string terminationDate)
    {
        BaseResult result = new BaseResult();
        try
        {
            SendEmail(email,firstName,middleName,lastName,contactNumber, productCode, cocNumber, effectiveDate, terminationDate);
            result.ResultStatus = ResultType.Success;
        }
        catch (Exception ex)
        {

            result.ResultStatus=ResultType.Error;
            result.Message = ex.ToString();
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
        return result;
    }

    public void SendEmail(string email, string firstName,string middleName,string lastName, string contactNumber, string productCode, string cocNumber, string effectiveDate, string terminationDate)
    {
        string[] getProductDescription = getList.GetProductDescription(productCode);
        string productDescription = getProductDescription[0];
        string productName = getProductDescription[1];

        string message = File.ReadAllText(ConfigurationManager.AppSettings["messagePath"]);
        string messageFinal = message.Replace("(client name)", firstName).Replace("(FirstName)",firstName).Replace("(MiddleName)",middleName).Replace("(LastName)",lastName).Replace("(ContactNumber)",contactNumber).Replace("(EmailAddress)",email).Replace("(ProductName)",productName).Replace("(COCReferenceNumber)", cocNumber).Replace("(Benefits)",productDescription).Replace("(EffectiveDate)", effectiveDate).Replace("(TerminationDate)", terminationDate);
        EmailUtils.SendEmail(email, ConfigurationManager.AppSettings["mailCC"], ConfigurationManager.AppSettings["mailBcc"], ConfigurationManager.AppSettings["mailSubject"], messageFinal, true);
    }

    public BaseResult TestMBPMail(TokenRequest token)
    {
        BaseResult result = new BaseResult();
        try
        {            
            result = SendMBPToClient(token.Email, token.FirstName, token.ReferenceCode, token.GroupMail);
        }
        catch (Exception ex)
        {

            result.ResultStatus = ResultType.Error;
            result.Message = ex.ToString();
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }


    public BaseResult SendMBPToClient(string email, string firstName, string referenceCode, string groupMail)
    {
        BaseResult result = new BaseResult();
        try
        {
            SendMBPEmail(email, firstName, referenceCode, groupMail);
            result.ResultStatus = ResultType.Success;
        }
        catch (Exception ex)
        {

            result.ResultStatus = ResultType.Error;
            result.Message = ex.ToString();
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
        return result;
    }

    public void SendMBPEmail(string email, string firstName, string referenceCode, string groupMail)
    {

        string strBody = "Dear Ka-Cebuana <b>" + firstName + ",</b> <br /><br />";
        strBody += "Thank you for choosing Cebuana Lhuillier Microbiz Protek Jr. insurance product! <br /><br />";
        strBody += "We have received your Business Insurance Application Form.  <br /><br />";

        strBody += "Your Application Number is <b>"+ referenceCode + "</b>, please keep this number to keep track your insurance application form.    <br /><br />";
        strBody += "To keep your information confidential, our Cebuana Insurance Specialist will get in touch with you via your email account or mobile number. <br />";
        strBody += "May we request to keep your communication lines open. <br /><br />";

        strBody += "For any inquiries or follow-up, you may contact our Cebuana Insurance Specialist with email address <b>" + groupMail + "</b> or call mobile number (0968)856.54.59. <br /><br />";
        strBody += "Thank you! <br /><br /><br />";
        strBody += "[This email is system generated and no signature is required, please do not reply to this email use the above stated email address.]  \r\n";
        

       

        MailMessage msg = new MailMessage("noreply-insurance@pjlhuillier.com", email, "Microbiz Protek Jr. Confirmation", strBody);
        msg.CC.Add(groupMail);
        msg.Bcc.Add("jynovelo@pjlhuillier.com");
        msg.IsBodyHtml = true;

        SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["Smtp.Client"]);
        
        client.Send(msg);

        //string message = File.ReadAllText(ConfigurationManager.AppSettings["MBPmessagePath"]);
        //string messageFinal = message.Replace("(client name)", firstName).Replace("(EmailAddress)", email).Replace("(Reference Code)", referenceCode);

        //EmailUtils.SendEmail(email, ConfigurationManager.AppSettings["mailCC"], ConfigurationManager.AppSettings["mailBcc"], ConfigurationManager.AppSettings["mailSubject"], messageFinal, true);
    }

    public BaseResult TestIQRMail(TokenRequest token)
    {
        BaseResult result = new BaseResult();
        try
        {
            result = SendIQRToClient(token.Email, token.FirstName, token.ReferenceCode, token.GroupMail, token.COCNumber, token.MiddleName, token.LastName, token.Suffix, token.DOB, token.ContactNumber, token.IssueDateTime, token.EffectiveDateTime, token.TerminationDate, token.ProductName, token.ProductDescription, token.Premium, token.DeadlineOfPayment);
        }
        catch (Exception ex)
        {

            result.ResultStatus = ResultType.Error;
            result.Message = ex.ToString();
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }
    public BaseResult SendIQRToClient(string email, string firstName, string referenceCode, string groupMail, string cocNumber, string middleName, string lastName, string suffix, string dob, string contactNumber, string issueDateTime, string effectiveDateTime, string terminationDate, string productName, string productDescription, string premium, string deadlineofPayment)
    {
        BaseResult result = new BaseResult();
        try
        {
            SendIQREmail(email, firstName, referenceCode, groupMail, cocNumber, middleName, lastName, suffix, dob, contactNumber, issueDateTime, effectiveDateTime, terminationDate, productName, productDescription, premium, deadlineofPayment);
            result.ResultStatus = ResultType.Success;
        }
        catch (Exception ex)
        {

            result.ResultStatus = ResultType.Error;
            result.Message = ex.ToString();
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
        return result;
    }

    public void SendIQREmail(string email, string firstName, string referenceCode, string groupMail, string cocNumber, string middleName, string lastName, string suffix, string dob, string contactNumber, string issueDateTime, string effectiveDateTime, string terminationDate, string productName, string productDescription, string premium, string deadlineofPayment)
    {
        string strBody = "<b>Hi Ka-Cebuana " + firstName + " " + lastName + "! <br><br></b>";
        strBody += "Thank you for choosing <b>Cebuana Lhuillier Insurance Brokers</b> to serve and protect you. <br><br> Below are the details of your insurance application. To activate your insurance policy, you may <br> pay at any Cebuana Lhuillier Branch using the Application Reference Number below.<br><br><br>";
        strBody += "<table style='border: 1px solid black; width: 50%; border-collapse: collapse'>";
        strBody += "<tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Application Reference Number: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left;  padding-left: 10px; padding-right: 10px'><b>" + referenceCode + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> COC Number: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b>" + cocNumber + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> First Name: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b>" + firstName + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Middle Name: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b>" + middleName + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Last Name: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b>" + lastName + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Suffix: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b>" + suffix + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Birthday: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b>" +  dob + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Contact Number: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b>" + contactNumber + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Email Address: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b>" + email + "</b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Issue Date: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b><i>" + issueDateTime  + "</i></b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Effective Date: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b><i>" + effectiveDateTime + "</i></b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Termination Date: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b><i>" + terminationDate + "</i></b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Product:</td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b><i>" + productName + "</i></b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Product Benefits:</td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b><i>" + productDescription + "</i></b></td>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Premium:</td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b><i>" + premium + "</i></b></td>";
        strBody += "</tr>";
        strBody += "</tr><tr>";
        strBody += "<td style='border: 1px solid black; text-align: right; padding-left: 10px; padding-right: 10px'> Deadline of Payment: </td>";
        strBody += "<td style='border: 1px solid black; text-align: left; padding-left: 10px; padding-right: 10px'><b><i>" + deadlineofPayment + "</i></b></td>";
        strBody += "</tr></table>";
        strBody += "<p><b>Note: The above application is valid up to 5 days from the Issue Date.</b></p>";
        strBody += "For any inquiries or follow-up, you may contact our Cebuana Insurance Specialist with email address <b>" + groupMail + "</b> or call mobile number 0968-856-5459. <br /><br />";
        strBody += "Thank you! <br /><br />";
        strBody += "<b>Cebuana Lhuillier Insurance Brokers, Inc. <br />Makati City</b><br /><br /><br />";
        strBody += "[This email is system generated and no signature is required, please do not reply to this email use the above stated email address.]  \r\n";

        MailMessage msg = new MailMessage("noreply-insurance@pjlhuillier.com", email, "Cebuana Lhuillier " + referenceCode.Substring(0, 3) + " " + productName + " " + firstName + " " + lastName, strBody);
        msg.CC.Add(groupMail);
        //msg.Bcc.Add("jynovelo@pjlhuillier.com");
        msg.IsBodyHtml = true;

        SmtpClient client = new SmtpClient();

        client.Send(msg);

    }
}