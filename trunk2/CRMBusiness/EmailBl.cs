using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;

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

       

        public bool SendEmail()
        {
            try
            {
                var cs = new ConfigurationSettingsBl().GetAllSettings();
                var c = new SmtpClient();
                var ucredentials = new System.Net.NetworkCredential();

                //var configs = Smtpconfigs();

                #region SMTP SETTINGS

                //get smtp settings
                foreach (var t in cs)
                {
                    switch (t.Setting)
                    {
                        case "SMTPClient":
                            c.Host = t.Value;
                            break;
                        case "SMTPPort":
                            c.Port = Convert.ToInt32(t.Value);
                            break;
                        case "SMTPUser":
                            ucredentials.UserName = t.Value;
                            break;
                        case "SMTPPassword":
                            ucredentials.Password = t.Value;
                            break;
                        case "SMTPSSL":
                            c.EnableSsl = Convert.ToBoolean(t.Value);
                            break;
                    }
                }
                c.Credentials = ucredentials;

                #endregion

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
                m.From = new MailAddress(ucredentials.UserName);
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
    }
    #endregion
}
