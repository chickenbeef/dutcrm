using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CRMData;
using Ext.Net;
using System.Web.Security;

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
                var membershipUser = Membership.GetUser();
                if (membershipUser == null)
                {
                }
                else
                {
                    var nm = membershipUser.UserName.ToString(CultureInfo.InvariantCulture);
                    var em = new CRMBusiness.EmployeeBl().GetEmployee(nm);
                    txtEmpId.Text = em.EMP_ID.ToString(CultureInfo.InvariantCulture);
                }
            }

            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();

            }

        }
        #endregion


        #region DIRECT EVENT SEARCH
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

                foreach (var t in prob)
                {
                    var sol = new CRMBusiness.SolutionBl().GetSolutions(t.PROB_ID);

                    if (sol.Count > 0)
                    {

                        lstprob.AddRange(new[] { t });
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

                var val = e.ExtraParams["Values"];
                var prob = JSON.Deserialize<Dictionary<string, string>[]>(val);

                foreach (var keyValuePair in prob.SelectMany(row => row.Where(keyValuePair => keyValuePair.Key == "PROB_ID")))
                {
                    txtprobid.Text = keyValuePair.Value;
                }


                var probid = Convert.ToInt32(txtprobid.Text);
                var sol = new CRMBusiness.SolutionBl().GetSolutions(probid);


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



        #region DIRECT EVENT ComboBox
        //pass selected description to textarea
        protected void PassDescription(object sender, DirectEventArgs e)
        {

            try
            {

                var soldesc = new CRMBusiness.SolutionBl().GetSolutions(Convert.ToInt32(txtprobid.Text));

                foreach (var solution in soldesc)
                {

                    if (Convert.ToInt32(cmbSolutions.SelectedItem.Value) == solution.SOL_ID)
                    {

                        newSolDesc.Text = solution.Description;

                    }

                }



                btnUpdate.Disabled = false;

            }

            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }

        }
        #endregion







        #region DIRECT EVENT Update Solution
        //save updated solution
        protected void UpdateSolutions(object sender, DirectEventArgs e)
        {

            try
            {
                new CRMBusiness.SolutionBl().UpdateSolution(Convert.ToInt32(cmbSolutions.SelectedItem.Text),
                                                                                                        newSolDesc.Text, DateTime.Now,
                                                                                                        Convert.ToInt32(txtprobid.Text),
                                                                                                        Convert.ToInt32(txtEmpId.Text));

                ExtNet.Msg.Notify("Update", "Solution Updated").Show();
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