using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Ext.Net;
using CRMBusiness;


namespace CRMUI.CallCentreManager
{
    public partial class EmailSettings : System.Web.UI.Page
    {
        #region PUBLIC VARIABLES FOR VALIDATION
        public static bool Validun = true;
        public static bool Validconfmpw = true;
        public static bool Validincserver = true;
        public static bool Validincport = true;
        public static bool Validoutserver = true;
        public static bool Validoutport = true;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Loadcurrentconfig();
                }
            }
            catch (Exception loadex)
            {
                ExtNet.MessageBox.Alert("Error", loadex.Message).Show();
            }

        }

        #region DIRECT EVENTS
        protected void SaveSettings(object sender, DirectEventArgs e)
        {
            try
            {

                //check if validation is met
                if (Validun == false)
                {
                    txtUsername.Focus();
                    InvalidMessage();
                }
                else if (Validconfmpw == false)
                {
                    txtConfirmPassword.Focus();
                    InvalidMessage();
                }
                else if (Validincserver == false)
                {
                    txtInServer.Focus();
                    InvalidMessage();
                }
                else if (Validincport == false)
                {
                    txtInPort.Focus();
                    InvalidMessage();
                }
                else if (Validoutserver == false)
                {
                    txtOtServer.Focus();
                    InvalidMessage();
                }
                else if (Validoutport == false)
                {
                    txtOtPort.Focus();
                    InvalidMessage();
                }
                else
                {
                    //if all validation is met then save
                    if (Savealldata())
                    {
                        ExtNet.MessageBox.Notify("Save Status", "Email Settings updated successfuly!").Show();
                        Disablecontrols();
                    }
                    else
                    {
                        ExtNet.MessageBox.Notify("Save Status", "Update incomplete!").Show();
                    }
                }
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Error", ex.Message).Show();
            }
        }

        #endregion

        #region HELPER METHODS
        protected void Disablecontrols()
        {
            txtInServer.ReadOnly = true;
            txtUsername.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtConfirmPassword.ReadOnly = true;
            txtInPort.ReadOnly = true;
            chkEnableInSSL.ReadOnly = true;
            txtOtServer.ReadOnly = true;
            txtOtPort.ReadOnly = true;
            chkEnableOtSSL.ReadOnly = true;
            BtnSave.Disabled = true;
        }

        protected void Loadcurrentconfig()
        {
            var allsets = new ConfigurationSettingsBl().GetAllSettings();

            foreach (var t in allsets)
            {
                switch (t.Setting)
                {
                    case "SMTPUser":
                        txtUsername.Text = t.Value;
                        break;

                    case "SMTPPassword":
                        txtPassword.Text = t.Value;
                        txtConfirmPassword.Text = t.Value;
                        break;

                    case "SMTPClient":
                        txtOtServer.Text = t.Value;
                        break;

                    case "SMTPPort":
                        txtOtPort.Text = t.Value;
                        break;

                    case "SMTPSSL":
                        chkEnableOtSSL.Checked = Convert.ToBoolean(t.Value);
                        break;

                    case "MailServer":
                        txtInServer.Text = t.Value;
                        break;

                    case "Port":
                        txtInPort.Text = t.Value;
                        break;

                    case "Encryption":
                        chkEnableInSSL.Checked = Convert.ToBoolean(t.Value);
                        break;
                }
            }
        }

        protected bool Savealldata()
        {
            var sslin = chkEnableInSSL.Checked;
            var sslout = chkEnableOtSSL.Checked;
            var saveconfigs = new ConfigurationSettingsBl();

            //Update outgoing SMTP settings
            saveconfigs.UpdateSetting("SMTPUser", txtUsername.Text);
            saveconfigs.UpdateSetting("SMTPPassword", txtConfirmPassword.Text);
            saveconfigs.UpdateSetting("SMTPClient", txtOtServer.Text);
            saveconfigs.UpdateSetting("SMTPPort", txtOtPort.Text);
            saveconfigs.UpdateSetting("SMTPSSL", sslout.ToString(CultureInfo.InvariantCulture));

            //Update incoming POP settings
            saveconfigs.UpdateSetting("UserName", txtUsername.Text);
            saveconfigs.UpdateSetting("MailServer", txtInServer.Text);
            saveconfigs.UpdateSetting("Port", txtInPort.Text);
            saveconfigs.UpdateSetting("Encryption", sslin.ToString(CultureInfo.InvariantCulture));

            saveconfigs.UpdateSetting("EmailAddr", txtUsername.Text);

            //save email settings for sending password recovery
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            var mailSettings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            if (mailSettings != null)
            {
                mailSettings.Smtp.From = txtUsername.Text;
                mailSettings.Smtp.Network.Host = txtOtServer.Text;
                mailSettings.Smtp.Network.UserName = txtUsername.Text;
                mailSettings.Smtp.Network.Password = txtPassword.Text;
                mailSettings.Smtp.Network.EnableSsl = chkEnableOtSSL.Checked;
                mailSettings.Smtp.Network.Port = Convert.ToInt32(txtOtPort.Text);
            }
            config.Save();
            return true;
        }
        #endregion

        #region VALIDATION
        //validation for all fields
        protected void InvalidMessage()
        {
            ExtNet.Msg.Alert("Error", "See fields marked with exclamations! /n Or reconfigure values before attempting to save!").Show();
        }

        protected void ValidateUsername(object sender, RemoteValidationEventArgs e)
        {
            Validun = false;
            var un = (TextField)sender;

            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);

            if (un.Text == "")
            {
                e.Success = false;
                e.ErrorMessage = "Field is required!";
            }
            else
            {
                if (re.IsMatch(un.Text))
                {
                    e.Success = true;
                    Validun = true;
                }
                else
                {
                    e.Success = false;
                    e.ErrorMessage = "Not a valid mail address";
                }
            }
            System.Threading.Thread.Sleep(1000);
        }

        protected void ValidateConfirmPassord(object sender, RemoteValidationEventArgs e)
        {
            Validconfmpw = false;
            var confirmpw = (TextField)sender;

            if (confirmpw.Text == "")
            {
                e.Success = false;
                e.ErrorMessage = "Field is required!";
            }
            else if (confirmpw.Text.Length < 6)
            {
                e.Success = false;
                e.ErrorMessage = "Minimum characters is 6!";
            }
            else if (confirmpw.Text.Length > 12)
            {
                e.Success = false;
                e.ErrorMessage = "Maximum of 12 characters allowed!";
            }
            else
            {
                if (confirmpw.Text == txtPassword.Text)
                {
                    e.Success = true;
                    Validconfmpw = true;
                }
                else
                {
                    e.Success = false;
                    e.ErrorMessage = "Passwords do not match!";
                }
            }
            System.Threading.Thread.Sleep(1000);
        }

        protected void ValidateIncomingServer(object sender, RemoteValidationEventArgs e)
        {
            Validincserver = false;
            var incserver = (TextField)sender;

            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);

            if (incserver.Text == "")
            {
                e.Success = false;
                e.ErrorMessage = "Field is required!";
            }
            else
            {
                if (re.IsMatch(incserver.Text))
                {
                    e.Success = true;
                    Validincserver = true;
                }
                else
                {
                    e.Success = false;
                    e.ErrorMessage = "Not a valid server address";
                }
            }
            System.Threading.Thread.Sleep(1000);
        }

        protected void ValidateIncomingPort(object sender, RemoteValidationEventArgs e)
        {
            Validincport = false;
            var incport = (TextField)sender;

            if (incport.Text == "")
            {
                e.Success = false;
                e.ErrorMessage = "Field is required!";
            }
            else if (Checknum(incport.Text) == false)
            {
                e.Success = false;
                e.ErrorMessage = "Port must be a number and between 1 and 999!";
            }
            else
            {
                e.Success = true;
                Validincport = true;
            }


        }

        protected void ValidateOutgoingServer(object sender, RemoteValidationEventArgs e)
        {
            Validoutserver = false;
            var outserver = (TextField)sender;

            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);

            if (outserver.Text == "")
            {
                e.Success = false;
                e.ErrorMessage = "Field is required!";
            }
            else
            {
                if (re.IsMatch(outserver.Text))
                {
                    e.Success = true;
                    Validoutserver = true;
                }
                else
                {
                    e.Success = false;
                    e.ErrorMessage = "Not a valid server address";
                }
            }
            System.Threading.Thread.Sleep(1000);
        }

        protected void ValidateOutgoingPort(object sender, RemoteValidationEventArgs e)
        {
            Validoutport = false;
            var outport = (TextField)sender;

            if (outport.Text == "")
            {
                e.Success = false;
                e.ErrorMessage = "Field is required!";
            }
            else if (Checknum(outport.Text) == false)
            {
                e.Success = false;
                e.ErrorMessage = "Port must be a number and between 1 and 999!";
            }
            else
            {
                e.Success = true;
                Validoutport = true;
            }
        }

        protected bool Checknum(string port)
        {
            double retNum;
            var isNum = Double.TryParse(Convert.ToString(port), System.Globalization.NumberStyles.Any,
                                         System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            if (!isNum)
            {
                return false;
            }
            else
            {
                if (Convert.ToInt32(port) < 0)
                {
                    return false;
                }
                else if (Convert.ToInt32(port) > 999)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
    }
}