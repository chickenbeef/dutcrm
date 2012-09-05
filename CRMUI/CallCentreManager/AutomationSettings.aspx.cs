using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;

namespace CRMUI.CallCentreManager
{
    public partial class AutomationSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var el = new EscalationBl().GetConfigInfo();
            txtMins.Text = el[3].Duration.ToString(CultureInfo.InvariantCulture);
            el.RemoveAt(3);
            strEscalation.DataSource = el;
            strEscalation.DataBind();
            
        }


        protected void SaveSettings__(object sender, EventArgs e)
        {
            
        }
    }
}