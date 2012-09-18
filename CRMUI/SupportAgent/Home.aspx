<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="CRMUI.SupportAgent.CallHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" DirectMethodNamespace="DM" />
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
            <ext:VBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <%--TOP CONTAINER--%>
            <ext:Container ID="topContainer" runat="server" Border="False" Flex="1">
                <LayoutConfig>
                    <ext:HBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--TOP LEFT GRIDPANEL--%>
                    <ext:GridPanel runat="server" ID="gpEmailProblems" Title="Email Problems Inbox" Icon="Email"
                        Flex="2">
                        <Store>
                            <ext:Store runat="server" ID="streEmailProbs" PageSize="500" Buffered="True" IgnoreExtraFields="True">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="EP_ID" />
                                            <ext:ModelField Name="From" />
                                            <ext:ModelField Name="Subject" />
                                            <ext:ModelField Name="Description" />
                                            <ext:ModelField Name="DateCreated" />
                                            <ext:ModelField Name="CLIENT_ID" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="30" Align="Center"
                                    Sortable="False" />
                                <ext:Column ID="Column1" runat="server" Hidden="True" DataIndex="EP_ID" />
                                <ext:Column ID="Column2" runat="server" Text="From" Width="140" DataIndex="From" />
                                <ext:Column ID="Column3" runat="server" Text="Subject" Width="245" DataIndex="Subject" />
                                <ext:DateColumn ID="DateColumn1" runat="server" Text="Date Created" Width="77" Align="Center"
                                    Format="dd MMM yy" DataIndex="DateCreated" />
                                <ext:Column ID="Column5" runat="server" Hidden="True" DataIndex="CLIENT_ID" />
                                <ext:CommandColumn runat="server" Width="20">
                                    <Commands>
                                        <ext:GridCommand Icon="EmailDelete" CommandName="Delete">
                                            <ToolTip Text="Delete Email" />
                                        </ext:GridCommand>
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="DM.DeleteEmail(record.data.EP_ID);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <DirectEvents>
                            <Select OnEvent="EmailProblemSelected">
                            </Select>
                        </DirectEvents>
                    </ext:GridPanel>
                    <%--TOP RIGHT FORMPANEL--%>
                    <ext:FormPanel runat="server" ID="pnlEmailDetails" Title="Email Details" Icon="EmailOpen"
                        BodyPadding="10" AutoScroll="True" Flex="3" ButtonAlign="Left" Border="false">
                        <Items>
                            <ext:Hidden runat="server" ID="hEClientId" />
                            <ext:Hidden runat="server" ID="hEPId" />
                            <ext:Label runat="server" ID="lblSubject" />
                            <ext:Label runat="server" ID="lblFrom" />
                            <ext:Label runat="server" ID="lblDate" />
                            <ext:Label runat="server" ID="lblEmailDesc" />
                        </Items>
                        <Buttons>
                            <ext:Button runat="server" ID="btnEViewImages" Text="View Images" Icon="Images" Disabled="True"
                                OnDirectClick="BtnViewImagesClick">
                            </ext:Button>
                        </Buttons>
                    </ext:FormPanel>
                </Items>
            </ext:Container>
            <%--BOTTOM CONTAINER--%>
            <ext:Container runat="server" Border="False" Flex="1">
                <LayoutConfig>
                    <ext:HBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--CONTAINER FOR SEARCH--%>
                    <ext:Panel runat="server" Border="False" Flex="2" Title="Search For Solutions" Icon="BookMagnify">
                        <LayoutConfig>
                            <ext:VBoxLayoutConfig Align="Stretch" />
                        </LayoutConfig>
                        <Items>
                            <%--BOTTOM LEFT SEARCH PANEL--%>
                            <ext:Container runat="server" Border="False" MaxHeight="60">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Middle" />
                                </LayoutConfig>
                                <Items>
                                    <ext:TextField runat="server" ID="txtEProbDesc" FieldLabel="Problem Description"
                                        Width="350" LabelWidth="120" Margin="15" AllowBlank="False" BlankText="Description cannot be empty"
                                        MsgTarget="Side">
                                        <Listeners>
                                            <ValidityChange Handler="#{btnESearchSolutions}.setDisabled(!isValid);">
                                            </ValidityChange>
                                        </Listeners>
                                    </ext:TextField>
                                    <ext:Button runat="server" ID="btnESearchSolutions" Text="Search" Icon="Magnifier"
                                        Margins="0 0 0 10" Padding="2" Disabled="True" OnDirectClick="BtnESearchSolClick">
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                            <%--BOTTOM LEFT GRID PANEL--%>
                            <ext:GridPanel runat="server" ID="gpSolutions" Flex="1">
                                <Store>
                                    <ext:Store runat="server" ID="streESolutions" PageSize="10" Buffered="True" IgnoreExtraFields="True">
                                        <Model>
                                            <ext:Model ID="mdlESolutions" runat="server">
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
                                        <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="30" Align="Center"
                                            Sortable="False" />
                                        <ext:DateColumn ID="clmEDateCreated" runat="server" Text="Created" Align="Center"
                                            Width="77" DataIndex="DateCreated" Format="dd MMM yy" />
                                        <ext:DateColumn ID="clmEModified" runat="server" Text="Modified" Align="Center" Width="77"
                                            DataIndex="DateModified" Format="dd MMM yy" />
                                        <ext:Column ID="clmESolution" runat="server" Text="Solution Description" Width="198"
                                            DataIndex="Description" />
                                        <ext:Column ID="clmESolId" runat="server" Text="SolutionID" Align="Center" Width="77"
                                            DataIndex="SOL_ID" />
                                        <ext:Column ID="clmEProbId" runat="server" Text="ProblemID" Align="Center" Width="77"
                                            DataIndex="PROB_ID" />
                                    </Columns>
                                </ColumnModel>
                                <DirectEvents>
                                    <Select OnEvent="GpESolutionSelected">
                                        <ExtraParams>
                                            <ext:Parameter Name="record" Value="record.data" Mode="Raw" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                    <%--BOTTOM RIGHT PANEL--%>
                    <ext:FormPanel runat="server" ID="pnlESolutionDetails" Title="Solution Details" Icon="FolderLightbulb"
                        Flex="3" BodyPadding="10" AutoScroll="True">
                        <Items>
                            <ext:Hidden runat="server" ID="hEProbId" />
                            <ext:Hidden runat="server" ID="hESolId" />
                            <ext:Label runat="server" ID="lblEEmployeeFullName" />
                            <ext:Label runat="server" Html="<br/>" />
                            <ext:Label runat="server" ID="lblEDateCreated" />
                            <ext:Label runat="server" ID="lblEDateModified" />
                            <ext:Label ID="lblESpacer" runat="server" Html="<br/><br/>" />
                            <ext:FieldSet runat="server" ID="fsEProbDesc" Title="Problem Description" Visible="False">
                                <Items>
                                    <ext:Label runat="server" ID="lblEProbDesc" />
                                </Items>
                            </ext:FieldSet>
                            <ext:Label ID="Label1" runat="server" Html="<br/>" />
                            <ext:FieldSet runat="server" ID="fsESolDesc" Title="Solution Description" Visible="False">
                                <Items>
                                    <ext:Label runat="server" ID="lblESolDesc" />
                                </Items>
                            </ext:FieldSet>
                        </Items>
                        <BottomBar>
                            <ext:Toolbar runat="server">
                                <Items>
                                    <ext:ToolbarFill runat="server" />
                                    <ext:ComboBox ID="cmbEPriority" runat="server" FieldLabel="<b>Priority</b>" LabelWidth="50"
                                        Text="Select a priority..." Disabled="True">
                                        <Items>
                                            <ext:ListItem Text="High" Value="HIGH" />
                                            <ext:ListItem Text="Medium" Value="MEDIUM" />
                                            <ext:ListItem Text="Low" Value="LOW" />
                                        </Items>
                                    </ext:ComboBox>
                                    <ext:ToolbarSpacer runat="server" />
                                    <ext:Button runat="server" ID="btnECreateTicketSol" Text="Create Ticket With Solution"
                                        Icon="Lightbulb" Disabled="True">
                                        <DirectEvents>
                                            <Click OnEvent="BtnECreateTicketSolClick">
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:ToolbarSeparator runat="server" />
                                    <ext:Button runat="server" ID="btnECreateTicketNoSol" Text="Create Ticket Without Solution"
                                        Icon="LightbulbOff" Disabled="True">
                                        <DirectEvents>
                                            <Click OnEvent="BtnECreateTicketNoSol">
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </BottomBar>
                    </ext:FormPanel>
                </Items>
            </ext:Container>
        </Items>
    </ext:Panel>
    <%--POPUP ADD PROBLEM TO DATABASE--%>
    <ext:Window runat="server" ID="wndAddProblem" Title="Add Problem" Hidden="True" Icon="TagBlueAdd"
        Width="400" Height="210" BodyPadding="10">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <ext:Label runat="server" Html="The problem does not exist, Use this window to add it.">
            </ext:Label>
            <ext:TextArea runat="server" ID="taEProbDesc" Height="110" FieldLabel="Problem Description"
                LabelAlign="Top" AllowBlank="False" MsgTarget="Under">
                <Listeners>
                    <ValidityChange Handler="#{btnEAddProblem}.setDisabled(!isValid);" />
                </Listeners>
            </ext:TextArea>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="btnEAddProblem" Text="Add Problem" Disabled="True">
                <DirectEvents>
                    <Click OnEvent="BtnEAddProblemClick">
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <DirectEvents>
            <Close OnEvent="WndAddProblemClosed">
            </Close>
        </DirectEvents>
    </ext:Window>
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
            <ext:FormPanel runat="server" Border="false" Frame="True" BodyPadding="20">
                <Items>
                    <ext:ComboBox runat="server" ID="cmbCategory" LabelWidth="70" FieldLabel="Category" OnDirectSelect="CmbCategorySelectedItem" Text="Choose a Category.." AnchorHorizontal="30%">
                        <%--<DirectEvents>
                            <Select OnEvent="CmbCategorySelectedItem"></Select>
                        </DirectEvents>--%>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="cmbTemplate" LabelWidth="70" FieldLabel="Template" OnDirectSelect="CmbTemplateSelectedItem" Text="Choose a Template.." Disabled="True" AnchorHorizontal="50%">
                        <%--<DirectEvents>
                            <Select OnEvent="CmbTemplateSelectedItem"></Select>
                        </DirectEvents>--%>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="txtSubject" LabelWidth="70" FieldLabel="Subject" AnchorHorizontal="70%">
                    </ext:TextField>
                    <ext:HtmlEditor runat="server" ID="heEmailBody" LabelWidth="70" Height="365" FieldLabel="Body" AnchorHorizontal="100%">
                    </ext:HtmlEditor>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSendEmail" Text="Send" Icon="EmailStart" OnDirectClick="BtnSendEmailClick" Margins="0 12 0 0">
                        <%--<DirectEvents>
                            <Click OnEvent="BtnSendEmailClick"></Click>
                        </DirectEvents>--%>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
    </ext:Window>
</asp:Content>
