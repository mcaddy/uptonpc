<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsComment.ascx.cs"
    Inherits="UptonParishCouncil.Site.News.NewsComment" %>
<asp:SqlDataSource ID="CommentsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
    SelectCommand="UptonPC_GetCommentsForNotice" SelectCommandType="StoredProcedure" OnSelecting="CommentsSqlDataSource_Selecting"
    InsertCommand="UptonPC_AddCommentToNotice" InsertCommandType="StoredProcedure" OnInserting="CommentsSqlDataSource_Inserting">
    <SelectParameters>
        <asp:Parameter Name="NoticeId" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="NoticeId" Type="Int32" />
        <asp:Parameter Name="Comment" Type="String" />
        <asp:Parameter Name="UserId" />
    </InsertParameters>
</asp:SqlDataSource>

<table style="width: 100%;" cellpadding="2" cellspacing="0">
    <tr>
        <td>
            <asp:LinkButton ID="DisplayCommentsLinkButton" runat="server" 
            Text="Be the first to comment on this article"/>
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="ShareLinkButton" runat="server" 
            Text="Share this article via…"/>
        </td>
    </tr>
</table>
<asp:Panel ID="SharePanel" runat="server" Style="display:none;">
    <table cellpadding="4">
        <tr>
            <td>
                 <asp:HyperLink ID="FacebookHyperLink" runat="server" ToolTip="Share on Facebook">
                    <asp:Image ID="FacebookImage" runat="server" ImageUrl="~/images/icons/facebook.png" /> Facebook
                </asp:HyperLink>
            </td>
            <td>
                <asp:HyperLink ID="DiggHyperLink" runat="server" ToolTip="Digg this">
                    <asp:Image ID="DiggImage" runat="server" ImageUrl="~/images/icons/digg.png" /> Digg
                </asp:HyperLink>
            </td>
            <td>
                <asp:HyperLink ID="RedditHyperLink" runat="server" ToolTip="Like this? Reddit!">
                    <asp:Image ID="RedditImage" runat="server" ImageUrl="~/images/icons/reddit.png" /> Reddit
                </asp:HyperLink>
            </td>
            <td>
                <asp:HyperLink ID="GoogleHyperLink" runat="server" ToolTip="Bookmark this on Google bookmarks">
                    <asp:Image ID="GoogleImage" runat="server" ImageUrl="~/images/icons/google.png" /> Google
                    bookmarks
                </asp:HyperLink>
            </td>
            <td>
                <asp:HyperLink ID="YahooHyperLink" runat="server" ToolTip="Bookmark this on Yahoo! Bookmarks">
                    <asp:Image ID="YahooImage" runat="server" ImageUrl="~/images/icons/yahoo.png" /> Yahoo!
                    My Web
                </asp:HyperLink>
            </td>
            <td>
                <asp:HyperLink ID="StumbleUponHyperLink" runat="server" ToolTip="Discover this on StumbleUpon">
                    <asp:Image ID="StumbleUponImage" runat="server" ImageUrl="~/images/icons/stumbleupon.png" /> StumbleUpon
                </asp:HyperLink>
            </td>
             <td>
                <asp:ImageButton ID="TwitterImageButton" runat="server" ToolTip="Tweet this to Twitter" ImageUrl="~/images/icons/twitter.png" onclick="TwitterButton_Click" />
                 <asp:LinkButton ID="TwitterLinkButton" runat="server" ToolTip="Tweet this to Twitter" Text="Twitter" onclick="TwitterButton_Click" />
            </td>
            <td>
                <asp:HyperLink ID="SlashdotHyperLink" runat="server" ToolTip="Post this to Slashdot">
                    <asp:Image ID="SlashdotImage" runat="server" ImageUrl="~/images/icons/slashdot.png" /> Slashdot
                </asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="CommentsPanel" runat="server" Style="display:none;">
        <asp:Panel ID="AddCommentPanel" runat="server" Visible="false">
            <asp:TextBox ID="AddCommentTextBox" TextMode="MultiLine" runat="server" Width="100%" />
            <asp:LinkButton ID="AddCommentLinkButton" runat="server" OnClick="AddCommentLinkButton_Click">Add Comment</asp:LinkButton>
        </asp:Panel>
        <asp:Panel ID="LoginPanel" runat="server" Visible="false">
            You must <asp:LoginStatus ID="LoginStatus1" runat="server" /> before you can add comments!</asp:Panel>
        
        <asp:Repeater ID="CommentsRepeater" runat="server" DataSourceID="CommentsSqlDataSource">
            <ItemTemplate>
            <div style="border-bottom:Solid 2px #8EBB8E;">
                <div>
                    <%# Eval("Comment") %></div>
                <div style="text-align: right;">
                    <%# Eval("Username") %>,
                    <%# Eval("AddDate") %></div>
                </div>                    
            </ItemTemplate>
        </asp:Repeater>
    
</asp:Panel>
