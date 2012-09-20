using System;
using System.Collections.Generic;
using System.Web.Security;
using CRMBusiness;
using CRMBusiness.CRM;
using Ext.Net;

namespace CRMUI.CallCentreManager
{
    public partial class Home : System.Web.UI.Page
		{
			#region page
			protected void Page_Load(object sender, EventArgs e)
        {
        	btnUpdateRole.Disabled = true;
        }
			#endregion



			#region Search By UserName
			//UserName Search
        protected void SearchByUserName(object sender, DirectEventArgs e)
        {
					
					txtSName.Reset();

					try
					 {	
				  						
						var emp = new EmployeeBl().GetEmployee(txtSUsername.Text);

					 	if(emp == null)
						{
							ExtNet.Msg.Alert("No Result", "Please Enter Valid Employee UserName").Show();
							txtSUsername.Reset();
						}

					 else
						{
							var lst = new List<vEmployee> {emp};

							if (lst.Count == 0)
							{
						
								ExtNet.Msg.Alert("No Result", "Please Enter Valid Employee UserName").Show();
								txtSUsername.Reset();
							}

							else
							{
								streEmployee.DataSource = lst;
								streEmployee.DataBind();
							}

						}

				    
					}

					catch (Exception ex)
					{

						ExtNet.Msg.Alert("Error", ex.Message).Show();
						txtSUsername.Reset();


					}
					
        }
				#endregion





			 #region Search By Name
				//Name Search
        protected void SearchByName(object sender, DirectEventArgs e)
        {

					try
					{

						var emp = new EmployeeBl().GetEmployeeByName(txtSName.Text);
						txtSUsername.Reset(); 


						if (emp.Count == 0)
						{

							ExtNet.Msg.Alert("No Result", "Please Enter Valid Employee Name").Show();
							txtSName.Reset();

						}

						else
						{
							streEmployee.DataSource = emp;
   						streEmployee.DataBind();
						}

					}

					catch (Exception ex)
					{

						ExtNet.Msg.Alert("Error", ex.Message).Show();
						txtSName.Reset();

					}

        }
				#endregion

			

				#region Grid Panel Row Details
				// Pass Row Details
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
                            txtEmpId.Text = keyValuePair.Value;
                        }

                        if (keyValuePair.Key == "UserName")
                        {

                            txtUname.Text = keyValuePair.Value;
                        }
                    }       
                }


								var current = Roles.GetRolesForUser(txtUname.Text);
	
							  txtCurrentRole.Text = current[0];

							  if(txtCurrentRole.Text == "Call Centre Manager")
							  {

							  	ExtNet.Msg.Alert("Roles", "Role Cannot Be Updated").Show();
							  	radGrpRoles.Reset();
							  	radGrpRoles.Disabled = true;
							  	btnUpdateRole.Disabled = true;
							  	streEmployee.RemoveAll();


							  	txtCurrentRole.Reset();
							  	txtEmpId.Reset();
							  	txtSName.Reset();
							  	txtSUsername.Reset();

							  }

							  else
							  {
							  	radGrpRoles.Reset();
							  	radGrpRoles.Disabled = false; 
							  }
            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }

        }
				#endregion


				#region Radio
				//change Role Selected
				protected void ChangeRoleValue(object sender, DirectEventArgs e)
				{

					btnUpdateRole.Disabled = false;
			
				}
				#endregion


				#region Update Role
				protected void UpdateRole(object sender, DirectEventArgs e)
        {

        	try
        	{

						if (radbtnCallSupport.Checked)
						{

							if (radbtnCallSupport.InputValue == txtCurrentRole.Text)

								ExtNet.Msg.Alert("Warning", "User Already Assigned To Role").Show();

							else
							{

								Roles.RemoveUserFromRole(txtUname.Text, txtCurrentRole.Text);
								Roles.AddUserToRole(txtUname.Text, radbtnCallSupport.InputValue);

								ExtNet.Msg.Notify("New Role", "User Assigned New Role").Show();

							}

						}



						if (radbtnEmailSupport.Checked)
						{

							if (radbtnEmailSupport.InputValue == txtCurrentRole.Text)

								ExtNet.Msg.Alert("Warning", "User Already Assigned To Role").Show();

							else
							{

								Roles.RemoveUserFromRole(txtUname.Text, txtCurrentRole.Text);
								Roles.AddUserToRole(txtUname.Text, radbtnEmailSupport.InputValue);

								ExtNet.Msg.Notify("New Role", "User Assigned New Role").Show();

							}

						}



						if (radbtnRM.Checked)
						{

							if (radbtnRM.InputValue == txtCurrentRole.Text)

								ExtNet.Msg.Alert("Warning", "User Already Assigned To Role").Show();

							else
							{

								Roles.RemoveUserFromRole(txtUname.Text, txtCurrentRole.Text);
								Roles.AddUserToRole(txtUname.Text, radbtnRM.InputValue);

								ExtNet.Msg.Notify("New Role", "User Assigned New Role").Show();

							}

						}


						if (radbtnSalesManager.Checked)
						{

							if (radbtnSalesManager.InputValue == txtCurrentRole.Text)

								ExtNet.Msg.Alert("Warning", "User Already Assigned To Role").Show();

							else
							{

								Roles.RemoveUserFromRole(txtUname.Text, txtCurrentRole.Text);
								Roles.AddUserToRole(txtUname.Text, radbtnSalesManager.InputValue);

								ExtNet.Msg.Notify("New Role", "User Assigned New Role").Show();

							}

						}



						if (radbtnTeamLeader.Checked)
						{

							if (radbtnTeamLeader.InputValue == txtCurrentRole.Text)

								ExtNet.Msg.Alert("Warning", "User Already Assigned To Role").Show();

							else
							{

								Roles.RemoveUserFromRole(txtUname.Text, txtCurrentRole.Text);
								Roles.AddUserToRole(txtUname.Text, radbtnTeamLeader.InputValue);

								ExtNet.Msg.Notify("New Role", "User Assigned New Role").Show();

							}

						}


        		txtSName.Reset();
        		txtSUsername.Reset();
        		txtCurrentRole.Reset();
        		txtUname.Reset();
        		txtEmpId.Reset();

        		radGrpRoles.Reset();
        		radGrpRoles.Disabled = true;
        		streEmployee.RemoveAll();

        		btnUpdateRole.Disabled = true;

        	}

        	catch (Exception ex)
        	{
				     ExtNet.Msg.Alert("Error", ex.Message).Show();
        		
        	}

				}
				#endregion



		}
}