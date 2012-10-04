<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupportUserControl.ascx.cs" Inherits="CRMUI.User_Controls.SupportUserControl" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
	Font-Size="8pt" Height="310mm" InteractiveDeviceInfos="(Collection)" 
	WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="211mm" 
	style="text-align: justify">
	<LocalReport ReportPath="Reports\SupportReport.rdlc">
		<DataSources>
			<rsweb:ReportDataSource DataSourceId="odsSupportReportData" 
                Name="SupportReportData" />
		    <rsweb:ReportDataSource DataSourceId="odsCommonProblem" Name="CommonProblem" />
		</DataSources>
	</LocalReport>
</rsweb:ReportViewer>



<asp:ObjectDataSource ID="odsCommonProblem" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="CRMUI.Reports.CRMDataSetTableAdapters.CommonProblemTableAdapter">
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsSupportReportData" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="CRMUI.Reports.CRMDataSetTableAdapters.SupportReportDataTableAdapter">
</asp:ObjectDataSource>




