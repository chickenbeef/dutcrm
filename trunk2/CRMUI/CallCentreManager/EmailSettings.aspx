<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true"
    CodeBehind="EmailSettings.aspx.cs" Inherits="CRMUI.CallCentreManager.EmailSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" MinHeight="618" Title="Email Settings" LayoutConfig="HBoxLayout"
        MaxWidth="1349">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel runat="server" ID="pnlEmailSettings" Width="500" Height="420" BodyPadding="30"
                Frame="True">
                <Items>
                    <%--USER CREDENTIALS--%>
                    <ext:TextField runat="server" ID="txtUsername" FieldLabel="Username/address" Anchor="80%"
                        IndicatorTip="It is required field" MsgTarget="Side" IsRemoteValidation="True">
                        <RemoteValidation OnValidation="ValidateUsername" />
                    </ext:TextField>
                    <ext:TextField runat="server" ID="txtPassword" FieldLabel="Password" InputType="Password"
                        Anchor="80%" AllowBlank="False" IndicatorTip="It is required field" MsgTarget="Side"
                        MinLength="6" MinLengthText="Minimum of 6 charachers required!" MaxLength="12"
                        MaxLengthText="Maximum of 12 characters accepted!" />
                    <ext:TextField ID="txtConfirmPassword" runat="server" FieldLabel="Confirm Password"
                        Vtype="password" InputType="Password" MsgTarget="Side" Anchor="80%" IsRemoteValidation="True">
                        <RemoteValidation OnValidation="ValidateConfirmPassord" />
                    </ext:TextField>
                    <%--INCOMING MAIL SETTINGS--%>
                    <ext:FieldSet runat="server" ID="fdstIncomingConfigs" Title="Incoming Mail Settings">
                        <Items>
                            <ext:TextField runat="server" ID="txtInServer" FieldLabel="(POP/IMAP)Mail Server"
                                Anchor="80%" MsgTarget="Side" IsRemoteValidation="True">
                                <RemoteValidation OnValidation="ValidateIncomingServer" />
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtInPort" FieldLabel="Incoming Port" Anchor="50%"
                                MsgTarget="Side" IsRemoteValidation="True">
                                <RemoteValidation OnValidation="ValidateIncomingPort" />
                            </ext:TextField>
                            <ext:Checkbox runat="server" ID="chkEnableInSSL" FieldLabel="Use SSL" />
                        </Items>
                    </ext:FieldSet>
                    <%--OUTGOING MAIL SETTINGS--%>
                    <ext:FieldSet runat="server" ID="fdstOutgoingConfigs" Title="Outgoing Mail Settings">
                        <Items>
                            <ext:TextField runat="server" ID="txtOtServer" FieldLabel="(SMTP) Mail Server" Anchor="80%"
                                MsgTarget="Side" IsRemoteValidation="True">
                                <RemoteValidation OnValidation="ValidateOutgoingServer" />
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtOtPort" FieldLabel="Outgoing Port" Anchor="50%"
                                IsRemoteValidation="True" MsgTarget="Side">
                                <RemoteValidation OnValidation="ValidateOutgoingPort" />
                            </ext:TextField>
                            <ext:Checkbox runat="server" ID="chkEnableOtSSL" FieldLabel="Use SSL" />
                        </Items>
                    </ext:FieldSet>
                    <ext:Container runat="server">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Button runat="server" ID="BtnSave" OnClientClick="Ext.net.Mask.show({msg: 'Please wait...'});" Text="Save" Icon="Disk" Padding="5" OnDirectClick="SaveSettings" />
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FormPanel>
        </Items>
    </ext:Panel>
</asp:Content>
