
using AjaxControlToolkit;

namespace CRMUI.Client
{
    public partial class Upload : System.Web.UI.Page
    {

        static int _count=0;
        static string[] arrfile = new string[10];
        static string[] arrType = new string[10];
        static byte[][] arrcontent = new byte[10][];
       
        protected void AjaxFileUpload_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
        {
            
            var ct = file.ContentType;
            var cont = file.GetContents();
            var id = file.FileId;
           

            arrfile[_count] = id;
            arrType[_count] = ct;
            arrcontent[_count] = cont;
            Session["fileId"] = arrfile;
            Session["fileContentType"] = arrType;
            Session["fileContents"] = arrcontent;
            Session["count"] = _count;
            _count += 1;
        }
    }
}