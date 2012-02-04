<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Account.Admin.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Manage Users</h1>

    <asp:Repeater ID="userRepeater" runat="server">
    <HeaderTemplate>
    <table>
        <tr style="height:120px;">
            <th></th>
            <th class="verticalText">isCouncillor</th>
            <th class="verticalText">User Admin</th>
            <th class="verticalText">Resource Admin</th>
            <th class="verticalText">News Admin</th>
            <th class="verticalText">Events Admin</th>
        </tr>
    </HeaderTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
        <ItemTemplate>
        <tr style="text-align:center;">
        <td><%# Eval("UserName")%> <asp:HiddenField ID="UserKeyHiddenField" runat="server" Value='<%# Eval("ProviderUserKey") %>' /></td>
        <td><asp:CheckBox ID="isCouncillorCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="IsCouncillorCheckBox_CheckedChanged" /></td>
        <td><asp:CheckBox ID="userAdminCheckBox" runat="server" /></td>
        <td><asp:CheckBox ID="resourceAdminCheckBox" runat="server" /></td>
        <td><asp:CheckBox ID="newsAdminCheckBox" runat="server" /></td>
        <td><asp:CheckBox ID="eventsAdminCheckBox" runat="server" /></td>
        </tr>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
