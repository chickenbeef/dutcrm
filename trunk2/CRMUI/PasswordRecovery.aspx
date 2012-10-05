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
                Height="250" Frame="true" BodyPadding="20" Title="Forgot Password">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig/>
                </LayoutConfig>
                <Items>
                    <ext:TextField runat="server" ID="txtUsername" Margins="0 0 10 30" Width="250" MsgTarget="Under" BlankText="Please enter a username" FieldLabel="Username" AllowBlank="False">
                        <Listeners>
                            <ValidityChange Handler="#{btnSubmit}.setDisabled(!isValid);"/>
                        </Listeners>
                    </ext:TextField>
                    <ext:Button runat="server" Margins="0 0 0 135" Disabled="True" Icon="KeyGo" Padding="5" ID="btnSubmit" Text="Request" OnDirectClick="BtnSubmitClick" OnClientClick="Ext.net.Mask.show({msg:'Please wait...'});"/>
                    <ext:HyperLink ID="HyperLink1" Margins="35 0 0 135" runat="server" Text="Back To Login" NavigateUrl="~/Default.aspx" />
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
