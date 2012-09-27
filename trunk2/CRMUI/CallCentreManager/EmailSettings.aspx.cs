using System;
using System.Globalization;
using Ext.Net;
using CRMBusiness;


namespace CRMUI.CallCentreManager
{
    public partial class EmailSettings : System.Web.UI.Page
    {
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

                //validate text input controls
                //----------------------------
                if (txtInServer.Text == null)
                {
                    txtInServer.Focus();
                }
                else if (txtUsername.Text == "")
                {
                    txtUsername.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    txtPassword.Focus();
                }
                else if (txtInPort.Text == "")
                {
                    txtInPort.Focus();
                }
                else if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    txtPassword.IndicatorIcon = Icon.Tick;
                    txtConfirmPassword.IndicatorIcon = Icon.Tick;
                }
                else if (txtConfirmPassword != txtPassword)
                {
                    txtPassword.IndicatorIcon = Icon.Exclamation;
                    txtConfirmPassword.IndicatorIcon = Icon.Exclamation;
                    txtConfirmPassword.IndicatorText = "Password do not match";
                    txtPassword.Focus();
                }
                else
                {
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
            return true;
        }
        #endregion
    }
}