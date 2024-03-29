﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupportUserControl.ascx.cs" Inherits="CRMUI.User_Controls.SupportUserControl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
	Font-Size="8pt" Height="29.7cm" InteractiveDeviceInfos="(Collection)" 
	WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="21cm" 
	style="text-align: justify">
	<LocalReport ReportPath="Reports\SupportReport.rdlc">
		<DataSources>
			<rsweb:ReportDataSource DataSourceId="odsSupportReport" Name="SupportData" />
		</DataSources>
	</LocalReport>
</rsweb:ReportViewer>
<asp:ObjectDataSource ID="odsSupportReport" runat="server" 
	OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
	TypeName="CRMUI.Reports.CRMDataSetTableAdapters.SupportDataTableAdapter">
</asp:ObjectDataSource>


