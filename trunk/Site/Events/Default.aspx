<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="Default.aspx.cs" Inherits="UptonParishCouncil.Site.Events.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Events</h2>
   
<mrc:DbEventCalendar runat="server" ID="EventsCalendar1" ConnectionString="<%$ ConnectionStrings:UptonPC %>"
DayStyle-HorizontalAlign="Left" DayStyle-VerticalAlign="Top" FirstDayOfWeek="Monday" DatabaseName="UptonPC"
DayStyle-Height="40px" TodayDayStyle-BackColor="#4b6c9e" TitleStyle-BackColor="#3a4f63" 
TitleStyle-ForeColor="White" TitleFormat="MonthYear" NextPrevStyle-ForeColor="White"/>

</asp:Content>




