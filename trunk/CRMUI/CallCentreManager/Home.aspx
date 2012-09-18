<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRMUI.CallCentreManager.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default"/>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="cphCallCMBody">
    <ext:Panel ID="pnlMain" runat="server" Height="615"  MaxWidth="1349" Title="Update Roles">       
        
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" DefaultMargins="2" />
        </LayoutConfig>
        <Items>
            
            <ext:Panel ID="pnlSearch" runat="server" Flex="100" Title="Search Employee" Icon="UserMagnify">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <ext:Panel ID="Panel1" runat="server" Flex="1">
                        <LayoutConfig>
                            <ext:AccordionLayoutConfig />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel ID="pnlUsername" runat="server" Title="SEARCH BY USERNAME" Icon="PageMagnify">
                                  <Defaults>
                            		<ext:Parameter Name = "AllowBlank" Value = "false" Mode = "Raw"/>
                            		<ext:Parameter Name = "msgTarget" Value = "side"/>	
                            	</Defaults>

                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Employee Username" Width="300" Margins="0 0 0 30" BlankText = "Please Enter Client Username" MinLength = 3 MaxLength = 13>
                                     
                                        <Listeners>
                                            <ValidityChange Handler = "#{btnUsernameSearch}.setDisabled(!isValid)"></ValidityChange>
                                        </Listeners>
                                    
                                    </ext:TextField>

                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier" OnDirectClick = "SearchByUserName" Disabled = "True"/>
                                </Items>
                            </ext:Panel>

                            <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY NAME" Icon="PageMagnify">
                                 <Defaults>
                            		<ext:Parameter Name = "AllowBlank" Value = "false" Mode = "Raw"/>
                            		<ext:Parameter Name = "msgTarget" Value = "side"/>	
                            	</Defaults>
                                
                               
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSName" FieldLabel="Employee Name" Width="300" Margins="0 0 0 30" BlankText = "Please Enter Name" MinLength = 3 MaxLength = 13>
                                    
                                         <Listeners>
                                            <ValidityChange Handler = "#{btnNameSearch}.setDisabled(!isValid)"></ValidityChange>
                                        </Listeners>
                                    
                                    </ext:TextField>
                                    <ext:Button runat="server" ID="btnNameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier" OnDirectClick = "SearchByName" Disabled = "True" />
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <%--Employee details populated in panel below--%>

                    <ext:Panel Title="Employee Details" runat="server" Border="false" Flex="3" Icon="Vcard">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch"/>
                        </LayoutConfig>
                        <Items>
                            <ext:GridPanel ID = "gpEmployees" runat="server" Flex="1" >
                                <Store>
                                    <ext:Store ID="streEmployee"  runat="server">
                                        <Model>
                                            <ext:Model ID="mdlEmployee" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="EMP_ID"/>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Surname"/>
                                                    <ext:ModelField Name="UserName"/>
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="txtEMP_ID" DataIndex = "EMP_ID" Text = "EMPID"/>
                                        <ext:Column runat="server" ID="txtName" DataIndex = "Name" Text="Name"/>
                                        <ext:Column runat="server" ID="txtSurname" DataIndex = "Surname" Text="Surname"/>
                                        <ext:Column runat="server" ID="txtUsername" DataIndex = "UserName" Text="UserName"/>
                                    </Columns>
                                </ColumnModel>
                                
                                	<SelectionModel>
											<ext:RowSelectionModel ID="RowSelectionModel1" runat="server"  Mode = "Single">
												
												   <DirectEvents>
																		 	
										                 <Select onEvent = "PassValue">
														           <ExtraParams>
													                        <ext:Parameter Name = "Values" Value = "Ext.encode(#{gpEmployees}.getRowsValues({selectedOnly:true}))" Mode = "Raw" />
													             </ExtraParams>
										                </Select>

							                    </DirectEvents>
												
											</ext:RowSelectionModel>

								</SelectionModel>

                                

                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <%--Role settings Panel --%>
            <ext:Panel ID="pnlSetRole" runat="server" Flex="90" Title="Set employee role" Icon="Wrench">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig/>
                </LayoutConfig>
                <Items>
                    
                    <ext:TextField runat="server" ID="txtEmpId" FieldLabel="Employee ID" ReadOnly="True" Margins="10 0 10 30" Width="300"/>
                    
                    <ext:TextField runat="server" ID="txtCurrentRole" FieldLabel="Current Role" ReadOnly="True" Margins="10 0 10 30" Width="300"/>
                    
                    <ext:TextField runat="server" ID="txtUname" FieldLabel= "Value" ReadOnly="True" Margins="10 0 10 30" Width="300"/>

                    <ext:FieldSet runat="server" Title="Current role is selected" Margins="10 0 10 135" Width="350">
                        <Items>
                                <ext:RadioGroup ID="radGrpRoles" FieldLabel="Please select a new role" runat="server"   GroupName="Available Roles" Height="250" ColumnsNumber="1" Layout="AnchorLayout" DefaultAnchor="100%" OnDirectChange = "ChangeRoleValue">
                                    <Items>
                                        <ext:Radio runat="server" ID="radbtnEmailSupport" FieldLabel="Email Support Agent" Name="Roles" />
                                        <ext:Radio runat="server" ID="radbtnCallSupport" FieldLabel="Call Support Agent" Name="Roles" />
                                        <ext:Radio runat="server" ID="radbtnRM" FieldLabel="Relationship Manager" Name="Roles" />
                                        <ext:Radio runat="server" ID="radbtnSalesManager" FieldLabel="SalesManager" Name="Roles"/>
                                    </Items>
                                </ext:RadioGroup>
                    </Items>
                    </ext:FieldSet>
                    <ext:Button runat="server" ID="btnUpdateRole" Text="Update Role" Padding="5" Margins="0 0 0 135" Icon="PageSave" OnDirectClick = "UpdateRole"/>
                    
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>

