<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="UptonParishCouncil.Site.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:SqlDataSource ID="InsertExtraInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
        InsertCommand="UptonPC_AddAddress" InsertCommandType="StoredProcedure"
        ProviderName="<%$ ConnectionStrings:ApplicationServices.ProviderName %>">
        <InsertParameters>
            <asp:ControlParameter Name="FirstName" Type="String" ControlID="FirstName" DefaultValue="" PropertyName="Text" />
            <asp:ControlParameter Name="Surname" Type="String" ControlID="Surname" DefaultValue="" PropertyName="Text" />
            <asp:ControlParameter Name="HouseNumber" Type="String" ControlID="HouseNumber" PropertyName="Text" />
            <asp:ControlParameter Name="Street" Type="String" ControlID="Street" PropertyName="Text" />
            <asp:ControlParameter Name="PostCode" Type="String" ControlID="PostCode" PropertyName="Text" />
        </InsertParameters>
    </asp:SqlDataSource>
    <h2>
        Create a New Account</h2>
    Before you can use the interactive components of our site we need to collect some
    information.<br />
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="RegisterUserValidationGroup" ShowMessageBox="True" ShowSummary="False" />

         <fieldset class="register">
                    <legend>Account Information</legend>
                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" Text="E-mail:" />
                    <asp:TextBox ID="Email" runat="server" CssClass="textEntry" />
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                        CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                        ValidationGroup="RegisterUserValidationGroup" Text="*" /><br />
                    
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="Password:" />
                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                        ValidationGroup="RegisterUserValidationGroup" Text="*" /><br />
                    
                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"
                        Text="Confirm Password:" />
                    <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password" />
                    <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired"
                        runat="server" ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup"
                        Text="*" />
                    
                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                        ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                        ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="RegisterUserValidationGroup"
                        Text="*" />
                </fieldset>

                <fieldset class="address">
                    <legend>Your Details</legend>
                    
                    <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstName" Text="First Name:" />
                    <asp:TextBox ID="FirstName" runat="server" CssClass="textEntry" />
                    <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" ControlToValidate="FirstName"
                        CssClass="failureNotification" ErrorMessage="First name is required."
                        ToolTip="First name is required." ValidationGroup="RegisterUserValidationGroup"
                        Text="*" /><br />
                    
                    <asp:Label ID="SurnameLabel" runat="server" AssociatedControlID="Surname"
                        Text="Surname:" />
                    <asp:TextBox ID="Surname" runat="server" CssClass="textEntry" />
                    <asp:RequiredFieldValidator ID="SurnameRequiredFieldValidator" runat="server" ControlToValidate="Surname"
                        CssClass="failureNotification" ErrorMessage="Surname is required."
                        ToolTip="Surname is required." ValidationGroup="RegisterUserValidationGroup"
                        Text="*" /><br />
                    
                    <asp:Label ID="HouseNumberLabel" runat="server" AssociatedControlID="HouseNumber"
                        Text="House / Flat Name or Number:" />
                    <asp:TextBox ID="HouseNumber" runat="server" CssClass="textEntry" />
                    <asp:RequiredFieldValidator ID="HouseNumberRequired" runat="server" ControlToValidate="HouseNumber"
                        CssClass="failureNotification" ErrorMessage="House Name / Number is required."
                        ToolTip="House Name / Number is required." ValidationGroup="RegisterUserValidationGroup"
                        Text="*" /><br />
                    
                    <asp:Label ID="StreetLabel" runat="server" AssociatedControlID="Street" Text="Street Name:" />
                    <asp:TextBox ID="Street" runat="server" CssClass="textEntry" />
                    <asp:RequiredFieldValidator ID="StreetRequired" runat="server" ControlToValidate="Street"
                        CssClass="failureNotification" ErrorMessage="Street Name is required." ToolTip="Street Name is required."
                        ValidationGroup="RegisterUserValidationGroup" Text="*" /><br />
                    
                    <asp:Label ID="PostCodeLabel" runat="server" AssociatedControlID="PostCode" Text="PostCode:" />
                    <asp:TextBox ID="PostCode" runat="server" CssClass="textEntry" />
                    <asp:RequiredFieldValidator ID="PostCodeRequired" runat="server" ControlToValidate="PostCode"
                        CssClass="failureNotification" ErrorMessage="PostCode is required." ToolTip="PostCode is required."
                        ValidationGroup="RegisterUserValidationGroup" Text="*" />
                </fieldset>
                <br />
            <asp:Button ID="CreateUserButton" runat="server" Text="Register" ValidationGroup="RegisterUserValidationGroup"
                OnClick="CreateUserButton_Click" />

        <p class="note">
            Passwords are required to be a minimum of
            <%= Membership.MinRequiredPasswordLength %>
            characters in length.</p>

        <p>We only collect the minimum amount of data required to allow us to keep track of who is using our site, we will not pass it on to anyone or use it for any purposes other than to contact you regarding matters relating to Upton Parish Council.</p>
</asp:Content>
