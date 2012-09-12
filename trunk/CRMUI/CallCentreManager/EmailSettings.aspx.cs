using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Web.Configuration;
using System.Configuration;


namespace CRMUI.CallCentreManager
{
    public partial class EmailSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SaveSettings(object sender, DirectEventArgs e)
        {
            try
            {
                ValidateInputs();
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
               
               Configuration config = WebConfigurationManager.OpenWebConfiguration("~/");
                var mailsection = config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
                if (mailsection != null)
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
                ;
            }
        }

        protected void ValidateInputs()
        {
            if(txtServer.Text == null)
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
            
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                txtPassword.IndicatorIcon = Icon.Tick;
                txtConfirmPassword.IndicatorIcon = Icon.Tick;
            }
            else
            {
                txtPassword.IndicatorIcon = Icon.Exclamation;
                txtConfirmPassword.IndicatorIcon = Icon.Exclamation;
                txtConfirmPassword.IndicatorText = "Password do not match";
                txtPassword.Focus();
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