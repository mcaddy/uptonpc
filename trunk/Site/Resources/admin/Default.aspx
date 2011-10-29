<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Resources.admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:SqlDataSource ID="ResourceTypeSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
        SelectCommand="UptonPC_GetResourceTypes" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ResourcesSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:UptonPC %>" 
        SelectCommand="UptonPC_GetResourcesByType" 
        SelectCommandType="StoredProcedure" DeleteCommand="UptonPC_DeleteResource" 
        DeleteCommandType="StoredProcedure" 
        onupdating="ResourcesSqlDataSource_Updating" 
        UpdateCommand="UptonPC_SetResource" UpdateCommandType="StoredProcedure" 
        ondeleted="ResourcesSqlDataSource_Deleted" 
        onupdated="ResourcesSqlDataSource_Updated">
        <DeleteParameters>
            <asp:Parameter Name="ResourceId" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ResourceTypeDropDownList" 
                Name="ResourceTypeId" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ResourceId" Type="Int32" />
            <asp:Parameter Name="TypeId" Type="Int32" />
            <asp:Parameter Name="Data" Type="Object" />
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="FileName" Type="String" />
            <asp:Parameter Name="MIMEType" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
    Resource Type: <asp:DropDownList ID="ResourceTypeDropDownList" runat="server" 
        AutoPostBack="True" DataSourceID="ResourceTypeSqlDataSource" 
        DataTextField="Description" DataValueField="ResourceTypeId">
    </asp:DropDownList>
    
    <fieldset>
    <legend>Add Resource</legend>
    <asp:Label ID="TitleLabel" runat="server" Text="Title:" AssociatedControlID="TitleTextBox"/>
    <asp:TextBox ID="TitleTextBox" runat="server" MaxLength="50"/><br />

    <asp:Label ID="ResourceUploadLabel" runat="server" Text="File:" AssociatedControlID="TitleTextBox"/>
    <asp:FileUpload ID="ResourceUpload" runat="server" /><br />
    <asp:LinkButton ID="AddResourceLinkButton" runat="server" Text="Add Resource" 
            onclick="AddResourceLinkButton_Click" />
    </fieldset>

    <fieldset>
    <legend>Current Resources</legend>
    <asp:GridView ID="ResourcesGridView" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ResourceId" DataSourceID="ResourcesSqlDataSource" 
        onrowupdating="ResourcesGridView_RowUpdating">
        <Columns>
            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Title" 
                SortExpression="Title">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" Text='<%# Bind("Title") %>' runat="server"></asp:TextBox>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="DataHyperLink" NavigateUrl='<%# UptonParishCouncil.Site.Utils.ResourceUrl(Eval("ResourceId"), Eval("Title")) %>' Text='<%# Eval("Title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FileName" HeaderText="FileName" ReadOnly="true" />
            <asp:BoundField DataField="MIMEType" HeaderText="MIMEType"  ReadOnly="true" />
            <asp:BoundField DataField="UploadDate" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="UploadDate" HtmlEncodeFormatString="False" ReadOnly="True" 
                SortExpression="UploadDate" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    </fieldset>
</asp:Content>
