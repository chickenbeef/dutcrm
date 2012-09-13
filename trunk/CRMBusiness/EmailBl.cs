using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenPop.Mime;
using OpenPop.Pop3;
using System.Net.Mail;
using System.Configuration;

namespace CRMBusiness
{
    #region Email Class
    public class EmailBl
    {
        #region Email Properties

        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Error { get; set; }
        public List<string> Bcc { get; set; }
        public string To { get; set; }
        public bool IsHtml { get; set; }

        #endregion

        //

        #region Send Emails

        public bool SendEmail()
        {
            try
            {
                var c = new SmtpClient(ConfigurationManager.AppSettings["SMTPClient"])
                             {
                                 Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]),
                                 Credentials =
                                     new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUser"],
                                                                      ConfigurationManager.AppSettings["SMTPPassword"]),
                                 EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPSSL"])
                             };

                var m = new MailMessage();

                if (To != string.Empty)
                {
                    m.To.Add(new MailAddress(To));
                }

                if (Bcc != null)
                {
                    foreach (var addrbcc in Bcc)
                    {
                        m.Bcc.Add(new MailAddress(addrbcc));
                    }
                }

                m.Subject = Subject;
                m.Body = Body;
                m.From = new MailAddress(ConfigurationManager.AppSettings["EmailAddr"]);
                m.IsBodyHtml = IsHtml;
                c.Send(m);
                return true;
            }
            catch (SmtpException m)
            {
                Error = m.Message;
                return false;
            }
        }
        #endregion

    }
    #endregion
}
