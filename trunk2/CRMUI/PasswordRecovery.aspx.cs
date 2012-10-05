using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.Login
{
    public partial class PaswordRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSubmitClick(object sender, DirectEventArgs e)
        {
            try
            {
                var membershipUser = Membership.GetUser(txtUsername.Text);
                if (membershipUser != null)
                {
                    var userPassword = membershipUser.GetPassword();
                    var userEmail = membershipUser.Email;
                    var username = membershipUser.UserName;
                    var body = "Hi " + username + "<br/>You have requested for your password on our system, Please see below<br/>" +
                               "<br/>Username: " + username + "<br/>Password: " + userPassword + "<br/><br/>Regards<br/>LawProperty Customer Support";
                    var email = new EmailBl
                    {
                        To = userEmail,
                        Subject = "Forgot Password",
                        Body = body,
                        IsHtml = true,
                    };
                    if (email.SendEmail())
                    {
                        ExtNet.Mask.Hide();
                        ExtNet.Msg.Notify("Password Sent", "Your password has been sent.<br/>Please check your email.").Show();
                    }
                    else
                    {
                        ExtNet.Mask.Hide();
                        ExtNet.Msg.Alert("Error", email.Error).Show();
                    }
                }
                else
                {
                    ExtNet.Mask.Hide();
                    ExtNet.Msg.Alert("Unknown Username", "The username does not exist.").Show();
                }
            }
            catch (Exception ex)
            {
                ExtNet.Mask.Hide();
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }
    }
}