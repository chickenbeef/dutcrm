<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="CRMUI.SupportAgent.CallHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <%--CALL Support Agent--%>
    <ext:Panel ID="pnlCallSupport" runat="server" MinHeight="615" Title="Home" TitleAlign="Center"
        Enabled="False" Visible="False">
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
                    <ext:Panel ID="pnlTopLeft" runat="server" Flex="1">
                        <LayoutConfig>
                            <ext:AccordionLayoutConfig />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel ID="pnlUsername" runat="server" Title="SEARCH BY USERNAME" Icon="PageMagnify">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" />
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Client Username" Width="300"
                                        Margins="0 0 0 30" />
                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10"
                                        Icon="Magnifier" />
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY CLIENT NAME" Icon="PageMagnify">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" />
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSName" FieldLabel="Client Name" Width="300"
                                        Margins="0 0 0 30" />
                                    <ext:Button runat="server" ID="btnNameSearch" Text="Search" Width="80" Margins="0 0 0 10"
                                        Icon="Magnifier" />
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <%--BOTTOM LEFT PANEL--%>
                    <ext:Panel ID="pnlBottomLeft" Title="Client Details" runat="server" Border="false"
                        Flex="3" Icon="Vcard">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch" />
                        </LayoutConfig>
                        <Items>
                            <ext:GridPanel ID="gpClient" runat="server" Flex="1">
                                <Store>
                                    <ext:Store ID="streClient" runat="server">
                                        <Model>
                                            <ext:Model ID="mdlClient" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="CLIENT_ID" />
                                                    <ext:ModelField Name="Name" />
                                                    <ext:ModelField Name="Surname" />
                                                    <ext:ModelField Name="UserName" />
                                                    <ext:ModelField Name="DateOfBirth" />
                                                    <ext:ModelField Name="Telephone" />
                                                    <ext:ModelField Name="Cell" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="txtCID" Text="Client ID" DataIndex="CLIENT_ID" />
                                        <ext:Column runat="server" ID="txtName" Text="Name" DataIndex="Name" />
                                        <ext:Column runat="server" ID="txtSurname" Text="Surname" DataIndex="Surname" />
                                        <ext:Column runat="server" ID="txtUsername" Text="Username" DataIndex="UserName" />
                                        <ext:DateColumn runat="server" ID="txtDateOfBirth" Text="Date Of Birth" Format="DD-MON-YYYY"
                                            DataIndex="DateOfBirth" />
                                        <ext:Column runat="server" ID="txtTelephone" Text="Telephone" DataIndex="Telephone" />
                                        <ext:Column runat="server" ID="txtCell" Text="Cell" DataIndex="Cell" />
                                    </Columns>
                                </ColumnModel>
                                <Buttons>
                                    <ext:Button runat="server" ID="btnAccept" Text="Accept" Padding="5" Icon="ArrowEw" />
                                </Buttons>
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <%--RIGHT PANEL--%>
            <ext:Panel ID="pnlRight" runat="server" Border="false" Flex="91">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--TOP RIGHT PANEL--%>
                    <ext:Panel runat="server" ID="pnlTopRight" Title="Create Ticket" Flex="2" CollapseDirection="Top"
                        Collapsible="True" Icon="TagBlueAdd">
                        <Items>
                            <ext:FormPanel runat="server" ID="FormPanel1" Border="false" Padding="5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtEPId" InputType="Hidden" />
                                    <%--add to main project--%>
                                    <ext:TextField runat="server" ID="txtClientId" InputType="Hidden" />
                                    <ext:TextField runat="server" ID="txtEmployeeId" InputType="Hidden" />
                                    <ext:TextField runat="server" ID="txtClientName" FieldLabel="Client Name" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtEmployeeName" FieldLabel="Employee Name" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtProblemId" FieldLabel="Problem ID" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtSolutionId" FieldLabel="Solution ID" AnchorHorizontal="70%" />
                                    <ext:ComboBox runat="server" ID="cmbPriority" FieldLabel="Priority" AnchorHorizontal="40%" />
                                </Items>
                                <Buttons>
                                    <ext:Button runat="server" ID="btnCreateTicket" Text="Create Ticket" Padding="5"
                                        Margins="0 5 0 0" Icon="Disk">
                                    </ext:Button>
                                </Buttons>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                    <%--BOTTOM RIGHT PANEL--%>
                    <ext:Panel runat="server" ID="pnlBottomRight" Title="Search For Solution" Flex="3"
                        Icon="BookMagnify">
                        <Items>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
    <%--EMAIL Support Agent--%>
    <ext:Panel ID="pnlEmailSupport" runat="server" MinHeight="615" Title="Home" TitleAlign="Center"
        Enabled="False" Visible="False">
        <LayoutConfig>
            <ext:TableLayoutConfig Columns="2"/>
        </LayoutConfig>
        <Items>
           <ext:GridPanel runat="server" ID="pnlETopLeft" Title="Email Problems Inbox" Icon="Email" Collapsible="True" CollapseDirection="Top">
               <Store>
                   <ext:Store runat="server" ID="streEmailProbs" PageSize="5000" Buffered="True" IgnoreExtraFields="True">
                       <Model>
                           <ext:Model runat="server">
                               <Fields>
                                   <ext:ModelField Name="From"/>
                                   <ext:ModelField Name="Subject"/>
                                   <ext:ModelField Name="Description"/>
                                   <ext:ModelField Name="DateCreated"/>
                                   <ext:ModelField Name="CLIENT_ID"/>
                               </Fields>
                           </ext:Model>
                       </Model>
                   </ext:Store>
               </Store>
               <ColumnModel>
                   <Columns>
                       <ext:RowNumbererColumn runat="server" Width="20" Sortable="False"/>
                       <ext:Column runat="server" Text="From" Width="150" DataIndex="From"/>
                       <ext:Column runat="server" Text="Subject" Width="300" DataIndex="Subject"/>
                       <ext:Column runat="server" Text="Date Created" DataIndex="DateCreated"/>
                   </Columns>
               </ColumnModel>
           </ext:GridPanel>
        </Items>
    </ext:Panel>
</asp:Content>
