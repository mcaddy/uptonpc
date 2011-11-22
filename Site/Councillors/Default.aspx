<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Councillors.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="councillorsLinksPanel" runat="server" style="float:right;">
<asp:HyperLink ID="ForReadingHyperLink" runat="server" NavigateUrl="~/Councillors/admin/ForReading.aspx" Text="For Reading" />
</asp:Panel>
<asp:Panel ID="titlePanel" runat="server">
<h1>Your Parish Councillors</h1>
<hr />
</asp:Panel>
    <asp:SqlDataSource ID="profilesSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
        SelectCommand="UptonPC_GetCouncillorProfiles" 
        SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:Repeater ID="profilesRepeater" runat="server" DataSourceID="profilesSqlDataSource">
    <SeparatorTemplate>
    <hr />
    </SeparatorTemplate>
    <ItemTemplate>

<span style="float:right;">
    <asp:Image ID="profileImage" runat="server" ImageUrl='<%# Eval("ResourceId","~/Resources/GetBlob.ashx?id={0}") %>' />
    </span>

    <span style="font-size:larger; font-weight:bolder;"><%# Eval("FirstName") %> <%# Eval("Surname") %></span><br />
    
    <p><%# Eval("Bio").ToString().Replace("\r", "</p><p>")%></p>
    
    <asp:Panel ID="ResponsibilitesPanel" runat="server" Visible='<%# !string.IsNullOrEmpty(Eval("Responsibilites").ToString()) %>'>
        <span style="font-style:italic; font-weight:bold;">Responsibilites:</span><br />
        <%# Eval("Responsibilites").ToString().Replace("\r","<br/>") %>
    </asp:Panel>
    <p>
        <span style="font-style:italic; font-weight:bold;">Email:</span> <a href="mailto:<%# Eval("ContactEmail") %>"><%# Eval("ContactEmail") %></a><br />
        <span style="font-style:italic; font-weight:bold;">Phone:</span> <%# Eval("ContactPhone") %>
    </p>
    <div style="text-align:right;">
        <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="Download vCard" NavigateUrl='<%# Eval("UserId","~/Councillor.vcf?UserId={0}") %>'><asp:Image ID="vcfImage" runat="server" ImageUrl="~/images/icons/vcf.gif" /></asp:HyperLink>
    </div>
    </ItemTemplate>
    </asp:Repeater>
</asp:Content>