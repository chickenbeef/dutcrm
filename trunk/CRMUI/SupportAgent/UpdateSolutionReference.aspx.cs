using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Web.Security; 

namespace CRMUI.SupportAgent
{
	public partial class UpdateSolutionReference : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			// get empid

			string nm =  Membership.GetUser().UserName.ToString(CultureInfo.InvariantCulture);
		  var em = new CRMBusiness.EmployeeBl().GetEmployeeByName(nm);		
		  txtEmpId.Text = em[0].EMP_ID.ToString(CultureInfo.InvariantCulture);
      int empid = em[0].EMP_ID;



			//string nm = (string) (Session["UserName"]);
 
			btnAccept.Disabled = true;
		}



		//problem search
		protected void SearchProb(object sender, DirectEventArgs e)
		{

			try
			{

				var prob = new CRMBusiness.ProblemBl().GetProblems(txtProbDesc.Text);

				if (prob.Count == 0)
				{

					ExtNet.Msg.Alert("No Result", "Please Enter Valid Problem").Show();

				}

				streProblems.DataSource = prob;
				streProblems.DataBind();

				btnAccept.Disabled = false;

			}

			catch (Exception ex)
			{

				ExtNet.Msg.Alert("Error", ex.Message).Show();
			}

		}



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


				strSolutions.DataSource = sol;
				strSolutions.DataBind();


				cmbSolutions.Text = sol[0].SOL_ID.ToString(CultureInfo.InvariantCulture);
				sol[0].Description = cmbSolutions.Value.ToString();


				btnAccept.Disabled = false;

			}

			catch (Exception ex)
			{

				ExtNet.Msg.Alert("Error", ex.Message).Show();
			}

		}




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





		//save updated solution
	    protected void UpdateSolution(object sender, EventArgs e)
	    {

            try
            {

                var savSol = new CRMBusiness.SolutionBl().UpdateSolution(Convert.ToInt32(cmbSolutions.SelectedItem.Text),
                                                                                                                 newSolDesc.Text, DateTime.Now,
                                                                                                                 Convert.ToInt32(txtprobid.Text),
                                                                                                                 Convert.ToInt32(txtEmpId.Text));
                ExtNet.Msg.Notify("Update", "Solution Added").Show();


                txtProbDesc.Text = string.Empty;
                txtprobid.Text = string.Empty;
                txtEmpId.Text = string.Empty;
                newSolDesc.Text = string.Empty;


            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();

            }

	    }
	}
}