using System;
using System.Globalization;
using System.Web.UI;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.Client
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string userName = Request.QueryString["UserName"];
                var objCl = new ClientBl().GetClientUserName(userName);
                if (!IsPostBack)
                {
                    txtName.Text = objCl.Name;
                    txtSurname.Text = objCl.Surname;
                    dfDob.Text = objCl.DateOfBirth.ToString(CultureInfo.InvariantCulture);
                    txtTel.Text = objCl.Telephone;
                    txtCell.Text = objCl.Cell;
                    txtFax.Text = objCl.Fax;
                    txtClientID.Text = objCl.CLIENT_ID.ToString(CultureInfo.InvariantCulture);
                    txtBranchID.Text = objCl.BRH_ID.ToString(CultureInfo.InvariantCulture);

                }
            }
            catch(Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
            



        }
        
        protected void BtnEditClick(object sender, DirectEventArgs e)
        {
            txtName.ReadOnly = false;
            txtSurname.ReadOnly = false;
            dfDob.ReadOnly = false;
            txtTel.ReadOnly = false;
            txtCell.ReadOnly = false;
            txtFax.ReadOnly = false;
            
        }

        
        protected void BtnCancelClick(object sender, DirectEventArgs e)
        {
            try
            {
                string userName = Request.QueryString["UserName"];
                var objCl = new ClientBl().GetClientUserName(userName);

                txtName.Value = objCl.Name;
                txtSurname.Value = objCl.Surname;
                dfDob.Value = objCl.DateOfBirth;
                txtTel.Value = objCl.Telephone;
                txtCell.Value = objCl.Cell;
                txtFax.Value = objCl.Fax;
                txtClientID.Value = objCl.CLIENT_ID;
                txtBranchID.Value = objCl.BRH_ID;

                txtName.ReadOnly = true;
                txtSurname.ReadOnly = true;
                dfDob.ReadOnly = true;
                txtTel.ReadOnly = true;
                txtCell.ReadOnly = true;
                txtFax.ReadOnly = true;
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
            

        }

        protected void BtnUpdateClick(object sender, DirectEventArgs e)
        {
            try
            {
                var objC = new ClientBl();
               if( objC.UpdateClient(Convert.ToInt32(txtClientID.Text), txtName.Text, txtSurname.Text, Convert.ToDateTime(dfDob.Text), txtTel.Text, txtCell.Text, txtFax.Text, DateTime.Now))
               {
                   txtName.ReadOnly = true;
                   txtSurname.ReadOnly = true;
                   dfDob.ReadOnly = true;
                   txtTel.ReadOnly = true;
                   txtCell.ReadOnly = true;
                   txtFax.ReadOnly = true;
                   ExtNet.Msg.Notify("Update Status", "Update successful").Show(); 
               }
               else
               {
                   ExtNet.Msg.Notify("Update Status", "Update was not completed").Show();
               }

                
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }
    }
 }
