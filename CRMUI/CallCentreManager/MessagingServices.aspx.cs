using System;
using System.Collections.Generic;
using System.Web;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.CallCentreManager
{
    public partial class MessagingServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var cats = new CategoriesBl().GetAllCategories();
            var cl = new ClientBl().GetAllClients();
            
                    if (!IsPostBack)
                    {
                        strCategory.DataSource = cats;
                        strCategory.DataBind();
                        
                        streClient.DataSource = cl;
                        streClient.DataBind();
                    }
                
                
        }

        #region Direct Events
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

                //for testing mechanism
                //List<string> custommail = new List<string>();
                //custommail.Add("akoob.iftekhar@gmail.com");
                //custommail.Add("zombiebunny01@gmail.com");

               Composemailmessage(emails);
                
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error Occured", ex.Message).Show();
            }
        }

        

        //gets selected item for category..passes and returns all templates
        protected void CmbCategorySelected(object sender, DirectEventArgs e)
        {
            var comms = new ComTemplateBl().GetAllTemplates(Convert.ToInt32(cmbCategory.SelectedItem.Value));
            streComTemplate.DataSource = comms;
            streComTemplate.DataBind();

            cmbComTemplate.Enable(true);

        }

        protected void BtnCancelClicked(object sender, DirectEventArgs e)
        {
            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }

        protected void CmbComTemplateSelectedItem(object sender, DirectEventArgs e)
        {
            var template = new ComTemplateBl().GetTemplateById(Convert.ToInt32(cmbComTemplate.SelectedItem.Value));
            editrPara.Value = template.Paragraph;
        }

        #endregion

        #region Helper Methods

        protected void Composemailmessage(List<string> mailadresses)
        {
            try
            {
                //CODE TO ADD EMAILS AND GENERATE EMAIL
                var objmailmessage = new EmailBl
                {
                    Bcc = mailadresses,
                    Subject = cmbComTemplate.SelectedItem.Text,
                    Body = editrPara.Text
                };

                if (objmailmessage.SendEmail())
                {
                    ExtNet.Msg.Alert(" Mail Message", "Message successfully sent to recipients").Show();
                }
                else
                {
                    ExtNet.Msg.Alert(" Mail Message Error", objmailmessage.Error).Show();
                }
            }
            catch (Exception msgex)
            {
                ExtNet.Msg.Alert("Error", msgex.Message).Show();
            }
        }

        #endregion

        //validation to do
        protected void OnTextChange(object sender, EventArgs e)
        {
            cmbComTemplate.Disable(true);
            btnCancel.Enable(true);
        }
    }
}