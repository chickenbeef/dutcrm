<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true" CodeBehind="CheckTicketStatus.aspx.cs" Inherits="CRMUI.SupportAgent.CheckTicketStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
   <ext:Panel ID="pnlMain" runat="server" Height="615" MaxWidth="1349" Title="Ticket Status">
        
        <Items>
        </Items>
        
    </ext:Panel>
</asp:Content>
