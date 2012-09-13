<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true" CodeBehind="UpdateSolutionReference.aspx.cs" Inherits="CRMUI.SupportAgent.UpdateSolutionReference" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default">
    </ext:ResourceManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
 	<ext:Panel ID="pnlMain" runat="server" Height="615" Title="Update Solution To Problem Database"> 
      <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <%--LEFT PANEL--%>
            <ext:Panel ID="pnlSrch" runat="server" Flex="100" Title="Search Problem" >
                    <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--TOP LEFT PANEL search BY accordion items--%>
                    <ext:Panel ID="pnlSearchby" runat="server" Flex="1" >
                           <LayoutConfig>
                            <ext:AccordionLayoutConfig HideCollapseTool="True" />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel ID="pnlProbDesc" runat="server" Title="SEARCH BY PROBLEM DESCRIPTION" Icon="PageMagnify">
                            	  <Defaults>
                            		<ext:Parameter Name = "AllowBlank" Value = "false" Mode = "Raw"/>
                            		<ext:Parameter Name = "msgTarget" Value = "side"/>	
																</Defaults>

                              <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtProbDesc" FieldLabel="Problem Description" Width="380" Margins="0 0 0 20" BlankText = "Please Enter Problem Description" LabelWidth="125" MinLength = "2" MaxLength = "30">
                                        <Listeners>
                                            <ValidityChange Handler = "#{btnProbDescSearch}.setDisabled(!isValid)"></ValidityChange>
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Button runat="server" ID="btnProbDescSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier" OnDirectClick = "SearchProb" Disabled = "True"/>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <%--BOTTOM LEFT PANEL PROBLEMS AND SOLUTIONS DISPLAY--%>
                <ext:Panel ID="Panel1" Title="Problems" runat="server" Border="false" Flex="3">
               					   																				                         		 			    
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch"/>
                        </LayoutConfig>
                        <Items>
                            <ext:GridPanel ID="gpProblems" runat="server" Flex="1" >
                                <Store>
                                    <ext:Store ID="streProblems" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlProblems" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name= "PROB_ID"/>
                                                    <ext:ModelField Name="Description"/>
                                                    <ext:ModelField Name="DateCreated"/>
                                                    <ext:ModelField Name="EMP_ID"/>
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="txtPROB_ID" Text="PROB_ID" DataIndex="PROB_ID" Width = 60 />
                                        <ext:Column runat="server" ID="txtDescription" Text="Description" DataIndex="Description" Width = 425 />
                                        <ext:DateColumn runat="server" ID="txtDateCreated" Text="Date Created" Format = "dd MMM yyyy" DataIndex="DateCreated" />
                                        <ext:Column runat="server" ID="EMPID" Text="EMP_ID" DataIndex="EMP_ID" Width = 55 />
                                    </Columns>
                                </ColumnModel>

																
										<SelectionModel>
											<ext:RowSelectionModel ID="RowSelectionModel2" runat="server"  Mode = "Single" >
												
												    <DirectEvents>
                                                     
                                  <Select OnEvent="PassValue">
                                                    
                                        <ExtraParams>
																					
													                     <ext:Parameter Name = "Values" Value = "Ext.encode(#{gpProblems}.getRowsValues({selectedOnly:true}))" Mode = "Raw" />

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
            <ext:Panel ID="pnlConfirmUpdate" runat="server" Flex="91" Title="Update Solution" Icon="Disk">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig/>
                </LayoutConfig>
                <Items>
                    <ext:TextField runat="server" ID="txtEmpId" FieldLabel="Employee ID" ReadOnly="True" Margins="30 0 10 30" Width="300" Hidden="True" />
										<ext:TextField runat="server" ID="txtprobid" FieldLabel="PROB_ID" ReadOnly="True" Margins="10 0 10 30" Width="300" Hidden="False" />

										<ext:ComboBox ID="cmbSolutions" runat="server"  FieldLabel="Solution ID" Margins="40 0 10 30"
                                    Editable="False" DisplayField = "SOL_ID" ValueField = "Description" OnDirectSelect = "PassDescription">
											
											<Store>
												   <ext:Store ID="strSolutions" runat="server">
                              <Model>
                                   <ext:Model ID="mdlSolutions" runat="server">
                                        <fields>
                                            <ext:ModelField Name="SOL_ID"/>
                                            <ext:ModelField Name="Description"/>
                                         </fields>
                                    </ext:Model>
                               </Model>
                           </ext:Store>
											</Store>
											
											  <Listeners>
                            <Change Handler = "#{newSolDesc}.setValue();"></Change>
                        </Listeners>
											
										</ext:ComboBox>


                    <ext:Label ID="Label1" runat="server" Text="Description:"  Margins="20 0 10 30"></ext:Label>
                    <ext:HtmlEditor ID="newSolDesc" runat="server" Margins="10 0 10 30" Width="500" Height="250" />
                    <ext:Button runat="server" ID="btnUpdate" Text="Update Solution" Padding="5" Margins="0 0 0 410" Icon="PageSave"  OnClick = "UpdateSolution" AutoPostBack = "True"/>
                </Items>
            </ext:Panel>
            
        </Items>
    </ext:Panel>
</asp:Content>
