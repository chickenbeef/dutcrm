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
        private static bool newcategory;
        private static bool newtemplate;
        protected void Page_Load(object sender, EventArgs e)
        {
            var cats = new CategoriesBl().GetAllCategories();
            strCategory.DataSource = cats;
            strCategory.DataBind();
        }

        //COMBOBOX SELECT CATEGORY
        protected void SelectedCategory(object sender, DirectEventArgs e)
        {
            cmbTemplateCategory.Disabled = true;
            btnCreateCatName.Disabled = true;
            cmbComTemplates.Disabled = false;
            btnCreateTempName.Disabled = false;
            cmbComTemplates.Text = "Select a template";

            var catid = Convert.ToInt32(cmbTemplateCategory.SelectedItem.Value);
            var comms = new ComTemplateBl().GetAllTemplates(catid);
            streComTemplate.DataSource = comms;
            streComTemplate.DataBind();
            newcategory = false;
        }

        //BUTTON CREATE CATEGORY
        protected void CreateCategoryName(object sender, DirectEventArgs e)
        {
            cmbTemplateCategory.Disabled = true;
            btnCreateCatName.Disabled = true;
            cmbComTemplates.Disabled = false;
            btnCreateTempName.Disabled = false;

            var comms = new ComTemplateBl().GetAllTemplates();
            streComTemplate.DataSource = comms;
            streComTemplate.DataBind();
            cmbComTemplates.Text = "Select a template";
            txtCatName.Hidden = false;
            txtCatName.Disabled = false;
            newcategory = true;
        }

        //COMBOBOX SELECT TEMPLATE
        protected void SelectedTemplate(object sender, DirectEventArgs e)
        {
            //validatenewtemp = false;
            newtemplate = false;
            if (newcategory)
            {
                //VALIDATE NEW CATEGORY DATA
                if (txtCatName.Text == string.Empty)
                {
                    txtCatName.Disabled = false;
                    ExtNet.Msg.Alert("Invalid Data", "Please provide a category name then reselect or create a template!").Show();
                    txtCatName.AutoFocus = true;
                }
                else
                {
                    LoadEditorParagrapgh();
                }
            }
            else
            {
                LoadEditorParagrapgh();
            }
        }

        //DISABLE ALL CONTROLS AND POPULATE THE HTML EDITOR WITH SELECTED TEMPLATE BODY
        protected void LoadEditorParagrapgh()
        {
            var tempid = Convert.ToInt32(cmbComTemplates.SelectedItem.Value);
            txtCatName.Disabled = true;
            editrPara.Disabled = false;
            btnCreateTempName.Disabled = true;
            cmbTemplateCategory.Disabled = true;
            cmbComTemplates.Disabled = true;
            btnCreateCatName.Disabled = true;
            var temp = new ComTemplateBl().GetTemplateById(tempid);
            editrPara.Value = temp.Paragraph;
            BtnSave.Disabled = false;
        }
        //BUTTON CREATE TEMPLATE
        protected void CreateTemplateName(object sender, DirectEventArgs e)
        {
            if (newcategory)
            {
                if (txtCatName.Text == string.Empty)
                {
                    txtCatName.Disabled = false;
                    txtCatName.AutoFocus = true;
                    ExtNet.Msg.Alert("Invalid Data", "Please provide a category name then reselect or create a template!").Show();
                }
                else
                {
                    EnableHtmlEditorForNewTemp();
                }
            }
            else
            {
                EnableHtmlEditorForNewTemp();
            }
        }

        //ENABLE HTMLEDITOR FOR NEW TEMPLATE BODY
        protected void EnableHtmlEditorForNewTemp()
        {
            txtCatName.Disabled = true;
            cmbComTemplates.Disabled = true;
            cmbTemplateCategory.Disabled = true;
            btnCreateCatName.Disabled = true;
            txtTemplateName.Hidden = false;
            newtemplate = true;
            btnCreateTempName.Disabled = true;
            editrPara.Disabled = false;
        }


        #region DIRECT EVENTS

        //SAVE BUTTONS
        protected void BtnSaveClicked(object sender, DirectEventArgs e)
        {
            if (editrPara.Text == string.Empty)
            {
                ExtNet.Msg.Alert("Invalid Paragraph", "A paragraph is reqiured for a template, please enter some text!").Show();
                editrPara.AutoFocus = true;
            }
            else if (editrPara.Text.Length < 1)
            {
                ExtNet.Msg.Alert("Invalid paragraph", "A paragraph is reqiured for a template!").Show();
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
                        if (txtTemplateName.Text == string.Empty)
                        {
                            ExtNet.Msg.Alert("Invalid Template Name", "A template name is reqiured for a template!").Show();
                            return;
                        }
                        else
                        {
                            //addtemplate(ctid)
                            AddTemplate(txtTemplateName.Value.ToString(), editrPara.Value.ToString(), catid + 1);
                            ExtNet.MessageBox.Notify("Templates Changes", "Template added with new category").Show();
                        }
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
                        if (txtTemplateName.Text == string.Empty)
                        {
                            ExtNet.Msg.Alert("Invalid Template Name", "A template name is reqiured for a template!").Show();
                            return;
                        }
                        else
                        {
                            //addtemplate(ctid)
                            AddTemplate(txtTemplateName.Value.ToString(), editrPara.Value.ToString(), catid);
                            ExtNet.MessageBox.Notify("Templates Changes", "Template added to existing category").Show();
                        }
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

                ResetData();
            }

        }
        //REFRESH BUTTON
        protected void BtnRefreshClicked(object sender, DirectEventArgs e)
        {
            ResetData();
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

        //ENABLE SAVE BUTTON ON PARAGRAPH TEXT CHANGE
        protected void EnableSave(object sender, DirectEventArgs e)
        {
            BtnSave.Disabled = false;
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

        private void ResetData()
        {
            cmbTemplateCategory.Reset();
            cmbTemplateCategory.Disabled = false;
            btnCreateCatName.Disabled = false;
            txtCatName.Hidden = true;
            txtCatName.Clear();
            cmbComTemplates.Reset();
            cmbComTemplates.Disabled = true;
            txtTemplateName.Hidden = true;
            txtTemplateName.Clear();
            btnCreateTempName.Disabled = true;
            editrPara.Reset();
            editrPara.Disabled = true;
            BtnSave.Disabled = true;
        }

        #endregion
    }
}