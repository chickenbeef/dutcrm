<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="CRMUI.CallCentreManager.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<ext:ResourceManager ID="ResourceManager1" runat="server">
	</ext:ResourceManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
	 <ext:Panel ID="pnlMain" runat="server" MinHeight="618" Title="Change Password" LayoutConfig="HBoxLayout"
        MaxWidth="1349">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel runat="server" ID="pnlPassword" Width="650" Height="200" BodyPadding="30"
                Frame="True">
                <Items>
                	    <ext:Container ID="cntrcurrentpassword" runat="server" Layout="HBoxLayout" > 
                	     <Items>
										   <ext:TextField runat="server" ID="txtOldPassword" FieldLabel="Current Password" InputType="Password"
                       Anchor = "60%" AllowBlank="False" IndicatorTip="It is required field" MsgTarget="Side" MinLength="7" MinLengthText="Password length minimum: 7" MaxLength="15"
                        MaxLengthText="Password length maximum: 15">
											</ext:TextField>
                	                <ext:Label ID="lblOldPasswordError" runat="server" Margins="0 0 0 30" StyleSpec = "color:red"/>
                           </Items>
                         </ext:Container>
											

                    <ext:Container ID="cntrnewpassword" runat="server" Layout="HBoxLayout" >
                    <Items>
                    <ext:TextField runat="server" ID="txtNewPassword" FieldLabel="New Password" InputType="Password"
                        Anchor="60%" AllowBlank="False" IndicatorTip="It is required field" MsgTarget="Side"
                        MinLength="7" MinLengthText="Password length minimum: 7" MaxLength="15"
                        MaxLengthText="Password length maximum: 15">
												</ext:TextField>
                                <ext:Label ID="lblNewPasswordError" runat="server"  Margins="0 0 0 30" StyleSpec = "color:red"/>
                    </Items> 
					        </ext:Container>
												

                    <ext:Container ID="cntnrconfirmpassword" runat="server" Layout="HBoxLayout" >
                       <Items>
                    <ext:TextField ID="txtConfirmPassword" runat="server" FieldLabel="Confirm Password"
                        Vtype="password" InputType="Password" Anchor="60%" AllowBlank = "False" IndicatorTip="It is required field" MsgTarget="Side" 
												MinLength="7" MinLengthText="Password length minimum: 7" MaxLength="15"
                        MaxLengthText="Password length maximum: 15">
                    </ext:TextField>
                                    <ext:Label ID="lblConfirmPasswordError" runat="server" Margins="0 0 0 30" StyleSpec = "color:red" />
                       </Items>
                    </ext:Container>	
																				
										  <ext:Container ID="Container1" runat="server">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Middle" Pack="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Button runat="server" ID="BtnSave" Text="Save" Icon="Disk" Padding="5" OnDirectClick = "SavePassword"/>
														
                        </Items>
                    </ext:Container>

                </Items>
            </ext:FormPanel>
        </Items>
    </ext:Panel>
</asp:Content>
