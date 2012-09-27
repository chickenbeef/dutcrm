using System;
using System.Globalization;
using System.Web.Security;
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
                //getting the user name of a user to populate their details
                string userName = Membership.GetUser().UserName;
                Session["UserName"] = userName;
                var objCl = new ClientBl().GetClientUserName(userName);
                 
                    if(!IsPostBack)
                    {
                        // loading the clients details when they login
                        Session["From"] = objCl[0].Name + " "+objCl[0].Surname;
                        txtName.Text = objCl[0].Name;
                        txtSurname.Text = objCl[0].Surname;
                        dfDob.Text = objCl[0].DateOfBirth.ToString(CultureInfo.InvariantCulture);
                        txtTel.Text = objCl[0].Telephone;
                        txtCell.Text = objCl[0].Cell;
                        txtFax.Text = objCl[0].Fax;
                        Session["txtClientID.Text"] = objCl[0].CLIENT_ID.ToString(CultureInfo.InvariantCulture);
                        txtClientID.Text = objCl[0].CLIENT_ID.ToString(CultureInfo.InvariantCulture);
                        txtBranchID.Text = objCl[0].BRH_ID.ToString(CultureInfo.InvariantCulture);
                        
                    }
                    
               



            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
            



        }
        
        protected void BtnEditClick(object sender, DirectEventArgs e)
        {
            //setting readonly to false so the user can edit their details
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

                //cancelling changes before they are updated
                string userName = Membership.GetUser().UserName;

                var objCl = new ClientBl().GetClientUserName(userName);

                txtName.Value = objCl[0].Name;
                txtSurname.Value = objCl[0].Surname;
                dfDob.Value = objCl[0].DateOfBirth;
                txtTel.Value = objCl[0].Telephone;
                txtCell.Value = objCl[0].Cell;
                txtFax.Value = objCl[0].Fax;
                txtClientID.Value = objCl[0].CLIENT_ID;
                txtBranchID.Value = objCl[0].BRH_ID;

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
               // checking if the data entered is correct and updating it accordingly
               if(txtName.Text=="")
               {
                   txtName.Focus();
               }
               else if(txtSurname.Text=="")
               {
                   txtSurname.Focus();
               }
               else if(dfDob.Text=="")
               {
                   dfDob.Focus();
               }
                  
               else if( objC.UpdateClient(Convert.ToInt32(txtClientID.Text), txtName.Text, txtSurname.Text, Convert.ToDateTime(dfDob.Text), txtTel.Text, txtCell.Text, txtFax.Text, DateTime.Now))
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
 
