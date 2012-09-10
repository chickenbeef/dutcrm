<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="CRMUI.Client.Upload" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60623.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" width="300" height="200">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Width="300" OnUploadComplete="AjaxFileUpload_OnUploadComplete" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
