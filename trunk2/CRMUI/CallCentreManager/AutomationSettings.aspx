<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true"
    CodeBehind="AutomationSettings.aspx.cs" Inherits="CRMUI.CallCentreManager.AutomationSettings" %>

<asp:Content ID="AutoSettingsRM" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="AtoSettingsContentPH" runat="server" ContentPlaceHolderID="cphCallCMBody">
  
    <ext:Panel ID="pnlMain" runat="server" Height="612" Title="Automation Settings" MaxWidth="1349">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" />
        </LayoutConfig>
        
        <Items>
            
            <ext:Container runat="server" Flex="35">
                <Items>
                </Items>
            </ext:Container>
            
            <%--MAIN PANEL--%>
            <ext:Panel ID="pnlPriority" runat="server"  Frame="True" Flex="30" Title="Priority Settings" TitleAlign="Center" Icon="ServerWrench">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Center"/>
                </LayoutConfig>
                
                <Items>
                  <ext:Container runat="server" Margins="10 0 10 0">
                        <Items>
                            <ext:Label runat="server" ID="lblpriorty" Text="Set escalation for unsolved tickets per priority" Margins="10 0 10 0"/> 

                                <ext:ComboBox ID="cmbPriorities" runat="server"  FieldLabel="Priority to update" Margins="20 0 10 0"
                                    DisplayField="Priority" ValueField="Duration" Editable="False">
                                   <Store>
                                    <ext:Store ID="strEscalation" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlEscalation" runat="server">
                                                <fields>
                                                    <ext:ModelField Name="Priority"/>
                                                    <ext:ModelField Name="Duration"/>
                                                </fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                   </Store>
                                   <listeners>
                                       <Select Handler="#{txtHours}.setValue(#{cmbPriorities}.getValue());"/>
                                   </listeners>
                                </ext:ComboBox>
                                
                                <ext:FieldSet runat="server">
                                    <Items>
                                        <ext:NumberField runat="server" ID="txtHours" FieldLabel="Number of hours" Width="150" 
                                        Padding="5" Margins="10 0 10 0" MinValue="1" MaxValue="84" Editable="False" AllowBlank="False" OnDirectChange="HoursChanged">
                                           </ext:NumberField>
                                    </Items>
                                </ext:FieldSet>
                        </Items>
                    </ext:Container>
                    
                    <ext:Container runat="server" Margins="10 0 10 0">
                        <Items>
                             <ext:Label runat="server" Text="Set notification period for unattended tickets" Margins="10 0 10 0"/>   
                             
                                <ext:FieldSet runat="server" ID="fdstHours" Margins="5 0 10 0">
                                    <Items>
                                        <ext:NumberField runat="server" ID="txtMins" FieldLabel="Unattended Minutes allowed" Width="150" 
                                                         Padding="5" Margins="10 0 10 0" MinValue="1" AllowDecimals="False" MaxValue="84" 
                                                         Editable="False" AllowBlank="false" OnDirectChange="MinsChanged"/>
                                                                   
                                       
                                    </Items>
                            </ext:FieldSet>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container1" runat="server" Margins="5 0 10 0">
                        <Items>
                            <ext:Button runat="server" ID="btnConfirm" Text="Save" Padding="5" Icon="PageSave" OnDirectClick="SaveSettings" Disabled="True"/>
                            <ext:Button ID="btnCancel" runat="server" Text="Cancel" Padding="5" OnDirectClick="CancelClicked" Icon="Cancel" Margin="10"/>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
             <ext:Container ID="cont4" runat="server" Flex="35">
                <Items>
                    
                </Items>
            </ext:Container>
           </Items>
          </ext:Panel>
         
</asp:Content>
