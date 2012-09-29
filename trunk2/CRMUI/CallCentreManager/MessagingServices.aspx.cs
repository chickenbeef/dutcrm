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
                if (!ValidData())
                {
                    ExtNet.Msg.Alert("Error Occured", "Ensure you have selected a template and entered a paragraph!").
                        Show();
                }
                else
                {
                    var val = e.ExtraParams["Values"];
                    if (val == "[]")
                    {
                        ExtNet.Msg.Alert("Error Occured", "Please select one or more clients!").
                            Show();
                    }
                    else
                    {
                       
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

                        Composemailmessage(emails);
                    }
                }
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

            cmbComTemplate.Text = "Select a template";
            cmbComTemplate.Enable(true);

        }

        protected void BtnCancelClicked(object sender, DirectEventArgs e)
        {
            cmbCategory.Reset();
            cmbComTemplate.Reset();
            cmbComTemplate.Disabled = true;
            editrPara.Reset();
        }

        protected void CmbComTemplateSelectedItem(object sender, DirectEventArgs e)
        {
            editrPara.Disabled = false;
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
                    To = string.Empty,
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

        #region VALIDATION
        //validation to do
        protected bool ValidData()
        {
            if (cmbComTemplate.Text=="Select a template")
            {
                return false;
            }
            else if (editrPara.Text.Length==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void OnTextChange(object sender, EventArgs e)
        {
            btnCancel.Disabled=false;
        }
        #endregion
    }
}