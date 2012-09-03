<%@ Page Title="" Language="C#" MasterPageFile="~/SalesManager/SM.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="CRMUI.SalesManager.WebForm1" %>

<%@ Register Src="~/User Controls/SalesDayUserControl.ascx" TagName="SalesDayUserControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/User Controls/SalesWeekUserControl.ascx" TagName="SalesWeekUserControl"
    TagPrefix="uc2" %>
<%@ Register Src="~/User Controls/SalesMonthUserControl.ascx" TagName="SalesMonthUserControl"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <ext:Panel ID="pnlMain" runat="server"  Height="1190" Title="Reports">
        <Items>
            <ext:TabPanel ID="ReportsTabPanel" runat="server" AutoHeight="True" Border="false">
                <Items>
                    <ext:Panel ID="Panel1" runat="server" Title="Sales Day" AutoHeight="True" Border="false">
                         <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel runat="server" Border="false" Height="1210" Width="798">
                                <Content>
                                      <uc1:SalesDayUserControl ID="SalesDayUserControl1" runat="server" />
                                </Content>
                              
                            </ext:Panel>
                            
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel2" runat="server" Title="Sales Week" AutoHeight="True" Border="false">
                         <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Center" />
                        </LayoutConfig>       
                        <Items>
                            <ext:Panel runat="server" AutoHeight="True" Border="false" Height="1210" Width="798" >
                                <Content>
                                  <uc2:SalesWeekUserControl ID="SalesWeekUserControl1" runat="server" />
                                </Content>

                            </ext:Panel>          
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel3" runat="server" Title="Sales Month" AutoHeight="True" Border="false">
                             <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Center" />
                        </LayoutConfig> 

                        <Items>
                            <ext:Panel runat="server" Height="1210" Width="798" Border="false" >
                                <Content>
                                   <uc3:SalesMonthUserControl ID="SalesMonthUserControl1" runat="server" /> 
                                </Content>

                            </ext:Panel>                       
                        </Items>

                    </ext:Panel>
                </Items>
            </ext:TabPanel>

        </Items>
    </ext:Panel>
</asp:Content>
