<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="Charts.aspx.cs" Inherits="CRMUI.CallCentreManager.Charts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManagerc" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="382" Title="System charts and graphs" LayoutConfig="HBoxLayout" Icon="ServerChart" MaxWidth="1349">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center"/>
        </LayoutConfig>
        <TopBar>
            <ext:Toolbar ID="tlbarChart" runat="server">
                <Items>
                    <ext:Button runat="server" Text="Refresh Data" Icon="DatabaseRefresh"/>
                    <ext:Button runat="server" Text="Save Chart" Icon="Disk" OnDirectClick="SaveChart"/>
                </Items>
            </ext:Toolbar>
        </TopBar>
        <Items>
            
        </Items>
    </ext:Panel>
</asp:Content>
