<%@ Page Title="" Language="C#" MasterPageFile="~/UserLogin.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CRMUI.Login.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default">
    </ext:ResourceManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="408" Title="Login" Layout="HBoxLayout" MaxWidth="1349">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:Panel ID="pnlLogin" runat="server" Layout="VBoxLayout" Width="400" Height="200"
                Border="False" Frame="True">
                <Items>
                    <ext:TextField ID="txtUserName" runat="server" FieldLabel="Username" Margins="40 0 5 30"
                        Width="300" AllowBlank="False" BlankText="Please enter your username" MsgTarget="Side" />
                    <ext:TextField ID="txtPassword" runat="server" FieldLabel="Password" Margins="5 0 0 30" MsgTarget="Side"
                        Width="300" InputType="Password" AllowBlank="False" BlankText="Please enter your password" />
                    <ext:Label ID="lblError" runat="server" Margins="5 0 5 30" StyleSpec="color:red"/>
                    <ext:Button ID="btnSignIn" runat="server" Text="Sign In" Padding="5" Margins="5 0 0 135"
                        Icon="ComputerConnect" OnDirectClick="BtnSignInClick" />
                    <ext:HyperLink ID="hlForgotPassword" runat="server" Text="Forgot your password?"
                        Margins="5 0 0 135" NavigateUrl="~/PasswordRecovery.aspx" />
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
