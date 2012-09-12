using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using Ext.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CRMUI.CallCentreManager
{
    public partial class MessagingServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var comms = new ComTemplateBl().GetAllTemplates();
            var cl = new ClientBl().GetAllClients();
            
                    if (!IsPostBack)
                    {
                            streComTemplate.DataSource = comms;
                            streComTemplate.DataBind();
            

                            streClient.DataSource = cl;
                            streClient.DataBind();
                    }
        }


        protected void SendMessage(object sender, DirectEventArgs e)
        {
            try
            {
                
                string val = e.ExtraParams["Values"];

                Dictionary<string, string> [] clients = JSON.Deserialize<Dictionary<string, string>[]>(val);

                List<string> emails;

                emails = new List<string>();

                foreach (Dictionary<string, string> row in clients)
                {
                    //for each fow in clients grid
                    foreach (KeyValuePair<string, string> keyValuePair in row)
                    {
                        if (keyValuePair.Key == "Email")
                        {
                                emails.Add(keyValuePair.Value);
                        }
                    }
                 }

                //add emails to
                foreach (var email in emails)
                {
                    editrPara.Html += email.ToString(CultureInfo.InvariantCulture);
                    editrPara.Html += "</br>";
                }
                
            }
            catch (Exception ex)
            {
                ExtNet.MessageBox.Alert("Error Occured", ex.Message).Show();
            }
        }

      
    }
}