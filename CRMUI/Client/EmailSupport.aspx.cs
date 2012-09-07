using System;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.Client
{
    public partial class EmailSupport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtName.Text = (string)(Session["txtName.Text"]);
            txtClID.Text = (string)(Session["txtClientID.Text"]);
        }

        protected void btnSend_OnDirectClick(object sender, DirectEventArgs e)
        {
            var objEp = new EmailProblemBl();
            if(objEp.AddEmailProblem(heDesc.Text,DateTime.Now,Convert.ToInt32(txtClID.Text),Convert.ToInt32(null),null))
            {
                ExtNet.Msg.Notify("Success","Your message has been sent");
            }
            else
            {
                ExtNet.Msg.Notify("Error", "Unable to send message, please try again");
            }
        }


    }
}