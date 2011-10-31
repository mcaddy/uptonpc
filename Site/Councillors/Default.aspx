<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Councillors.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Your Parish Councillors</h1>
    <asp:SqlDataSource ID="profilesSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
        SelectCommand="UptonPC_GetCouncillorProfiles" 
        SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:Repeater ID="profilesRepeater" runat="server" DataSourceID="profilesSqlDataSource">
    <HeaderTemplate>
    <table>
    <tr>
    <td colspan="3"><hr /></td>
    </HeaderTemplate>
    <FooterTemplate>
    </tr>
    </table>
    </FooterTemplate>
    <ItemTemplate>
    <tr>
    <td rowspan="4" style="vertical-align:top;">
    <asp:Image ID="profileImage" runat="server" ImageUrl='<%# Eval("ResourceId","~/Resources/GetBlob.ashx?id={0}") %>' />
    </td>
    <td colspan="2" style="font-size:larger; font-weight:bolder;"><%# Eval("FirstName") %> <%# Eval("Surname") %></td>
    </tr>
    <tr>
    <td><span style="font-style:italic; font-weight:bold;">Email:</span> <a href="mailto:<%# Eval("ContactEmail") %>"><%# Eval("ContactEmail") %></a></td> 
    <td style="text-align:right;"><span style="font-style:italic; font-weight:bold;">Phone:</span> <%# Eval("ContactPhone") %>
    </tr>

        <tr>
    <td colspan="2"><span style="font-style:italic; font-weight:bold;">Bio:</span><br /><%# Eval("Bio").ToString().Replace("\r", "<br/>")%></td></tr>

        <tr>
    <td colspan="2"><span style="font-style:italic; font-weight:bold;">Responsibilites:</span><br /><%# Eval("Responsibilites").ToString().Replace("\r","<br/>") %></td></tr>
    <tr><td colspan="3"><hr /></td></tr>
    
    </ItemTemplate>
    </asp:Repeater>
</asp:Content>
