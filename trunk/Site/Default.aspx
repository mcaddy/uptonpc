<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Default" %>

<%@ Register Src="~/News/Preview.ascx" TagName="Preview" TagPrefix="UptonPC" %>
<%@ Register Src="TwitterControl.ascx" TagName="TwitterControl" TagPrefix="uc1" %>
<%@ Register Src="Events/EventsPreview.ascx" TagName="EventsPreview" TagPrefix="uc2" %>
<%@ Register src="ImageCarousel.ascx" tagname="ImageCarousel" tagprefix="uc3" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellspacing="0" cellpadding="4px">
        <tr>
            <td style="vertical-align: top; border-right:1px solid gray; padding-right:12px;">

                <uc3:ImageCarousel ID="ImageCarousel1" runat="server" ImageSourcePath="~/images/frontPage/" CycleEffect="scrollUp"/>
            
                <h2>
                    Latest News</h2>
                    <hr />
                <UptonPC:Preview ID="Preview1" runat="server" NewsType="General" Count="3" Width="585px" />
            </td>
            <td style="vertical-align: top; padding-left:12px;">
                <h2>
                    Events</h2>
                    <hr />
                <uc2:EventsPreview ID="EventsPreview1" runat="server" Count="3" />
                <h2>
                    Twitter</h2>
                    <hr />
                    <uc1:TwitterControl ID="TwitterControl1" runat="server" Search="#uptonpc OR from:NorthantsPolice" Mode="Search" />
                    
            </td>
        </tr>
    </table>
</asp:Content>
