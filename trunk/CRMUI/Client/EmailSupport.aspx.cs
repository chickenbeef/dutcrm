using System;
using System.Collections.Generic;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.Client
{
    public partial class EmailSupport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //sending the name, surname and id of a client using a session variable specific to that user
            txtFrom.Text = (string)(Session["From"]);
            txtClID.Text = (string)(Session["txtClientID.Text"]);
        }

        protected void btnSend_OnDirectClick(object sender, DirectEventArgs e)
        {
            try
            {
               //Declaration of variables and assigning the values in the public properties from the upload page to the values 
                var objEp = new EmailProblemBl();
                var objUp = new Upload();
                var fileId = objUp.FileId;
                var ct = objUp.FileType;
                var content = objUp.Content;
                
                var objI = new ImageBl();

                //if the subject textbox is left empty no subject is assigned to the textbox
                if(txtSubject.Text=="")
                {
                    txtSubject.Text = "No Subject";
                }

                //saving an emailProblem
                var id = objEp.AddEmailProblem(txtFrom.Text,txtSubject.Text,heDesc.Text, DateTime.Now, Convert.ToInt32(txtClID.Text), null);
                //checking if the email problem was saved
                if (id != 0)
                {
                    //checking if an image has been uploaded
                    if (fileId != null)
                    {
                        //looping through the list of images uploaded by the user and saving it to the images table in the database
                        for (var i = 0; i <= (fileId.Count-1); i++)
                        {
                            //checking if the file uploaded by the user is an image and the correct type
                            if (ct[i].Contains("jpg") || ct[i].Contains("gif") || ct[i].Contains("png") ||
                                ct[i].Contains("jpeg"))
                            {
                                objI.AddImage(content[i], id);
                            }
                            else
                            {
                                ExtNet.Msg.Notify("Error",
                                                  "The file type is incorrect please ensure that you are saving an image of type jpg,gif,png or jpeg");
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