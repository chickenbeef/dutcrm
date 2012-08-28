<%@ Page Title="" Language="C#" MasterPageFile="~/RelationshipManager/RM.Master"
    AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRMUI.RelationshipManager.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="612" Title="Record Sale">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" DefaultMargins="2" />
        </LayoutConfig>
        <Items>
            <ext:Panel ID="pnlSearch" runat="server" Flex="1" Title="Search Client By" Icon="VcardEdit">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <ext:Panel ID="Panel1" runat="server" Flex="1">
                        <LayoutConfig>
                            <ext:AccordionLayoutConfig />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel ID="pnlUsername" runat="server" Title="Username" BodyPadding="30" Layout="HBoxLayout">
                                <Items>
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Client Username" Flex="4"/>
                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Flex="1"/>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlName" runat="server" Title="Name" BodyPadding="30" Layout="HBoxLayout">
                                <Items>
                                    <ext:TextField runat="server" ID="txtSName" FieldLabel="Client Name" Flex="4"/>
                                    <ext:Button runat="server" ID="btnNameSearch" Text="Search" Flex="1"/>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel2" runat="server" Flex="3" Title="Client Details" Icon="Vcard">
                        <LayoutConfig>
                            <ext:VBoxLayoutConfig Padding="30" Align="Stretch"/>
                        </LayoutConfig>
                        <Items>
                            <ext:TextField runat="server" ID="txtName" FieldLabel="Name" ReadOnly="True"/>
                            <ext:TextField runat="server" ID="txtSurname" FieldLabel="Surname" ReadOnly="True"/>
                            <ext:TextField runat="server" ID="txtUsername" FieldLabel="Username" ReadOnly="True"/>
                            <ext:TextField runat="server" ID="txtDateOfBirth" FieldLabel="Date Of Birth" ReadOnly="True"/>
                            <ext:TextField runat="server" ID="txtTelephone" FieldLabel="Telephone" ReadOnly="True"/>
                            <ext:TextField runat="server" ID="txtCell" FieldLabel="Cell" ReadOnly="True"/>
                            <ext:TextField runat="server" ID="txtFax" FieldLabel="Fax" ReadOnly="True" />
                            <ext:Button runat="server" ID="btnAccept" Text="Accept"/>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlConfirm" runat="server" Flex="1" Title="Confirm Sale">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" Padding="30"/>
                </LayoutConfig>
                <Items>
                    <ext:TextField runat="server" ID="txtDate" FieldLabel="Date" ReadOnly="True"/>
                    <ext:TextField runat="server" ID="txtClientId" FieldLabel="Client ID" ReadOnly="True"/>
                    <ext:TextField runat="server" ID="txtEmpId" FieldLabel="Employee ID" ReadOnly="True"/>
                    <ext:Button runat="server" ID="btnConfirm" Text="Record Sale"/>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
