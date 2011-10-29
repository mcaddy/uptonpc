<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" Inherits="UptonParishCouncil.Site.News.Admin.UpdateNewsCategory" Title="Redruth Rugby - Update News Category" Codebehind="UpdateNewsCategory.aspx.cs" %>
<asp:Content ID="cTitle" ContentPlaceHolderID="cphPageTitle" runat="server">
Update News Category
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainSite" Runat="Server">
    <asp:SqlDataSource ID="sdsNews" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
        ProviderName="<%$ ConnectionStrings:UptonPC.ProviderName %>"
        SelectCommand="SELECT [ID], [Subject], [TeamTypeId] FROM [UptonPC_News] ORDER BY [TStamp] DESC" 
        UpdateCommand="UPDATE [UptonPC_News] SET [TeamTypeId] = @TeamTypeId WHERE [ID] = @ID">
        <UpdateParameters>
            <asp:Parameter Name="TeamTypeId" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="sdsNews">
        <Columns>
            <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" ReadOnly="true"/>
            <asp:TemplateField HeaderText="TeamTypeId" SortExpression="TeamTypeId">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlTeamType" runat="server" Text='<%# Bind("TeamTypeId") %>'>
                        <asp:ListItem Text="General" Value="2" />
                        <asp:ListItem Text="Mini / Juniors" Value="3" />
                        <asp:ListItem Text="Colts" Value="4" />
                        <asp:ListItem Text="Social / Clubhouse" Value="5" />
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# TeamType(Eval("TeamTypeId")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>

</asp:Content>