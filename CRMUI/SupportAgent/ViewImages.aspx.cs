using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using Ext.Net;
using Image = Ext.Net.Image;

namespace CRMUI.SupportAgent
{
    public partial class ViewImages1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var id = Request.QueryString["epid"];
                var listOfImages = new ImageBl().GetImages(Convert.ToInt32(id));
                foreach (var row in listOfImages)
                {
                    var imageUrl = "data:image/png;base64," + Convert.ToBase64String(row.ImageFile);
                    var imageViewer = new Image
                                          {
                                              ID = "imgView" + row.IMG_ID,
                                              ImageUrl = imageUrl,
                                              Padding = 20
                                          };
                    pnlImages.Items.Add(imageViewer);
                }
            }
            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }
    }
}