using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
            //enables options of email support agent
            pnlEmailSupport.Visible = true;
            pnlEmailSupport.Enabled = true;

            //#region Redirect based on role
            //if (!IsPostBack)
            //{
            //    if (Roles.IsUserInRole("Email Support Agent"))
            //    {
            //        //enables options of email support agent
            //        pnlEmailSupport.Visible = true;
            //        pnlEmailSupport.Enabled = true;
            //    }
            //    else if (Roles.IsUserInRole("Call Support Agent"))
            //    {
            //        //enables options of call support agent
            //        pnlCallSupport.Visible = true;
            //        pnlCallSupport.Enabled = true;
            //    }
            //}
            //#endregion

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
                //no problem found
                if (probs.Count <= 0)
                {
                    CheckTicketButtons(true);
                    ExtNet.Msg.Alert("No solution found", "Problem not in database, please create a ticket without solution.").Show();
                    streESolutions.Reload();
                }
                //found 1 problem
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
                //found more than 1 problem that mathes keywords
                else
                {
                    var listOfSols = new List<Solution>();
                    foreach (var problem in probs)
                    {
                        //check for solution to each problem
                        var sols = new SolutionBl().GetSolutions(problem.PROB_ID);
                        listOfSols.AddRange(sols);
                    }
                    //if solutions exist
                    if (listOfSols.Count > 0)
                    {
                        //
                        streESolutions.DataSource = listOfSols;
                        streESolutions.DataBind();
                        CheckTicketButtons(false);
                    }
                    //if no solutions found for problems that exist in database
                    else
                    {

                        //CheckTicketButtons(true);
                        ExtNet.Msg.Alert("No solution found", "More than 1 problem was found but no solution exists for them. please create a ticket without solution").Show();

                        //TO TRY
                        //bind the problems source to the store and for each problem chosen show
                        //probdesc on the probdesc label on right
                        //NOTE: this method might not run at all because the way the search algorithm is done:
                        //LIKE %description%

                        clmEModified.Hide();
                        clmESolution.Text = "Problem Description";
                        gpSolutions.StoreID = "streEProblems";
                        streEProblems.DataSource = probs;
                        streEProblems.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //selected a solution *
        protected void GpESolutionSelected(object sender, DirectEventArgs e)
        {
            try
            {
                //get selected row details
                var selRow = e.ExtraParams["record"];

                if (gpSolutions.StoreID == "streEProblems")
                {
                    //convert it to problem object
                    var probRow = JSON.Deserialize(selRow, typeof(Problem)) as Problem;
                    //set details to allow creation of ticket with no solution
                    if (probRow != null)
                    {
                        hEProbId.Value = probRow.PROB_ID;
                        lblEProbDesc.Text = probRow.Description;
                        lblEDateCreated.Text = "Problem Created By: " +
                                               probRow.DateCreated.Date.ToString("dd MMMM yyyy");
                        //show controls
                        fsEProbDesc.Visible = true;
                    }
                }
                else
                {
                    //convert to solution object
                    var solRow = JSON.Deserialize(selRow, typeof(Solution)) as Solution;

                    //set employee details
                    if (solRow != null)
                    {
                        var emp = new EmployeeBl().GetEmployeeById(solRow.EMP_ID);

                        //get problem description
                        var prob = new ProblemBl().GetProblem(solRow.PROB_ID);

                        //set solution details
                        hESolId.Value = solRow.SOL_ID;
                        hEProbId.Value = solRow.PROB_ID;
                        if (solRow.DateModified != null)
                        {
                            lblEEmployeeFullName.Text = "Modified By: " + emp.Name + " " + emp.Surname;
                            lblEDateModified.Text = "Date Modified: " + solRow.DateModified.Value.ToString("dd MMMM yyyy");
                        }
                        else
                        {
                            lblEEmployeeFullName.Text = "Created By: " + emp.Name + " " + emp.Surname;
                            lblEDateCreated.Text = "Date Created: " + solRow.DateCreated.ToString("dd MMMM yyyy");
                        }
                        lblEProbDesc.Text = prob.Description;
                        lblESolDesc.Html = solRow.Description;

                        //show controls
                        fsEProbDesc.Visible = true;
                        fsESolDesc.Visible = true;
                    }
                }

                //refresh to show details
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
                if (cmbEPriority.SelectedItem.Index == -1)
                {
                    ExtNet.Msg.Alert("No priority selected", "Please select a priority before creating a ticket.").Show();
                    cmbEPriority.Focus(true);
                }
                else
                {
                    //if no email problem has been selected
                    if (hEClientId.Value.ToString() == "")
                    {
                        ExtNet.Msg.Alert("No Client Details", "Please select an email problem to attach this solution to").Show();
                    }
                    else
                    {
                        ////get details of currently logged in employee
                        //var username = Membership.GetUser().UserName;
                        //var emp = new EmployeeBl().GetEmployee(username);
                        //var cid = Convert.ToInt32(hEClientId.Value);
                        //var solid = Convert.ToInt32(hESolId.Value);
                        //var probid = Convert.ToInt32(hEProbId.Value);
                        //var date = DateTime.Now.Date;
                        //var priority = cmbEPriority.SelectedItem.Value.ToString(CultureInfo.InvariantCulture);

                        ////get details of client
                        //var client = new ClientBl().GetClientByClientId(cid);

                        //var cpl = new ClientProblemLogBl();
                        //cpl.AddClientProblem(true, date, date, false, cid, emp.EMP_ID, probid, solid, priority);
                        //ExtNet.Msg.Notify("Completed Successfully","The ticket has been created").Show();

                        //TODO: allow agent to contact client via email
                    }

                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //clicked create a ticket without solution *
        protected void BtnECreateTicketNoSol(object sender, DirectEventArgs e)
        {
            try
            {
                if (cmbEPriority.SelectedItem.Index == -1)
                {
                    ExtNet.Msg.Alert("No priority selected", "Please select a priority before creating a ticket.").Show();
                    cmbEPriority.Focus(true);
                }
                else
                {
                    //if no email problem has been selected
                    if (hEClientId.Value.ToString() == "")
                    {
                        ExtNet.Msg.Alert("No Client Details", "Please select an email problem to attach this solution to").Show();
                    }
                    else
                    {
                        //TODO: create ticket with no sol and allow agent to contact client via email
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

        //enables and disables bottombar buttons(createTicket and comobox buttons)
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