<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventsPreview.ascx.cs" Inherits="UptonParishCouncil.Site.Events.EventsPreview" %>
<asp:SqlDataSource ID="EventsPreviewSqlDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
    SelectCommand="UptonPC_GetFeaturedEvents" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
<asp:Repeater ID="Repeater1" runat="server" 
    DataSourceID="EventsPreviewSqlDataSource">

    <ItemTemplate>
    <%# DateHeader(Eval("Date")) %>
    <div> <asp:HyperLink runat="server" ID="eventPreviewHyperLink" NavigateUrl='<%# Eval("EventId", "~/Events/Default.aspx?Id={0}") %>' ToolTip='<%# Eval("Description") %>' ><%# Eval("Time") %> - <%# Eval("Title") %></asp:HyperLink>, <%# Eval("Location") %> </div>
    </ItemTemplate>
</asp:Repeater>

