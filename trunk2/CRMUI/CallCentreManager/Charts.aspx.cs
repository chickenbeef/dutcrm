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

        protected void SaveChart(object sender, DirectEventArgs e)
        {
            try
            {
                //ExtNet.MessageBox.Confirm('Confirm Download', 'Would you like to download this chart as an image',Func<choice>
                //{if(choice == 'yes')
                //{
                //    btn
                //} 
                //})
            }
            catch(Exception ex)
            {
                ExtNet.MessageBox.Alert("Error", ex.Message).Show();
            }
        }

        protected void RefreshData(object sender, DirectEventArgs e)
        {
            var data = new ChartBL().Getsolvedoncreate();

            Store1.DataSource = data;
            Store1.DataBind();
        }
    }
}