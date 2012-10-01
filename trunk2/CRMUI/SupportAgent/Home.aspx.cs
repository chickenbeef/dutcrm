using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using CRMBusiness;
using CRMBusiness.CRM;
using Ext.Net;
using Image = Ext.Net.Image;

namespace CRMUI.SupportAgent
{
    public partial class CallHome : Page
    {
        public static string Role;
        private static bool _haveProblem;
        private static bool _haveSolution;
        private static int _clientid;

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
        }

        #region Direct Methods EMAIL PANEL

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

        #region Direct Events EMAIL PANEL

        //selected an email on top left panel (email inbox)
        protected void EmailProblemSelected(object sender, DirectEventArgs e)
        {
            //get details of selected email
            var epid = Convert.ToInt32(e.ExtraParams["epid"]);
            var ep = new EmailProblemBl().GetEmailProblemById(epid);

            //show details of email
            hEPId.Value = ep.EP_ID;
            hEClientId.Value = ep.CLIENT_ID;
            lblSubject.Html = "<b>" + ep.Subject + "</b><hr/>";
            lblFrom.Html = "<table style=width:100%><tr><td style=width:80%>Sent From: " + ep.From +
                           "</td><td style=align:right;>" + ep.DateCreated + "</td></tr></table><hr/>";
            lblEmailDesc.Html = "<br/>" + ep.Description + "<br/><br/>";
            lblEmailDesc.Disabled = false;
            lblEmailDesc.Visible = true;

            //if the email problem has images attached
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
                if (_haveSolution)
                {
                    //get selected row details
                    var solid = Convert.ToInt32(e.ExtraParams["solid"]);
                    //convert to solution object
                    var solRow = new SolutionBl().GetSolutionBySolId(solid);

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
                        var probid = Convert.ToInt32(e.ExtraParams["probid"]);
                        //convert it to problem object
                        var probRow = new ProblemBl().GetProblem(probid);
                        //set details to allow creation of ticket with no solution
                        if (probRow != null)
                        {
                            CheckTicketButtons(_haveSolution);
                            hEProbId.Value = probRow.PROB_ID;
                            lblEProbDesc.Text = probRow.Description;
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
                        ExtNet.Msg.Alert("No Client Details", "Please select an email problem to get the client that this ticket should be created for.").Show();
                    }
                    else
                    {
                        //get details of currently logged in employee
                        var username = Membership.GetUser().UserName;
                        var emp = new EmployeeBl().GetEmployee(username);
                        var cid = Convert.ToInt32(hEClientId.Value);
                        var epid = Convert.ToInt32(hEPId.Value);
                        var solid = Convert.ToInt32(hESolId.Value);
                        var probid = Convert.ToInt32(hEProbId.Value);
                        var date = DateTime.Now.Date;
                        var priority = cmbEPriority.SelectedItem.Value.ToString(CultureInfo.InvariantCulture);

                        //get details of client
                        var client = new ClientBl().GetClientByClientId(cid);

                        //set global variable to send email to correct client
                        _clientid = cid;

                        //create the ticket
                        var cpl = new ClientProblemLogBl();
                        cpl.AddClientProblem(true, date, date, false, cid, emp.EMP_ID, probid, solid, priority);

                        //get ticket number
                        var cprid = cpl.GetLastCprId();

                        //update the email problem to be attended
                        var ep = new EmailProblemBl();
                        ep.UpdateEmailProblem(epid, true, emp.EMP_ID);

                        //get any images attached to email and assign to the ticket
                        var objImg = new ImageBl();
                        var images = objImg.GetImages(epid);
                        if (images.Count > 0)
                        {
                            foreach (var image in images)
                            {
                                objImg.UpdateImage(image.IMG_ID, cprid);
                            }
                        }

                        ExtNet.Msg.Notify("Ticket Created", "The ticket has been created with a solution").Show();

                        var message = "Hi " + client.Name + " " + client.Surname + "<br/>Your Ticket number is: " +
                                       (cprid + "<br/><br/>");
                        wndSendEmail.Show();
                        heEmailBody.Disabled = false;
                        heEmailBody.Value = message;

                        //disable buttons
                        cmbEPriority.Disabled = true;
                        btnECreateTicketNoSol.Disabled = true;
                        btnECreateTicketSol.Disabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //clicked create a ticket without solution
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
                        ExtNet.Msg.Alert("No Client Details", "Please select an email problem to get the client that this ticket should be created for.").Show();
                    }
                    else
                    {
                        //get details of currently logged in employee
                        var username = Membership.GetUser().UserName;
                        var emp = new EmployeeBl().GetEmployee(username);
                        var cid = Convert.ToInt32(hEClientId.Value);
                        var epid = Convert.ToInt32(hEPId.Value);
                        var probid = Convert.ToInt32(hEProbId.Value);
                        var date = DateTime.Now.Date;
                        var priority = cmbEPriority.SelectedItem.Value.ToString(CultureInfo.InvariantCulture);

                        //get details of client
                        var client = new ClientBl().GetClientByClientId(cid);

                        //set global variable to send email to correct client
                        _clientid = cid;

                        var cpl = new ClientProblemLogBl();
                        cpl.AddClientProblem(false, date, null, false, cid, emp.EMP_ID, probid, null, priority);
                        ExtNet.Msg.Notify("Ticket Created", "The ticket has been created Without a solution").Show();

                        //get ticket number
                        var cprid = cpl.GetLastCprId();

                        //update the email problem to be attended
                        var ep = new EmailProblemBl();
                        ep.UpdateEmailProblem(epid, true, emp.EMP_ID);

                        //get any images attached to email and assign to the ticket
                        var objImg = new ImageBl();
                        var images = objImg.GetImages(epid);
                        if (images.Count > 0)
                        {
                            foreach (var image in images)
                            {
                                objImg.UpdateImage(image.IMG_ID, cprid);
                            }
                        }

                        var message = "Hi " + client.Name + " " + client.Surname + "<br/>Your Ticket number is: " +
                                       (cprid + "<br/><br/>");
                        wndSendEmail.Show();
                        heEmailBody.Disabled = false;
                        heEmailBody.Value = message;

                        //disable buttons
                        cmbEPriority.Disabled = true;
                        btnECreateTicketNoSol.Disabled = true;
                        btnECreateTicketSol.Disabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }


        #endregion

        #region Direct Events CALL SUPPORT PANEL

        //search client by username
        protected void BtnUsernameSearchClick(object sender, DirectEventArgs e)
        {
            var client = new ClientBl().GetClientUserName(txtSUsername.Text);
            if (client.Count > 0)
            {
                streClient.DataSource = client;
                streClient.DataBind();
            }
            else
            {
                ExtNet.Msg.Alert("No client found", "Could not find a client with that username").Show();
            }
        }

        //search client by name
        protected void BtnNameSearchClick(object sender, DirectEventArgs e)
        {
            var clients = new ClientBl().GetClientName(txtSName.Text);
            if (clients.Count > 0)
            {
                streClient.DataSource = clients;
                streClient.DataBind();
            }
            else
            {
                ExtNet.Msg.Alert("No client found", "Could not find a client with that name").Show();
            }
        }

        //Client selected on grid panel
        protected void GpClientSelected(object sender, DirectEventArgs e)
        {
            hCClientId.Value = e.ExtraParams["clientid"];
        }

        //searched for solution
        protected void BtnCSearchSolClick(object sender, DirectEventArgs e)
        {
            try
            {
                cmbCPriority.Disabled = true;
                btnCCreateTicketNoSol.Disabled = true;
                btnCCreateTicketSol.Disabled = true;
                var probs = new ProblemBl().GetProblems(txtCProbDesc.Text);
                //no problem found
                if (probs.Count <= 0)
                {
                    _haveProblem = false;
                    _haveSolution = false;
                    taCProbDesc.Text = txtCProbDesc.Text;
                    wndCAddProblem.Show();
                    streCSolutions.Reload();
                }
                //found 1 problem
                else if (probs.Count == 1)
                {
                    _haveProblem = true;
                    var sols = new SolutionBl().GetSolutions(probs[0].PROB_ID);
                    if (sols.Count > 0)
                    {
                        _haveSolution = true;
                        clmCModified.Show();
                        clmCSolId.Show();
                        clmCSolution.Text = "Solution Description";
                        streCSolutions.DataSource = sols;
                        streCSolutions.DataBind();
                    }
                    else
                    {
                        _haveSolution = false;
                        hCProbId.Value = probs[0].PROB_ID;
                        lblCProbDesc.Text = probs[0].Description;
                        fsCProbDesc.Visible = true;
                        CheckTicketButtonsCall(_haveSolution);
                        pnlCSolutionDetails.UpdateContent();
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
                        streCSolutions.DataSource = listOfSols;
                        streCSolutions.DataBind();
                    }
                    //if no solutions found for problems that exist in database
                    else
                    {
                        _haveSolution = false;
                        ExtNet.Msg.Alert("No solution found", "More than 1 problem was found but no solution exists for them. please create a ticket without solution").Show();
                        clmCModified.Hide();
                        clmCSolId.Hide();
                        clmCSolution.Text = "Problem Description";
                        mdlCSolutions.Fields.RemoveAt(0);
                        streCSolutions.DataSource = probs;
                        streCSolutions.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //selected solution/problem in gridpanel
        protected void GpCSolutionSelected(object sender, DirectEventArgs e)
        {
            try
            {
                if (_haveSolution)
                {
                    //get selected row details
                    var solid = Convert.ToInt32(e.ExtraParams["solid"]);
                    var solRow = new SolutionBl().GetSolutionBySolId(solid);

                    //set employee details
                    if (solRow != null)
                    {
                        var emp = new EmployeeBl().GetEmployeeById(solRow.EMP_ID);

                        //get problem description
                        var prob = new ProblemBl().GetProblem(solRow.PROB_ID);

                        //set solution details
                        hCSolId.Value = solRow.SOL_ID;
                        hCProbId.Value = solRow.PROB_ID;
                        if (solRow.DateModified != null)
                        {
                            lblCEmployeeFullName.Text = "Modified By: " + emp.Name + " " + emp.Surname;
                            lblCDateModified.Text = "Date Modified: " + solRow.DateModified.Value.ToString("dd MMMM yyyy");
                        }
                        else
                        {
                            lblCEmployeeFullName.Text = "Created By: " + emp.Name + " " + emp.Surname;
                            lblCDateCreated.Text = "Date Created: " + solRow.DateCreated.ToString("dd MMMM yyyy");
                        }
                        lblCSpacer.Html = "<br/><hr/><br/>";
                        lblCProbDesc.Text = prob.Description;
                        lblCSolDesc.Html = solRow.Description;
                        CheckTicketButtonsCall(_haveSolution);

                        //show controls
                        fsCProbDesc.Visible = true;
                        fsCSolDesc.Visible = true;
                    }
                }
                else
                {
                    if (_haveProblem)
                    {
                        //get selected row details
                        var probid = Convert.ToInt32(e.ExtraParams["probid"]);
                        var probRow = new ProblemBl().GetProblem(probid);
                        //set details to allow creation of ticket with no solution
                        if (probRow != null)
                        {
                            CheckTicketButtonsCall(_haveSolution);
                            hCProbId.Value = probRow.PROB_ID;
                            lblCProbDesc.Text = probRow.Description;
                            //show controls
                            fsCProbDesc.Visible = true;
                        }
                    }
                }

                //refresh to show details
                pnlCSolutionDetails.UpdateContent();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //create a ticket with solution
        protected void BtnCCreateTicketSolClick(object sender, DirectEventArgs e)
        {
            try
            {
                if (cmbCPriority.SelectedItem.Index == -1)
                {
                    ExtNet.Msg.Alert("No priority selected", "Please select a priority before creating a ticket.").Show();
                    cmbCPriority.Focus(true);
                }
                else
                {
                    //if no email problem has been selected
                    if (hCClientId.Value.ToString() == "")
                    {
                        ExtNet.Msg.Alert("No Client Details", "Please select a client whom you want to create a ticket for").Show();
                    }
                    else
                    {
                        //get details of currently logged in employee
                        var username = Membership.GetUser().UserName;
                        var emp = new EmployeeBl().GetEmployee(username);

                        //get details for ticket
                        var cid = Convert.ToInt32(hCClientId.Value);
                        var solid = Convert.ToInt32(hCSolId.Value);
                        var probid = Convert.ToInt32(hCProbId.Value);
                        var date = DateTime.Now.Date;
                        var priority = cmbCPriority.SelectedItem.Value.ToString(CultureInfo.InvariantCulture);

                        //get details of client
                        var client = new ClientBl().GetClientByClientId(cid);

                        //set global variable to send email to correct client
                        _clientid = cid;

                        //create the ticket
                        var cpl = new ClientProblemLogBl();
                        cpl.AddClientProblem(true, date, date, true, cid, emp.EMP_ID, probid, solid, priority);

                        //get ticket number
                        var cprid = cpl.GetLastCprId();

                        ExtNet.Msg.Notify("Ticket Created", "The ticket has been created with a solution").Show();

                        var message = "Hi " + client.Name + " " + client.Surname + "<br/>Your Ticket number is: " +
                                       (cprid + "<br/><br/>");
                        wndSendEmail.Show();
                        heEmailBody.Disabled = false;
                        heEmailBody.Value = message;

                        //disable buttons
                        cmbEPriority.Disabled = true;
                        btnECreateTicketNoSol.Disabled = true;
                        btnECreateTicketSol.Disabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //create a ticket without solution
        protected void BtnCCreateTicketNoSol(object sender, DirectEventArgs e)
        {
            try
            {
                if (cmbCPriority.SelectedItem.Index == -1)
                {
                    ExtNet.Msg.Alert("No priority selected", "Please select a priority before creating a ticket.").Show();
                    cmbCPriority.Focus(true);
                }
                else
                {
                    //if no email problem has been selected
                    if (hCClientId.Value.ToString() == "")
                    {
                        ExtNet.Msg.Alert("No Client Details", "Please select a client whom you want to create a ticket for").Show();
                    }
                    else
                    {
                        //get details of currently logged in employee
                        var username = Membership.GetUser().UserName;
                        var emp = new EmployeeBl().GetEmployee(username);
                        var cid = Convert.ToInt32(hCClientId.Value);
                        var probid = Convert.ToInt32(hCProbId.Value);
                        var date = DateTime.Now.Date;
                        var priority = cmbCPriority.SelectedItem.Value.ToString(CultureInfo.InvariantCulture);

                        //get details of client
                        var client = new ClientBl().GetClientByClientId(cid);

                        //set global variable to send email to correct client
                        _clientid = cid;

                        var cpl = new ClientProblemLogBl();
                        cpl.AddClientProblem(false, date, null, true, cid, emp.EMP_ID, probid, null, priority);
                        ExtNet.Msg.Notify("Ticket Created", "The ticket has been created Without a solution").Show();

                        //get ticket number
                        var cprid = cpl.GetLastCprId();

                        var message = "Hi " + client.Name + " " + client.Surname + "<br/>Your Ticket number is: " +
                                       (cprid + "<br/><br/>");
                        wndSendEmail.Show();
                        heEmailBody.Disabled = false;
                        heEmailBody.Value = message;

                        //disable buttons
                        cmbCPriority.Disabled = true;
                        btnCCreateTicketNoSol.Disabled = true;
                        btnCCreateTicketSol.Disabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        #endregion

        #region Direct Events ADD PROBLEM WINDOW EMAIL
        //Add problem window events
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
        #endregion

        #region Direct Events ADD PROBLEM WINDOW CALL
        //Add problem window events
        protected void WndCAddProblemClosed(object sender, DirectEventArgs e)
        {
            taCProbDesc.Clear();
        }

        protected void BtnCAddProblemClick(object sender, DirectEventArgs e)
        {
            try
            {
                var username = Membership.GetUser().UserName;
                var empid = new EmployeeBl().GetEmployee(username).EMP_ID;
                var prob = new ProblemBl();
                var lastProbId = new ProblemBl().GetLastId();
                prob.AddProblem(taCProbDesc.Text, DateTime.Now, empid);
                hCProbId.Value = lastProbId + 1;
                lblCProbDesc.Text = taCProbDesc.Text;
                fsCProbDesc.Visible = true;
                pnlCSolutionDetails.UpdateContent();
                CheckTicketButtonsCall(_haveSolution);
                wndCAddProblem.Close();
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message);
            }
        }
        #endregion

        #region Direct Events SEND EMAIL WINDOW
        //send email window events
        protected void CmbCategorySelectedItem(object sender, DirectEventArgs e)
        {
            try
            {
                var catid = Convert.ToInt32(cmbCategory.SelectedItem.Value);
                var templates = new ComTemplateBl().GetAllTemplates(catid);
                cmbTemplate.Disabled = false;
                streTemplates.DataSource = templates;
                streTemplates.DataBind();
                cmbTemplate.Text = "Choose a category...";
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        protected void CmbTemplateSelectedItem(object sender, DirectEventArgs e)
        {
            heEmailBody.Value += cmbTemplate.SelectedItem.Value;
        }

        protected void WndSendEmailShow(object sender, DirectEventArgs e)
        {
            //setup send email window
            var cats = new CategoriesBl().GetAllCategories();
            streCategories.DataSource = cats;
            streCategories.DataBind();
        }

        protected void WndSendEmailClose(object sender, DirectEventArgs e)
        {
            cmbCategory.Text = "Choose a Category..";
            cmbTemplate.Text = "Choose a Template..";
            cmbTemplate.Disabled = true;
        }

        protected void BtnSendEmailClick(object sender, DirectEventArgs e)
        {
            try
            {
                if (heEmailBody.Value != null)
                {
                    var objE = new EmailBl();
                    objE.Subject = txtSubject.Text;
                    objE.Body = heEmailBody.Value.ToString();
                    objE.IsHtml = true;
                    objE.To = new ClientBl().GetClientByClientId(_clientid).Email;
                    objE.SendEmail();
                    ExtNet.Mask.Hide();
                    ExtNet.Msg.Notify("Email Sent", "Your Email has been sent").Show();
                    wndSendEmail.Close();
                }
                else
                {
                    ExtNet.Mask.Hide();
                    ExtNet.Msg.Alert("All fields not filled", "Please enter values for all fields").Show();
                }
            }
            catch (Exception ex)
            {
                ExtNet.Mask.Hide();
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
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

        //enables and disables bottombar buttons(createTicket and comobox buttons)
        //FOR CALL SUPPORT AGENT
        private void CheckTicketButtonsCall(bool haveSolution)
        {
            cmbCPriority.Disabled = false;

            if (haveSolution)
            {
                btnCCreateTicketSol.Disabled = false;
                btnCCreateTicketNoSol.Disabled = true;
            }
            else
            {
                btnCCreateTicketNoSol.Disabled = false;
                btnCCreateTicketSol.Disabled = true;
            }
        }
        #endregion
    }
}