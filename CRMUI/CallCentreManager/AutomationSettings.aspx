<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true"
    CodeBehind="AutomationSettings.aspx.cs" Inherits="CRMUI.CallCentreManager.AutomationSettings" %>

<asp:Content ID="AutoSettingsRM" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="AtoSettingsContentPH" runat="server" ContentPlaceHolderID="cphCallCMBody">
  
    <ext:Panel ID="pnlMain" runat="server" Height="612" Title="Automation Settings">
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

                                <ext:ComboBox ID="cmbPriorities" runat="server"  FieldLabel="Select priority to update" Margins="10 0 10 0"
                                    DisplayField="Priority" ValueField="Duration">
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
                                   <Listeners>
                                       <select Handler="#{txtHours.clearValue();
                                                            #{txtHours}.setValue(#cmbPriorities.strEscalation.get)"></select>
                                   </Listeners>
                                </ext:ComboBox>
                                
                                <ext:FieldSet runat="server">
                                    <Items>
                                        <ext:SpinnerField runat="server" ID="txtHours" FieldLabel="Number of hours" Width="150"
                                       Padding="5" Margins="10 0 10 0" />
                                    </Items>
                                </ext:FieldSet>
                        </Items>
                    </ext:Container>
                    
                    <ext:Container runat="server" Margins="10 0 10 0">
                        <Items>
                             <ext:Label runat="server" Text="Set notification period for unattended tickets" Margins="10 0 10 0"/>   
                             
                                <ext:FieldSet runat="server" ID="fdstHours" Title="Set unattended time for notification" Margins="10 0 10 0">
                                    <Items>
                                        <ext:SpinnerField runat="server" ID="txtMins" FieldLabel="Number of mins" Width="150"
                                             Padding="5" Margins="10 0 10 0"/>
                                    </Items>
                            </ext:FieldSet>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container1" runat="server" Margins="10 0 10 0">
                        <Items>
                            <ext:Button runat="server" ID="btnConfirm" Text="Save" Padding="5" Icon="PageSave" OnClick="SaveSettings__"/>
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
