using System;
using System.Globalization;
using System.Web;
using Ext.Net;
using CRMBusiness;

namespace CRMUI.CallCentreManager
{
    public partial class CreateTemplate : System.Web.UI.Page
    {
        private static bool newcategory;
        private static bool newtemplate;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            var cats = new CategoriesBl().GetAllCategories();

            if (!IsPostBack)
            {
                strCategory.DataSource = cats;
                strCategory.DataBind();
            }

        }


        #region COMBOBOXES SELECT EVENTS

        protected void SelectedCategory(object sender, DirectEventArgs e)
        {
            btnCreateCatName.Disabled = true;
            cmbComTemplates.Disabled = false;
            btnCreateTempName.Disabled = false;
            cmbTemplateCategory.Disabled = true;
            cmbComTemplates.Text = "Select a template";
            

            var catid = Convert.ToInt32(cmbTemplateCategory.SelectedItem.Value);
            var comms = new ComTemplateBl().GetAllTemplates(catid);
            streComTemplate.DataSource = comms;
            streComTemplate.DataBind();
        }


        protected void SelectedTemplate(object sender, DirectEventArgs e)
        {
            if (txtCatName.Text == string.Empty)
            {
                txtCatName.Disabled = false;
                txtCatName.AutoFocus = true;
                ExtNet.Msg.Alert("Invalid Data", "Please provide a category name then reselect or create a template!").Show();
            }
            else
            {
                txtCatName.Disabled = true;
                editrPara.Disabled = false;
                var tempid = Convert.ToInt32(cmbComTemplates.SelectedItem.Value);
                btnCreateTempName.Disabled = true;
                cmbTemplateCategory.Disabled = true;
                cmbComTemplates.Disabled = true;
                btnCreateCatName.Disabled = true;
                var temp = new ComTemplateBl().GetTemplateById(tempid);
                editrPara.Value = temp.Paragraph;
            }
        }
        #endregion

        #region DIRECT EVENTS


        #region BUTTONS TO CREATE NEW NAMES
        //BUTTON CREATE CATEGORY
        protected void CreateCategoryName(object sender, DirectEventArgs e)
        {
            cmbTemplateCategory.Disabled = true;
            btnCreateCatName.Disabled = true;
            
            var comms = new ComTemplateBl().GetAllTemplates();
            streComTemplate.DataSource = comms;
            streComTemplate.DataBind();
            cmbComTemplates.Text = "Select a template";
            txtCatName.Hidden = false;
            newcategory = true;
        }

        //BUTTON CREATE TEMPLATE
        protected void CreateTemplateName(object sender, DirectEventArgs e)
        {
            if (txtCatName.Text == string.Empty)
            {
                txtCatName.Disabled = false;
                txtCatName.AutoFocus = true;
                ExtNet.Msg.Alert("Invalid Data", "Please provide a category name then reselect or create a template!").Show();
            }
            else
            {
                txtCatName.Disabled = true;
                cmbComTemplates.Disabled = true;
                cmbTemplateCategory.Disabled = true;
                btnCreateCatName.Disabled = true;
                txtTemplateName.Hidden = false;
                newtemplate = true;
            }
        }
        #endregion

        //SAVE BUTTONS
        protected void BtnSaveClicked(object sender, DirectEventArgs e)
        {
            if (editrPara.Text == string.Empty)
            {
                ExtNet.Msg.Alert("Invalid Data", "A category, Template Name and Paragraph is reqiured for a template!").Show();
                editrPara.AutoFocus = true;
            }
            else if (txtTemplateName.Text==string.Empty)
            {
                ExtNet.Msg.Alert("Invalid Data", "A category, Template Name and Paragraph is reqiured for a template!").Show();
                txtTemplateName.AutoFocus = true;
            }
            else if (editrPara.Text.Length<1)
            {
                ExtNet.Msg.Alert("Invalid Data", "A category, Template Name and Paragraph is reqiured for a template!").Show();
                editrPara.AutoFocus = true;
            }
            else
            {
                if (newcategory)
                {
                    var cat = new CategoriesBl();
                    var catid = cat.GetAllCategories().Count;
                    cat.AddCategory(txtCatName.Value.ToString());

                    if (newtemplate)
                    {
                        //addtemplate(ctid)
                        AddTemplate(txtTemplateName.Value.ToString(), editrPara.Value.ToString(), catid + 1);
                        ExtNet.MessageBox.Notify("Templates Changes", "Template added with new category").Show();
                    }
                    else
                    {
                        //updatetemplate
                        UpdateTemplate(Convert.ToInt32(cmbComTemplates.SelectedItem.Value), editrPara.Value.ToString(), catid + 1);
                        ExtNet.MessageBox.Notify("Templates Changes", "Template updated with new category").Show();
                    }
                }
                else
                {
                    var catid = Convert.ToInt32(cmbTemplateCategory.SelectedItem.Value);

                    if (newtemplate)
                    {
                        //addtemplate(ctid)
                        AddTemplate(txtTemplateName.Value.ToString(), editrPara.Value.ToString(), catid);
                        ExtNet.MessageBox.Notify("Templates Changes", "Template added to existing category").Show();
                    }
                    else
                    {
                        //upadate template(cmbtempindex,ctid)
                        UpdateTemplate(Convert.ToInt32(cmbComTemplates.SelectedItem.Value), editrPara.Value.ToString(), catid);
                        ExtNet.MessageBox.Notify("Templates Changes", "Template updated with existing category").Show();
                    }
                }
                newtemplate = false;
                newcategory = false;
                Resetcontrols();
            }
        }
        //CANCEL
        protected void BtnCancelClicked(object sender, DirectEventArgs e)
        {
            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }

        #endregion

        #region HELPER METHODS
        //ADD TEMPLATE
        protected void AddTemplate(string nwname, string nwparagraph, int catid)
        {
            try
            {
                new ComTemplateBl().AddTemplate(nwname, nwparagraph, catid);
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Error", ex.Message).Show();
            }
        }

        //UPDATE TEMPLATE
        protected void UpdateTemplate(int tempid, string paragraph, int catid)
        {
            try
            {
                new ComTemplateBl().UpdateTemplate(tempid, paragraph, catid);
            }
            catch (Exception ex)
            {
                ExtNet.Msg.Alert("Update Error", ex.Message).Show();
            }
        }

        //RESET CONTROLS
        protected void Resetcontrols()
        {
            cmbTemplateCategory.Reset();
            txtCatName.Reset();
            txtCatName.Hidden = true;
            cmbComTemplates.Reset();
            cmbComTemplates.Disabled = true;
            txtTemplateName.Reset();
            txtTemplateName.Hidden = true;
            btnCreateTempName.Disabled = true;
            editrPara.Reset();
            editrPara.ReadOnly = true;
        }

        #endregion

        protected void TxtTemplateNameEntered(object sender, EventArgs e)
        {
            editrPara.Disabled = false;
        }

        protected void TxtCategoryNameChanged(object sender, DirectEventArgs e)
        {
            if (txtCatName.Text.Length > 0)
            {
                cmbComTemplates.Disabled = false;
                btnCreateTempName.Disabled = false;
            }
        }
    }
}