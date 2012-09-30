using System;
using System.Collections.Generic;
using System.Text;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.Client
{
    public partial class EmailSupport : System.Web.UI.Page
    {
       
        static List<byte[]> _lstCont = new List<byte[]>();
        private static int _count;
        static StringBuilder sb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            //sending the name, surname and id of a client using a session variable specific to that user
            txtFrom.Text = (string)(Session["From"]);
            txtClID.Text = (string)(Session["txtClientID.Text"]);
            if(!IsPostBack)
            {
                _count = 0;
                _lstCont.Clear();
                sb.Clear();
            }
        }

        protected void btnSend_OnDirectClick(object sender, DirectEventArgs e)
        {
            try
            {
                //Declaration of variables and assigning the values in the public properties from the upload page to the values 
                var objEp = new EmailProblemBl();
                var objI = new ImageBl();

                //if the subject textbox is left empty no subject is assigned to the textbox
                if (txtSubject.Text == "")
                {
                    txtSubject.Text = "No Subject";
                }

                //saving an emailProblem
                var id = objEp.AddEmailProblem(txtFrom.Text, txtSubject.Text, heDesc.Text, DateTime.Now, Convert.ToInt32(txtClID.Text), null);
                //checking if the email problem was saved
                if (id != 0)
                {
                    //checking if an image has been uploaded
                    if (_lstCont != null)
                    {
                        //looping through the list of images uploaded by the user and saving it to the images table in the database
                        for (var i = 0; i <= (_lstCont.Count - 1); i++)
                        {
                            //saving image to the database
                            objI.AddImage(_lstCont[i], id);
                        }
                    }

                    ExtNet.Msg.Notify("Success", "Your message has been sent").Show();
                    _lstCont.Clear();
                    heDesc.Clear();
                    _count = 0;
                   
                }

                else
                {
                    ExtNet.Msg.Notify("Error", "Unable to send message, please try again").Show();
                    _lstCont.Clear();
                    _count = 0;
                    
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
            if (uploadImage.HasFile)
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
                //check if maximum number of files allowed to be uploaded has not been reached
                if (_count < 5)
                {
                    //check if file uploaded is an image
                    if (ct == "jpg" || ct == "gif" || ct == "png" ||
                                 ct == "jpeg")
                    {
                        //add image to a list
                        _lstCont.Insert(_lstCont.Count, cont);

                        //get filename
                        var pos = fname.LastIndexOf('\\') + 1;
                        var filename = fname.Substring(pos);

                        sb.Append("<b>" + filename + " -</b> <b style='color:Green'>Uploaded</b><br/>");
                         lblFiles.Html = sb.ToString();
                        _count += 1;
                    }
                    else
                    {
                        ExtNet.Msg.Notify("Error",
                                                  "The file type is incorrect please ensure that you are saving an image of type jpg,gif,png or jpeg").Show();
                    }
                }

                else
                {
                    ExtNet.Msg.Notify("Maximum amount of files",
                                      "Maximum amount of files allowed to upload has been reached").Show();
                }

            }
            else
            {
                ExtNet.Msg.Notify("No Image selected", "Please select image to upload").Show();
            }
        } //end btnUploadImgs


       

    }
}