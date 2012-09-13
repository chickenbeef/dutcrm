using System;
using System.Collections.Generic;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.CallCentreManager
{
    public partial class MessagingServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var comms = new ComTemplateBl().GetAllTemplates();
            var cl = new ClientBl().GetAllClients();
            
                    if (!IsPostBack)
                    {
                            streComTemplate.DataSource = comms;
                            streComTemplate.DataBind();
            

                            streClient.DataSource = cl;
                            streClient.DataBind();
                    }
        }


        protected void SendMessage(object sender, DirectEventArgs e)
        {
            try
            {
                
                string val = e.ExtraParams["Values"];

                var clients = JSON.Deserialize<Dictionary<string, string>[]>(val);

                List<string> emails = new List<string>();

                foreach (Dictionary<string, string> row in clients)
                {
                    //for each fow in clients grid
                    foreach (KeyValuePair<string, string> keyValuePair in row)
                    {
                        if (keyValuePair.Key == "Email")
                        {
                                emails.Add(keyValuePair.Value);
                        }
                    }
                 }

                //List<string> custommail = new List<string>();
                //custommail.Add("nnn.williamson@gmail.com");
                //custommail.Add("ruvz21@gmail.com");

               Composemailmessage(emails);
                
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Error Occured", ex.Message).Show();
            }
        }

        protected void Composemailmessage(List<string> mailadresses )
        {
            try
            {
                //CODE TO ADD EMAILS AND GENERATE EMAIL
                var objmailmessage = new EmailBl
                                             {
                                                Bcc = mailadresses, Subject = cmbComTemplate.SelectedItem.Text, Body= editrPara.Text
                                             };

                if (objmailmessage.SendEmail())
                {
                    ExtNet.MessageBox.Notify(" Mail Message", "Message successfully sent to recipients").Show();
                }
                else
                {
                    ExtNet.MessageBox.Notify(" Mail Message Error", objmailmessage.Error).Show();
                }
            }
            catch(Exception msgex)
            {
                ExtNet.MessageBox.Alert("Error", msgex.Message).Show();
            }
        }
    }
}