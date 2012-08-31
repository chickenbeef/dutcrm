<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true"
    CodeBehind="AutomationSettings.aspx.cs" Inherits="CRMUI.CallCentreManager.WebForm2" %>

<asp:Content ID="AutoSettingsRM" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="AtoSettingsContentPH" runat="server" ContentPlaceHolderID="cphCallCMBody">
    <ext:Panel ID="pnlMain" runat="server" Height="615" MaxWidth="1349" Title="Automation Settings">
        <Items>
            
        </Items>
    </ext:Panel>
</asp:Content>
