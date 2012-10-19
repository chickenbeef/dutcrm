using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.CallCentreManager
{
    public partial class Charts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var data = new ChartBL().Getsolvedoncreate();

            Store1.DataSource = data;
            Store1.DataBind();

        }


        protected void RefreshData(object sender, DirectEventArgs e)
        {
            var data = new ChartBL().Getsolvedoncreate();

            Store1.DataSource = data;
            Store1.DataBind();
        }
    }
}