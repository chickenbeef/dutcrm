using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using Ext.Net;
using ParameterCollection = System.Web.UI.WebControls.ParameterCollection;

namespace CRMUI.RelationshipManager
{
    public partial class Home : Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {


			      btnConfirm.Disabled = true;

            try
            {

                txtEmpName.Text = Membership.GetUser().UserName;


                var em = new EmployeeBl().GetEmployee(txtEmpName.Text);

                if (!IsPostBack)
                {

                    txtEmpId.Text = em.EMP_ID.ToString(CultureInfo.InvariantCulture);

                }

            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();

            }

        }



        // UserName search
        protected void SearchByUserName(object sender, DirectEventArgs e)
        {

            try
            {

                var cl = new ClientBl().GetClientUserName(txtSUsername.Text);

                if (cl.Count == 0)
                {

                    ExtNet.Msg.Alert("No Result", "Please Enter Valid UserName").Show();

                }

                streClient.DataSource = cl;

                streClient.DataBind();

            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();

            }

        }





        // Name search
        protected void SearchByName(object sender, DirectEventArgs e)
        {

            try
            {

                var cl = new ClientBl().GetClientName(txtSName.Text);


                if (cl.Count == 0)
                {

                    ExtNet.Msg.Alert("No Result", "Please Enter Valid Name").Show();

                }

                streClient.DataSource = cl;
                streClient.DataBind();

            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();

            }

        }






        // Pass Client ID
        protected void PassValue(object sender, DirectEventArgs e)
        {

            try
            {
                string val = e.ExtraParams["Values"];


                Dictionary<string, string>[] clients = JSON.Deserialize<Dictionary<string, string>[]>(val);


                string cname = " ";
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




        //save Sale
        protected void SaveSave(object sender, EventArgs e)
        {

            try
            {

                var sale = new SaleBl().SaveSale(DateTime.Now, Convert.ToInt32(txtClientId.Text), Convert.ToInt32(txtEmpId.Text));

                ExtNet.Msg.Notify("Sale", "Sale Added").Show();


            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();

            }

            txtSName.Text = "";
            txtSUsername.Text = "";


            txtClientname.Text = string.Empty;
            btnConfirm.Disabled = true;



        }
    }

}