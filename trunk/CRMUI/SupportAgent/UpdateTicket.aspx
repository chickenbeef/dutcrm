<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true"
    CodeBehind="UpdateTicket.aspx.cs" Inherits="CRMUI.SupportAgent.UpdateTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="615" MaxWidth="1349" Title="Update Ticket" TitleAlign="Center">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch"/>
        </LayoutConfig>
        <Items>
             <%--LEFT PANEL--%>
            <ext:Panel ID="pnlSearch" runat="server" Flex="100" Title="Search Tickets" Icon="BookMagnify" Collapsible="True" CollapseDirection="Left">
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
                            <ext:Panel ID="pnlTicketId" runat="server" Title="SEARCH BY TICKET ID" Icon="PageMagnify" Collapsed="False">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSCPRId" FieldLabel="Ticket ID" Width="300" Margins="0 0 0 30"/>
                                    <ext:Button runat="server" ID="btnCPRIdSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier"/>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlUsername" runat="server" Title="SEARCH BY USERNAME" Icon="PageMagnify" Collapsed="True">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle"/>
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Client Username" Width="300" Margins="0 0 0 30"/>
                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10" Icon="Magnifier"/>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY CLIENT NAME" Icon="PageMagnify" Collapsed="True">
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
                    <ext:Panel ID="pnlBottomLeft" Title="Ticket Details" runat="server" Border="false" Flex="3" Icon="TagBlueDelete">
                        <Content>
                            <%--TO DO: ClientProblemsLog Design here--%>
                        </Content>
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
                    <ext:Panel runat="server" ID="pnlTopRight" Title="Update Ticket" Flex="100" CollapseDirection="Top"
                        Collapsible="True" Icon="TagBlueEdit">
                        <Items>
                            <ext:FormPanel runat="server" ID="FormPanel1" Border="false" Padding="5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtClientId" InputType="Hidden" />
                                    <ext:TextField runat="server" ID="txtEmployeeId" InputType="Hidden" />
                                    <ext:TextField runat="server" ID="txtCPRId" InputType="Hidden"/>
                                    <ext:TextField runat="server" ID="txtClientName" FieldLabel="Client Name" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtEmployeeName" FieldLabel="Employee Name" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtProblemId" FieldLabel="Problem ID" AnchorHorizontal="70%" />
                                    <ext:TextField runat="server" ID="txtSolutionId" FieldLabel="Solution ID" AnchorHorizontal="70%" />
                                    <ext:TextArea runat="server" ID="taNotes" FieldLabel="Notes" EmptyText="Enter any notes here" AnchorHorizontal="70%"/>
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
                    <ext:Panel runat="server" ID="pnlBottomRight" Title="Search For Solution" Flex="97" Icon="BookMagnify">
                        <Items>
                            <%--TO DO: Solutions/Problems design here--%>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
