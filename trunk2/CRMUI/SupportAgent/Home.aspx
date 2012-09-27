<%@ Page Title="" Language="C#" MasterPageFile="~/SupportAgent/SA.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="CRMUI.SupportAgent.CallHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" DirectMethodNamespace="DM" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <%--CALL Support Agent--%>
    <ext:Panel ID="pnlCallSupport" runat="server" MinHeight="615" Title="Home" TitleAlign="Center"
        Enabled="False" Visible="False" MaxWidth="1349">
        <LayoutConfig>
            <ext:TableLayoutConfig Columns="2" />
        </LayoutConfig>
        <Items>
            <%--TOP LEFT--%>
            <ext:Panel ID="pnlSearch" runat="server" Width="540" Height="200" Title="Search Client"
                Icon="UserMagnify">
                <LayoutConfig>
                    <ext:AccordionLayoutConfig />
                </LayoutConfig>
                <ToolTips>
                    <ext:ToolTip runat="server" Html="Search for a client by any of these 3 options<br/>Click on the headings to change option" />
                </ToolTips>
                <Items>
                    <ext:Panel ID="pnlUsername" runat="server" Title="SEARCH BY USERNAME" Icon="PageMagnify">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Middle" />
                        </LayoutConfig>
                        <Items>
                            <ext:TextField runat="server" ID="txtSUsername" FieldLabel="Client Username" Width="300"
                                Margins="0 0 0 30" AllowBlank="False">
                                <Listeners>
                                    <ValidityChange Handler="#{btnUsernameSearch}.setDisabled(!isValid);" />
                                </Listeners>
                            </ext:TextField>
                            <ext:Button runat="server" ID="btnUsernameSearch" Text="Search" Width="80" Margins="0 0 0 10"
                                Icon="Magnifier" OnDirectClick="BtnUsernameSearchClick" Disabled="True" />
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="pnlName" runat="server" Title="SEARCH BY CLIENT NAME" Icon="PageMagnify">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Middle" />
                        </LayoutConfig>
                        <Items>
                            <ext:TextField runat="server" ID="txtSName" FieldLabel="Client Name" Width="300"
                                Margins="0 0 0 30" AllowBlank="False">
                                <Listeners>
                                    <ValidityChange Handler="#{btnNameSearch}.setDisabled(!isValid);" />
                                </Listeners>
                            </ext:TextField>
                            <ext:Button runat="server" ID="btnNameSearch" Text="Search" Width="80" Margins="0 0 0 10"
                                Icon="Magnifier" Disabled="True" OnDirectClick="BtnNameSearchClick" />
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <%--TOP RIGHT--%>
            <ext:GridPanel ID="gpClient" Title="Client Details" runat="server" Width="807" Height="200"
                Icon="Vcard" SortableColumns="True">
                <ToolTips>
                    <ext:ToolTip runat="server" Html="Select which client you want to create a ticket for" />
                </ToolTips>
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
                        <ext:DateColumn runat="server" ID="txtDateOfBirth" Text="Date Of Birth" Format="dd-MMMM-yyyy"
                            DataIndex="DateOfBirth" />
                        <ext:Column runat="server" ID="txtTelephone" Text="Telephone" DataIndex="Telephone" />
                        <ext:Column runat="server" ID="txtCell" Text="Cell" DataIndex="Cell" />
                    </Columns>
                </ColumnModel>
                <DirectEvents>
                    <Select OnEvent="GpClientSelected">
                        <ExtraParams>
                            <ext:Parameter Name="clientid" Value="record.data.CLIENT_ID" Mode="Raw" />
                        </ExtraParams>
                    </Select>
                </DirectEvents>
            </ext:GridPanel>
            <%--BOTTOM LEFT--%>
            <ext:Panel ID="Panel1" runat="server" Width="540" Height="388" Title="Search For Solutions"
                Icon="BookMagnify">
                <LayoutConfig>
                    <ext:VBoxLayoutConfig Align="Stretch" />
                </LayoutConfig>
                <Items>
                    <%--BOTTOM LEFT SEARCH PANEL--%>
                    <ext:Container ID="Container1" runat="server" Border="False" MaxHeight="60">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Middle" />
                        </LayoutConfig>
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="Use this to search for a solution to a problem" />
                        </ToolTips>
                        <Items>
                            <ext:TextField runat="server" ID="txtCProbDesc" FieldLabel="Problem Description"
                                Width="350" LabelWidth="120" Margin="15" AllowBlank="False" BlankText="Description cannot be empty"
                                MsgTarget="Side">
                                <Listeners>
                                    <ValidityChange Handler="#{btnCSearchSolutions}.setDisabled(!isValid);">
                                    </ValidityChange>
                                </Listeners>
                            </ext:TextField>
                            <ext:Button runat="server" ID="btnCSearchSolutions" Text="Search" Icon="Magnifier"
                                Margins="0 0 0 10" Padding="2" Disabled="True" OnDirectClick="BtnCSearchSolClick">
                            </ext:Button>
                        </Items>
                    </ext:Container>
                    <%--BOTTOM LEFT GRID PANEL--%>
                    <ext:GridPanel runat="server" ID="gpCSolution" Flex="1" SortableColumns="True">
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="Select any 1 to view full details on right panel" />
                        </ToolTips>
                        <Store>
                            <ext:Store runat="server" ID="streCSolutions" PageSize="10" Buffered="True" IgnoreExtraFields="True">
                                <Model>
                                    <ext:Model ID="mdlCSolutions" runat="server">
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
                                <ext:DateColumn ID="clmCDateCreated" runat="server" Text="Created" Align="Center"
                                    Width="77" DataIndex="DateCreated" Format="dd MMM yy" />
                                <ext:DateColumn ID="clmCModified" runat="server" Text="Modified" Align="Center" Width="77"
                                    DataIndex="DateModified" Format="dd MMM yy" />
                                <ext:Column ID="clmCSolution" runat="server" Text="Solution Description" Width="198"
                                    DataIndex="Description" />
                                <ext:Column ID="clmCSolId" runat="server" Text="SolutionID" Align="Center" Width="77"
                                    DataIndex="SOL_ID" />
                                <ext:Column ID="clmCProbId" runat="server" Text="ProblemID" Align="Center" Width="77"
                                    DataIndex="PROB_ID" />
                            </Columns>
                        </ColumnModel>
                        <DirectEvents>
                            <Select OnEvent="GpCSolutionSelected">
                                <ExtraParams>
                                    <ext:Parameter Name="solid" Value="record.data.SOL_ID" Mode="Raw" />
                                    <ext:Parameter Name="probid" Value="record.data.PROB_ID" Mode="Raw" />
                                </ExtraParams>
                            </Select>
                        </DirectEvents>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <%--BOTTOM RIGHT--%>
            <ext:FormPanel runat="server" ID="pnlCSolutionDetails" Title="Solution Details" Icon="FolderLightbulb"
                Width="807" Height="388" BodyPadding="10" AutoScroll="True">
                <Items>
                    <ext:Hidden runat="server" ID="hCClientId" />
                    <ext:Hidden runat="server" ID="hCProbId" />
                    <ext:Hidden runat="server" ID="hCSolId" />
                    <ext:Label runat="server" ID="lblCEmployeeFullName" />
                    <ext:Label ID="Label3" runat="server" Html="<br/>" />
                    <ext:Label runat="server" ID="lblCDateCreated" />
                    <ext:Label runat="server" ID="lblCDateModified" />
                    <ext:Label ID="lblCSpacer" runat="server" Html="<br/><br/>" />
                    <ext:FieldSet runat="server" ID="fsCProbDesc" Title="Problem Description" Visible="False">
                        <Items>
                            <ext:Label runat="server" ID="lblCProbDesc" />
                        </Items>
                    </ext:FieldSet>
                    <ext:Label ID="Label8" runat="server" Html="<br/>" />
                    <ext:FieldSet runat="server" ID="fsCSolDesc" Title="Solution Description" Visible="False">
                        <Items>
                            <ext:Label runat="server" ID="lblCSolDesc" />
                        </Items>
                    </ext:FieldSet>
                </Items>
                <BottomBar>
                    <ext:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                            <ext:ComboBox ID="cmbCPriority" runat="server" FieldLabel="<b>Priority</b>" LabelWidth="50"
                                Text="Select a priority..." Disabled="True">
                                <Items>
                                    <ext:ListItem Text="High" Value="HIGH" />
                                    <ext:ListItem Text="Medium" Value="MEDIUM" />
                                    <ext:ListItem Text="Low" Value="LOW" />
                                </Items>
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip4" runat="server" Html="Select a priority for this problem" />
                                </ToolTips>
                            </ext:ComboBox>
                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" />
                            <ext:Button runat="server" ID="btnCCreateTicketSol" Text="Create Ticket With Solution"
                                Icon="Lightbulb" Disabled="True">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip3" runat="server" Html="Click if you have a solution" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="BtnCCreateTicketSolClick">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server" />
                            <ext:Button runat="server" ID="btnCCreateTicketNoSol" Text="Create Ticket Without Solution"
                                Icon="LightbulbOff" Disabled="True">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip2" runat="server" Html="Click if you do not have a solution" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="BtnCCreateTicketNoSol">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </BottomBar>
            </ext:FormPanel>
        </Items>
    </ext:Panel>
    <%--EMAIL Support Agent--%>
    <ext:Panel ID="pnlEmailSupport" runat="server" MinHeight="615" Title="Home" TitleAlign="Center"
        Enabled="False" Visible="False" MaxWidth="1349">
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
                        Flex="2" SortableColumns="True">
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="Select an email to view details on right panel" />
                        </ToolTips>
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
                                <ExtraParams>
                                    <ext:Parameter Name="epid" Value="record.data.EP_ID" Mode="Raw" />
                                </ExtraParams>
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
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="Click to view image attachements" />
                                </ToolTips>
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
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="Use this to search for solutions to a problem" />
                        </ToolTips>
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
                            <ext:GridPanel runat="server" ID="gpSolutions" Flex="1" SortableColumns="True">
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="Select any 1 to view full details on right panel" />
                                </ToolTips>
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
                                            <ext:Parameter Name="solid" Value="record.data.SOL_ID" Mode="Raw" />
                                            <ext:Parameter Name="probid" Value="record.data.PROB_ID" Mode="Raw" />
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
                                        <ToolTips>
                                            <ext:ToolTip runat="server" Html="Select a priority for this problem" />
                                        </ToolTips>
                                        <Items>
                                            <ext:ListItem Text="High" Value="HIGH" />
                                            <ext:ListItem Text="Medium" Value="MEDIUM" />
                                            <ext:ListItem Text="Low" Value="LOW" />
                                        </Items>
                                    </ext:ComboBox>
                                    <ext:ToolbarSpacer runat="server" />
                                    <ext:Button runat="server" ID="btnECreateTicketSol" Text="Create Ticket With Solution"
                                        Icon="Lightbulb" Disabled="True">
                                        <ToolTips>
                                            <ext:ToolTip runat="server" Html="Click if you have a solution" />
                                        </ToolTips>
                                        <DirectEvents>
                                            <Click OnEvent="BtnECreateTicketSolClick">
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:ToolbarSeparator runat="server" />
                                    <ext:Button runat="server" ID="btnECreateTicketNoSol" Text="Create Ticket Without Solution"
                                        Icon="LightbulbOff" Disabled="True">
                                        <ToolTips>
                                            <ext:ToolTip ID="ToolTip1" runat="server" Html="Click if you do not have a solution" />
                                        </ToolTips>
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
    <%--POPUP ADD PROBLEM TO DATABASE EMAIL--%>
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
    <%--POPUP ADD PROBLEM TO DATABASE CALL--%>
    <ext:Window runat="server" ID="wndCAddProblem" Title="Add Problem" Hidden="True"
        Icon="TagBlueAdd" Width="400" Height="210" BodyPadding="10">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Stretch" />
        </LayoutConfig>
        <Items>
            <ext:Label ID="Label2" runat="server" Html="The problem does not exist, Use this window to add it.">
            </ext:Label>
            <ext:TextArea runat="server" ID="taCProbDesc" Height="110" FieldLabel="Problem Description"
                LabelAlign="Top" AllowBlank="False" MsgTarget="Under">
                <Listeners>
                    <ValidityChange Handler="#{btnCAddProblem}.setDisabled(!isValid);" />
                </Listeners>
            </ext:TextArea>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="btnCAddProblem" Text="Add Problem" Disabled="True">
                <DirectEvents>
                    <Click OnEvent="BtnCAddProblemClick">
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <DirectEvents>
            <Close OnEvent="WndCAddProblemClosed">
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
                    <ext:ComboBox runat="server" DisplayField="Name" ValueField="CAT_ID" ID="cmbCategory"
                        LabelWidth="70" AllowBlank="False" FieldLabel="Category" Text="Choose a Category.."
                        AnchorHorizontal="30%">
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="Select a template category"/>
                        </ToolTips>
                        <Store>
                            <ext:Store runat="server" ID="streCategories">
                                <Model>
                                    <ext:Model runat="server">
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
                            <ext:ToolTip runat="server" Html="Select a template from the chosen category<br/>to populate the body with."/>
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
                            <ext:ToolTip runat="server" Html="Subject of the email"/>
                        </ToolTips>
                    </ext:TextField>
                    <ext:HtmlEditor runat="server" ID="heEmailBody" LabelWidth="70" Height="365" FieldLabel="Body"
                        AnchorHorizontal="100%">
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="Body of the email"/>
                        </ToolTips>
                    </ext:HtmlEditor>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="btnSendEmail" Text="Send" Icon="EmailStart" Margins="0 12 0 0">
                        <ToolTips>
                            <ext:ToolTip runat="server" Html="Click to send the email to the client"/>
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
