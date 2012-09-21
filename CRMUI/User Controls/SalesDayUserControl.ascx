<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesDayUserControl.ascx.cs" Inherits="CRMUI.User_Controls.SalesDayUserControl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<rsweb:ReportViewer ID="SalesDayReportViewer" runat="server" Font-Names="Verdana" 
	Font-Size="8pt" Height="29.7cm" InteractiveDeviceInfos="(Collection)" 
	WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="21cm">
	<LocalReport ReportPath="Reports\SalesDay.rdlc">
	

		<DataSources>
			<rsweb:ReportDataSource DataSourceId="odsSalesDay" Name="SalesDay" />
		</DataSources>
	</LocalReport>
</rsweb:ReportViewer>


<asp:ObjectDataSource ID="odsSalesDay" runat="server" 
	OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
	TypeName="CRMUI.Reports.CRMDataSetTableAdapters.SalesDayTableAdapter">
</asp:ObjectDataSource>


