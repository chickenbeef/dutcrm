<%@ Page Title="" Language="C#" MasterPageFile="~/CallCentreManager/CCM.Master" AutoEventWireup="true" CodeBehind="Charts.aspx.cs" Inherits="CRMUI.CallCentreManager.Charts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <ext:ResourceManager ID="ResourceManagerc" runat="server" Theme="Default" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCallCMBody" runat="server">
    <ext:ChartTheme 
            ID="FancyTheme" 
            runat="server" 
            ThemeName="Fancy" 
            Colors="url(#v-1),url(#v-2),url(#v-3),url(#v-4),url(#v-5)">
            <Axis Fill="#eee" Stroke="#eee" />
            <AxisLabelLeft Fill="#eee" />
            <AxisLabelBottom Fill="#eee" />
            <AxisTitleLeft Fill="#eee" />
            <AxisTitleBottom Fill="#eee" />
        </ext:ChartTheme>
    <ext:Panel ID="pnlMain" runat="server" Height="600" Title="System charts and graphs" LayoutConfig="FitLayout" Icon="ServerChart" MaxWidth="1349">
        <TopBar>
            <ext:Toolbar ID="tlbarChart" runat="server">
                <Items>
                    <ext:Button runat="server" Text="Refresh Data" Icon="DatabaseRefresh" OnDirectClick="RefreshData"/>
                    <ext:Button runat="server" Text="Save Chart" Icon="Disk" OnDirectClick="SaveChart"/>
                </Items>
            </ext:Toolbar>
        </TopBar>
        <Items>
            <ext:Chart 
                    ID="Chart1" 
                    runat="server"
                    Theme="Fancy"
                    Shadow="true">
                    <AnimateConfig Easing="BounceOut" Duration="750" />
                    <Store>
                        <ext:Store ID="Store1" 
                            runat="server" 
                            AutoDataBind="true">                           
                            <Model>
                                <ext:Model ID="Model1" runat="server">
                                    <Fields>
                                        <ext:Modelfield Name="DateV"/>
                                        <ext:Modelfield Name="ValueV"/>
                                        <ext:Modelfield Name="ValueZ"/>
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>

                    <Background Fill="rgb(14, 111, 167)" />

                    <Gradients>
                        <ext:Gradient GradientID="v-1" Angle="0">
                            <Stops>
                                <ext:GradientStop Offset="0" Color="rgb(212, 40, 40)" />
                                <ext:GradientStop Offset="100" Color="rgb(117, 14, 14)" />
                            </Stops>
                        </ext:Gradient>

                        <ext:Gradient GradientID="v-2" Angle="0">
                            <Stops>
                                <ext:GradientStop Offset="0" Color="rgb(180, 216, 42)" />
                                <ext:GradientStop Offset="100" Color="rgb(94, 114, 13)" />
                            </Stops>
                        </ext:Gradient>

                        <ext:Gradient GradientID="v-3" Angle="0">
                            <Stops>
                                <ext:GradientStop Offset="0" Color="rgb(43, 221, 115)" />
                                <ext:GradientStop Offset="100" Color="rgb(14, 117, 56)" />
                            </Stops>
                        </ext:Gradient>

                        <ext:Gradient GradientID="v-4" Angle="0">
                            <Stops>
                                <ext:GradientStop Offset="0" Color="rgb(45, 117, 226)" />
                                <ext:GradientStop Offset="100" Color="rgb(14, 56, 117)" />
                            </Stops>
                        </ext:Gradient>

                        <ext:Gradient GradientID="v-5" Angle="0">
                            <Stops>
                                <ext:GradientStop Offset="0" Color="rgb(187, 45, 222)" />
                                <ext:GradientStop Offset="100" Color="rgb(85, 10, 103)" />
                            </Stops>
                        </ext:Gradient>
                    </Gradients>

                    <Axes>
                        <ext:NumericAxis                             
                            Fields="ValueV,ValueZ"                                                        
                            Title="Problems "
                            Minimum="0"
                            Maximum="8">
                             <Label>
                                <Renderer Handler="return Ext.util.Format.number(value, '0');" />
                            </Label> 
                            <GridConfig>
                                <Odd Stroke="#555" />
                                <Even Stroke="#555" />
                            </GridConfig>
                        </ext:NumericAxis>                            

                        <ext:CategoryAxis 
                            Position="Bottom"
                            Fields="DateV"
                            Title="Days">
                            <Label>
                                <Renderer Handler="return Ext.util.Format.date(value, 'd-M-y');"/>
                            </Label>
                        </ext:CategoryAxis>

                    </Axes>

                    <Series>
                        <ext:ColumnSeries
                            Axis="Left"
                            Highlight="true" 
                            XField="DateV" 
                            YField="ValueV,ValueZ" runat="server">
                                                      
                            <Style Opacity="0.95" />
                            <Renderer Handler="var colors = ['url(#v-1)','url(#v-2)'];attributes.fill = colors[index % colors.length]; return attributes;" />
                        </ext:ColumnSeries>
                    </Series>
                </ext:Chart>
        </Items>
    </ext:Panel>
</asp:Content>
