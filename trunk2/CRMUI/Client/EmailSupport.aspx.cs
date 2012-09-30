using System;
using System.Collections.Generic;

using CRMBusiness;
using Ext.Net;

namespace CRMUI.Client
{
    public partial class EmailSupport : System.Web.UI.Page
    {
        static List<string> _lstFileName = new List<string>();
        static List<string>  _lstFileType = new List<string>();
        static List<byte[]> _lstCont= new List<byte[]>(); 
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
                    if (_lstCont  != null)
                    {
                        //looping through the list of images uploaded by the user and saving it to the images table in the database
                        for (var i = 0; i <= (_lstCont.Count-1); i++)
                        {
                            //checking if the file uploaded by the user is an image and the correct type
                            if (_lstFileType[i].Contains("jpg") || _lstFileType[i].Contains("gif") || _lstFileType[i].Contains("png") ||
                                _lstFileType[i].Contains("jpeg"))
                            {
                                objI.AddImage(_lstCont[i], id);
                            }
                            else
                            {
                                ExtNet.Msg.Notify("Error",
                                                  "The file type is incorrect please ensure that you are saving an image of type jpg,gif,png or jpeg");
                                _lstCont.Clear();
                                _lstFileName.Clear();
                                _lstFileType.Clear();
                            }

                        }
                    }
                   
                   
                    ExtNet.Msg.Notify("Success", "Your message has been sent").Show();
                    _lstCont.Clear();
                    _lstFileName.Clear();
                    _lstFileType.Clear();
                }

                else
                {
                    ExtNet.Msg.Notify("Error", "Unable to send message, please try again").Show();
                    _lstCont.Clear();
                    _lstFileName.Clear();
                    _lstFileType.Clear();
                }
                
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }//end btnSend



        protected void btnUploadImgs_OnDirectClick(object sender, DirectEventArgs e)
        {
            //check if file has been selected
            if(uploadImage.HasFile)
            {
                //get content, name and the type of image being uploaded
                var cont = uploadImage.FileBytes;
                var fname = uploadImage.FileName;
                var index = fname.IndexOf(".", StringComparison.Ordinal);
                var ct = "";
                if (index > 0)
                {
                    ct = fname.Substring(index + 1, fname.Length - (index + 1));
                }

                //add image to a list
                _lstFileName.Insert(_lstFileName.Count,fname);
                _lstFileType.Insert(_lstFileType.Count, ct);
                _lstCont.Insert(_lstCont.Count, cont);
              
    
            }
            else
            {
                ExtNet.Msg.Notify("No Image selected","Please select image to upload").Show();
            }
        }//end btnUploadImgs



       
    }
}