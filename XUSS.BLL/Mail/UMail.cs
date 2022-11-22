using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using System.Net;

namespace XUSS.BLL.Mail
{
    public class UMail
    {
        public string ErrorMessage = String.Empty;

        public static Boolean IsMail(string p_email)
        {
            System.Text.RegularExpressions.Regex l_reg = new System.Text.RegularExpressions.Regex(("^(([^<;>;()[\\]\\\\.,;:\\s@\\\"]+" + ("(\\.[^<;>;()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@" + ("((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}" + ("\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+" + "[a-zA-Z]{2,}))$")))));
            return l_reg.IsMatch(p_email);
        }

        //  New To DataTable
        public static DataTable NewToDataTable()
        {
            DataTable lDTTo = new DataTable("MailTo");
            lDTTo.Columns.Add("email", Type.GetType("System.String"));
            lDTTo.Columns.Add("FirstName", Type.GetType("System.String"));
            lDTTo.Columns.Add("LastName", Type.GetType("System.String"));
            return lDTTo;
        }

        //  Using configuration on webconfig
        public Boolean SendMail(NameValueCollection pConfiguration, DataTable pTo, string pSubject, string pBody, string lDirectory, string[] pFiles)
        {
            try
            {
                //  Classes declaration
                MailMessage lMail = new MailMessage();
                SmtpClient lSmtp = new SmtpClient();
                //  Server Setup
                lSmtp.Host = pConfiguration["MAIL_SMTPServer"];
                lSmtp.UseDefaultCredentials = true;
                lMail.From = new MailAddress(pConfiguration["MAIL_From"]);
                lMail.ReplyTo = new MailAddress(pConfiguration["MAIL_ReplayTo"]);
                lMail.Subject = pSubject;
                lMail.Body = pBody;
                lMail.IsBodyHtml = false;
                lMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //  To
                MailAddress lMailAddress;
                for (int i = 0; (i
                            <= (pTo.Rows.Count - 1)); i++)
                {
                    lMailAddress = new MailAddress((string)pTo.Rows[i]["email"], (pTo.Rows[i]["FirstName"] + (" " + pTo.Rows[i]["LastName"].ToString())));
                    lMail.To.Add(lMailAddress);
                }
                //  Attachments
                Attachment lAttachment;
                for (int i = 0; (i
                            <= (pFiles.Length - 1)); i++)
                {
                    lAttachment = new Attachment((lDirectory + ("\\" + pFiles[i])));
                    lMail.Attachments.Add(lAttachment);
                }
                // Send Mail
                lSmtp.Send(lMail);
            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message;
                return false;
            }
            ErrorMessage = String.Empty;
            return true;
        }

        //  Given SMTP configuration and attached file
        public Boolean SendMail(string pSMTPServer, string pUser, string pPassword, string pMailFrom, string pMailReplayTo, DataTable pTo, string pSubject, string pBody, string lDirectory, string[] pFiles)
        {
            try
            {
                //  Classes declaration
                MailMessage lMail = new MailMessage();
                SmtpClient lSmtp = new SmtpClient();

                //  Server Setup
                lSmtp.Host = pSMTPServer;                
                //  Credentials
                if (string.IsNullOrEmpty(pUser))
                {
                    lSmtp.UseDefaultCredentials = true;
                }
                else
                {
                    lSmtp.UseDefaultCredentials = true;
                    lSmtp.Credentials = new NetworkCredential(pUser, pPassword);
                }

                //  Mail Setup
                lMail.From = new MailAddress(pMailFrom);
                lMail.ReplyTo = new MailAddress(pMailReplayTo);                
                lMail.Subject = pSubject;
                lMail.Body = pBody;
                lMail.IsBodyHtml = true;
                lMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                //  To
                MailAddress lMailAddress;
                for (int i = 0; i <= (pTo.Rows.Count - 1); i++)
                {
                    if (!string.IsNullOrEmpty((string)pTo.Rows[i]["email"]))
                    {
                        if (pTo.Rows[i]["email"].ToString().IndexOf("@") > 2)
                        {
                            try
                            {
                                lMailAddress = new MailAddress((string)pTo.Rows[i]["email"], (pTo.Rows[i]["FirstName"] + (" " + pTo.Rows[i]["LastName"].ToString())));
                                lMail.To.Add(lMailAddress);
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                //  Attachments
                Attachment lAttachment = null;
                if (pFiles != null)
                {
                    for (int i = 0; i <= (pFiles.Length - 1); i++)
                    {
                        lAttachment = new Attachment((lDirectory + pFiles[i]));
                        lMail.Attachments.Add(lAttachment);
                    }
                }

                // Send Mail
                lSmtp.Send(lMail);

                //  Dispose objects
                if (lAttachment != null)
                {
                    lAttachment.Dispose();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
                return false;
            }

            ErrorMessage = String.Empty;
            return true;
        }

        //  Given SMTP configuration and attached file and port
        public string SendMail(string pSMTPServer, string pUser, string pPassword, string pMailFrom, string pMailReplayTo, DataTable pTo, string pSubject, string pBody, string lDirectory, string[] pFiles,int pPort)
        {
            try
            {
                //  Classes declaration
                MailMessage lMail = new MailMessage();
                SmtpClient lSmtp = new SmtpClient();

                //  Server Setup
                lSmtp.Host = pSMTPServer;
                //Port
                lSmtp.Port = pPort;
                lSmtp.EnableSsl = true;
                //  Credentials
                if (string.IsNullOrEmpty(pUser))
                {
                    lSmtp.UseDefaultCredentials = true;
                }
                else
                {
                    lSmtp.UseDefaultCredentials = false;
                    lSmtp.Credentials = new NetworkCredential(pUser, pPassword);
                    lSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                }

                //  Mail Setup
                lMail.Headers.Add("Disposition-Notification-To", pMailFrom);
                lMail.From = new MailAddress(pMailFrom);
                lMail.ReplyTo = new MailAddress(pMailReplayTo);
                lMail.Subject = pSubject;                
                lMail.Body = pBody;
                lMail.IsBodyHtml = true;
                lMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                //  To
                MailAddress lMailAddress;
                for (int i = 0; i <= (pTo.Rows.Count - 1); i++)
                {
                    if (!string.IsNullOrEmpty((string)pTo.Rows[i]["email"]))
                    {
                        if (pTo.Rows[i]["email"].ToString().IndexOf("@") > 2)
                        {
                            try
                            {
                                lMailAddress = new MailAddress((string)pTo.Rows[i]["email"], (pTo.Rows[i]["FirstName"] + (" " + pTo.Rows[i]["LastName"].ToString())));
                                lMail.To.Add(lMailAddress);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                //  Attachments
                Attachment lAttachment = null;
                if (pFiles != null)
                {
                    for (int i = 0; i <= (pFiles.Length - 1); i++)
                    {
                        lAttachment = new Attachment((lDirectory + pFiles[i]));
                        lMail.Attachments.Add(lAttachment);
                    }
                }
                // Send Mail
                lSmtp.Send(lMail);

                //  Dispose objects
                if (lAttachment != null)
                {
                    lAttachment.Dispose();
                }
                return "Send OK!";
            }
            catch (Exception ex)
            {
                //return ErrorMessage = ex.ToString();
                //return false;
                ErrorMessage = ex.ToString();
                throw ex;
            }
            finally
            {

            }
            //ErrorMessage = String.Empty;
            //return true;
        }
    }
}
