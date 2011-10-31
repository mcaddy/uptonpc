<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwitterControl.ascx.cs"
    Inherits="UptonParishCouncil.Site.TwitterControl" %>
<style type="text/css">
    #twitter_custom_style .ajax__twitter
    {
        background-color: White;
        color: Black;
        -moz-border-radius: 0px;
        -webkit-border-radius: 0px;
        border-radius: 0px;
    }
</style>

<div id="twitter_custom_style">
    <ajaxToolkit:Twitter ID="Twitter1"
        runat="Server">
        <LayoutTemplate>
            <table width="100%">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <StatusTemplate>
            <tr>
                <td>
                    <img src="<%# Eval("User.ProfileImageUrl") %>" />
                </td>
                <td style="vertical-align: top;">
                    <div>
                        <%# Eval("Text") %></div>
                    <div style="text-align: right;">
                        posted: <i>
                            <%# Twitter.Ago((DateTime)Eval("CreatedAt")) %></i>
                    </div>
                </td>
            </tr>
        </StatusTemplate>
        <EmptyDataTemplate>
            No Recent Mentions for <%=Search %>
        </EmptyDataTemplate>
    </ajaxToolkit:Twitter>
</div>
