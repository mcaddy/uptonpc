<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" Inherits="UptonParishCouncil.Site.News.Admin.ManagePolls" Title="Untitled Page" Codebehind="ManagePolls.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPageTitle" Runat="Server">
Manage Polls
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainSite" Runat="Server">
    <table>
    <tr>
    <td>
        <asp:Repeater ID="rptPolls" runat="server">
        <HeaderTemplate><table></HeaderTemplate>
        <FooterTemplate></table></FooterTemplate>
        <ItemTemplate>
            <tr>
            <td><asp:LinkButton ID="lbPoll" runat="server" Text='<%# XPath("@Question") %>' CommandArgument='<%# XPath("@Guid") %>' OnClick="lbPoll_Click" /> (<%# ((XPath("@Active")!= null)&&(XPath("@Active").ToString()=="True") ? "Active" : "Inactive") %>)</td>
            </tr>
        </ItemTemplate>
        <SeparatorTemplate></SeparatorTemplate>
        </asp:Repeater>
    </td>
    <td style="vertical-align:top;">
        <asp:FormView ID="fvPoll" runat="server">
        <ItemTemplate>
        <table>
        <tr>
        <td style="font-size:larger;"><%# XPath("@Question") %> (<asp:LinkButton runat="server" ID="lbToggleActive" Text='<%# ((XPath("@Active")!= null)&&(XPath("@Active").ToString()=="True") ? "Active" : "Inactive") %>' CommandArgument='<%# ((XPath("@Active")!= null)&&(XPath("@Active").ToString()=="True") ? "False" : "True") %>' OnClick="lbToggleActive_Click" />)</td>
        </tr>
        <tr>
        <td>
        
        <asp:Repeater ID="rptPolls" runat="server" DataSource='<%#XPathSelect ("Answer") %>'>
        <HeaderTemplate><table></HeaderTemplate>
        <FooterTemplate></table></FooterTemplate>
        <ItemTemplate>
            <tr>
            <td><%# XPath(".") %></td>
            </tr>
        </ItemTemplate>
        <SeparatorTemplate></SeparatorTemplate>
        </asp:Repeater>
        
        </td>
        </tr>
        </table>
        </ItemTemplate>
        </asp:FormView>
    </td>
    </tr>
    </table>
</asp:Content>


