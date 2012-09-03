<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesWeekUserControl.ascx.cs" Inherits="CRMUI.User_Controls.SalesWeekUserControl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<rsweb:ReportViewer ID="SalesWeekReportViewer" runat="server" 
	Font-Names="Verdana" Font-Size="8pt" Height="0px" 
	InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
	WaitMessageFont-Size="14pt" Width="" SizeToReportContent="True">
	<LocalReport ReportPath="Reports\SalesWeek.rdlc">
		<DataSources>
			<rsweb:ReportDataSource DataSourceId="odsSalesWeek" Name="SalesWeek" />
		</DataSources>
	</LocalReport>
</rsweb:ReportViewer>
<asp:ObjectDataSource ID="odsSalesWeek" runat="server" 
	OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
	TypeName="CRMUI.Reports.CRMDataSetTableAdapters.SalesWeekTableAdapter">
</asp:ObjectDataSource>


