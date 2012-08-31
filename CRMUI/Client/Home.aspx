<%@ Page Title="" Language="C#" MasterPageFile="~/Client/CM.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="CRMUI.Client.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCMHead" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default">
    </ext:ResourceManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="615" Title="Update Client">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align='Stretch' />
        </LayoutConfig>
        <Items>
            <ext:Panel ID="pnlDetails" runat="server" Flex="1" Border="false" Layout="VBoxLayout">
                <Items>
                    <ext:TextField ID="txtName" runat="server" FieldLabel="Name" Width="300" Margins="30 0 10 30" />
                    <ext:TextField ID="txtSurname" runat="server" FieldLabel="Surname" Width="300" Margins="10 0 10 30" />
                    <ext:DateField ID="dfDob" runat="server" Vtype="daterange" FieldLabel="Date Of Birth"
                        EnableKeyEvents="true" Width="300" Margins="10 0 10 30" />
                    <ext:TextField ID="txtTel" runat="server" FieldLabel="Telephone No" Width="300" Margins="10 0 10 30" />
                    <ext:TextField ID="txtCell" runat="server" FieldLabel="Cell No" Width="300" Margins="10 0 10 30" />
                    <ext:TextField ID="txtFax" runat="server" FieldLabel="Fax No" Width="300" Margins="10 0 10 30" />
                    <ext:Panel runat="server" Layout="HBoxLayout" Border="false">
                        <Items>
                            <ext:Button ID="btnUpdate" runat="server" Text="Update" Padding="5" Margins="0 0 0 135" />
                            <ext:Button ID="btnCancel" runat="server" Text="Cancel" Padding="5" Margins="0 0 0 10" />
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlIDs" runat="server" Flex="1" Border="false" Layout="VBoxLayout">
                <Items>
                    <ext:TextField ID="txtClientID" runat="server" FieldLabel="Client ID" ReadOnly="True"
                        margins="30 0 10 30" />
                    <ext:TextField ID="txtBranchID" runat="server" FieldLabel="Branch ID" 
                        margins="10 0 10 30" ReadOnly="True" />
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
