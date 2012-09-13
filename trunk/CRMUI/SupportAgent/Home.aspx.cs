using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using CRMBusiness.CRM;
using Ext.Net;
using Button = Ext.Net.Button;
using Image = Ext.Net.Image;

namespace CRMUI.SupportAgent
{
    public partial class CallHome : Page
    {
        public static string Role;
        private string _evtViewEmailHandler;
        private static bool _newProblem;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Redirect based on role
            if (!IsPostBack)
            {
                //Role = Request.QueryString["role"].ToString(CultureInfo.InvariantCulture);

                //enables options of email support agent
                pnlEmailSupport.Visible = true;
                pnlEmailSupport.Enabled = true;

                //if (Role.Equals("EmailSupportAgent", StringComparison.InvariantCultureIgnoreCase))
                //{
                //    //enables options of email support agent
                //    pnlEmailSupport.Visible = true;
                //    pnlEmailSupport.Enabled = true;
                //}
                //else if (Role.Equals("CallSupportAgent", StringComparison.InvariantCultureIgnoreCase))
                //{
                //    //enables options of call support agent
                //    pnlCallSupport.Visible = true;
                //    pnlCallSupport.Enabled = true;
                //}
            }
            #endregion

            #region Populate GridPanel With emails
            var ep = new EmailProblemBl().GetAllUnAttendedEmailProblems();
            streEmailProbs.DataSource = ep;
            streEmailProbs.DataBind();
            #endregion

            #region Add Listeners To Events

            //When email is selected
            _evtViewEmailHandler = "#{hEPId}.setValue(record.data.EP_ID);";
            _evtViewEmailHandler += "#{lblSubject}.setText('<b>' + record.data.Subject + '</b><hr/>', false);";
            _evtViewEmailHandler += "#{lblFrom}.setText('<table style=width:100%><tr><td style=width:80%>Sent From: ' + record.data.From + '</td><td style=align:right;>' + record.data.DateCreated + '</td></tr></table><hr/>', false);";
            _evtViewEmailHandler += "#{lblEmailDesc}.setText('<br/>' + record.data.Description + '<br/><br/>', false); #{lblEmailDesc}.setDisabled(false); #{lblEmailDesc}.setVisible(true);";
            _evtViewEmailHandler += "#{hEClientId}.setValue(record.data.CLIENT_ID);";
            gpEmailProblems.Listeners.Select.Handler = _evtViewEmailHandler;
            #endregion
        }

        #region Direct Methods

        [DirectMethod]
        public void DeleteEmail(int epid)
        {
            try
            {
                var ep = new EmailProblemBl();
                ep.DeleteEmailProblem(epid);
                ExtNet.Msg.Notify("Delete", "Email Deleted.").Show();
                gpEmailProblems.ReRender();

            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }
        #endregion

        #region Direct Events

        //selected an email on top left panel (email inbox)
        protected void EmailProblemSelected(object sender, DirectEventArgs e)
        {
            //if the email problem has images attached
            var epid = Convert.ToInt32(hEPId.Value);
            var imageCount = new ImageBl().GetImageCount(epid);
            btnEViewImages.Disabled = imageCount <= 0;
        }

        //clicked view images button
        protected void BtnViewImagesClick(object sender, DirectEventArgs e)
        {
            try
            {
                var listOfImages = new ImageBl().GetImages(Convert.ToInt32(hEPId.Value));
                foreach (var row in listOfImages)
                {
                    var imageUrl = "data:image/png;base64," + Convert.ToBase64String(row.ImageFile);
                    var imageViewer = new Image
                    {
                        ID = "imgView" + row.IMG_ID,
                        ImageUrl = imageUrl,
                        Padding = 20
                    };
                    wndImageViewer.Items.Add(imageViewer);
                }
                wndImageViewer.Render();
                wndImageViewer.Show();
            }
            catch (Exception ex)
            {

                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //searched for a solution
        protected void BtnESearchSolClick(object sender, DirectEventArgs e)
        {
            try
            {
                var probs = new ProblemBl().GetProblems(txtEProbDesc.Text);
                if (probs.Count <= 0)
                {
                    CheckTicketButtons(true);
                    ExtNet.Msg.Alert("No solution found", "Problem not in database, please create a ticket without solution.").Show();
                    streESolutions.Reload();
                }
                else if (probs.Count == 1)
                {
                    var sols = new SolutionBl().GetSolutions(probs[0].PROB_ID);
                    if (sols.Count > 0)
                    {
                        streESolutions.DataSource = sols;
                        streESolutions.DataBind();
                        CheckTicketButtons(false);
                    }
                    else
                    {
                        lblEProbDesc.Text = probs[0].Description;
                        CheckTicketButtons(true);
                        ExtNet.Msg.Alert("No solution found", "A problem was found but no solution exists for it. please create a ticket without solution").Show();
                    }
                }
                else
                {
                    var listOfSols = new List<Solution>();
                    foreach (var problem in probs)
                    {
                        var sols = new SolutionBl().GetSolutions(problem.PROB_ID);
                        listOfSols.AddRange(sols);
                    }
                    if (listOfSols.Count > 0)
                    {
                        streESolutions.DataSource = listOfSols;
                        streESolutions.DataBind();
                        CheckTicketButtons(false);
                    }
                    else
                    {

                        CheckTicketButtons(true);
                        ExtNet.Msg.Alert("No solution found", "More than 1 problem was found but no solution exists for them. please create a ticket without solution").Show();

                        //TO TRY
                        //bind the problems source to the store and for each problem chosen show
                        //probdesc on the probdesc label on right
                        //NOTE: this method might not run at all because the way the search algorithm is done:
                        //LIKE %description%
                    }
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //selected a solution
        protected void GpESolutionSelected(object sender, DirectEventArgs e)
        {
            try
            {
                //get selected solution details
                var solDesc = e.ExtraParams["record"];
                var sol = JSON.Deserialize(solDesc, typeof(Solution)) as Solution;

                if (sol != null)
                {
                    //set client name
                    var emp = new EmployeeBl().GetEmployeeById(sol.EMP_ID);

                    //set problem description
                    var prob = new ProblemBl().GetProblem(sol.PROB_ID);

                    //set solution details
                    hESolId.Value = sol.SOL_ID;
                    hEProbId.Value = sol.PROB_ID;

                    if (sol.DateModified != null)
                    {
                        lblEEmployeeFullName.Text = "Modified By: " + emp.Name + " " + emp.Surname;
                        lblEDateModified.Text = "Date Modified: " + sol.DateModified.Value.ToString("dd MMMM yyyy");
                    }
                    else
                    {
                        lblEEmployeeFullName.Text = "Created By: " + emp.Name + " " + emp.Surname;
                        lblEDateCreated.Text = "Date Created: " + sol.DateCreated.ToString("dd MMMM yyyy");
                    }

                    lblEProbDesc.Text = prob.Description;
                    lblESolDesc.Html = sol.Description;
                }

                fsEProbDesc.Visible = true;
                fsESolDesc.Visible = true;
                pnlESolutionDetails.UpdateContent();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //clicked create a ticket with solution button
        protected void BtnECreateTicketSolClick(object sender, DirectEventArgs e)
        {
            try
            {
                if(cmbEPriority.SelectedItem.Index == -1)
                {
                    ExtNet.Msg.Alert("No priority selected", "Please select a priority before creating a ticket.").Show();
                    cmbEPriority.Focus(true);
                }
                else
                {
                    if(hEClientId.Value.ToString() == "")
                    {
                        ExtNet.Msg.Alert("No Client Details", "Please select an email problem to attach this solution to").Show();
                    }
                    else
                    {
                        //get details of currently logged in employee
                        var username = Membership.GetUser().UserName;
                        var emp = new EmployeeBl().GetEmployee(username);
                        var cid = Convert.ToInt32(hEClientId.Value);
                        var solid = Convert.ToInt32(hESolId.Value);
                        var probid = Convert.ToInt32(hEProbId.Value);
                        var date = DateTime.Now.Date;
                        var priority = cmbEPriority.SelectedItem.Value.ToString(CultureInfo.InvariantCulture);

                        var cpl = new ClientProblemLogBl();
                        cpl.AddClientProblem(true, date, date, false, cid, emp.EMP_ID, probid, solid, priority);
                    }

                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        #endregion

        #region helper methods

        private void CheckTicketButtons(bool noSolution)
        {
            cmbEPriority.Disabled = false;

            if (noSolution)
            {
                btnECreateTicketNoSol.Disabled = false;
                btnECreateTicketSol.Disabled = true;
            }
            else
            {
                btnECreateTicketSol.Disabled = false;
                btnECreateTicketNoSol.Disabled = true;
            }
        }

        #endregion
    }
}