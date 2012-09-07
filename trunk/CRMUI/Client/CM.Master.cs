using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;


namespace CRMUI.Client
{
    public partial class CM : System.Web.UI.MasterPage
    {
        protected void lnkUpdateClient_OnClick(object sender, EventArgs e)
        {
           var userName = ((string) (Session["UserName"]));

            Response.Redirect("~/client/Home.aspx?UserName="+userName);
        }
    }
}