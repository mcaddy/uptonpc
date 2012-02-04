<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateProfile.aspx.cs" Inherits="UptonParishCouncil.Site.Councillors.admin.UpdateProfile" %>
<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
<!-- User Picker -->
    <asp:SqlDataSource ID="councillorsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPC %>"
            ProviderName="<%$ ConnectionStrings:UptonPC.ProviderName %>"
                SelectCommand="UptonPC_GetCouncillorProfiles" 
    SelectCommandType="StoredProcedure" ></asp:SqlDataSource>
    <asp:Panel ID="SelectCouncillorPanel" runat="server" >
        Select Councillor: <asp:DropDownList ID="councillorDropDownList" runat="server" 
            DataSourceID="councillorsSqlDataSource" DataTextField="DisplayName" 
            DataValueField="UserId" AutoPostBack="true" 
            onselectedindexchanged="councillorDropDownList_SelectedIndexChanged">
        </asp:DropDownList>
        <hr />
    </asp:Panel>
    <!-- Main Update Form -->
    <asp:SqlDataSource ID="profileSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPC %>"
            ProviderName="<%$ ConnectionStrings:UptonPC.ProviderName %>" 
    SelectCommand="UptonPC_GetCouncillorProfile" 
    SelectCommandType="StoredProcedure" 
    UpdateCommand="UptonPC_SetCouncillorProfile" 
    UpdateCommandType="StoredProcedure" 
        onselecting="profileSqlDataSource_SetUser" OnUpdating="profileSqlDataSource_SetUser">
        <SelectParameters>
            <asp:Parameter Name="UserId" Type="Empty" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="UserId" Type="Empty" />
            <asp:ControlParameter ControlID="BioTextBox" Name="Bio" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="PhoneTextBox" Name="ContactPhone" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="ResponsibilitesTextBox" Name="Responsibilites" PropertyName="Text" Type="String" />
        </UpdateParameters>
</asp:SqlDataSource>
<table>
<tr>
<td style="text-align:right;">First Name:</td>
<td><asp:Label ID="FirstNameLabel" runat="server"/></td>
</tr>

<tr>
<td style="text-align:right;">Surname:</td>
<td><asp:Label ID="SurnameLabel" runat="server"/></td>
</tr>

<tr>
<td style="text-align:right;">Email Address:</td>
<td><asp:Label ID="EmailLabel" runat="server"/></td>
</tr>

<tr>
<td style="text-align:right;">Phone Number:</td>
<td><asp:TextBox ID="PhoneTextBox" runat="server"/></td>
</tr>

<tr>
<td style="text-align:right;">Bio:</td>
<td><asp:TextBox ID="BioTextBox" runat="server" TextMode="MultiLine" Rows="5" Columns="80"/></td>
</tr>

<tr>
<td style="text-align:right;">Responsibilites:</td>
<td><asp:TextBox ID="ResponsibilitesTextBox" runat="server" TextMode="MultiLine" Rows="3"  Columns="80"/>
</td>
</tr>

<tr>
<td style="text-align:right;">Photo:</td>
<td>
    <asp:Image ID="profileImage" runat="server" Visible="false" />
    <asp:FileUpload ID="photoFileUpload" runat="server" /></td>
</tr>
</table>
 
<asp:Button ID="updateButton" runat="server" Text="Update" 
    onclick="updateButton_Click" />
</asp:Content>
