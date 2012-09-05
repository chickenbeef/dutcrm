<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true"
    CodeBehind="EmailSettings.aspx.cs" Inherits="CRMUI.CallCentreManager.EmailSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="382" Title="Email Settings" LayoutConfig="HBoxLayout">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center"/>
        </LayoutConfig>
        <Items>
            <ext:FormPanel runat="server" ID="pnlEmailSettings" Width="400" Height="240" BodyPadding="30" Frame="True">
                <Items>
                    <ext:TextField runat="server" ID="txtServer" FieldLabel="POP Server" Anchor="100%" AllowBlank="False" />
                    <ext:TextField runat="server" ID="txtUsername" FieldLabel="Username" Anchor="100%" AllowBlank="False"/>
                    <ext:TextField runat="server" ID="txtPassword" FieldLabel="Password" InputType="Password"
                        Anchor="100%" AllowBlank="False"/>
                    <ext:TextField ID="txtConfirmPassword" runat="server" FieldLabel="Confirm Password" Vtype="password"
                        InputType="Password" MsgTarget="Side" Anchor="100%" AllowBlank="False"/>
                    <ext:TextField runat="server" ID="txtPort" FieldLabel="Port"  Anchor="50%" AllowBlank="False" />
                    <ext:Checkbox runat="server" ID="chkEnableSSL" FieldLabel="Use SSL" />
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="txtSave" Text="Save" Icon="Disk" Padding="5" />
                </Buttons>
            </ext:FormPanel>
        </Items>
    </ext:Panel>
</asp:Content>
