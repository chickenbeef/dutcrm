using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.SupportAgent
{
    public partial class CallHome : Page
    {
        public static string Role;
        private static int _totalPages;

        protected void Page_Init(object sender, EventArgs e)
        {
            //code for left panel
            #region Left Panel

            try
            {
                //create status variables for email problems
                int totalProblems;
                var cards = new EmailProblemGuiBl
                {
                    DataSource = new EmailProblemBl().GetAllUnAttendedEmailProblems(),
                    PageSize = 5,
                    BtnLogOnDirectClick = BtnLogOnDirectClick
                }.CreateListOfCardPanels(out totalProblems, out _totalPages);
                pnlLeft.Items.Add(cards);
                statusBar.DefaultText = "<b>Total Problems: " + totalProblems + "</b>";
                lblPage.Html = "<b>Page " + 1 + " of " + _totalPages + "</b>";
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }

            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Redirect  based on role
            if (!IsPostBack)
            {
                Role = Request.QueryString["role"].ToString(CultureInfo.InvariantCulture);

                if (Role.Equals("EmailSupportAgent", StringComparison.InvariantCultureIgnoreCase))
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
            #endregion
        }

        #region Left Panel

        private static void BtnLogOnDirectClick(object sender, DirectEventArgs directEventArgs)
        {
            try
            {
                var epid = directEventArgs.ExtraParams[0].Value;
                var clientid = directEventArgs.ExtraParams[1].Value;
                ExtNet.Msg.Notify(epid, clientid).Show();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        protected void BtnNextClick(object sender, DirectEventArgs e)
        {
            try
            {
                var index = int.Parse(e.ExtraParams["index"]);

                if ((index + 1) < pnlLeft.Items.Count)
                {
                    pnlLeft.ActiveIndex = index + 1;
                }
                //show current page
                lblPage.Html = "<b>Page " + (pnlLeft.ActiveIndex + 1) + " of " + _totalPages + "</b>";

                CheckButtons();

                //refresh panels to add any new emails that have arrived
                pnlLeft.Render();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        protected void BtnPrevClick(object sender, DirectEventArgs e)
        {
            try
            {
                var index = int.Parse(e.ExtraParams["index"]);

                if ((index - 1) >= 0)
                {
                    pnlLeft.ActiveIndex = index - 1;
                }
                //show current page
                lblPage.Html = "<b>Page " + (pnlLeft.ActiveIndex + 1) + " of " + _totalPages + "</b>";

                CheckButtons();
                //refresh panels to add any new emails that have arrived
                pnlLeft.Render();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        private void CheckButtons()
        {
            try
            {
                var index = pnlLeft.ActiveIndex;

                btnNext.Disabled = index == (pnlLeft.Items.Count - 1);
                btnPrev.Disabled = index == 0;
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        #endregion
    }
}