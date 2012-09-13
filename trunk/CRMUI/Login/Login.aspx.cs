using System;
using System.Web.Security;
using Ext.Net;

namespace CRMUI.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void BtnSignInClick(object sender, DirectEventArgs e)
        {
             try
             {
                 if (Membership.ValidateUser(txtUserName.Text, txtPassword.Text))
                 {
                     FormsAuthentication.SetAuthCookie(txtUserName.Text, true);
                     if (Roles.IsUserInRole(txtUserName.Text, "Call Center Manager"))
                     {
                         Response.Redirect("~/CallCentreManager/Home.aspx");

                     }

                     if (Roles.IsUserInRole(txtUserName.Text, "Client"))
                     {
                         Response.Redirect("~/Client/Home.aspx?UserName=" + txtUserName.Text);

                     }

                     if (Roles.IsUserInRole(txtUserName.Text, "Relationship Manager"))
                     {
                         Response.Redirect("~/RelationshipManager/Home.aspx?UserName=" + txtUserName.Text);

                     }

                     if (Roles.IsUserInRole(txtUserName.Text, "Sales Manager"))
                     {
                         Response.Redirect("~/SalesManager/Home.aspx");

                     }
                     if(Roles.IsUserInRole(txtUserName.Text,"Call Support Agent"))
                     {
                         Response.Redirect("~/SupportAgent/Home.aspx");
                     }
                     if(Roles.IsUserInRole(txtUserName.Text,"Email Support Agent"))
                     {
                         Response.Redirect("~/SupportAgent/Home.aspx");
                     }
                 }
                 else
                 {
                     
                     lblError.Text = "Invalid username or password";
                 }
             }
             catch(Exception ex)
             {
                 ExtNet.Msg.Alert("Error",ex.Message).Show();
             }
        }
    }
}