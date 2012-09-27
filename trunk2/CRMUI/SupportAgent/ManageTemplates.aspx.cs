using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMBusiness;
using Ext.Net;

namespace CRMUI.SupportAgent
{
    public partial class ManageTemplates : System.Web.UI.Page
    {
        private static bool newcategory = false;
        private static bool newtemplate = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            var cats = new CategoriesBl().GetAllCategories();

            if (!IsPostBack)
            {
                strCategory.DataSource = cats;
                strCategory.DataBind();
            }

        }

        #region DIRECT EVENTS
        #region COMBOBOXES SELECT EVENTS

        protected void SelectedCategory(object sender, DirectEventArgs e)
        {
            var comms = new ComTemplateBl().GetAllTemplates(Convert.ToInt32(cmbTemplateCategory.SelectedItem.Value));
            streComTemplate.DataSource = comms;
            streComTemplate.DataBind();

            cmbComTemplates.Text = "Select a template";
            btnCreateCatName.Disabled = true;
            cmbComTemplates.Disabled = false;
            btnCreateTempName.Disabled = false;
        }

        protected void SelectedTemplate(object sender, DirectEventArgs e)
        {
            btnCreateTempName.Disabled = true;
            cmbTemplateCategory.Disabled = true;
            btnCreateCatName.Disabled = true;
            var temp = new ComTemplateBl().GetTemplateById(Convert.ToInt32(cmbComTemplates.SelectedItem.Value));
            editrPara.Value = temp.Paragraph;
        }
        #endregion

        #region BUTTONS TO CREATE NEW NAMES
        //BUTTON CREATE CATEGORY
        protected void CreateCategoryName(object sender, DirectEventArgs e)
        {
            cmbTemplateCategory.Disabled = true;
            cmbComTemplates.Disabled = false;
            btnCreateTempName.Disabled = false;

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
            cmbComTemplates.Disabled = true;
            cmbTemplateCategory.Disabled = true;
            btnCreateCatName.Disabled = true;
            txtTemplateName.Hidden = false;
            newtemplate = true;
        }
        #endregion

        //SAVE BUTTONS
        protected void BtnSaveClicked(object sender, DirectEventArgs e)
        {
            if (editrPara.Text == string.Empty)
            {
                ExtNet.Msg.Alert("Invalid Paragrapgh", "A category, templatename and paragrapgh is reqiured for a template!").Show();
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
                    }
                    else
                    {
                        //updatetemplate
                        UpdateTemplate(Convert.ToInt32(cmbComTemplates.SelectedItem.Value), editrPara.Value.ToString(), catid + 1);
                    }
                }
                else
                {
                    var cat = new CategoriesBl();
                    var catid = cat.GetAllCategories().Count;


                    if (newtemplate)
                    {
                        //addtemplate(ctid)
                        AddTemplate(txtTemplateName.Value.ToString(), editrPara.Value.ToString(), catid);
                    }
                    else
                    {
                        //upadate template(cmbtempindex,ctid)
                        UpdateTemplate(Convert.ToInt32(cmbComTemplates.SelectedItem.Value), editrPara.Value.ToString(), catid);
                    }
                }

                newtemplate = false;
                newcategory = false;

                Resetcontrols();

                ExtNet.MessageBox.Notify("Templates Changes", "Changes Saved").Show();
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
                ExtNet.Msg.Alert("Error", ex.Message).Show();
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
        }

        #endregion
    }
}