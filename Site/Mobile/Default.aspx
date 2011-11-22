<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Mobile.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Mobile.Default" %>

<%@ Register Src="~/News/Preview.ascx" TagName="Preview" TagPrefix="UptonPC" %>
<%@ Register Src="~/Events/EventsPreview.ascx" TagName="EventsPreview" TagPrefix="UptonPC" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Latest News</h2>
    <hr />
    <UptonPC:Preview ID="newsPreview" runat="server" NewsType="General" Count="3" ShowBody="false" />
    
    <h2>
        Events</h2>
    <hr />
    <UptonPC:EventsPreview ID="eventsPreview" runat="server" Count="3" />
    
    <h2>
        Latest Minutes</h2>
    <hr />
    <asp:HyperLink ID="minutesHyperLink" runat="server" />
    <h2>
        Latest NewsLetter</h2>
    <hr />
    <asp:HyperLink ID="newsletterHyperLink" runat="server" />

    <h2>
        Your Councillors</h2>
    
    <hr />
        <asp:SqlDataSource ID="profilesSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
        SelectCommand="UptonPC_GetCouncillorProfiles" 
        SelectCommandType="StoredProcedure" />

           <asp:Repeater ID="profilesRepeater" runat="server" DataSourceID="profilesSqlDataSource">
    <ItemTemplate>
        <asp:HyperLink ID="profileLink" runat="server" NavigateUrl='<%# Eval("UserId","~/Councillors/Default.aspx?UserId={0}") %>' Text='<%# string.Format("{0} {1}",Eval("FirstName"), Eval("Surname")) %>' /><br />
    </ItemTemplate>
    </asp:Repeater>
        <h2>
        Local Info</h2>
    <hr />
    <i>Comming Soon</i>
</asp:Content>
