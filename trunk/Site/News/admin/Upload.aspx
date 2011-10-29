<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" Inherits="UptonParishCouncil.Site.News.Admin.Upload" Title="Untitled Page" Codebehind="Upload.aspx.cs" %>

<%@ Register Src="UploadFiles.ascx" TagName="UploadFiles" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPageTitle" Runat="Server">
    Upload Files
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainSite" Runat="Server">
    <uc1:UploadFiles id="UploadFiles1" runat="server" FilesBaseLocation="~/Images/News/" />
</asp:Content>

