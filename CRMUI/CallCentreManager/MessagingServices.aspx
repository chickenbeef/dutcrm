<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="MessagingServices.aspx.cs" Inherits="CRMUI.CallCentreManager.MessaginServices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManagerMessaginServices" runat="server" Theme= "Default"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <%--Markup for messaging servces using comtemplate--%>
    <ext:Panel ID="pnlMain" runat="server" Height="615" Title="Messaging Services" Layout="HBoxLayout" >
       <Items>
          <ext:Panel id="pnlMessage" Height="614" runat="server" Layout="vboxLayout" Flex="1" Border="false">
            <Items >
                <ext:Panel ID="pnlDetails" runat="server"  Height="314" Flex="2" Layout="vboxLayout" border="false">
                    <Items >
                            <ext:TextField ID="txtName" runat="server" FieldLabel="Client Name " margins="30 0 10 30" />
                            <ext:TextField  ID="txtClID" runat="server" FieldLabel="Client ID " margins="10 0 10 30"/>
                            <ext:Label ID="Label1" runat="server" Text="Description:" Margins="10 0 0 30" />
                            <ext:HtmlEditor ID="heDesc" runat="server" Margins="10 0 10 30" Width="610" Height="250" />
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnlbuttons" runat="server" Layout="hboxLayout" Height="300" Flex="1" Border="false" >
                    <Items >
                        <ext:Button ID="btnSend" runat="server" Text="Send" Margins="10 0 0 135" Padding="5" Width="50" />
                        <ext:Button ID="btnCancel" runat="server" Text="Cancel" Padding="5" Margins="10 0 0 10" />  
                    </Items>
                </ext:Panel>
            </Items>   
          </ext:Panel> 
          <ext:Panel ID="pnlUploadImg" runat="server" Flex="1" Margins="150 0 10 0" Border="false">
              <Content>
                  
                  
              </Content>
          </ext:Panel>
       </Items>
    </ext:Panel>

</asp:Content>
