<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"
    Inherits="UptonParishCouncil.Site.News.Admin.Default" Title="Untitled Page" CodeBehind="Default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManagerProxy ID="smpNews" runat="server" />
    <asp:SqlDataSource ID="NewsItemSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPC %>"
        ProviderName="<%$ ConnectionStrings:UptonPC.ProviderName %>" SelectCommand="UptonPC_GetNewsItem"
        SelectCommandType="StoredProcedure" DeleteCommand="UptonPC_DeleteNewsItem" DeleteCommandType="StoredProcedure"
        OnDeleted="sdsNewsItem_Deleted" InsertCommand="UptonPC_SetNewsItem" InsertCommandType="StoredProcedure"
        OnInserting="sdsNewsItem_Inserting" OnInserted="sdsNewsItem_Inserted" UpdateCommand="UptonPC_SetNewsItem"
        UpdateCommandType="StoredProcedure" OnUpdated="sdsNewsItem_Updated">
        <DeleteParameters>
            <asp:Parameter Name="NoticeId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="TStamp" Type="DateTime" />
            <asp:Parameter Name="Subject" Type="String" />
            <asp:Parameter Name="Body" Type="String" />
            <asp:ControlParameter Name="TypeId" Type="Int32" ControlID="newsTypeDropDownList" />
            <asp:Parameter Name="Keywords" Type="String" />
            <asp:Parameter Name="NoticeId" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="newsGridView" Name="NoticeID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="TStamp" Type="DateTime" />
            <asp:Parameter Name="Subject" Type="String" />
            <asp:Parameter Name="Body" Type="String" />
            <asp:ControlParameter Name="TypeId" Type="Int32" ControlID="newsTypeDropDownList" />
            <asp:Parameter Name="Keywords" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <table style="width: 100%;">
        <tr style="vertical-align: top;">
            <td style="width: 300px;">
                <h1>
                    Manage
                    <asp:Label ID="lblNewsTitle" runat="server" />
                    <asp:DropDownList runat="server" ID="newsTypeDropDownList" AutoPostBack="true" />
                </h1>
                <asp:GridView ID="newsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="NoticeID"
                    ShowHeader="false" OnSelectedIndexChanged="NewsGridView_SelectedIndexChanged"
                    BorderStyle="None" BorderWidth="0px">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <%# Eval("TStamp", "{0:dd/MM/yyyy}")%>
                                -
                                <asp:LinkButton ID="selectLinkButton" runat="server" CausesValidation="False" CommandName="Select"><%# Eval("Subject") %></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td style="text-align: left;">
                <asp:FormView ID="fvNewsItem" runat="server" CellPadding="4" DataKeyNames="NoticeID"
                    DataSourceID="NewsItemSqlDataSource" HeaderText="View Existing" Width="100%"
                    BorderWidth="1px" BorderStyle="Solid" BorderColor="#4b6c9e">
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="header" ForeColor="White" />
                    <ItemTemplate>
                        <div>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit" />
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="Delete" OnClientClick="if (!confirm('Are you sure you want to delete this news item?')) {return false;}" />
                        </div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div>
                                        <b>Subject:</b>
                                        <asp:Label ID="SubjectLabel" runat="server" Text='<%# Eval("Subject") %>' />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <b>Date:</b>
                                        <asp:Label ID="TStampLabel" runat="server" Text='<%# Eval("TStamp", "{0:yyyy-MM-dd}") %>' />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <b>Body:</b><br />
                            <asp:Label ID="BodyLabel" runat="server" Text='<%# Bind("Body") %>' />
                        </div>
                        <div>
                            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit" />
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="Delete" OnClientClick="if (!confirm('Are you sure you want to delete this news item?')) {return false;}" />
                        </div>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <div>
                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="True" CommandName="Insert"
                                Text="Insert" ValidationGroup="Insert" />&nbsp;
                            <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel" />&nbsp;
                            <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="False" OnClientClick="window.open('UploadImage.aspx','_blank','width=500,height=200'); return false;"
                                Text="Upload Image" />
                        </div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div>
                                        <b>Subject:</b><br />
                                        <asp:TextBox ID="tbSubject" runat="server" Width="35em" Text='<%# Bind("Subject") %>' />
                                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" Display="Dynamic" ControlToValidate="tbSubject"
                                            ErrorMessage="Enter a subject" Font-Bold="True" Font-Size="Larger" Text="&nbsp;*"
                                            ValidationGroup="Insert" />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <b>Date:</b><br />
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="tbTStamp" Format="yyyy-MM-dd" />
                                        <asp:TextBox ID="tbTStamp" runat="server" Text='<%# Bind("TStamp", "{0:yyyy-MM-dd}") %>' />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <b>Body:</b><br />
                            <mrc:HtmlEditor runat="server" ID="htmlEditor" Content='<%# Bind("Body") %>' Height="300px" />
                        </div>
                        <div>
                            <asp:ValidationSummary ID="vsInsert" ValidationGroup="Insert" runat="server" ShowMessageBox="true"
                                ShowSummary="false" />
                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                Text="Insert" ValidationGroup="Insert" />&nbsp;
                            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel" />&nbsp;
                            <asp:LinkButton ID="lbUploadWindow" runat="server" CausesValidation="False" OnClientClick="window.open('UploadImage.aspx','_blank','width=500,height=200'); return false;"
                                Text="Upload Image" />
                        </div>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <div>
                            <asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update" />&nbsp;
                            <asp:LinkButton ID="LinkButton9" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel" />&nbsp;
                            <asp:LinkButton ID="LinkButton10" runat="server" CausesValidation="False" OnClientClick="window.open('UploadImage.aspx','_blank','width=500,height=200'); return false;"
                                Text="Upload Image" />
                        </div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div>
                                        Subject:<br />
                                        <asp:TextBox ID="TextBox1" runat="server" Width="35em" Text='<%# Bind("Subject") %>' />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        Date:<br />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbTStamp"
                                            Format="yyyy-MM-dd" />
                                        <asp:TextBox ID="tbTStamp" runat="server" Text='<%# Bind("TStamp", "{0:yyyy-MM-dd}") %>' />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div>
                            Body:<br />
                            <mrc:HtmlEditor runat="server" ID="htmlEditor" Content='<%# Bind("Body") %>' Height="300px" />
                        </div>
                        <div>
                            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update" />&nbsp;
                            <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel" />&nbsp;
                            <asp:LinkButton ID="lbUploadWindow" runat="server" CausesValidation="False" OnClientClick="window.open('UploadImage.aspx','_blank','width=500,height=200'); return false;"
                                Text="Upload Image" />
                        </div>
                    </EditItemTemplate>
                </asp:FormView>
            </td>
        </tr>
    </table>
</asp:Content>
