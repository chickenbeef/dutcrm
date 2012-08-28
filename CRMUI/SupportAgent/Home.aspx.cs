using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRMUI.SupportAgent
{
    public partial class CallHome : System.Web.UI.Page
    {
        public static string Role;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Role = Request.QueryString["role"].ToString(CultureInfo.InvariantCulture);

                if(Role.Equals("EmailSupportAgent", StringComparison.InvariantCultureIgnoreCase))
                {
                    //enables options of email support agent
                    pnlEmailSupport.Visible = true;
                    pnlEmailSupport.Enabled = true;
                }
                else if (Role.Equals("CallSupportAgent", StringComparison.InvariantCultureIgnoreCase))
                {
                    //enables options of call support agent
                    pnlCallSupport.Visible = true;
                    pnlCallSupport.Enabled = true;
                }
            }
        }
    }
}