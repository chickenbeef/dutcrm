using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;

namespace CRMUI.CallCentreManager
{
    public partial class MessagingServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var comms = new ComTemplateBl().GetAllTemplates();
            streComTemplate.DataSource = comms;
            streComTemplate.DataBind();
        }
    }
}