using System;
using System.Web;
using AjaxControlToolkit;
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
            var fileId = (string[])(Session["fileId"]);
            var ct = (string[])(Session["fileContentType"]);
            var content = (byte[][])(Session["fileContents"]);
            var count = (int) (Session["count"]);
            var objI = new ImageBl();
            var id = objEp.AddEmailProblem(heDesc.Text, DateTime.Now, Convert.ToInt32(txtClID.Text), null);
            if (id != 0)
            {
                if (fileId!= null)
                {
                    for (var i = 0; i <= count; i++)
                    {
                        if (ct[i].Contains("jpg") || ct[i].Contains("gif") || ct[i].Contains("png") || ct[i].Contains("jpeg"))
                        {
                            objI.AddImage(content[i], id);
                        }

                    }
                }




                ExtNet.Msg.Notify("Success", "Your message has been sent").Show();
            }

            else
            {
                ExtNet.Msg.Notify("Error", "Unable to send message, please try again").Show();
            }
        }

        protected void btnCancel_OnDirectClick(object sender, DirectEventArgs e)
        {
            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            
        }
    }
}