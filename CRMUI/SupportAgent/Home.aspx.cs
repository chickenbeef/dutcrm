using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using Ext.Net;
using Button = Ext.Net.Button;

namespace CRMUI.SupportAgent
{
    public partial class CallHome : Page
    {
        public static string Role;
        private string _evtViewEmailDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Redirect based on role
            if (!IsPostBack)
            {
                //Role = Request.QueryString["role"].ToString(CultureInfo.InvariantCulture);

                //enables options of email support agent
                pnlEmailSupport.Visible = true;
                pnlEmailSupport.Enabled = true;

                //if (Role.Equals("EmailSupportAgent", StringComparison.InvariantCultureIgnoreCase))
                //{
                //    //enables options of email support agent
                //    pnlEmailSupport.Visible = true;
                //    pnlEmailSupport.Enabled = true;
                //}
                //else if (Role.Equals("CallSupportAgent", StringComparison.InvariantCultureIgnoreCase))
                //{
                //    //enables options of call support agent
                //    pnlCallSupport.Visible = true;
                //    pnlCallSupport.Enabled = true;
                //}
            }
            #endregion

            #region Populate GridPanel With emails
            var ep = new EmailProblemBl().GetAllUnAttendedEmailProblems();
            streEmailProbs.DataSource = ep;
            streEmailProbs.DataBind();
            #endregion

            #region Add Listeners To Events

            _evtViewEmailDetails = "#{lblSubject}.setText('<b>' + record.data.Subject + '</b><hr/>', false);";
            _evtViewEmailDetails += "#{lblFrom}.setText('<table style=width:100%><tr><td style=width:80%>Sent From: ' + record.data.From + '</td><td style=align:right;>' + record.data.DateCreated + '</td></tr></table><hr/>', false);";
            gpEmailProblems.Listeners.Select.Handler = _evtViewEmailDetails;

            #endregion
        }

        #region Direct Methods
            
        [DirectMethod]
        public void DeleteEmail(int epid)
        {
            try
            {
                var ep = new EmailProblemBl();
                ep.DeleteEmailProblem(epid);
                ExtNet.Msg.Notify("Delete", "Email Deleted.").Show();
                gpEmailProblems.ReRender();

            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        #endregion
    }
}