using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.RelationshipManager
{
    public partial class Home : Page
		{

			#region Get Employee UserName
			protected void Page_Load(object sender, EventArgs e)
        {

			      btnConfirm.Disabled = true;

            try
            {

                txtEmpName.Text = Membership.GetUser().UserName;
                var em = new EmployeeBl().GetEmployee(txtEmpName.Text);
                txtEmpId.Text = em.EMP_ID.ToString(CultureInfo.InvariantCulture);
              
            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            	  txtEmpName.Reset();

            }

        }
			#endregion


			#region DIRECT EVENT SEARCH
			// UserName search
        protected void SearchByUserName(object sender, DirectEventArgs e)
        {

        	txtSName.Reset();

            try
            {

                var cl = new ClientBl().GetClientUserName(txtSUsername.Text);

                if (cl.Count == 0)
                {

                    ExtNet.Msg.Alert("No Result", "Please Enter Valid Client UserName").Show();
                	  txtSUsername.Reset();
                }

								else
                {
									streClient.DataSource = cl;
									streClient.DataBind();
                }

            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            	  txtSUsername.Reset();

            }

        }
			#endregion





			#region DIRECT EVENT SEARCH 
				// Name search
        protected void SearchByName(object sender, DirectEventArgs e)
        {

        	txtSUsername.Reset();

            try
            {

                var cl = new ClientBl().GetClientName(txtSName.Text);


                if (cl.Count == 0)
                {

                    ExtNet.Msg.Alert("No Result", "Please Enter Valid Client Name").Show();
                	  txtSName.Reset();

                }

                else
                {
									streClient.DataSource = cl;
									streClient.DataBind();
                }

            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();

            }

        }
				#endregion






			#region Grid Panel Row Details
				// Pass Client ID
        protected void PassValue(object sender, DirectEventArgs e)
        {

            try
            {
                string val = e.ExtraParams["Values"];
                Dictionary<string, string>[] clients = JSON.Deserialize<Dictionary<string, string>[]>(val);


                string cname;
                int cid;


                foreach (Dictionary<string, string> row in clients)
                {

                    foreach (KeyValuePair<string, string> keyValuePair in row)
                    {

                        if (keyValuePair.Key == "CLIENT_ID")
                        {
                            cid = Convert.ToInt32(keyValuePair.Value);
                            txtClientId.Text = cid.ToString(CultureInfo.InvariantCulture);
                        }

                        if (keyValuePair.Key == "Name")
                        {

                            cname = keyValuePair.Value;
                            txtClientname.Text = cname;

                        }
                    }
                }

                btnConfirm.Disabled = false;
            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }

        }
				#endregion



			#region DIRECT EVENT Add Sale
				//save Sale
    	protected void SaveSaves(object sender, DirectEventArgs e)
    	{

				try
				{
					 new SaleBl().SaveSale(DateTime.Now, Convert.ToInt32(txtClientId.Text), Convert.ToInt32(txtEmpId.Text));

					ExtNet.Msg.Notify("Sale Details", "Sale Added").Show();
				}

				catch (Exception ex)
				{

					ExtNet.Msg.Alert("Error", ex.Message).Show();

				}

				txtSName.Reset();
				txtSUsername.Reset();


				txtClientname.Text = string.Empty;
				btnConfirm.Disabled = true;

    		streClient.RemoveAll();
				//


			}
				#endregion
		}

}