<%@ Page Title="" Language="C#" MasterPageFile="~/RelationshipManager/RM.Master"
    AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRMUI.RelationshipManager.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="612" Title="Record Sale">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <%--LEFT PANEL--%>
            <ext:Panel ID="pnlSearch" runat="server" Flex="100" Title="Search Client" Icon="UserMagnify">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--TOP LEFT PANEL--%>
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
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Client Username" Width="300" Margins="0 0 0 30"/>
                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier"/>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY CLIENT NAME" Icon="PageMagnify">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSName" FieldLabel="Client Name" Width="300" Margins="0 0 0 30"/>
                                    <ext:Button runat="server" ID="btnNameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier"/>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <%--BOTTOM LEFT PANEL--%>
                    <ext:Panel Title="Client Details" runat="server" Border="false" Flex="3" Icon="Vcard">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch"/>
                        </LayoutConfig>
                        <Items>
                            <ext:GridPanel ID="gpClient" runat="server" Flex="1" >
                                <Store>
                                    <ext:Store ID="streClient" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlClient" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="CLIENT_ID"/>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Surname"/>
                                                    <ext:ModelField Name="UserName"/>
                                                    <ext:ModelField Name="DateOfBirth"/>
                                                    <ext:ModelField Name="Telephone"/>
                                                    <ext:ModelField Name="Cell"/>
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="txtCID" Text="Client ID" DataIndex="CLIENT_ID"/>
                                        <ext:Column runat="server" ID="txtName" Text="Name" DataIndex="Name"/>
                                        <ext:Column runat="server" ID="txtSurname" Text="Surname" DataIndex="Surname"/>
                                        <ext:Column runat="server" ID="txtUsername" Text="Username" DataIndex="UserName"/>
                                        <ext:DateColumn runat="server" ID="txtDateOfBirth" Text="Date Of Birth" Format="DD-MON-YYYY" DataIndex="DateOfBirth"/>
                                        <ext:Column runat="server" ID="txtTelephone" Text="Telephone" DataIndex="Telephone"/>
                                        <ext:Column runat="server" ID="txtCell" Text="Cell" DataIndex="Cell"/>
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
            <%--RIGHT PANEL--%>
            <ext:Panel ID="pnlConfirm" runat="server" Flex="91" Title="Confirm Sale" Icon="Disk">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig/>
                </LayoutConfig>
                <Items>
                    <ext:TextField runat="server" ID="txtClientId" FieldLabel="Client ID" ReadOnly="True" Margins="30 0 10 30" Width="300"/>
                    <ext:TextField runat="server" ID="txtEmpId" FieldLabel="Employee ID" ReadOnly="True" Margins="10 0 10 30" Width="300"/>
                    <ext:Button runat="server" ID="btnConfirm" Text="Record Sale" Padding="5" Margins="0 0 0 135" Icon="PageSave"/>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
