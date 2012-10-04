using System;
using System.Globalization;
using System.Web;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.CallCentreManager
{
    public partial class AutomationSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 var el = new EscalationBl().GetConfigInfo();
                 txtMins.Text = el[3].Duration.ToString(CultureInfo.InvariantCulture);
                 el.RemoveAt(3);
                 strEscalation.DataSource = el;
                 strEscalation.DataBind();

                 cmbPriorities.Text = el[0].Priority;
                 txtHours.Text = el[0].Duration.ToString(CultureInfo.InvariantCulture);
            }
        }

        #region DIRECT EVENTS
        //SAVE/UPDATE AUTOMATION SETTINGS IN DATABASE
        protected void SaveSettings(object sender, EventArgs e)
       {

           var elsave = new EscalationBl();

           try
           {
               if (elsave.UpdateConfig(cmbPriorities.SelectedItem.Text.ToString(CultureInfo.InvariantCulture), Convert.ToInt16(txtHours.Value)))
               {

                   elsave.UpdateConfig("UNATTENDED", Convert.ToInt16(txtMins.Value));

                   cmbPriorities.Disable(true);
                   txtHours.Disable(true);
                   txtMins.Disable(true);
                   ExtNet.Mask.Hide();
                   ExtNet.Msg.Notify("Update Status", " Escalation update successful").Show();
               }
               else
               {
                   ExtNet.Mask.Hide();
                   ExtNet.Msg.Notify("Update Status", "Escalation update was not completed").Show();
               }
           }
           catch (Exception ex)
           {
               ExtNet.Mask.Hide();
               ExtNet.Msg.Alert("Error", ex.Message).Show();
           }
       }
        //REDIRECT TO HOME PAGE ON CANCEL CLICK
        protected void CancelClicked(object sender, DirectEventArgs e)
        {
            Response.Redirect("~/CallCentreManager/Home.aspx");
        }

        #endregion

        #region VALIDATION 
        protected void HoursChanged(object sender, DirectEventArgs e)
        {
            btnConfirm.Enable(true);
        }

        protected void MinsChanged(object sender, DirectEventArgs e)
        {
            btnConfirm.Enable(true);
        }
        #endregion


    }
}