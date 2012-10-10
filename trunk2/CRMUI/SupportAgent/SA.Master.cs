using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CRMUI.SupportAgent
{
    public partial class SA : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lnkHome.PostBackUrl = string.Format("~/SupportAgent/Home.aspx?role={0}", CallHome.Role);
            }

        }

        protected void LnkLogoutClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}