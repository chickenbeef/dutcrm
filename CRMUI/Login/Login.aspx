<%@ Page Title="" Language="C#" MasterPageFile="~/Login/UserLogin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CRMUI.Login.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="408" Title="Login" Layout="HBoxLayout">
        <Items>
            <ext:Panel ID="pnlLogin" runat="server" Layout="VBoxLayout" Width="600"  Border="False">
                <Items>
                    <ext:TextField ID="txtUserName" runat="server" FieldLabel="Username"  Margins="30 0 5 30" />
                    <ext:TextField ID="txtPassword" runat="server" FieldLabel="Password"  Margins="5 0 0 30" />
                    <ext:Button ID="btnSignIn" runat="server" Text="Sign In" Padding="5" Margins="5 0 0 135"/>
                    <ext:HyperLink ID="hlForgotPassword" runat="server" Text="Forgot your password?" Margins="5 0 0 135"/>
                </Items>
            </ext:Panel>
            
            <ext:Panel ID="pnlPasswordrecover" runat="server" Layout="vboxLayout" Flex="1" >
                <Items>
                    <ext:Label Text="Enter your email address:" runat="server" Margins="30 0 5 30"/>
                    <ext:TextField ID="txtEmail" runat="server" Margins="5 0 0 30"/>

                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
