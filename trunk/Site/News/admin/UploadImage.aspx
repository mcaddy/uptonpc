<%@ Page Language="C#" MasterPageFile="~/Window.master" AutoEventWireup="true" Inherits="UptonParishCouncil.Site.News.Admin.UploadImage" Title="Upload Image" Codebehind="UploadImage.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<table style="width:100%; border:solid 2px #D9291C;" cellspacing="0">
<tr>
<td style="height:2em; background-color:#D9291C; color:White; font-weight:bold;">Upload Photo</td>
</tr>
<tr>
<td>
Select the File to upload<br/>
<input id="fuNewFile" type="file" runat="server" />&nbsp;
<input id="btnUploadFile" type="button" value="Upload" runat="server" onserverclick="btnUploadFile_ServerClick" />
</td>
</tr>
<tr>
<td>
    <asp:Panel ID="plPreviousFileDetails" runat="server" Visible="false">
    <h3>File Uploaded</h3>
    Image:<asp:HyperLink ID="hlImageLink" runat="server" Target="_blank" /> (<asp:Label id="lblFileSize" runat="server" />kb)<br />
    Link Helper: &nbsp;<asp:Label id="lblHelperLink" runat="server" />

    </asp:Panel>
    <asp:Panel ID="plError" runat="server" Visible="false">
    <h3>An error occoured</h3>
        <asp:Label ID="lblErrorText" runat="server" />
    </asp:Panel>

</td>
</tr>
</table>
</asp:Content>

