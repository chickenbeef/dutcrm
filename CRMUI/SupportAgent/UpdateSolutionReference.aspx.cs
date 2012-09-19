using System;
using System.Collections.Generic;
using System.Globalization;
using CRMBusiness.CRM;
using Ext.Net;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace CRMUI.SupportAgent
{
    public partial class UpdateSolutionReference : System.Web.UI.Page
		{
			  #region Get Employee ID
			protected void Page_Load(object sender, EventArgs e)
        {

					btnUpdate.Disabled = true;

        	try
        	{

						// get empid
						string nm = Membership.GetUser().UserName.ToString(CultureInfo.InvariantCulture);
						var em = new CRMBusiness.EmployeeBl().GetEmployee(nm);
						txtEmpId.Text = em.EMP_ID.ToString(CultureInfo.InvariantCulture);

        	}

        	catch (Exception ex)
        	{

						ExtNet.Msg.Alert("Error", ex.Message).Show();
        		
        	}
 
        }
			#endregion


       #region Problem Search
			//problem search
        protected void SearchProb(object sender, DirectEventArgs e)
        {

            try
            {

                var prob = new CRMBusiness.ProblemBl().GetProblems(txtProbDesc.Text);


								if (prob.Count == 0)
								{

									ExtNet.Msg.Alert("No Result", "Please Enter Valid Problem Description").Show();
									txtProbDesc.Reset();
									txtprobid.Reset();
									newSolDesc.Reset();
									strSolutions.RemoveAll();
									cmbSolutions.Reset();

								}



							//Remove Problems With No Solutions
              var lstprob = new List<Problem>();

							for(var i = 0; i<prob.Count; i++)
							{

								var sol = new CRMBusiness.SolutionBl().GetSolutions(prob[i].PROB_ID);

								if(sol.Count > 0)
								{

									lstprob.AddRange(new[] {prob[i]});
								}

							}


							streProblems.DataSource = lstprob;
							streProblems.DataBind();


            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }

        }
#endregion



			#region Grid Panel Row Details
				//pass selected prob_id
        protected void PassValue(object sender, DirectEventArgs e)
        {
            try
            {

                string val = e.ExtraParams["Values"];
                Dictionary<string, string>[] prob = JSON.Deserialize<Dictionary<string, string>[]>(val);

                foreach (Dictionary<string, string> row in prob)
                {

                    foreach (KeyValuePair<string, string> keyValuePair in row)
                    {

                        if (keyValuePair.Key == "PROB_ID")
                        {

                            txtprobid.Text = keyValuePair.Value;
                        }
                    }

                }


                int probid = Convert.ToInt32(txtprobid.Text);
                var sol = new CRMBusiness.SolutionBl().GetSolutions(probid);
							 
							  
                foreach (Solution t in sol)
                {
                    t.Description = Regex.Replace(t.Description, "<(.|\n)*?>", "");
                }


                strSolutions.DataSource = sol;
                strSolutions.DataBind();


                cmbSolutions.Text = sol[0].SOL_ID.ToString(CultureInfo.InvariantCulture);

                newSolDesc.Text = sol[0].Description;


                btnUpdate.Disabled = false;

            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }

        }
				#endregion



				#region ComboBox
				//pass selected description to textarea
        protected void PassDescription(object sender, DirectEventArgs e)
        {

            try
            {

                newSolDesc.Text = cmbSolutions.SelectedItem.Value;

                btnUpdate.Disabled = false;

            }

            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }

        }
				#endregion







				#region Update Solution
				//save updated solution
        protected void UpdateSolutions(object sender, DirectEventArgs e)
        {

            try
            {
                new CRMBusiness.SolutionBl().UpdateSolution(Convert.ToInt32(cmbSolutions.SelectedItem.Text),
                                                                                                        newSolDesc.Text, DateTime.Now,
                                                                                                        Convert.ToInt32(txtprobid.Text),
                                                                                                        Convert.ToInt32(txtEmpId.Text));

                ExtNet.Msg.Notify("Update", "Solution Added").Show();
            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();

            }


            txtProbDesc.Reset();


            txtprobid.Text = string.Empty;
            txtEmpId.Text = string.Empty;
            newSolDesc.Text = string.Empty;


            streProblems.RemoveAll();
            strSolutions.RemoveAll();
            cmbSolutions.Reset();


            btnUpdate.Disabled = true;

				}
				#endregion


		}
}