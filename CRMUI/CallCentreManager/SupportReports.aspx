<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="SupportReports.aspx.cs" Inherits="CRMUI.CallCentreManager.SupportReports" %>
<%@ Register src="~/User Controls/ProblemsPerAgentUserControl.ascx" tagname="ProblemsPerAgentUserControl" tagprefix="uc1" %>
<%@ Register src="~/User Controls/SupportUserControl.ascx" tagname="SupportUserControl" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	
	<ext:ResourceManager ID="ResourceManager1" runat="server" Theme = "Default">
	</ext:ResourceManager>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
	
	
	<ext:Panel ID="pnlMain" runat="server" Height = "1000" Width = "815" Title="Reports">
		<Content>
			
	
			<ext:TabPanel ID="ReportsTabPanel"  AutoHeight = "True" runat="server" >
				<Items>
					

					<ext:Panel ID="Panel1" runat="server" Title="Problems Per Agent" AutoHeight = "True" >
					  <Content>
					  	

				     <uc1:ProblemsPerAgentUserControl ID="ProblemsPerAgentUserControl1" runat="server" />

					  </Content>
					</ext:Panel>
					

					<ext:Panel ID="Panel2" runat="server" Title="Support Data" AutoHeight = "True">
						<Content>
							
							<uc2:SupportUserControl ID="SupportUserControl1" runat="server" />

						</Content>
					</ext:Panel>


				</Items>
			</ext:TabPanel>
			
			
			<asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>

		</Content>
	</ext:Panel>

</asp:Content>

