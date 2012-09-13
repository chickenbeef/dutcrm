using System;
using System.Globalization;
using System.Net.Configuration;
using Ext.Net;
using System.Web.Configuration;
using System.Configuration;


namespace CRMUI.CallCentreManager
{
    public partial class EmailSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/");
                var mailsection = config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
                


                if(!IsPostBack)
                {
                    if (mailsection != null)
                    {
                        var loadsmtp = mailsection.Smtp;
                        txtServer.Text = loadsmtp.Network.Host.ToString(CultureInfo.InvariantCulture);
                        txtUsername.Text = loadsmtp.Network.UserName;
                        txtPort.Text = loadsmtp.Network.Port.ToString(CultureInfo.InvariantCulture);
                        chkEnableSSL.Checked = loadsmtp.Network.EnableSsl;

                    }
                }
            }
            catch (Exception loadex)
            {

                ExtNet.MessageBox.Alert("Error", loadex.Message).Show();
            }
            
        }

        protected void SaveSettings(object sender, DirectEventArgs e)
        {
            try
            {
                //declarations
                            //mailsettings
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/");
                var mailsection = config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
                            //check ssl
                bool ssltype = false;

                switch (chkEnableSSL.Checked)
                {
                    case true:
                        ssltype = true;
                        break;
                    case false:
                        ssltype = false;
                        break;
                }
                //validate text input controls
                //-----------------------
                if (txtServer.Text == null)
                {
                    txtServer.Focus();
                }
                else if (txtUsername.Text == "")
                {
                    txtUsername.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    txtPassword.Focus();
                }
                else if (txtPort.Text == "")
                {
                    txtPort.Focus();
                }

                else if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    txtPassword.IndicatorIcon = Icon.Tick;
                    txtConfirmPassword.IndicatorIcon = Icon.Tick;
                }
                else if (txtConfirmPassword!=txtPassword)
                {
                    txtPassword.IndicatorIcon = Icon.Exclamation;
                    txtConfirmPassword.IndicatorIcon = Icon.Exclamation;
                    txtConfirmPassword.IndicatorText = "Password do not match";
                    txtPassword.Focus();
                }
                else if(mailsection != null)
                {
                    var smtpSection = mailsection.Smtp;

                    smtpSection.Network.Host = txtServer.Text;
                    smtpSection.Network.UserName = txtUsername.Text;
                    smtpSection.Network.Password = txtConfirmPassword.Text;
                    smtpSection.Network.Port = Convert.ToInt32(txtPort.Text);
                    smtpSection.Network.EnableSsl = ssltype;
                    config.Save(ConfigurationSaveMode.Modified);

                    ExtNet.MessageBox.Notify("Save Status", "Email Settings updated successfuly!").Show();
                    Disablecontrols();
                }
                else
                {
                    ExtNet.MessageBox.Notify("Save Status", "Update incomplete!").Show();
                }
            }
            catch (Exception ex)
            {

                ExtNet.MessageBox.Alert("Error", ex.Message).Show();
                
            }
        }

        protected void Disablecontrols()
        {
            txtServer.ReadOnly = true;
            txtUsername.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtConfirmPassword.ReadOnly = true;
            txtPort.ReadOnly = true;
            chkEnableSSL.ReadOnly = true;
        }
    }
}