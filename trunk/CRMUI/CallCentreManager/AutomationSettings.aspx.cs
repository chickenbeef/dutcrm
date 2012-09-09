using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                   ExtNet.Msg.Notify("Update Status", " Escalation update successful").Show();
               }
               else
               {
                   ExtNet.Msg.Notify("Update Status", "Escalation update was not completed").Show();
               }
           }
           catch (Exception ex)
           {
               ExtNet.Msg.Alert("Error", ex.Message).Show();
           }
       }
        
    }
}