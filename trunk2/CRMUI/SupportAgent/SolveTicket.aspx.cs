using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.SupportAgent
{
    public partial class SolveTicket : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var unsolvedTickets = new ClientProblemLogBl().GetUnsolvedProblems();
            streTickets.DataSource = unsolvedTickets;
            streTickets.DataBind();
        }

        protected void GpTicketSelected(object sender, DirectEventArgs e)
        {
            try
            {
                var cprid = Convert.ToInt32(e.ExtraParams["cprid"]);
                var cpl = new ClientProblemLogBl().GetClientProblem(cprid);
                if (cpl != null)
                {
                    hCPRId.Value = cpl.CPR_ID;
                    hClientId.Value = cpl.CLIENT_ID;
                    hProbId.Value = cpl.PROB_ID;
                    lblProbDesc.Text = cpl.ProblemDescription;
                    hEProbDesc.Value = cpl.ProblemDescription;

                    //show client contact info
                    var client = new ClientBl().GetClientByClientId(Convert.ToInt32(hClientId.Value));
                    lblClientName.Html = "Client Name: " + client.Name + " " + client.Surname + "<br/><br/>";
                    lblTelephone.Html = "Client Telephone: " + client.Telephone + "<br/><br/>";
                    lblCell.Html = "Client Cell: " + client.Cell + "<br/><br/>";
                    lblFax.Html = "Client Fax: " + client.Fax;

                    //disable/enable view images button
                    var imageCount = new ImageBl().GetImageCountForTicket(Convert.ToInt32(hCPRId.Value));
                    btnViewImages.Disabled = imageCount <= 0;

                    //enable solve button
                    btnSolveTicket.Disabled = false;
                }
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        protected void BtnSolveTicketClick(object sender, DirectEventArgs e)
        {
            try
            {
                if (heSolutionDesc.Value.ToString() != "")
                {
                    //get employee details
                    var username = Membership.GetUser().UserName;
                    var emp = new EmployeeBl().GetEmployee(username);

                    //save solution to database
                    var objSol = new SolutionBl();
                    objSol.AddSolution(heSolutionDesc.Value.ToString(), DateTime.Now, Convert.ToInt32(hProbId.Value),
                                       emp.EMP_ID);

                    //get the solution id of created solution
                    var solid = objSol.GetLastSolId();

                    //update the ticket to have the new solution
                    var objCpl = new ClientProblemLogBl();
                    var cprid = Convert.ToInt32(hCPRId.Value);
                    objCpl.UpdateClientProblem(cprid, true, DateTime.Now, emp.EMP_ID, solid);
                    hECprId.Value = cprid;
                    hEClientId.Value = hClientId.Value;
                    wndSendEmail.Show();

                    //check for other unsolved tickets with same problem and solve them automatically
                    //---------------------------

                    //get problem id of previously updated ticket
                    var probId = new ClientProblemLogBl().GetClientProblem(cprid).PROB_ID;

                    //get unsolved tickets with same problem
                    var unsolvedTickets = new ClientProblemLogBl().GetClientProblemsByProbId(probId);

                    foreach (var ticket in unsolvedTickets)
                    {
                        if (ticket.CPR_ID != cprid)
                        {
                            //update the ticket with new solution
                            objCpl.UpdateClientProblem(ticket.CPR_ID, true, DateTime.Now, emp.EMP_ID, solid);
                            //send email to client that ticket belongs to
                            //---------

                            //get client details
                            var client = new ClientBl().GetClientByClientId(ticket.CLIENT_ID);
                            //create the body to use in body of email
                            var sb = new StringBuilder();
                            sb.Append("Hi " + client.Name + " " + client.Surname + "<br/>Your Ticket number is: " +
                                      ticket.CPR_ID + "<br/><br/>");
                            sb.Append("We have a solution to your problem please see below. Thank You");
                            sb.Append("<br/><br/><b>Problem:</b><br/><br/>");
                            sb.Append(ticket.ProblemDescription);
                            sb.Append("<br/><br/><b>Solution:<br/><br/>");
                            sb.Append(heSolutionDesc.Value.ToString());

                            //send email
                            var email = new EmailBl
                                            {
                                                To = client.Email,
                                                Bcc = new List<string> {emp.Email},
                                                Subject = "LawProperty Customer Support",
                                                Body = sb.ToString(),
                                                IsHtml = true
                                            };
                            var message = "Ticket ID: " + ticket.CPR_ID + "<br/>has also been solved. ";
                            if (email.SendEmail())
                            {
                                ExtNet.Mask.Hide();
                                ExtNet.Msg.Notify("Auto Ticket Update", message).Show();
                            }
                            else
                            {
                                ExtNet.Mask.Hide();
                                ExtNet.Msg.Notify("Auto Send Email Failed", "Client: " +
                                                                            client.Name + " " + client.Surname + " - " +
                                                                            email.Error).Show();
                            }
                            ExtNet.Mask.Hide();
                        }

                    }
                    ExtNet.Mask.Hide();
                }
                else
                {
                    ExtNet.Mask.Hide();
                    ExtNet.Msg.Alert("No Solution Entered", "Please enter a solution then click solve.").Show();
                    heSolutionDesc.Focus();
                }
            }
            catch (Exception ex)
            {
                ExtNet.Mask.Hide();
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

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
            try
            {
                var client = new ClientBl().GetClientByClientId(Convert.ToInt32(hEClientId.Value));
                var ctid = Convert.ToInt32(cmbTemplate.SelectedItem.Value);
                var template = new ComTemplateBl().GetTemplateById(ctid);
                heEmailBody.Value = "Hi " + client.Name + " " + client.Surname + "<br/>Your Ticket number is: " +
                                       (hECprId.Value + "<br/><br/>");
                heEmailBody.Value += template.Paragraph;
                heEmailBody.Value += "<br/><br/><b>Problem:</b><br/><br/>" + hEProbDesc.Value;
                heEmailBody.Value += "<br/><br/><b>Solution Details:</b><br/><br/>" + heSolutionDesc.Value;
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        protected void WndSendEmailShow(object sender, DirectEventArgs e)
        {
            try
            {
                //setup send email window
                var cats = new CategoriesBl().GetAllCategories();
                streCategories.DataSource = cats;
                streCategories.DataBind();

                var client = new ClientBl().GetClientByClientId(Convert.ToInt32(hEClientId.Value));
                heEmailBody.Value = "Hi " + client.Name + " " + client.Surname + "<br/>Your Ticket number is: " +
                                       (hECprId.Value + "<br/><br/>");
                heEmailBody.Value += "<br/><br/><b>Problem:</b><br/><br/>" + hEProbDesc.Value;
                heEmailBody.Value += "<br/><br/><b>Solution Details:</b><br/>" + heSolutionDesc.Value;
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        protected void WndSendEmailClose(object sender, DirectEventArgs e)
        {
            cmbCategory.Text = "Choose a Category..";
            cmbTemplate.Text = "Choose a Template..";
            cmbTemplate.Disabled = true;
            btnSolveTicket.Disabled = true;
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

        protected void BtnViewImagesClick(object sender, DirectEventArgs e)
        {
            try
            {
                var listOfImages = new ImageBl().GetImagesForTicket(Convert.ToInt32(hCPRId.Value));
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
    }
}