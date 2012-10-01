using System;
using System.Collections.Generic;
using System.Web.Security;
using CRMBusiness;
using CRMBusiness.CRM;
using Ext.Net;

namespace CRMUI.SupportAgent
{
    public partial class UpdateTicket : System.Web.UI.Page
    {
        private static bool _haveNewSolutions;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Direct Events

        //searched using ticket id
        protected void BtnCPRIdSearchClick(object sender, DirectEventArgs e)
        {
            try
            {
                var ticket = new ClientProblemLogBl().GetSolvedClientProblemById(Convert.ToInt32(txtSCPRId.Text));
                
                if(ticket != null)
                {
                    var lt = new List<vClientProblemsLog>() { ticket };
                    streTickets.DataSource = lt;
                    streTickets.DataBind();
                }
                else
                {
                    ExtNet.Msg.Alert("Not Found", "No ticket found with that ticket ID.").Show();
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //searched using client username
        protected void BtnUsernameSearchClick(object sender, DirectEventArgs e)
        {
            try
            {
                var tickets = new ClientProblemLogBl().GetClientProblemByUsername(txtSUsername.Text);
                
                if (tickets.Count > 0)
                {
                    streTickets.DataSource = tickets;
                    streTickets.DataBind();
                }
                else
                {
                    ExtNet.Msg.Alert("Not Found", "No tickets found for that username.").Show();
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //searched using client name
        protected void BtnNameSearchClick(object sender, DirectEventArgs e)
        {
            try
            {
                var tickets = new ClientProblemLogBl().GetClientProblemByClientName(txtSName.Text);
                
                if (tickets.Count > 0)
                {
                    streTickets.DataSource = tickets;
                    streTickets.DataBind();
                }
                else
                {
                    ExtNet.Msg.Alert("Not Found", "No tickets found for that username.").Show();
                }
                
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //selected a ticket
        protected void GpTicketSelected(object sender, DirectEventArgs e)
        {
            try
            {
                streSolutions.RemoveAll();
                var cprid = Convert.ToInt32(e.ExtraParams["cprid"]);
                var cpl = new ClientProblemLogBl().GetClientProblem(cprid);

                
                if (cpl != null)
                {
                    //view notes
                    var notes = new NoteBl().GetNotes(cprid);
                    if (notes.Count > 0)
                    {
                        streNotes.DataSource = notes;
                        streNotes.DataBind();
                    }

                    //view new solutions
                    var solutions = new SolutionBl().GetSolutions(cpl.PROB_ID);
                    if(solutions.Count > 0)
                    {
                        for (int i = 0; i < solutions.Count; i++)
                        {
                            if(solutions[i].SOL_ID == cpl.SOL_ID)
                            {
                                solutions.RemoveAt(i);
                            }
                        }
                        if(solutions.Count > 0)
                        {
                            _haveNewSolutions = true;
                            streSolutions.DataSource = solutions;
                            streSolutions.DataBind();
                        }
                    }

                    //view ticket details
                    hCprId.Value = cprid;
                    lblProbDesc.Text = cpl.ProblemDescription;
                    lblOSolDesc.Html = cpl.SolutionDescription;
                    heNote.Value = cpl.SolutionDescription;

                    //set details for sending of email
                    hEClientId.Value = cpl.CLIENT_ID;
                    hEProbDesc.Value = cpl.ProblemDescription;
                    hESolDesc.Value = cpl.SolutionDescription;
                }
                
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //selected a note
        protected void GpNotesSelected(object sender, DirectEventArgs e)
        {
            try
            {
                var noteid = Convert.ToInt32(e.ExtraParams["noteid"]);
                var empid = Convert.ToInt32(e.ExtraParams["empid"]);
                var note = new NoteBl().GetNoteById(noteid);
                
                if (note != null)
                {
                    //get employee who added the note
                    var emp = new EmployeeBl().GetEmployeeById(empid);

                    //view note details
                    lblEmployeeNameOfNote.Html = "Added by: " + emp.Name + " " + emp.Surname + "<br/>";
                    lblNote.Html = note.Description;
                }

                
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //selected a new solution
        protected void GpSolutionSelected(object sender, DirectEventArgs e)
        {
            try
            {
                var solid = Convert.ToInt32(e.ExtraParams["solid"]);
                var solution = new SolutionBl().GetSolutionBySolId(solid);

                if (solution != null)
                {
                    //view new solution details
                    hNSolId.Value = solid;
                    lblNSolDesc.Html = solution.Description;
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //updated the ticket
        protected void BtnUpdateTicketClick(object sender, DirectEventArgs e)
        {
            try
            {
                //if no new solution was selected
                if(hNSolId.Value.ToString() == "")
                {
                    //check if we have new solutions
                    if (_haveNewSolutions)
                    {
                        ExtNet.Msg.Alert("No solution selected", "Please select a new solution to update this ticket").Show();
                    }
                    else
                    {
                        var cprid = Convert.ToInt32(hCprId.Value);
                        var emp = new EmployeeBl().GetEmployee(Membership.GetUser().UserName);
                        var objCpl = new ClientProblemLogBl();
                        objCpl.UpdateClientProblem(cprid, false, null, emp.EMP_ID, null);
                        ExtNet.Msg.Notify("Ticket Updated", "Ticket has been updated as unsolved and can be viewed under 'Solve Ticket'").Show();
                        var note = new NoteBl();
                        note.AddNote(heNote.Value.ToString(), DateTime.Now, cprid, emp.EMP_ID);

                        wndSendEmail.Show();
                    }
                }
                //if new solution was selected
                else
                {
                    var cprid = Convert.ToInt32(hCprId.Value);
                    var solid = Convert.ToInt32(hNSolId.Value);
                    var emp = new EmployeeBl().GetEmployee(Membership.GetUser().UserName);
                    var objCpl = new ClientProblemLogBl();
                    objCpl.UpdateClientProblem(cprid, true, DateTime.Now, emp.EMP_ID, solid);
                    ExtNet.Msg.Notify("Ticket Updated", "Ticket has been updated with the selected new solution").Show();
                    var note = new NoteBl();
                    note.AddNote(heNote.Value.ToString(), DateTime.Now, cprid, emp.EMP_ID);

                    wndSendEmail.Show();
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
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
            var client = new ClientBl().GetClientByClientId(Convert.ToInt32(hEClientId.Value));
            heEmailBody.Value = "Hi " + client.Name + " " + client.Surname + "<br/><br/>";
            heEmailBody.Value += cmbTemplate.SelectedItem.Value + "<br/><br/>";
            heEmailBody.Value += "<b>Problem Description:</b><br/><br/>" + hEProbDesc.Value + "<br/><br/>";
            if(_haveNewSolutions)
            {
                heEmailBody.Value += "<b>New Solution Description:</b><br/><br/>" + hESolDesc.Value;
            }
        }

        protected void WndSendEmailShow(object sender, DirectEventArgs e)
        {
            //setup send email window
            var cats = new CategoriesBl().GetAllCategories();
            streCategories.DataSource = cats;
            streCategories.DataBind();

            var client = new ClientBl().GetClientByClientId(Convert.ToInt32(hEClientId.Value));
            heEmailBody.Value = "Hi " + client.Name + " " + client.Surname + "<br/><br/>";
            heEmailBody.Value += "<b>Problem Description:</b><br/><br/>" + hEProbDesc.Value + "<br/><br/>";
            if (_haveNewSolutions)
            {
                heEmailBody.Value += "<b>New Solution Description:</b><br/><br/>" + hESolDesc.Value;
            }
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
                    var objE = new EmailBl
                                   {
                                       Subject = txtSubject.Text,
                                       Body = heEmailBody.Value.ToString(),
                                       IsHtml = true,
                                       To = new ClientBl().GetClientByClientId(Convert.ToInt32(hEClientId.Value)).Email
                                   };
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
    }
}