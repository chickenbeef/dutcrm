<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProblemsPerAgentUserControl.ascx.cs" Inherits="CRMUI.User_Controls.ProblemsPerAgentUserControl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
	Font-Size="8pt" Height="310mm" InteractiveDeviceInfos="(Collection)" 
	WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="211mm">
	<LocalReport ReportPath="Reports\ProblemsPerAgent.rdlc">
		<DataSources>
			<rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
				Name="ProblemsPerAgent" />
		</DataSources>
	</LocalReport>
</rsweb:ReportViewer>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="CRMUI.Reports.CRMDataSetTableAdapters.ProblemsPerAgentTableAdapter">
</asp:ObjectDataSource>



