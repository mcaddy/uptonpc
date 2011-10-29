<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UptonParishCouncil.Site.News.Admin.FileUploader" Codebehind="UploadFiles.ascx.cs" %>
<table>
    <tr>
        <td>
            <b>File (or Zip):</b></td>
        <td>
            <asp:FileUpload ID="fuPhoto1" runat="server" Width="400px" /></td>
    </tr>
    <tr>
        <td>
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /></td>
    </tr>
</table>