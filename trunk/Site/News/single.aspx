<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="UptonParishCouncil.Site.News.Single"
    Title="Upton Parish Council News" CodeBehind="single.aspx.cs" %>

<%@ Register Src="NewsComment.ascx" TagName="NewsComment" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <span style="float: right;">
        <asp:Label runat="server" ID="lblDate" />
    </span>
    <h1>
        <asp:Label runat="server" ID="lblTitle" /></h1>
    <asp:Label runat="server" ID="lblBody" />
    <uc1:NewsComment ID="NewsComment1" runat="server" />
</asp:Content>
