<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true"
    CodeBehind="ManageTemplates.aspx.cs" Inherits="CRMUI.SupportAgent.ManageTemplates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManagerCreateTemplate" runat="server" Theme= "Default"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" MinHeight="612" Title="Create or edit templates" MaxWidth="1349">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
        </LayoutConfig>
        <Items>
            <%--MODIFY OR CREATE MESSAGE PAGE--%>
            <ext:Panel ID="pnlContent" Height="350" runat="server" Layout="hboxLayout" Border="false"
                Frame="True">
                <Items>
                    <%--LEFT CONTAINER--%>
                    <ext:Container ID="CntrNames" Height="350" runat="server" Layout="vboxLayout" Border="false"
                        Margins="0 10 0 10">
                        <Items>
                            <%--FIELDSET CATEGORY DATA--%>
                            <ext:FieldSet ID="fdsetSelections" runat="server" Margins="10 0 5 0" Width="380"
                                Layout="HBoxLayout" Title="Select or create a template category">
                                <Items>
                                    <ext:ComboBox ID="cmbTemplateCategory" runat="server" Margins="10 0 10 20"
                                        DisplayField="Name" ValueField="CAT_ID" AllowBlank="False" Editable="False" LabelAlign="Top"
                                         OnDirectSelect="SelectedCategory" Width="200" FieldLabel="Select Category">
                                        <Store>
                                            <ext:Store ID="strCategory" runat="server">
                                                <Model>
                                                    <ext:Model ID="mdlCategory" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="CAT_ID" />
                                                            <ext:ModelField Name="Name" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                    </ext:ComboBox>
                                    <ext:Button runat="server" ID="btnCreateCatName" Text="New Category" Padding="5"
                                        Margins="10 0 10 20" OnDirectClick="CreateCategoryName" Icon="add" />
                                </Items>
                            </ext:FieldSet>
                            <ext:TextField ID="txtCatName" runat="server" Width="200" Margins="5 0 10 30" LabelAlign="Top"
                                FieldLabel="Enter category name" Hidden="True" AllowBlank="False"/>
                            <%--FIELDSET TEMPLATE DATA--%>
                            <ext:FieldSet ID="FieldSet1" runat="server" Margins="5 0 10 0" Width="380" Layout="HBoxLayout" Title="Select or create a template">
                                <Items>
                                    <ext:ComboBox ID="cmbComTemplates" runat="server" Margins="10 0 10 20"
                                        Width="200" Disabled="True" DisplayField="Name" ValueField="CT_ID" AllowBlank="False"
                                        Editable="False" LabelAlign="Top" OnDirectSelect="SelectedTemplate" FieldLabel="Select Template">
                                        <Store>
                                            <ext:Store ID="streComTemplate" runat="server">
                                                <Model>
                                                    <ext:Model ID="mdlComTemplate" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="CT_ID" />
                                                            <ext:ModelField Name="Name" />
                                                            <ext:ModelField Name="Paragraph" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                    </ext:ComboBox>
                                    <ext:Button runat="server" ID="btnCreateTempName" Text="New Template" Padding="5"
                                        Margins="10 0 10 20" OnDirectClick="CreateTemplateName" Disabled="True" Icon="Add" />
                                </Items>
                            </ext:FieldSet>
                            <ext:TextField ID="txtTemplateName" runat="server" Width="200" Margins="5 0 10 30" LabelAlign="Top" FieldLabel="Enter template name" Hidden="True"/>
                                
                            
                        </Items>
                    </ext:Container>
                    <%--RIGHT CONTAINER--%>
                    <ext:Container ID="cntrMessageBody" Height="350" runat="server" Layout="vboxLayout"
                        Border="false" Margins="0 10 0 5">
                        <Items>
                            <ext:HtmlEditor ID="editrPara" runat="server" Margins="10 0 10 15" Width="550" Height="250" FieldLabel="Enter template paragragh" LabelAlign="Top" Disabled="True" OnDirectChange="EnableSave"/>
                            <ext:Container ID="Container1" runat="server" Layout="hboxLayout">
                                <Items>
                                    <ext:Button ID="BtnSave" runat="server" Text="Save Template" Margins="10 0 10 20"
                                                Padding="5" OnDirectClick="BtnSaveClicked" Icon="ScriptSave" Disabled="true" />
                                    <ext:Button ID="btnRefresh" runat="server" Text="Reset" Margins="10 0 10 20"
                                                Padding="5" OnDirectClick="BtnRefreshClicked" Icon="PageRefresh"/>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
