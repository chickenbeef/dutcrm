<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true"
    CodeBehind="SupportReports.aspx.cs" Inherits="CRMUI.CallCentreManager.SupportReports" %>

<%@ Register Src="~/User Controls/ProblemsPerAgentUserControl.ascx" TagName="ProblemsPerAgentUserControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/User Controls/SupportUserControl.ascx" TagName="SupportUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default">
    </ext:ResourceManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ext:Panel ID="pnlMain" runat="server" Height="1212" Title="Reports">
        <Items>
            <ext:TabPanel ID="ReportsTabPanel" AutoHeight="True" runat="server" Border="false">
                <Items>
                    <ext:Panel ID="Panel1" runat="server" Title="Problems Per Agent" AutoHeight="True" Border="false">
                        <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel runat="server" Border="false" Height="1210" Width="798">
                                <Content>
                                    <uc1:ProblemsPerAgentUserControl ID="ProblemsPerAgentUserControl1" runat="server" />
                                </Content>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel2" runat="server" Title="Support Data" AutoHeight="True" Border="false">
                        <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel runat="server" Border="false" Height="1210" Width="798">
                                <Content>
                                    <uc2:SupportUserControl ID="SupportUserControl1" runat="server"/>
                                </Content>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:TabPanel>
        </Items>
    </ext:Panel>
</asp:Content>
