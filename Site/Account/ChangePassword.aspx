<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="UptonParishCouncil.Site.Account.ChangePassword" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Panel runat="server" ID="ChangePasswordPanel">
    <h2>
        Change Password
    </h2>
    <p>
        Use the form below to change your password.
    </p>
    <p>
        New passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
    </p>
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>

    <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
            ValidationGroup="ChangeUserPasswordValidationGroup"/>

    <div class="accountInfo">
        <fieldset class="changePassword">
            <legend>Account Information</legend>
            <p>
                <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword" Text="Old Password:" />
                <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password" />
                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required." 
                    ValidationGroup="ChangeUserPasswordValidationGroup" Text="*" />
                <asp:CustomValidator ID="CorrectPasswordCustomValidator" runat="server" CssClass="failureNotification" 
                    ValidationGroup="ChangeUserPasswordValidationGroup"
                    ErrorMessage="Incorrect Old Password Supplied" 
                    ToolTip="Correct Old Password is required" Text="*" 
                    onservervalidate="CorrectPasswordCustomValidator_ServerValidate" />
            </p>
            <p>
                <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword" Text="New Password:" />
                <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password" />
                <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                        CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                        ValidationGroup="ChangeUserPasswordValidationGroup" Text="*" />
            </p>
            <p>
                <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword" Text="Confirm New Password:" />
                <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password" />
                <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                        CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                        ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup" Text="*" />
                <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                        CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                        ValidationGroup="ChangeUserPasswordValidationGroup" Text="*" />
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="CancelButton" runat="server" CausesValidation="False" Text="Cancel" onclick="CancelButton_Click"/>
            <asp:Button ID="ChangePasswordButton" runat="server" Text="Change Password" 
                ValidationGroup="ChangeUserPasswordValidationGroup" onclick="ChangePasswordButton_Click"/>
        </p>
    </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="PasswordChangedPanel" Visible="false">
        <h2>
            Change Password
        </h2>
        <p>
            Your password has been changed successfully.
        </p>
    </asp:Panel>

</asp:Content>