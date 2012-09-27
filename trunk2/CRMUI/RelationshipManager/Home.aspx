<%@ Page Title="" Language="C#" MasterPageFile="~/RelationshipManager/RM.Master"
    AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRMUI.RelationshipManager.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="612" Title="Record Sale" MaxWidth="1349">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <%--LEFT PANEL--%>
            <ext:Panel ID="pnlSearch" runat="server" Flex="100" Title="Search Client" Icon="UserMagnify">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--TOP LEFT PANEL--%>
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
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Client Username" Width="300" Margins="0 0 0 30" BlankText = "Please Enter Client Username" MinLength = "3" MaxLength = "13">
                                         <Listeners>
                                            <ValidityChange Handler = "#{btnUsernameSearch}.setDisabled(!isValid)"></ValidityChange>
                                        </Listeners>
                                    </ext:TextField>

                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier" OnDirectClick = "SearchByUserName" Disabled = "True">             
                                   </ext:Button>
                                  

                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY CLIENT NAME" Icon="PageMagnify">
                            	 <Defaults>
                            		<ext:Parameter Name = "AllowBlank" Value = "false" Mode = "Raw"/>
                            		<ext:Parameter Name = "msgTarget" Value = "side"/>	
                            	</Defaults>
																		  
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSName" FieldLabel="Client Name" Width="300" Margins="0 0 0 30" BlankText = "Please Enter Name" MinLength = "3" MaxLength = "13">                                
                                        <Listeners>
                                            <ValidityChange Handler = "#{btnNameSearch}.setDisabled(!isValid)"></ValidityChange>
                                        </Listeners>
                                        
                                    </ext:TextField>
                                    <ext:Button runat="server" ID="btnNameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier" OnDirectClick = "SearchByName" Disabled = "True"  />
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <%--BOTTOM LEFT PANEL--%>
                    <ext:Panel Title="Client Details" runat="server" Border="false" Flex="3" Icon="Vcard">
                    	       <Defaults>
                            		<ext:Parameter Name = "AllowBlank" Value = "false" Mode = "Raw"/>
                            		<ext:Parameter Name = "msgTarget" Value = "side"/>	
                            	</Defaults>

											
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch"/>
                        </LayoutConfig>
                        <Items>
                            <ext:GridPanel ID="gpClient" runat="server" Flex="1">

                                <Store>
                                    <ext:Store ID="streClient" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlClient" runat="server" >
                                                <Fields>
                                                    <ext:ModelField Name="CLIENT_ID"/>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Surname"/>
                                                    <ext:ModelField Name="UserName"/>
                                                    <ext:ModelField Name="Telephone"/>
                                                    <ext:ModelField Name="Cell"/>
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="txtCID" Text="Client ID" DataIndex="CLIENT_ID" Width = "65"/>
                                        <ext:Column runat="server" ID="txtName" Text="Name" DataIndex="Name"/>
                                        <ext:Column runat="server" ID="txtSurname" Text="Surname" DataIndex="Surname"/>
                                        <ext:Column runat="server" ID="txtUsername" Text="Username" DataIndex="UserName"/>
                                        <ext:Column runat="server" ID="txtTelephone" Text="Telephone" DataIndex="Telephone" Width = "80"/>
                                        <ext:Column runat="server" ID="txtCell" Text="Cell" DataIndex="Cell" Width = "80" />
                                    </Columns>
                                </ColumnModel>
																
										<SelectionModel>
											<ext:RowSelectionModel runat="server"  Mode = "Single"  >
												
												          <DirectEvents>
																		 	
										                 <Select onEvent = "PassValue">
														           <ExtraParams>
																					
													               <ext:Parameter Name = "Values" Value = "Ext.encode(#{gpClient}.getRowsValues({selectedOnly:true}))" Mode = "Raw" />

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
            <%--RIGHT PANEL--%>
            <ext:Panel ID="pnlConfirm" runat="server" Flex="91" Title="Confirm Sale" Icon="Disk">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig/>
                </LayoutConfig>
                <Items>
                	<ext:TextField runat="server" ID = "txtClientname" FieldLabel = "Client Name" Margins = "30 0 10 30" Width = "300" ReadOnly = "True"/>
                	<ext:TextField runat="server" ID = "txtEmpName" FieldLabel = "Employee Name" Margins = "30 0 10 30" Width = "300" ReadOnly = "True"/>
                    <ext:TextField runat="server" ID="txtClientId" FieldLabel="Client ID" Margins="30 0 10 30" Width="300" ReadOnly="True"    Hidden="True" />
                    <ext:TextField runat="server" ID="txtEmpId" FieldLabel="Employee ID" Margins="10 0 10 30" Width="300" ReadOnly = "True"  Hidden="True" />
                    <ext:Button runat="server" ID="btnConfirm" Text="Record Sale" Padding="5" Margins="10 0 0 236" Icon="PageSave" OnDirectClick = "SaveSaves"/>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
