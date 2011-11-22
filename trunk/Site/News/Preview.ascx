<%@ Control Language="C#" AutoEventWireup="true" Inherits="UptonParishCouncil.Site.News.Preview"
    CodeBehind="Preview.ascx.cs" %>
<script type="text/javascript">
    function exiting() {
        alert('You are now leaving the Upton Parish Council site\r\n\r\n Upton Parish Council cannot be held responsible for the content of external sites');
    }
</script>
<asp:Panel ID="plNews" runat="server" Style="">
    <asp:Repeater ID="rptNews" runat="server">
        <ItemTemplate>
            <asp:HyperLink runat="server" ID="NewsLink" NavigateUrl='<%# NewsUrl(Eval("NoticeId"))%>'
                Text='<%#Eval("Subject")%>' /><br />
            <asp:Panel ID="bodyPanel" runat="server" style="font-size: Smaller" Visible='<%# ShowBody %>'>
                <%# UptonParishCouncil.Site.Utils.TruncateString(UptonParishCouncil.Site.Utils.Strip(Eval("Body")), 260)%></asp:Panel>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
<asp:Panel ID="NewsRssPanel" runat="server" Style="" HorizontalAlign="Right">
    <asp:HyperLink runat="server" ID="hlRssLink">
        <asp:Image runat="server" ID="iRssLink" ImageUrl="~/images/RSS-mini.gif" /></asp:HyperLink>
        </asp:Panel>