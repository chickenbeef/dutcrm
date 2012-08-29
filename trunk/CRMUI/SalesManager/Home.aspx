<%@ Page Title="" Language="C#" MasterPageFile="~/SalesManager/SM.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRMUI.SalesManager.WebForm1" %>

<%@ Register Src="~/User Controls/SalesDayUserControl.ascx" tagname="SalesDayUserControl" tagprefix="uc1" %>

<%@ Register Src="~/User Controls/SalesWeekUserControl.ascx" tagname="SalesWeekUserControl" tagprefix="uc2" %>

<%@ Register Src="~/User Controls/SalesMonthUserControl.ascx" tagname="SalesMonthUserControl" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    
    <ext:Panel ID="pnlMain" runat="server" Height="1000" Width="815" Title="Reports">
        <Content>
        					
					<ext:TabPanel ID="ReportsTabPanel" runat="server" AutoHeight = "True">
						<Items>
							<ext:Panel ID="Panel1" runat="server" Title="Sales Day" AutoHeight = "True"  >
								<Content >
									
                 <uc1:SalesDayUserControl ID="SalesDayUserControl1" runat="server" />
								</Content>
								
							</ext:Panel>
							
							

							<ext:Panel ID="Panel2" runat="server" Title="Sales Week" AutoHeight = "True"  >
			        <Content>
			        	
							<uc2:SalesWeekUserControl ID="SalesWeekUserControl1" runat="server" />

			        </Content>

							</ext:Panel>
							
							

							<ext:Panel ID="Panel3" runat="server" Title="Sales Month" AutoHeight = "True">
								<Content>
									
									
						 <uc3:SalesMonthUserControl ID="SalesMonthUserControl1" runat="server" />
								</Content>

							</ext:Panel>
							

						</Items>

					</ext:TabPanel>
					
					<asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>

        </Content>     
    </ext:Panel>
</asp:Content>

