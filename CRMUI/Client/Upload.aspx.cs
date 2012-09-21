
using System.Collections.Generic;
using AjaxControlToolkit;

namespace CRMUI.Client
{

    public partial class Upload : System.Web.UI.Page
    {


        //declaring lists to store, image type, content and id
        static List<string> _lstFileId = new List<string>();
        static List<string> _lstFileType = new List<string>();
        static List<byte[]> _lstContent = new List<byte[]>();

        protected void AjaxFileUpload_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
        {
            // getting the fileID, content and type of image uploaded by the user
            var ct = file.ContentType;
            var size = file.FileSize;
            var cont = file.GetContents();
            var id = file.FileId;


            //Inserting the fileId, image and type to a list so that multiple images can be saved
            _lstFileId.Insert(_lstFileId.Count, id);
            _lstFileType.Insert(_lstFileType.Count, ct);
            _lstContent.Insert(_lstContent.Count, cont);


        }
        // public properties used to send the lists to the emailSupport page
        public List<string> FileId
        {
            get { return _lstFileId; }
        }

        public List<string> FileType
        {
            get { return _lstFileType; }
        }
        public List<byte[]> Content
        {
            get { return _lstContent; }
        }
    }


}