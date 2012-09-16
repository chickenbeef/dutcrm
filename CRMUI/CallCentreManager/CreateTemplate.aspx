<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="CreateTemplate.aspx.cs" Inherits="CRMUI.CallCentreManager.CreateTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManagerCreateTemplate" runat="server" Theme= "Default"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    
    <ext:Panel ID="pnlMain" runat="server" Height="610" Title="Create or edit templates" >
         <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center"/>
        </LayoutConfig>
                <Items>
                        <%--MODIFY OR CREATE MESSAGE--%>
                        
                            <ext:Panel id="pnlMessageBody" Height="614" runat="server" Layout="vboxLayout" Border="false" Frame="True">
                                <Items >
                            <ext:FieldSet ID="fdsetradios" runat="server" Title="Please select a process" Margins="10 0 10 135" Width="400" Layout="HBoxLayout">
                                <Items>
                                    <ext:RadioGroup ID="radGrpCreationType"  runat="server"   GroupName="Processes" Height="35">
                                        <Items>
                                            <ext:Radio runat="server" ID="radbtnCreate" FieldLabel="Create new template" Name="Process"/>
                                            <ext:Radio runat="server" ID="radbtnUpdate" FieldLabel="Update existing template" Name="Process" Margins="0 0 0 30"/>
                                        </Items>
                                    </ext:RadioGroup>
                                </Items>
                            </ext:FieldSet>   
                            <ext:FieldSet ID="fdsetSelections" runat="server"  Margins="10 0 10 135" Width="450" Layout="HBoxLayout">
                            <Items>
                                <ext:ComboBox ID="cmbTemplateCategory" runat="server"  FieldLabel="Select a category" Margins="10 0 10 20"
                                    DisplayField="Category" ValueField="Name" AllowBlank="False" Editable="False" LabelAlign="Top">
                                   <Store>
                                    <ext:Store ID="strCategory" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlCategory" runat="server">
                                                <fields>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Category"/>
                                                </fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                   </Store>
                                </ext:ComboBox>         
                
                            <ext:ComboBox ID="cmbComTemplates" runat="server"  FieldLabel="Select a template" Margins="10 0 10 20"
                                    DisplayField="Name" ValueField="Paragraph" AllowBlank="False" Editable="False" LabelAlign="Top">
                                   <Store>
                                    <ext:Store ID="streComTemplate" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlComTemplate" runat="server">
                                                <fields>
                                                    <ext:ModelField Name="CT_ID"/>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Paragraph"/>
                                                </fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                   </Store>
                                </ext:ComboBox>
                                </Items>
                                </ext:FieldSet>
                                
                                    <ext:TextField ID="txtTemplateName" runat="server" Margins="10 0 10 135" LabelAlign="Top" FieldLabel="Enter template name"/>
                                    <ext:HtmlEditor ID="editrPara" runat="server" Margins="10 0 10 15" Width="610" Height="250" FieldLabel="Enter template paragragh" LabelAlign="Top"/>
                
                   <ext:Panel ID="pnlbuttons" runat="server" Layout="hboxLayout" Height="300" Flex="1" Border="false" Frame="True">
                    <Items >
                        <ext:Button ID="btnCreateMessage" runat="server" Text="Create Template" Margins="10 0 0 135" Padding="5"  />
                        <ext:Button ID="btnSaveMessage" runat="server" Text="Save Template" Margins="10 0 0 135" Padding="5"  />
                        <ext:Button ID="btnDelete" runat="server" Text="Delete Template" Padding="5" Margins="10 0 0 20" />
                        <ext:Button ID="btnCancel" runat="server" Text="Cancel" Padding="5" Margins="10 0 0 20" />  
                        
                    </Items>
                </ext:Panel>
            </Items>   
          </ext:Panel> 
       </Items>
    </ext:Panel>

 </asp:Content>
