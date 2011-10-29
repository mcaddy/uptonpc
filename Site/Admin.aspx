<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="UptonParishCouncil.Site.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:SiteMapDataSource ID="AdminSiteMapDataSource" runat="server" StartFromCurrentNode="true" />
    <asp:TreeView ID="TreeView1" runat="server" DataSourceID="AdminSiteMapDataSource">

    </asp:TreeView>
</asp:Content>
