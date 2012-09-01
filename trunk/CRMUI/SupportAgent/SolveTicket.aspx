<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true"
    CodeBehind="SolveTicket.aspx.cs" Inherits="CRMUI.SupportAgent.SolveTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="615" MaxWidth="1349" Title="Solve Ticket">
        <LayoutConfig>
            <ext:HBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <%--LEFT PANEL--%>
            <ext:Panel ID="pnlBottomLeft" Title="Unsolved Tickets" runat="server"
                Flex="1" Icon="TagBlueDelete">
                <Content>
                    <%--TO DO: ClientProblemsLog Design here--%>
                </Content>
            </ext:Panel>
            <%--RIGHT PANEL--%>
            <ext:Panel runat="server" ID="pnlRightPanel" Title="Add solution to ticket" Flex="1" CollapseDirection="Right" Collapsible="True" Icon="LightbulbAdd">
                <Items>
                    <ext:FormPanel runat="server" Border="false" BodyPadding="20" ButtonAlign="Left" >
                        <Items>
                            <ext:TextField runat="server" ID="txtCPRId" InputType="Hidden" />
                            <ext:TextField runat="server" ID="txtClientId" InputType="Hidden" />
                            <ext:TextField runat="server" ID="txtEmployeeId" InputType="Hidden" />
                            <ext:TextField runat="server" ID="txtProblemId"  InputType="Hidden" />
                            <ext:TextField runat="server" ID="txtClientName" FieldLabel="Client Name" AnchorHorizontal="70%" />
                            <ext:TextArea runat="server" ID="taSolutionDescription" FieldLabel="Solution" EmptyText="Enter solution to problem here" Height="200" AnchorHorizontal="70%" />
                            <ext:TextArea runat="server" ID="taNotes" FieldLabel="Notes" EmptyText="Enter any notes here"
                                AnchorHorizontal="70%" />
                        </Items>
                        <Buttons>
                            <ext:Button runat="server" ID="btnCreateTicket" Text="Submit" Padding="5"
                                Margins="0 0 0 119" Icon="Disk">
                            </ext:Button>
                        </Buttons>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
</asp:Content>
