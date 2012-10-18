<%@ Page Title="" Language="C#" MasterPageFile="~/UserLogin.Master" AutoEventWireup="true"
    CodeBehind="PasswordRecovery.aspx.cs" Inherits="CRMUI.Login.PaswordRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Title="Login" Layout="HBoxLayout" Height="408" MaxWidth="1349">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center"/>
        </LayoutConfig>
        <Items>
            <ext:Panel ID="pnlPasswordrecover" runat="server" Width="400" Icon="UserKey"
                Height="300" Frame="true" BodyPadding="20" Title="Forgot Password">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig/>
                </LayoutConfig>
                <Items>
                    <ext:TextField runat="server" ID="txtUsername" Margins="0 0 10 30" Width="250" MsgTarget="Under" FieldLabel="Username" IsRemoteValidation = "True">
                    <RemoteValidation  onValidation = "ValidateUserName"></RemoteValidation>
										   										   
                    </ext:TextField>
										
<%--										   <Listeners>
                            <ValidityChange Handler="#{btnSubmit}.setDisabled(!isValid);"/>
                        </Listeners>--%>
										

                    <ext:Button runat="server" Margins="0 0 20 135" Disabled="True" Icon="KeyGo" Padding="5" ID="btnSubmit" Text="Request" OnDirectClick="BtnSubmitClick" OnClientClick="Ext.net.Mask.show({msg:'Please wait...'});"/>

										<ext:Button runat="server" ID = "btnReset" Text = "Reset" Icon = "KeyAdd" OnDirectClick = "ResetPassword" Disabled = "True" Margins = "0 0 10 135" Padding = "5" OnClientClick="Ext.net.Mask.show({msg:'Please wait...'});" ></ext:Button>
										

                    <ext:HyperLink ID="HyperLink1" Margins="35 0 0 135" runat="server" Text="Back To Login" NavigateUrl="~/Default.aspx" />
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
