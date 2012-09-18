using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using CRMBusiness.CRM;
using Ext.Net;

namespace CRMUI.CallCentreManager
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            btnUpdateRole.Disabled = true;


        }



        //UserName Search
        protected void SearchByUserName(object sender, DirectEventArgs e)
        {

					try
					{

						var emp = new EmployeeBl().GetEmployee(txtSUsername.Text);
						txtSName.Reset();


						if (emp == null)
						{
							ExtNet.Msg.Alert("No Result", "Please Enter Valid Name").Show();
							txtSUsername.Reset();
                            
						}
					    var le = new List<vEmployee> {emp};
					    streEmployee.DataSource = le;
                        streEmployee.DataBind();
					}

					catch (Exception ex)
					{

						ExtNet.Msg.Alert("Error", ex.Message).Show();

					}

        }






        //Name Search
        protected void SearchByName(object sender, DirectEventArgs e)
        {

					try
					{

						var emp = new EmployeeBl().GetEmployeeByName(txtSName.Text);
						txtSUsername.Reset(); 


						if (emp.Count == 0)
						{

							ExtNet.Msg.Alert("No Result", "Please Enter Valid UserName").Show();
							txtSName.Reset();

						}

						streEmployee.DataSource = emp;

						streEmployee.DataBind();


					}

					catch (Exception ex)
					{

						ExtNet.Msg.Alert("Error", ex.Message).Show();

					}

  
        }



        // Pass EMP ID
        protected void PassValue(object sender, DirectEventArgs e)
        {

            try
            {
                string val = e.ExtraParams["Values"];

                Dictionary<string, string>[] clients = JSON.Deserialize<Dictionary<string, string>[]>(val);



                foreach (Dictionary<string, string> row in clients)
                {

                    foreach (KeyValuePair<string, string> keyValuePair in row)
                    {

                        if (keyValuePair.Key == "EMP_ID")
                        {
                            //empid = Convert.ToInt32(keyValuePair.Value);
                            txtEmpId.Text = keyValuePair.Value;
                        }

                        if (keyValuePair.Key == "UserName")

                        {

                            txtUname.Text = keyValuePair.Value;

                        }

                    }

										var rol = Roles.GetRolesForUser(txtUname.Text);

                    txtCurrentRole.Text = rol[0];
            
                }


                btnUpdateRole.Disabled = false;  
            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }

        }



			  //change Role Selected
				protected void ChangeRoleValue(object sender, DirectEventArgs e)
				{

					btnUpdateRole.Disabled = false;

				}



        protected void UpdateRole(object sender, DirectEventArgs e)
        {

	     	//var current = Roles.GetRolesForUser(txtCurrentRole.Text);
			//var added = new List<string>();
			//var removed = new List<string>();

        	//Roles.RemoveUserFromRole(txtName.Text, txtCurrentRole.Text);


        	try
        	{

        	    //Membership.CreateUser("BOB", "BOB###1");

        	    Roles.AddUserToRole("kim", "AAAAA");


        	    ExtNet.Msg.Notify("New Role", "Added to db").Show();

        	    //ExtNet.Msg.Notify("New User", "User Added to DB").Show();

        	    //ExtNet.Msg.Notify("New Role", "Role Added to DB").Show();

        	}



        	catch (Exception ex)
        	{
				ExtNet.Msg.Alert("Error", ex.Message).Show();
        		
        	}




					/*if (radGrpRoles.CheckedItems.Equals(txtCurrentRole.Text) && !current.Contains(txtCurrentRole.Text))
					{
						added.Add(radGrpRoles.CheckedItems.ToString());
					}



					if (added.Count > 0)
					{
						Roles.AddUsersToRoles(new[] { txtCurrentRole.Text }, added.ToArray());
						ExtNet.Msg.Alert("Roles", "Role Updated").Show();

					}
					 */


        }



    }
}