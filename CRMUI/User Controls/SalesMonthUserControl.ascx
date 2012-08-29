﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesMonthUserControl.ascx.cs" Inherits="CRMUI.User_Controls.SalesMonthUserControl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
	Font-Size="8pt" Height="1210px" InteractiveDeviceInfos="(Collection)" 
	WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="815px">
	<LocalReport ReportPath="Reports\SaleMonth.rdlc">
		<DataSources>
			<rsweb:ReportDataSource DataSourceId="odsSalesMonth" Name="SalesMonth" />
		</DataSources>
	</LocalReport>
</rsweb:ReportViewer>
<asp:ObjectDataSource ID="odsSalesMonth" runat="server" 
	OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
	TypeName="CRMUI.Reports.CRMDataSetTableAdapters.SalesMonthTableAdapter">
</asp:ObjectDataSource>

