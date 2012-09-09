using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            ExtNet.MessageBox.Prompt("Template Body", cmbComTemplate.SelectedItem.Value).Show();
        }
    }
}