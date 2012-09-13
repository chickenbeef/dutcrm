﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="MessagingServices.aspx.cs" Inherits="CRMUI.CallCentreManager.MessagingServices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManagerMessaginServices" runat="server" Theme= "Default"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    

    <ext:Panel ID="pnlMain" runat="server" Height="610" Title="Messaging Services" >
         <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
       <Items>
           <%--LEFT PANEL FOR CONFIGURE MESSAGE--%>
           <ext:Panel ID="Panel1" runat="server" Height="615" Title="Message Settings" Layout="HBoxLayout" Flex="50" Padding="2" Icon="Mail">
            <Items>
            <ext:Panel id="pnlSub1" Height="614" runat="server" Layout="vboxLayout" Flex="2" Border="false">
            <Items >
                
                            <ext:ComboBox ID="cmbComTemplate" runat="server"  FieldLabel="Select a template" Margins="10 0 10 20"
                                    DisplayField="Name" ValueField="Paragraph" AllowBlank="False" Editable="False" LabelAlign="Top">
                                   <Store>
                                    <ext:Store ID="streComTemplate" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlComTemplate" runat="server">
                                                <fields>
                                                    <ext:ModelField Name="CT_ID"/>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Paragraph"/>
                                                </fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                   </Store>
                                    <listeners>
                                       <Select Handler="#{editrPara}.setValue(#{cmbComTemplate}.getValue());"/>
                                   </listeners>
                                </ext:ComboBox>
                            <ext:HtmlEditor ID="editrPara" runat="server" Margins="10 0 10 15" Width="610" Height="250"/>
                
                <ext:Panel ID="pnlbuttons" runat="server" Layout="hboxLayout" Height="300" Flex="1" Border="false" >
                    <Items >
                        <ext:Button ID="btnSave" runat="server" Text="Save" Margins="10 0 0 135" Padding="5" Width="50" />
                        <ext:Button ID="btnCancel" runat="server" Text="Cancel" Padding="5" Margins="10 0 0 20" />  
                        <ext:Label runat="server" ID="lblnames"></ext:Label>
                    </Items>

                </ext:Panel>
            </Items>   
          </ext:Panel> 
       </Items>
    </ext:Panel>
            <%--RIGHT PANEL FOR RECIPIENTS--%>
           <ext:Panel ID="pnlMessageClients" Title="Client Details" runat="server" Border="false" Flex="50" Icon="Vcard" Padding="2" >
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch"/>
                        </LayoutConfig>
                        <Items>
                            <ext:GridPanel ID="gpClient" runat="server" Flex="2">
                               
                                <Store>
                                    <ext:Store ID="streClient" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlClient" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Surname"/>
                                                    <ext:ModelField Name="DateOfBirth"/>
                                                    <ext:ModelField Name="Cell"/>
                                                    <ext:ModelField Name="DateCreated"/>
                                                    <ext:ModelField Name="Email"/>
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="clmnName" Text="Name" DataIndex="Name"/>
                                        <ext:Column runat="server" ID="clmnSurname" Text="Surname" DataIndex="Surname"/>
                                        <ext:Column runat="server" ID="clmnEmail" Text="Email Address" DataIndex="Email" MinWidth="180"/>
                                        <ext:DateColumn runat="server" ID="clmnDateOfBirth" Text="Date Of Birth" DataIndex="DateOfBirth" Width="75"/>
                                        <ext:Column runat="server" ID="clmnCell" Text="Cell" DataIndex="Cell" Width="72"/>
                                        <ext:DateColumn runat="server" ID="clmnDateCreated" Text="Date Created" DataIndex="DateCreated"/>
                                        
                                    </Columns>
                                </ColumnModel>
                                
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" Mode="Multi"/>
                                </SelectionModel>
                                
                               
                                <Buttons>
                                    <ext:Button runat="server" Margins="2 300 2 5" ID="btnSend" Text="Send" Padding="5" Icon="ArrowEw" Flex="1" MaxWidth="80">
                                        <DirectEvents>
                                            <Click OnEvent="SendMessage">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Values" Value="Ext.encode(#{gpClient}.getRowsValues({selectedOnly:true}))" Mode="Raw"/>
                                                </ExtraParams>
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Buttons>
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                    
                    <%--END OF RIGHT PANEL--%>
                </Items>
            </ext:Panel>
</asp:Content>
