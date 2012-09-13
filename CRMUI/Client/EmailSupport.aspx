<%@ Page Title="" Language="C#" MasterPageFile="~/Client/CM.Master" AutoEventWireup="true"
    CodeBehind="EmailSupport.aspx.cs" Inherits="CRMUI.Client.EmailSupport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCMHead" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="615" Title="Email Support" Layout="hboxLayout">
        <Items>
            <ext:Panel ID="pnlSub1" Height="614" runat="server" Layout="vboxLayout" Flex="2"
                Border="false">
                <Items>
                    <ext:Panel ID="pnlDetails" runat="server" Height="314" Flex="2" Layout="vboxLayout"
                        Border="false">
                        <Items>
                            <ext:TextField ID="txtClID" runat="server" FieldLabel="Client ID " Margins="30 0 10 30"
                                ReadOnly="True"/>
                            <ext:TextField ID="txtFrom" runat="server" FieldLabel="From " Margins="10 0 10 30"
                                ReadOnly="True" />
                            <ext:TextField ID="txtSubject" runat="server" FieldLabel="Subject" Margins="20 0 10 30" Width="600" Grow="True" />
                            <ext:Label ID="Label1" runat="server" Text="Description:" Margins="10 0 0 30" />
                            <ext:HtmlEditor ID="heDesc" runat="server" Margins="10 0 10 30" Width="610" Height="190" AutoFocus="True" />
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="pnlbuttons" runat="server" Layout="hboxLayout" Height="300" Flex="1"
                        Border="false">
                        <Items>
                            <ext:Button ID="btnSend" runat="server" Text="Send" Margins="10 0 0 135" Padding="5"
                                Icon="EmailGo" OnDirectClick="btnSend_OnDirectClick" />
                            <ext:Button ID="btnCancel" runat="server" Text="Cancel" Padding="5" Margins="10 0 0 10"
                                OnDirectClick="btnCancel_OnDirectClick" Icon="Cancel" />
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlUploadImg" runat="server" Flex="1" Margins="200 0 10 0" Border="false">
                <Content>
                    <iframe name="ifUploadImg" height="250" width="400" seamless="seamless" src="upload.aspx">
                    </iframe>
                </Content>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
