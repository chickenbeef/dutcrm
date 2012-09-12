<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/ImageView.Master" AutoEventWireup="true" CodeBehind="ViewImages.aspx.cs" Inherits="CRMUI.SupportAgent.ViewImages1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel runat="server" ID="pnlImages" Title="Images" Frame="True" Width="800" Height="600">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Center"/>
            </LayoutConfig>
        </ext:Panel>
</asp:Content>
