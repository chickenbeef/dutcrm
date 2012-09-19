using System;
using System.Web;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.Client
{
    public partial class EmailSupport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFrom.Text = (string)(Session["From"]);
            txtClID.Text = (string)(Session["txtClientID.Text"]);
        }

        protected void btnSend_OnDirectClick(object sender, DirectEventArgs e)
        {
            try
            {
                var objEp = new EmailProblemBl();
                var fileId = (string[])(Session["fileId"]);
                var ct = (string[])(Session["fileContentType"]);
                var content = (byte[][])(Session["fileContents"]);
                var count = (int)(Session["count"]);
                var objI = new ImageBl();
                if(txtSubject.Text=="")
                {
                    txtSubject.Text = "No Subject";
                }
                var id = objEp.AddEmailProblem(txtFrom.Text,txtSubject.Text,heDesc.Text, DateTime.Now, Convert.ToInt32(txtClID.Text), null);
                if (id != 0)
                {
                    if (fileId != null)
                    {
                        for (var i = 0; i <= count; i++)
                        {
                            if (ct[i].Contains("jpg") || ct[i].Contains("gif") || ct[i].Contains("png") ||
                                ct[i].Contains("jpeg"))
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
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

       
    }
}