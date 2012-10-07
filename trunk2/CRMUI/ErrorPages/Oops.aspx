<%@ Page Title="" Language="C#" MasterPageFile="~/ErrorPages/CustomError.Master" AutoEventWireup="true" CodeBehind="Oops.aspx.cs" Inherits="CRMUI.ErrorPages.Oops" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager runat="server" Theme="Default"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel runat="server" Height="410" MaxWidth="1349">
        <Content>
            <asp:Label runat="server" Text="An Error Has Occurred<br/>" Font-Size="XX-Large" Font-Bold="True" ForeColor="#000066" />
            <asp:Label runat="server" Text="<br/>An unexpected error occurred on our website. The website administrator has been notified." Font-Size="X-Large"></asp:Label>
        </Content>
    </ext:Panel>
</asp:Content>
