<%@ Page Title="Articles for Reading" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ForReading.aspx.cs" Inherits="UptonParishCouncil.Site.Councillors.admin.ForReading" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:SqlDataSource ID="forReadingSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPC %>"
        SelectCommand="UptonPC_GetResourcesByType" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="4" Name="ResourceTypeId" Type="Int32" />
            <asp:Parameter DefaultValue="182" Name="maxAge" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Repeater ID="forReadingRepeater" runat="server" DataSourceID="forReadingSqlDataSource">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <FooterTemplate>
            </ul></FooterTemplate>
        <ItemTemplate>
            <%# MonthTitle(Eval("UploadDate")) %>
            <li>
                <asp:HyperLink runat="server" ID="dataHyperLink" NavigateUrl='<%# UptonParishCouncil.Site.Utils.ResourceUrl(Eval("ResourceId"), Eval("Title")) %>'
                    Text='<%# Eval("Title") %>' /></li>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
