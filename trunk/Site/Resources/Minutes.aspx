<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Minutes.aspx.cs" Inherits="UptonParishCouncil.Site.Resources.Minutes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
Parish Meeting Minutes
    <asp:SqlDataSource ID="MinutesSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
        SelectCommand="UptonPC_GetResourcesByType" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="2" Name="ResourceTypeId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Repeater ID="MinutesRepeater" runat="server" 
        DataSourceID="MinutesSqlDataSource">
    <HeaderTemplate><ul></HeaderTemplate>
    <FooterTemplate></ul></FooterTemplate>
        <ItemTemplate>
            <li><asp:HyperLink runat="server" ID="DataHyperLink" NavigateUrl='<%# UptonParishCouncil.Site.Utils.ResourceUrl(Eval("ResourceId"), Eval("Title")) %>' Text='<%# Eval("Title") %>' /></li>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
