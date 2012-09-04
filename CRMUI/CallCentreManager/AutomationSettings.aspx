<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true"
    CodeBehind="AutomationSettings.aspx.cs" Inherits="CRMUI.CallCentreManager.AutomationSettings" %>

<asp:Content ID="AutoSettingsRM" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default"/>
</asp:Content>
<asp:Content ID="AtoSettingsContentPH" runat="server" ContentPlaceHolderID="cphCallCMBody">
    <ext:Panel ID="pnlMain" runat="server" Height="612" Title="Automation Settings">
         <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
           <%--TOP PANEL--%>  
            <ext:Panel ID="pnlPriority" runat="server" Flex="100" Title="Priority Configuration" Icon="Wrench" TitleAlign="Center">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig runat="server" Align="Center"/>
                </LayoutConfig>
                <Items>
                    
                    
                    <ext:Container runat="server" Flex="1">
                        <Items>
                            <ext:Label runat="server" ID="lblpriorty" Text="Select priority to set" />
                                <ext:ComboBox ID="cmbPriorities" runat="server"  Margins="30 0 10 30" Width="250">
                                    <Items>
                                        <ext:ListItem Text="High Priority" Value="High" />
                                        <ext:ListItem Text="Medium Priority" Value="Medium   " />
                                        <ext:ListItem Text="Low Priority" Value="Low" />
                                    </Items>
                                </ext:ComboBox>
                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" Flex="1">
                        <Items>
                                <ext:FieldSet runat="server" ID="fdstHours" Width="180" Title="Set duration for selected priority">
                                    <Items>
                                        <ext:SpinnerField runat="server" ID="txtHours" FieldLabel="Number of hours"  Width="150" InputType="Number" Padding="5"/>
                                        <ext:SpinnerField runat="server" ID="txtMins" FieldLabel="Number of mins"   Width="150" InputType="Number" Padding="5"/>
                                    </Items>
                                 </ext:FieldSet>
                        </Items>
                    </ext:Container>
                   
                      <ext:Container ID="Container1" runat="server" Flex="1">
                        <Items>
                            <ext:Button runat="server" ID="btnConfirm" Text="Save" Padding="5"  Icon="PageSave" /> 
                        </Items>
                    </ext:Container>
                    
                    <ext:Container runat="server" Flex="2">
                        <Items>
                            
                        </Items>
                    </ext:Container>
                    
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
