<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="MessagingServices.aspx.cs" Inherits="CRMUI.CallCentreManager.MessagingServices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManagerMessaginServices" runat="server" Theme= "Default"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    

    <ext:Panel ID="pnlMain" runat="server" Height="615" Title="Messaging Services" >
         <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
       <Items>
           <%--LEFT PANEL FOR CONFIGURE MESSAGE--%>
           <ext:Panel ID="Panel1" runat="server" Height="615" Title="Message Settings" Layout="HBoxLayout" Flex="50" Padding="2" Icon="Mail">
            <Items>
            <ext:Panel id="pnlSub1" Height="614" runat="server" Layout="vboxLayout" Flex="2" Border="false">
            <Items >
                <ext:Panel ID="pnlDetails" runat="server"  Height="314" Flex="2" Layout="vboxLayout" border="false">
                    <Items >
                            <ext:ComboBox ID="cmbComTemplate" runat="server"  FieldLabel="Select a template" Margins="10 0 10 0"
                                    DisplayField="Name" ValueField="Paragraph" AllowBlank="False" Editable="False" >
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
                            
                            <ext:HtmlEditor ID="editrPara" runat="server" Margins="10 0 10 30" Width="610" Height="250"/>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnlbuttons" runat="server" Layout="hboxLayout" Height="300" Flex="1" Border="false" >
                    <Items >
                        <ext:Button ID="btnSave" runat="server" Text="Save" Margins="10 0 0 135" Padding="5" Width="50" OnDirectClick="SendMessage" />
                        <ext:Button ID="btnCancel" runat="server" Text="Cancel" Padding="5" Margins="10 0 0 10" />  
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
                            <ext:GridPanel ID="gpClient" runat="server" Flex="1" >
                               
                                <Store>
                                    <ext:Store ID="streClient" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlClient" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="CLIENT_ID"/>
                                                    <ext:ModelField Name="Name"/>
                                                    <ext:ModelField Name="Surname"/>
                                                    <ext:ModelField Name="DateOfBirth"/>
                                                    <ext:ModelField Name="Telephone"/>
                                                    <ext:ModelField Name="Cell"/>
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="txtCID" Text="Client ID" DataIndex="CLIENT_ID"/>
                                        <ext:Column runat="server" ID="txtName" Text="Name" DataIndex="Name"/>
                                        <ext:Column runat="server" ID="txtSurname" Text="Surname" DataIndex="Surname"/>
                                        
                                        <ext:DateColumn runat="server" ID="txtDateOfBirth" Text="Date Of Birth" Format="DD-MON-YYYY" DataIndex="DateOfBirth"/>
                                        <ext:Column runat="server" ID="txtTelephone" Text="Telephone" DataIndex="Telephone"/>
                                        <ext:Column runat="server" ID="txtCell" Text="Cell" DataIndex="Cell"/>
                                        <ext:CheckColumn runat="server" ID="checkc" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    
                                    <%--<ext:CheckboxSelectionModel runat="server" Mode="Multi"/>--%>
                                    <ext:RowSelectionModel runat="server" Mode="Multi"/>
                                </SelectionModel>
                                <Buttons>
                                    <ext:Button runat="server" ID="btnSend" Text="Send" Padding="5" Icon="ArrowEw"/>
                                </Buttons>
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                    
                    <%--END OF RIGHT PANEL--%>
                </Items>
            </ext:Panel>
</asp:Content>
