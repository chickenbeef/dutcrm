<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRMUI.CallCentreManager.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default"/>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="cphCallCMBody">
    <ext:Panel ID="pnlMain" runat="server" Height="615"  MaxWidth="1349" Title="Update Roles">       
        
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" DefaultMargins="2" />
        </LayoutConfig>
        <Items>
            
            <ext:Panel ID="pnlSearch" runat="server" Flex="100" Title="Search Employee" Icon="UserMagnify">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <ext:Panel ID="Panel1" runat="server" Flex="1">
                        <LayoutConfig>
                            <ext:AccordionLayoutConfig />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel ID="pnlUsername" runat="server" Title="SEARCH BY USERNAME" Icon="PageMagnify">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Employee Username" Width="300" Margins="0 0 0 30"/>
                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier"/>
                                </Items>
                            </ext:Panel>

                            <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY NAME" Icon="PageMagnify">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSName" FieldLabel="Employee Name" Width="300" Margins="0 0 0 30"/>
                                    <ext:Button runat="server" ID="btnNameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier"/>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <%--Employee details populated in panel below--%>

                    <ext:Panel Title="Employee Details" runat="server" Border="false" Flex="3" Icon="Vcard">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch"/>
                        </LayoutConfig>
                        <Items>
                            <ext:GridPanel runat="server" Flex="1" >
                                <Store>
                                    <ext:Store ID="streEmployee"  runat="server">
                                        <Model>
                                            <ext:Model ID="mdlEmployee" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="Employee_ID"/>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Surname"/>
                                                    <ext:ModelField Name="Username"/>
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="txtName" Text="Name"/>
                                        <ext:Column runat="server" ID="txtSurname" Text="Surname"/>
                                        <ext:Column runat="server" ID="txtUsername" Text="Username"/>
                                    </Columns>
                                </ColumnModel>
                                <Buttons>
                                    <ext:Button runat="server" ID="btnAccept" Text="Accept" Padding="5" Icon="ArrowEw"/>
                                </Buttons>
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <%--Role settings Panel --%>
            <ext:Panel ID="pnlSetRole" runat="server" Flex="90" Title="Set employee role" Icon="Wrench">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig/>
                </LayoutConfig>
                <Items>
                    
                    <ext:TextField runat="server" ID="txtEmpId" FieldLabel="Employee ID" ReadOnly="True" Margins="10 0 10 30" Width="300"/>
                    
                    <ext:FieldSet runat="server" Title="Current role is selected" Margins="10 0 10 135" Width="350">
                        <Items>
                                <ext:RadioGroup ID="radGrpRoles" FieldLabel="Please select a new role" runat="server"   GroupName="Available Roles" Height="250" ColumnsNumber="1" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:Radio runat="server" ID="radbtnEmailSupport" FieldLabel="Email Support Agent" Name="Roles"/>
                                        <ext:Radio runat="server" ID="radbtnCallSupport" FieldLabel="Call Support Agent" Name="Roles"/>
                                        <ext:Radio runat="server" ID="radbtnRM" FieldLabel="Relationship Manager" Name="Roles"/>
                                        <ext:Radio runat="server" ID="radbtnSalesManager" FieldLabel="SalesManager" Name="Roles"/>
                                    </Items>
                                </ext:RadioGroup>
                    </Items>
                    </ext:FieldSet>
                    <ext:Button runat="server" ID="btnUpdateRole" Text="Update Role" Padding="5" Margins="0 0 0 135" Icon="PageSave"/>
                    
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>

