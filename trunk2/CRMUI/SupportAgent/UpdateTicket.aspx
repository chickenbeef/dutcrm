<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true"
    CodeBehind="UpdateTicket.aspx.cs" Inherits="CRMUI.SupportAgent.UpdateTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <ext:Panel ID="pnlMain" runat="server" Height="615" MaxWidth="1349" Title="Update Ticket"
        TitleAlign="Center">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <%--TOP CONTAINER--%>
            <ext:Container runat="server" Flex="1">
                <LayoutConfig>
                    <ext:HBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--LEFT PANEL--%>
                    <ext:Panel ID="pnlSearchTicket" runat="server" Flex="8">
                        <LayoutConfig>
                            <ext:AccordionLayoutConfig />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel ID="pnlTicketId" runat="server" Title="SEARCH BY TICKET ID" Icon="PageMagnify"
                                Collapsed="False">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" />
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSCPRId" FieldLabel="Ticket ID" Width="300" Margins="0 0 0 30" AllowBlank="False">
                                        <Listeners>
                                            <ValidityChange Handler="#{btnCPRIdSearch}.setDisabled(!isValid);"></ValidityChange>
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Button runat="server" ID="btnCPRIdSearch" Text="Search" Width="80" Margins="0 0 0 10"
                                        Icon="Magnifier" OnDirectClick="BtnCPRIdSearchClick" Disabled="True"/>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlUsername" runat="server" Title="SEARCH BY USERNAME" Icon="PageMagnify"
                                Collapsed="True">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" />
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Client Username" Width="300"
                                        Margins="0 0 0 30" AllowBlank="False">
                                        <Listeners>
                                            <ValidityChange Handler="#{btnUsernameSearch}.setDisabled(!isValid);"></ValidityChange>
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10"
                                        Icon="Magnifier" OnDirectClick="BtnUsernameSearchClick" Disabled="True"/>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY CLIENT NAME" Icon="PageMagnify"
                                Collapsed="True">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" />
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtSName" FieldLabel="Client Name" Width="300"
                                        Margins="0 0 0 30" AllowBlank="False" >
                                        <Listeners>
                                            <ValidityChange Handler="#{btnNameSearch}.setDisabled(!isValid);"></ValidityChange>
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Button runat="server" ID="btnNameSearch" Text="Search" Width="80" Margins="0 0 0 10"
                                        Icon="Magnifier" OnDirectClick="BtnNameSearchClick" Disabled="True"/>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <%--MIDDLE PANEL--%>
                    <ext:GridPanel runat="server" ID="gpTickets" Flex="10" Title="Tickets" AutoScroll="True">
                        <Store>
                            <ext:Store runat="server" ID="streTickets" PageSize="500" Buffered="True" IgnoreExtraFields="True">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="CPR_ID" />
                                            <ext:ModelField Name="DateCreated" />
                                            <ext:ModelField Name="EMP_ID" />
                                            <ext:ModelField Name="EmployeeName" />
                                            <ext:ModelField Name="EmployeeSurname" />
                                            <ext:ModelField Name="PROB_ID" />
                                            <ext:ModelField Name="ProblemDescription" />
                                            <ext:ModelField Name="SOL_ID" />
                                            <ext:ModelField Name="SolutionDescription" />
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
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="30" />
                                <ext:DateColumn ID="DateColumn1" runat="server" Text="Date Created" DataIndex="DateCreated"
                                    Format="dd-MMM-yy" Width="75" />
                                <ext:Column runat="server" Text="Client Name" DataIndex="ClientName" Width="75" />
                                <ext:Column runat="server" Text="Client Surname" DataIndex="ClientSurname" Width="85" />
                                <ext:Column runat="server" Text="Description" DataIndex="ProblemDescription" Width="151" />
                                <ext:Column runat="server" Text="Employee Name" DataIndex="EmployeeName" Width="90" />
                                <ext:Column runat="server" Text="Employee Surname" DataIndex="EmployeeSurname" Width="105" />
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
                    <ext:GridPanel ID="gpNotes" runat="server" Flex="4" Title="Notes" AutoScroll="True">
                        <Store>
                            <ext:Store ID="streNotes" runat="server" PageSize="10" Buffered="True">
                                <Model>
                                    <ext:Model runat="server">
                                        <Fields>
                                            <ext:ModelField Name="NOTE_ID" />
                                            <ext:ModelField Name="Description" />
                                            <ext:ModelField Name="DateCreated" />
                                            <ext:ModelField Name="EMP_ID" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="30" />
                                <ext:Column runat="server" Text="Description" DataIndex="Description" Width="113" />
                                <ext:DateColumn runat="server" Text="Date Created" DataIndex="DateCreated" />
                            </Columns>
                        </ColumnModel>
                        <DirectEvents>
                            <Select OnEvent="GpNotesSelected">
                                <ExtraParams>
                                    <ext:Parameter Name="noteid" Value="record.data.NOTE_ID" Mode="Raw" />
                                    <ext:Parameter Name="empid" Value="record.data.EMP_ID" Mode="Raw" />
                                </ExtraParams>
                            </Select>
                        </DirectEvents>
                    </ext:GridPanel>
                </Items>
            </ext:Container>
            <%--BOTTOM CONTAINER--%>
            <ext:Container runat="server" Flex="2">
                <LayoutConfig>
                    <ext:HBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--LEFT SOLUTIONS PANEL--%>
                    <ext:Panel runat="server" Border="false" Title="Select New Solution" Flex="8" Collapsible="True"
                        CollapseDirection="Left">
                        <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Stretch" />
                        </LayoutConfig>
                        <Items>
                            <%--SOLUTION GRIDPANEL--%>
                            <ext:GridPanel runat="server" ID="gpSolution" Flex="1" AutoScroll="True" SortableColumns="True">
                                <Store>
                                    <ext:Store runat="server" ID="streSolutions" PageSize="10" Buffered="True" IgnoreExtraFields="True">
                                        <Model>
                                            <ext:Model ID="mdlSolutions" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="SOL_ID" />
                                                    <ext:ModelField Name="Description" />
                                                    <ext:ModelField Name="DateCreated" />
                                                    <ext:ModelField Name="DateModified" />
                                                    <ext:ModelField Name="EMP_ID" />
                                                    <ext:ModelField Name="PROB_ID" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn ID="RowNumbererColumn3" runat="server" Width="30" Align="Center"
                                            Sortable="False" />
                                        <ext:DateColumn ID="clmDateCreated" runat="server" Text="Created" Align="Center"
                                            Width="77" DataIndex="DateCreated" Format="dd MMM yy" />
                                        <ext:DateColumn ID="clmModified" runat="server" Text="Modified" Align="Center" Width="77"
                                            DataIndex="DateModified" Format="dd MMM yy" />
                                        <ext:Column ID="clmSolution" runat="server" Text="Solution Description" Width="149"
                                            DataIndex="Description" />
                                        <ext:Column ID="clmSolId" runat="server" Text="SolutionID" Align="Center" Width="77"
                                            DataIndex="SOL_ID" />
                                        <ext:Column ID="clmProbId" runat="server" Text="ProblemID" Align="Center" Width="77"
                                            DataIndex="PROB_ID" />
                                    </Columns>
                                </ColumnModel>
                                <DirectEvents>
                                    <Select OnEvent="GpSolutionSelected">
                                        <ExtraParams>
                                            <ext:Parameter Name="solid" Value="record.data.SOL_ID" Mode="Raw" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:GridPanel>
                            <%--SOLUTION DETAILS PANEL--%>
                            <ext:FormPanel runat="server" ID="pnlSolDetails" Flex="1" BodyPadding="10" AutoScroll="True">
                                <Items>
                                    <ext:Hidden runat="server" ID="hNSolId" />
                                    <ext:FieldSet runat="server" ID="fsNSolDesc" Title="Solution Description">
                                        <Items>
                                            <ext:Label runat="server" ID="lblNSolDesc" />
                                        </Items>
                                    </ext:FieldSet>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                    <%--RIGHT DETAILS PANEL--%>
                    <ext:Container runat="server" Flex="14" Border="false">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch" />
                        </LayoutConfig>
                        <Items>
                            <%--TICKET DETAILS--%>
                            <ext:FormPanel runat="server" Flex="1" Title="Ticket Details" BodyPadding="10" AutoScroll="True"
                                Collapsible="True" CollapseDirection="Left">
                                <Items>
                                    <ext:Hidden runat="server" ID="hCprId" />
                                    <ext:FieldSet runat="server" ID="fsProbDesc" Title="Problem Description" AnchorHorizontal="100%">
                                        <Items>
                                            <ext:Label runat="server" ID="lblProbDesc" />
                                        </Items>
                                    </ext:FieldSet>
                                    <ext:Label runat="server" Html="<br/>" />
                                    <ext:FieldSet runat="server" ID="fsOSolDesc" Title="Solution Description" AnchorHorizontal="100%">
                                        <Items>
                                            <ext:Label runat="server" ID="lblOSolDesc" />
                                        </Items>
                                    </ext:FieldSet>
                                    <ext:Button runat="server" ID="btnUpdateTicket" Text="Update Ticket" OnDirectClick="BtnUpdateTicketClick" />
                                </Items>
                            </ext:FormPanel>
                            <%--NOTE DETAILS--%>
                            <ext:FormPanel runat="server" Flex="1" Title="Note Details" BodyPadding="10" AutoScroll="True"
                                Collapsible="True" CollapseDirection="Right">
                                <Items>
                                    <ext:Label runat="server" ID="lblEmployeeNameOfNote"></ext:Label>
                                    <ext:FieldSet runat="server" ID="fsNote" Title="Note Description" AnchorHorizontal="100%">
                                        <Items>
                                            <ext:Label runat="server" ID="lblNote" />
                                        </Items>
                                    </ext:FieldSet>
                                    <ext:Label ID="Label1" runat="server" Html="<br/>" />
                                    <ext:HtmlEditor runat="server" ID="heNote" FieldLabel="Add Note" LabelAlign="Top" AnchorHorizontal="100%" />
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Container>
        </Items>
    </ext:Panel>
    <%--POPUP SEND EMAIL--%>
    <ext:Window runat="server" ID="wndSendEmail" Width="800" Height="600" BodyPadding="20"
        Hidden="True" Maximizable="true" Icon="EmailGo" Title="Send Email">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Border="false" Frame="True" BodyPadding="20">
                <Items>
                    <ext:Hidden runat="server" ID="hEClientId"/>
                    <ext:Hidden runat="server" ID="hEProbDesc"/>
                    <ext:Hidden runat="server" ID="hESolDesc"/>
                    <ext:ComboBox runat="server" DisplayField="Name" ValueField="CAT_ID" ID="cmbCategory"
                        LabelWidth="70" AllowBlank="False" FieldLabel="Category" Text="Choose a Category.."
                        AnchorHorizontal="30%">
                        <Store>
                            <ext:Store runat="server" ID="streCategories">
                                <Model>
                                    <ext:Model ID="Model2" runat="server">
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
                        <Store>
                            <ext:Store ID="streTemplates" runat="server">
                                <Model>
                                    <ext:Model ID="Model3" runat="server">
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
                    </ext:TextField>
                    <ext:HtmlEditor runat="server" ID="heEmailBody" LabelWidth="70" Height="365" FieldLabel="Body"
                        AnchorHorizontal="100%">
                    </ext:HtmlEditor>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSendEmail" Text="Send" Icon="EmailStart" Margins="0 12 0 0">
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
