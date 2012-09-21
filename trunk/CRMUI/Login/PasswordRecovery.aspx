<%@ Page Title="" Language="C#" MasterPageFile="~/Login/UserLogin.Master" AutoEventWireup="true"
    CodeBehind="PasswordRecovery.aspx.cs" Inherits="CRMUI.Login.PaswordRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Title="Login" Layout="HBoxLayout" Height="408">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center"/>
        </LayoutConfig>
        <Items>
            <ext:Panel ID="pnlPasswordrecover" runat="server" Width="400"
                Height="250" Frame="true">
                <Content>
                    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" Height="200" Width="300"
                        SubmitButtonStyle-Height="30" SubmitButtonStyle-Width="80">
                    </asp:PasswordRecovery>
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Back To Login" NavigateUrl="../Default.aspx" />
                </Content>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
