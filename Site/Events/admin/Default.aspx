<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Events.Admin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Event Admin</h2>
    <asp:Panel runat="server" ID="recuringEventPanel" Visible="false">
    <h3>
        Recurring Events</h3>
    <fieldset>
        <legend>Add Recurring Event</legend>
        <asp:Label ID="TitleRecuringLabel" runat="server" Text="Title:" AssociatedControlID="TitleRecuringTextBox" />
        <asp:TextBox ID="TitleRecuringTextBox" runat="server" CssClass="textEntry" /><br />
        <asp:Label ID="DescriptionRecuringLabel" runat="server" Text="Description:" AssociatedControlID="DescriptionRecuringTextBox" />
        <asp:TextBox ID="DescriptionRecuringTextBox" runat="server" CssClass="textEntry" /><br />
        <asp:Label ID="LocationRecuringLabel" runat="server" Text="Location:" 
            AssociatedControlID="LocationRecuringTextBox" />
        <asp:TextBox ID="LocationRecuringTextBox" runat="server" CssClass="textEntry" /><br />
        <asp:Label ID="StartDateLabel" runat="server" Text="Start Date:" AssociatedControlID="StartDateTextBox"
            Width="120px" />
        <asp:TextBox ID="StartDateTextBox" runat="server" Width="6em" />
        <ajaxToolkit:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="yyyy-MM-dd"
            TargetControlID="StartDateTextBox" />
        <br />
        <asp:Label ID="EndDateLabel" runat="server" Text="End Date:" AssociatedControlID="EndDateTextBox"
            Width="120px" />
        <asp:TextBox ID="EndDateTextBox" runat="server" Width="6em" />
        <ajaxToolkit:CalendarExtender ID="EndDateCalendarExtender" runat="server" Format="yyyy-MM-dd"
            TargetControlID="EndDateTextBox" />
        <br />
        <asp:Label ID="FrequencyLabel" runat="server" Text="Frequency:" Width="120px" />
        <asp:DropDownList ID="FrequencyWeekDropDownList" runat="server">
            <asp:ListItem Text="First" Value="1" />
            <asp:ListItem Text="Second" Value="2" />
            <asp:ListItem Text="Third" Value="3" />
            <asp:ListItem Text="Forth" Value="4" />
            <asp:ListItem Text="Last" Value="-1" />
        </asp:DropDownList>
        <asp:DropDownList ID="FrequencyDayDropDownList" runat="server">
            <asp:ListItem Text="Sunday" Value="1" />
            <asp:ListItem Text="Monday" Value="2" />
            <asp:ListItem Text="Tuesday" Value="3" />
            <asp:ListItem Text="Wednesday" Value="4" />
            <asp:ListItem Text="Thursday" Value="5" />
            <asp:ListItem Text="Friday" Value="6" />
            <asp:ListItem Text="Saturday" Value="7" />
        </asp:DropDownList>
        <br />
        <asp:Label ID="StartTimeRecuringLabel" runat="server" Text="Start Time:" AssociatedControlID="StartTimeRecuringTextBox" />
        <asp:TextBox ID="StartTimeRecuringTextBox" runat="server" MaxLength="5" Width="4em" />
        <br />
        <asp:Label ID="FeaturedRecuringLabel" runat="server" Text="Featured:" 
            AssociatedControlID="FeaturedRecuringCheckBox" />
            <asp:CheckBox ID="FeaturedRecuringCheckBox" runat="server" />
        <br />
        <br />
        <div class="submitButton">
            <asp:Button ID="CreateEventRecuringButton" runat="server" Text="Create Event" OnClick="CreateEventRecuringButton_Click" /></div>
    </fieldset>
    <asp:SqlDataSource ID="EventsRecuringSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPC %>"
        SelectCommand="UptonPC_GetAllEventsRecuring" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
    <asp:Repeater ID="EventsRecuringRepeater" runat="server" OnItemCommand="EventsRecuringRepeater_ItemCommand"
        DataSourceID="EventsRecuringSqlDataSource">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
        <ItemTemplate>
            <li><b>
                <%# this.RecurranceDescription(Eval("DayOfWeek"),Eval("WeekNumber")) %></b> <span style="font-size: smaller;">
                    (<%# Eval("StartDate","{0:dd/MM/yyyy}") %>-
                    <%# (Eval("EndDate") != System.DBNull.Value ? Eval("EndDate","{0:dd/MM/yyyy}") : ">") %>)</span>
                <%# Eval("Title") %>
                <asp:LinkButton ID="DeleteLinkButton" CommandName="Delete" CommandArgument='<%# Eval("RecuringEventId") %>'
                    runat="server" Text="Delete" />
            </li>
        </ItemTemplate>
    </asp:Repeater>
    </asp:Panel>
    <h3>
        Events</h3>
    <fieldset>
        <legend>Add Event</legend>
        <asp:Label ID="DateLabel" runat="server" Text="Date:" AssociatedControlID="DateTextBox"
            Width="120px" />
        <asp:TextBox ID="DateTextBox" runat="server" Width="6em" />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd"
            TargetControlID="DateTextBox" />
        <br />
        <asp:Label ID="StartTimeLabel" runat="server" Text="Start Time:" AssociatedControlID="StartTimeTextBox" />
        <asp:TextBox ID="StartTimeTextBox" runat="server" MaxLength="5" Width="4em" /> (24 Hrs) <br />
        <asp:RegularExpressionValidator runat="server" ValidationGroup="AddEvent" ID="StartTimeRegularExpressionValidator" ValidationExpression="{0..2}{0..9}:{0.5}{0..9}" ControlToValidate="StartTimeTextBox" />
        <asp:Label ID="TitleLabel" runat="server" Text="Summary:" AssociatedControlID="TitleTextBox" />
        <asp:TextBox ID="TitleTextBox" runat="server" CssClass="textEntry" /><br />
        <asp:Label ID="DescriptionLabel" runat="server" Text="Description:" AssociatedControlID="DescriptionTextBox" />
        <asp:TextBox ID="DescriptionTextBox" runat="server" CssClass="textEntry" /><br />
        <asp:Label ID="LocationLabel" runat="server" Text="Location:" AssociatedControlID="LocationTextBox" />
        <asp:TextBox ID="LocationTextBox" runat="server" CssClass="textEntry" />
        <br />
        <asp:Label ID="FeaturedLabel" runat="server" Text="Featured:" 
            AssociatedControlID="FeaturedCheckBox" />
            <asp:CheckBox ID="FeaturedCheckBox" runat="server" />
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="AddEvent" runat="server" />
        <div class="submitButton">
            <asp:Button ID="CreateEventButton" runat="server" ValidationGroup="AddEvent" Text="Create Event" OnClick="CreateEventButton_Click" /></div>
    </fieldset>
    <asp:SqlDataSource ID="EventsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPC %>"
        SelectCommand="UptonPC_GetAllEvents" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:Repeater ID="EventsRepeater" runat="server" OnItemCommand="EventsRepeater_ItemCommand"
        DataSourceID="EventsSqlDataSource">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
        <ItemTemplate>
            <li><b>
                <%# Eval("Date","{0:dd/MM/yyyy}") %></b> -
                <%# Eval("Title") %>
                -
                <asp:LinkButton ID="DeleteLinkButton" CommandName="Delete" CommandArgument='<%# Eval("EventId") %>'
                    runat="server" Text="Delete" />
            </li>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
