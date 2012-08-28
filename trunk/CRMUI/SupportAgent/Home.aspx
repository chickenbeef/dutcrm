<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRMUI.SupportAgent.CallHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme = "Default"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <%--CALL Support Agent--%>
    <ext:Panel ID="pnlCallSupport" runat="server" Height="615" MaxWidth="1349" Title="Log Call" Enabled="False" Visible="False">
        <Items>
        </Items>
    </ext:Panel>
    
    <%--EMAIL Support Agent--%>
    <ext:Panel ID="pnlEmailSupport" runat="server" Height="615" MaxWidth="1349" Title="Email Problems log" Enabled="False" Visible="False">
        <Items>
        </Items>
    </ext:Panel>
</asp:Content>
