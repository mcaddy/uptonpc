<%@ Control Language="C#" AutoEventWireup="true" Inherits="UptonParishCouncil.Site.News.Preview" Codebehind="Preview.ascx.cs" %>
<script type="text/javascript">
function exiting() {
  alert('You are now leaving the Upton Parish Council site\r\n\r\n Upton Parish Council cannot be held responsible for the content of external sites');
}
</script>
<asp:Panel ID="plNews" runat="server" style="padding:4px 4px 4px 4px;">
    <asp:Repeater ID="rptNews" runat="server">
        <ItemTemplate>
        <asp:HyperLink runat="server" ID="NewsLink" NavigateUrl='<%# NewsUrl(Eval("NoticeId"))%>' Text='<%#Eval("Subject")%>' /><br />
                <div style="font-size:Smaller"><%# UptonParishCouncil.Site.Utils.TruncateString(UptonParishCouncil.Site.Utils.Strip(Eval("Body")), 260)%></div>
        </ItemTemplate>
    </asp:Repeater>    
</asp:Panel>
<div style="text-align:right;"><asp:HyperLink runat="server" ID="hlRssLink"><asp:Image runat="server" ID="iRssLink" ImageUrl="~/images/RSS-mini.gif" /></asp:HyperLink></div>