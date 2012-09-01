<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="CRMUI.SupportAgent.CallHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <%--CALL Support Agent--%>
    <ext:Panel ID="pnlCallSupport" runat="server" Height="615" MaxWidth="1349" Title="Home" TitleAlign="Center"
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
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Client Username" Width="300" Margins="0 0 0 30"/>
                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier"/>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY CLIENT NAME" Icon="PageMagnify">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSName" FieldLabel="Client Name" Width="300" Margins="0 0 0 30"/>
                                    <ext:Button runat="server" ID="btnNameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier"/>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <%--BOTTOM LEFT PANEL--%>
                    <ext:Panel ID="pnlBottomLeft" Title="Client Details" runat="server" Border="false" Flex="3" Icon="Vcard">
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
                                                    <ext:ModelField Name="UserName"/>
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
                                        <ext:Column runat="server" ID="txtUsername" Text="Username" DataIndex="UserName"/>
                                        <ext:DateColumn runat="server" ID="txtDateOfBirth" Text="Date Of Birth" Format="DD-MON-YYYY" DataIndex="DateOfBirth"/>
                                        <ext:Column runat="server" ID="txtTelephone" Text="Telephone" DataIndex="Telephone"/>
                                        <ext:Column runat="server" ID="txtCell" Text="Cell" DataIndex="Cell"/>
                                    </Columns>
                                </ColumnModel>
                                <Buttons>
                                    <ext:Button runat="server" ID="btnAccept" Text="Accept" Padding="5" Icon="ArrowEw"/>
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
                    <ext:Panel runat="server" ID="pnlBottomRight" Title="Search For Solution" Flex="3" Icon="BookMagnify">
                        <Items>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>

    <%--EMAIL Support Agent--%>
    <ext:Panel ID="pnlEmailSupport" runat="server" Height="615" MaxWidth="1349" Title="Home" TitleAlign="Center"
        Enabled="False" Visible="False">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <%--LEFT PANEL--%>
            <ext:Panel runat="server" ID="pnlLeft2" Flex="1" Title="Client Problems" CollapseDirection="Left" Collapsible="True" AutoScroll="True" Icon="EmailGo">
                <Content>
                    <asp:Repeater ID="rptrEmailProblems" runat="server">
                        <ItemTemplate>
                            <ext:FormPanel ID="pnlProb" runat="server" BodyPadding="10" Border="false" Collapsible="True" Collapsed="True"
                                           Title='<%# Eval("EP_ID") + " - " + Eval("DateCreated") %>'>  
                                <Items>
                                    <ext:Label runat="server" ID="lblClientID" Html='<%# "<b>Client ID: " + Eval("CLIENT_ID") + "</b><hr/>" %>' />
                                    <ext:FieldSet ID="FieldSet1" runat="server" Title="Description">
                                        <Items>
                                            <ext:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                                <Buttons>
                                    <ext:Button runat="server" ID="btnLog" Text="Log Problem" Padding="5" Margins="0 3 10 0" Icon="ArrowEw">
                                    </ext:Button>
                                </Buttons>
                            </ext:FormPanel>
                        </ItemTemplate>
                    </asp:Repeater>
                </Content>
            </ext:Panel>
            <%--RIGHT PANEL--%>
            <ext:Panel ID="pnlRight2" runat="server" Border="false" Flex="1">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--TOP RIGHT PANEL--%>
                    <ext:Panel runat="server" ID="pnlTopRight2" Title="Create Ticket" Flex="2" CollapseDirection="Top"
                        Collapsible="True" Icon="TagBlueAdd">
                        <Items>
                            <ext:FormPanel runat="server" ID="FormPanel2" Border="false" Padding="5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtClientId2" InputType="Hidden" />
                                    <ext:TextField runat="server" ID="txtEmployeeId2" InputType="Hidden" />
                                    <ext:TextField runat="server" ID="txtClientName2" FieldLabel="Client Name" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtEmployeeName2" FieldLabel="Employee Name" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtProblemId2" FieldLabel="Problem ID" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtSolutionId2" FieldLabel="Solution ID" AnchorHorizontal="70%" />
                                    <ext:ComboBox runat="server" ID="cmbPriority2" FieldLabel="Priority" AnchorHorizontal="40%" />
                                </Items>
                                <Buttons>
                                    <ext:Button runat="server" ID="btnCreateTicket2" Text="Create Ticket" Padding="5"
                                        Margins="0 5 0 0" Icon="Disk">
                                    </ext:Button>
                                </Buttons>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                    <%--BOTTOM RIGHT PANEL--%>
                    <ext:Panel runat="server" ID="pnlBottomRight2" Title="Search For Solution" Flex="3" Icon="BookMagnify">
                        <Items>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
