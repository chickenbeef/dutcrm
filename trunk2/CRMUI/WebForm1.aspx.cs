using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRMUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var user = Membership.GetAllUsers();
            foreach (MembershipUser u in user)
            {
                string pass;
                if(u.UserName.Length == 4)
                {
                    pass = u.UserName + "##1";
                }
                else
                {
                    pass = u.UserName + "#1";
                }
                var changed = u.ChangePassword(u.ResetPassword(), pass);
            }
            
        }
    }
}