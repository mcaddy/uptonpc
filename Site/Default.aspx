<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Default" %>

<%@ Register Src="~/News/Preview.ascx" TagName="Preview" TagPrefix="UptonPC" %>
<%@ Register Src="TwitterControl.ascx" TagName="TwitterControl" TagPrefix="uc1" %>
<%@ Register Src="Events/EventsPreview.ascx" TagName="EventsPreview" TagPrefix="uc2" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellspacing="0" cellpadding="4px">
        <tr>
            <td style="vertical-align: top; border-right:1px solid gray;">
            <asp:Image ImageUrl="~/images/ElgarCentre.jpg" runat="server" AlternateText="Elgar Centre" />
                <h2>
                    Latest News</h2>
                <UptonPC:Preview ID="Preview1" runat="server" NewsType="General" />
            </td>
            <td style="vertical-align: top;">
                <h2>
                    Events</h2>
                <uc2:EventsPreview ID="EventsPreview1" runat="server" />
                <h2>
                    Twitter</h2>
                                    <style>
                #twitter_custom_style .ajax__twitter 
                {
                    background-color:White;
                    color: Black;
                                        -moz-border-radius: 0px;
                    -webkit-border-radius: 0px;
                    border-radius: 0px;
                }
                
                </style>
                <div id="twitter_custom_style">
                    <ajaxToolkit:Twitter Mode="Search" Search="#uptonpc OR from:NorthantsPolice" runat="Server" >
                    <LayoutTemplate>
                    <table>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </table>
                    </LayoutTemplate>
                    <StatusTemplate>
                            <tr>
                            <td><img src="<%# Eval("User.ProfileImageUrl") %>" /></td>
                            <td style="vertical-align:top;">
                            <div><%# Eval("Text") %></div>
                            <div style="text-align:right;">
                            posted: <i><%# Twitter.Ago((DateTime)Eval("CreatedAt")) %></i>
                            </div>
                            </td>
                            </tr>
                            <tr>
                            <td colspan="2">
                            <hr />
</td>
</tr>
                    </StatusTemplate>
                    <EmptyDataTemplate>
                    No Recent Mentions for #UptonPc
                    </EmptyDataTemplate>
                    </ajaxToolkit:Twitter>
                    </div>
            </td>
        </tr>
    </table>
</asp:Content>
