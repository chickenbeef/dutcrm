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
        private static bool _haveProblem;
        private static bool _haveSolution;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Redirect based on role
            if (!IsPostBack)
            {
                if (Roles.IsUserInRole("Email Support Agent"))
                {
                    //enables options of email support agent
                    pnlEmailSupport.Visible = true;
                    pnlEmailSupport.Enabled = true;
                }
                else if (Roles.IsUserInRole("Call Support Agent"))
                {
                    //enables options of call support agent
                    pnlCallSupport.Visible = true;
                    pnlCallSupport.Enabled = true;
                }
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
                cmbEPriority.Disabled = true;
                btnECreateTicketNoSol.Disabled = true;
                btnECreateTicketSol.Disabled = true;
                var probs = new ProblemBl().GetProblems(txtEProbDesc.Text);
                //no problem found
                if (probs.Count <= 0)
                {
                    _haveProblem = false;
                    _haveSolution = false;
                    taEProbDesc.Text = txtEProbDesc.Text;
                    wndAddProblem.Show();
                    streESolutions.Reload();
                }
                //found 1 problem
                else if (probs.Count == 1)
                {
                    _haveProblem = true;
                    var sols = new SolutionBl().GetSolutions(probs[0].PROB_ID);
                    if (sols.Count > 0)
                    {
                        _haveSolution = true;
                        clmEModified.Show();
                        clmESolId.Show();
                        clmESolution.Text = "Solution Description";
                        streESolutions.DataSource = sols;
                        streESolutions.DataBind();
                    }
                    else
                    {
                        _haveSolution = false;
                        hEProbId.Value = probs[0].PROB_ID;
                        lblEProbDesc.Text = probs[0].Description;
                        fsEProbDesc.Visible = true;
                        CheckTicketButtons(_haveSolution);
                        pnlESolutionDetails.UpdateContent();
                        ExtNet.Msg.Alert("No solution found", "1 problem was found but no solution exists for it. please create a ticket without solution").Show();
                    }
                }
                //found more than 1 problem that mathes keywords
                else
                {
                    _haveProblem = true;
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
                        _haveSolution = true;
                        streESolutions.DataSource = listOfSols;
                        streESolutions.DataBind();
                    }
                    //if no solutions found for problems that exist in database
                    else
                    {
                        _haveSolution = false;
                        ExtNet.Msg.Alert("No solution found", "More than 1 problem was found but no solution exists for them. please create a ticket without solution").Show();
                        clmEModified.Hide();
                        clmESolId.Hide();
                        clmESolution.Text = "Problem Description";
                        mdlESolutions.Fields.RemoveAt(0);
                        streESolutions.DataSource = probs;
                        streESolutions.DataBind();
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
                //get selected row details
                var selRow = e.ExtraParams["record"];

                if (_haveSolution)
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
                        lblESpacer.Html = "<br/><hr/><br/>";
                        lblEProbDesc.Text = prob.Description;
                        lblESolDesc.Html = solRow.Description;
                        CheckTicketButtons(_haveSolution);

                        //show controls
                        fsEProbDesc.Visible = true;
                        fsESolDesc.Visible = true;
                    }
                }
                else
                {
                    if (_haveProblem)
                    {
                        //convert it to problem object
                        var probRow = JSON.Deserialize(selRow, typeof(Dictionary<string, string>)) as Dictionary<string, string>;
                        //set details to allow creation of ticket with no solution
                        if (probRow != null)
                        {
                            CheckTicketButtons(_haveSolution);
                            hEProbId.Value = probRow["PROB_ID"];
                            lblEProbDesc.Text = probRow["Description"];
                            //show controls
                            fsEProbDesc.Visible = true;
                        }
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
                        //get details of currently logged in employee
                        var username = Membership.GetUser().UserName;
                        var emp = new EmployeeBl().GetEmployee(username);
                        var cid = Convert.ToInt32(hEClientId.Value);
                        var solid = Convert.ToInt32(hESolId.Value);
                        var probid = Convert.ToInt32(hEProbId.Value);
                        var date = DateTime.Now.Date;
                        var priority = cmbEPriority.SelectedItem.Value.ToString(CultureInfo.InvariantCulture);

                        //get details of client
                        var client = new ClientBl().GetClientByClientId(cid);

                        var cpl = new ClientProblemLogBl();
                        cpl.AddClientProblem(true, date, date, false, cid, emp.EMP_ID, probid, solid, priority);
                        ExtNet.Msg.Notify("Ticket Created", "The ticket has been created").Show();

                        //TODO: display popup to email client
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
                        //get details of currently logged in employee
                        var username = Membership.GetUser().UserName;
                        var emp = new EmployeeBl().GetEmployee(username);
                        var cid = Convert.ToInt32(hEClientId.Value);
                        var probid = Convert.ToInt32(hEProbId.Value);
                        var date = DateTime.Now.Date;
                        var priority = cmbEPriority.SelectedItem.Value.ToString(CultureInfo.InvariantCulture);

                        //get details of client
                        var client = new ClientBl().GetClientByClientId(cid);

                        var cpl = new ClientProblemLogBl();
                        cpl.AddClientProblem(false, date, null, false, cid, emp.EMP_ID, probid, null, priority);
                        ExtNet.Msg.Notify("Ticket Created", "The ticket has been created").Show();

                        //TODO: display popup to email client
                    }

                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        protected void WndAddProblemClosed(object sender, DirectEventArgs e)
        {
            taEProbDesc.Clear();
        }

        protected void BtnEAddProblemClick(object sender, DirectEventArgs e)
        {
            try
            {
                var username = Membership.GetUser().UserName;
                var empid = new EmployeeBl().GetEmployee(username).EMP_ID;
                var prob = new ProblemBl();
                var lastProbId = new ProblemBl().GetLastId();
                prob.AddProblem(taEProbDesc.Text, DateTime.Now, empid);
                hEProbId.Value = lastProbId + 1;
                lblEProbDesc.Text = taEProbDesc.Text;
                fsEProbDesc.Visible = true;
                pnlESolutionDetails.UpdateContent();
                CheckTicketButtons(_haveSolution);
                wndAddProblem.Close();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message);
            }
        }

        protected void BtnSendEmailClick(object sender, DirectEventArgs e)
        {
            
        }

        protected void CmbCategorySelectedItem(object sender, DirectEventArgs e)
        {
            
        }

        protected void CmbTemplateSelectedItem(object sender, DirectEventArgs e)
        {
            
        }

        #endregion

        #region helper methods

        //enables and disables bottombar buttons(createTicket and comobox buttons)
        private void CheckTicketButtons(bool haveSolution)
        {
            cmbEPriority.Disabled = false;

            if (haveSolution)
            {
                btnECreateTicketSol.Disabled = false;
                btnECreateTicketNoSol.Disabled = true;
            }
            else
            {
                btnECreateTicketNoSol.Disabled = false;
                btnECreateTicketSol.Disabled = true;
            }
        }

        //fills category and template comboxes with data
        private void PopulateTemplateComboboxes()
        {
            
        }
        #endregion

       
    }
}