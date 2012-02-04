<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ForgotPassword.aspx.cs" Inherits="UptonParishCouncil.Site.Account.ForgotPassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="ForgotPasswordPanel">
        <p>
            Enter your email to receive your password.</p>
        <p>
            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Text="Email:" />
            <asp:TextBox ID="UserName" runat="server" />
            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                ErrorMessage="Email" ToolTip="Email" ValidationGroup="PasswordRecovery" Text="*" /></p>
        <p>
            <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery"
                OnClick="SubmitButton_Click" />
        </p>
    </asp:Panel>
    <asp:Panel runat="server" ID="PasswordSentPanel" Visible="false">
        Your password has been sent to you.
    </asp:Panel>
</asp:Content>
