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
            <ext:GridPanel ID="gpTickets" Title="Unsolved Tickets" SortableColumns="True" AutoScroll="True" runat="server"
                Flex="1" Icon="TagBlueDelete">
                <ToolTips>
                    <ext:ToolTip runat="server" Html="Select a ticket here that you have a solution to"/>
                </ToolTips>
                <Store>
                    <ext:Store runat="server" ID="streTickets" PageSize="500" Buffered="True" IgnoreExtraFields="True">
                        <Model>
                            <ext:Model runat="server">
                                <Fields>
                                    <ext:ModelField Name="CPR_ID" />
                                    <ext:ModelField Name="DateCreated" />
                                    <ext:ModelField Name="EMP_ID" />
                                    <ext:ModelField Name="EmployeeName" />
                                    <ext:ModelField Name="EmployeeSurname" />
                                    <ext:ModelField Name="PROB_ID" />
                                    <ext:ModelField Name="ProblemDescription" />
                                    <ext:ModelField Name="Priority" />
                                    <ext:ModelField Name="CLIENT_ID" />
                                    <ext:ModelField Name="ClientName" />
                                    <ext:ModelField Name="ClientSurname" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel>
                    <Columns>
                        <ext:RowNumbererColumn runat="server" Width="30" />
                        <ext:DateColumn runat="server" Text="Date Created" DataIndex="DateCreated" Format="dd-MMM-yy"
                            Width="75" />
                        <ext:Column runat="server" Text="Client Name" DataIndex="ClientName" Width="75" />
                        <ext:Column runat="server" Text="Client Surname" DataIndex="ClientSurname" Width="85" />
                        <ext:Column runat="server" Text="Description" DataIndex="ProblemDescription" Width="135" />
                        <ext:Column runat="server" Text="Employee Name" DataIndex="EmployeeName" Width="90" />
                        <ext:Column runat="server" Text="Employee Surname" DataIndex="EmployeeSurname" Width="105" />
                        <ext:Column runat="server" Text="Priority" DataIndex="Priority" Width="75" />
                    </Columns>
                </ColumnModel>
                <DirectEvents>
                    <Select OnEvent="GpTicketSelected">
                        <ExtraParams>
                            <ext:Parameter Name="cprid" Value="record.data.CPR_ID" Mode="Raw" />
                        </ExtraParams>
                    </Select>
                </DirectEvents>
            </ext:GridPanel>
            <%--RIGHT PANEL--%>
            <ext:FormPanel runat="server" ID="pnlRightPanel" Title="Add solution to ticket" Flex="1"
                CollapseDirection="Right" Collapsible="True" Icon="LightbulbAdd" BodyPadding="10"
                 AutoScroll="True">
                <Items>
                    <ext:Hidden runat="server" ID="hCPRId" />
                    <ext:Hidden runat="server" ID="hClientId" />
                    <ext:Hidden runat="server" ID="hProbId" />
                    <ext:FieldSet runat="server" ID="fsProbDesc" Title="Problem Description" AnchorHorizontal="100%">
                        <Items>
                            <ext:Label runat="server" ID="lblProbDesc" />
                            <ext:Label ID="Label1" runat="server" Html="<hr/>" />
                            <ext:Button runat="server" ID="btnViewImages" Text="ViewImages" Disabled="True" OnDirectClick="BtnViewImagesClick">
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="View images related to this problem"/>
                                </ToolTips>
                            </ext:Button>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet runat="server" ID="fsExtraDetails" Title="Client Contact Details" AnchorHorizontal="100%">
                        <Items>
                            <ext:Label runat="server" ID="lblTelephone" />
                            <ext:Label runat="server" ID="lblCell" />
                            <ext:Label runat="server" ID="lblFax" />
                        </Items>
                    </ext:FieldSet>
                    <ext:HtmlEditor runat="server" ID="heSolutionDesc" FieldLabel="Solution Description"
                        LabelAlign="Top" AnchorHorizontal="100%">
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="Type your solution here and click submit to solve the ticket"/>
                        </ToolTips>
                    </ext:HtmlEditor>
                    <ext:Label runat="server" Html="<hr/>"></ext:Label>
                    <ext:Button runat="server" ID="btnSolveTicket" OnDirectClick="BtnSolveTicketClick"
                        Text="Submit" Icon="Disk">
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="When clicked, the typed solution is added to the database aswell."/>
                        </ToolTips>
                    </ext:Button>
                </Items>
            </ext:FormPanel>
        </Items>
    </ext:Panel>
    <%--POPUP VIEW IMAGES--%>
    <ext:Window runat="server" ID="wndImageViewer" Title="View Images" Icon="EmailOpenImage"
        MinWidth="800" MinHeight="600" AutoScroll="True" Hidden="True" Maximizable="true">
    </ext:Window>
    <%--POPUP SEND EMAIL--%>
    <ext:Window runat="server" ID="wndSendEmail" Width="800" Height="600" BodyPadding="20"
        Hidden="True" Maximizable="true" Icon="EmailGo" Title="Send Email">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Border="false" Frame="True" BodyPadding="20">
                <Items>
                    <ext:Hidden runat="server" ID="hEClientId" />
                    <ext:Hidden runat="server" ID="hEProbDesc" />
                    <ext:ComboBox runat="server" DisplayField="Name" ValueField="CAT_ID" ID="cmbCategory"
                        LabelWidth="70" AllowBlank="False" FieldLabel="Category" Text="Choose a Category.."
                        AnchorHorizontal="30%">
                        <ToolTips>
                            <ext:ToolTip ID="ToolTip1" runat="server" Html="Select a template category"/>
                        </ToolTips>
                        <Store>
                            <ext:Store runat="server" ID="streCategories">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="CAT_ID" />
                                            <ext:ModelField Name="Name" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <DirectEvents>
                            <Select OnEvent="CmbCategorySelectedItem">
                            </Select>
                        </DirectEvents>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cmbTemplate" DisplayField="Name" ValueField="Paragraph"
                        AllowBlank="False" LabelWidth="70" FieldLabel="Template" Text="Choose a Template.."
                        Disabled="True" AnchorHorizontal="50%">
                        <ToolTips>
                            <ext:ToolTip ID="ToolTip2" runat="server" Html="Select a template from the chosen category<br/>to populate the body with."/>
                        </ToolTips>
                        <Store>
                            <ext:Store ID="streTemplates" runat="server">
                                <Model>
                                    <ext:Model ID="Model2" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="Name" />
                                            <ext:ModelField Name="Paragraph" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <DirectEvents>
                            <Select OnEvent="CmbTemplateSelectedItem">
                            </Select>
                        </DirectEvents>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtSubject" AllowBlank="False" LabelWidth="70"
                        FieldLabel="Subject" AnchorHorizontal="70%">
                        <ToolTips>
                            <ext:ToolTip ID="ToolTip3" runat="server" Html="Subject of the email"/>
                        </ToolTips>
                    </ext:TextField>
                    <ext:HtmlEditor runat="server" ID="heEmailBody" LabelWidth="70" Height="365" FieldLabel="Body"
                        AnchorHorizontal="100%">
                        <ToolTips>
                            <ext:ToolTip ID="ToolTip4" runat="server" Html="Body of the email"/>
                        </ToolTips>
                    </ext:HtmlEditor>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSendEmail" Text="Send" Icon="EmailStart" Margins="0 12 0 0">
                        <ToolTips>
                            <ext:ToolTip ID="ToolTip5" runat="server" Html="send the email to the client"/>
                        </ToolTips>
                        <DirectEvents>
                            <Click OnEvent="BtnSendEmailClick">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
        <DirectEvents>
            <Close OnEvent="WndSendEmailClose">
            </Close>
            <Show OnEvent="WndSendEmailShow">
            </Show>
        </DirectEvents>
    </ext:Window>
</asp:Content>
