<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
Inherits="UptonParishCouncil.Site.News.Default" Title="Upton Parish News" Codebehind="Default.aspx.cs" %>
<%@ Register src="NewsComment.ascx" tagname="NewsComment" tagprefix="UptonPC" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
    function exiting() {
        alert('You are now leaving the Upton Parish Council site\r\n\r\n Upton Parish Council cannot be held responsible for the content of external sites');
    }
    </script>

    
        <asp:SqlDataSource ID="NewsTypeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:UptonPc %>"
            ProviderName="<%$ ConnectionStrings:UptonPc.ProviderName %>" SelectCommand="uptonpc_GetNewsTypes" 
            SelectCommandType="StoredProcedure"/>
<span style="float:right;">
            <asp:HyperLink runat="server" ID="hlRssLink">
                <asp:Image runat="server" ID="iRssLink" ImageUrl="~/images/RSS.gif" />
            </asp:HyperLink>
    </span>
<h1><asp:Label runat="server" ID="NewsTitleLabel" /></h1>        

<asp:Repeater ID="rptNews" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
    <div style="border-bottom-width:1px; border-bottom-style:solid; border-bottom-color:#4b6c9e;">
    <span style="color:#666666; font-size:larger; font-weight:bold;"><a id="n<%# Eval("NoticeID")%>"><asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="false"
    NavigateUrl='<%# GetLink(Eval("Subject"), Eval("NoticeID"))%>' Text='<%# Eval("Subject") %>' /></a></span>
    <span style="float:right; vertical-align:bottom; font-size:smaller;"><%# ((DateTime)Eval("TStamp")).ToShortDateString()%></span>
    </div>
                <div><%# Eval("Body")%></div>
                <UptonPC:NewsComment ID="NewsComment1" runat="server" NoticeId='<%# Eval("NoticeID")%>' NoticeTypeId='<%# newsType%>' NoticeTitle='<%# Eval("Subject")%>'  />
    </ItemTemplate>
    <SeparatorTemplate>
    <div style="height:16px">&nbsp;</div>
    </SeparatorTemplate>
    <FooterTemplate>
    </FooterTemplate>
</asp:Repeater>

<asp:Panel runat="server" ID="searchPanel" Visible="false">
    <script type="text/javascript">
<!--

        function setForm(searchContextObj, DatesObj) {
            change(document.getElementById(searchContextObj));
            date_change(document.getElementById(DatesObj));
        }

        function change(obj) {
            document.getElementById('OTHERS').style.display = 'None';
            document.getElementById('DATES').style.display = 'None';
            if (obj.selectedIndex == 3)
                document.getElementById('DATES').style.display = '';
            else
                document.getElementById('OTHERS').style.display = '';
        }

        function date_change(obj) {
            document.getElementById('DATE2').style.display = 'None';
            if (obj.selectedIndex == 2)
                document.getElementById('DATE2').style.display = '';
            else
                document.getElementById('DATE1').style.display = '';
        }
// -->
</script>

    Search for entrys where
    <asp:DropDownList ID="ddlSearchContext" runat="server" onchange="change(this);">
        <asp:ListItem>Subject</asp:ListItem>
        <asp:ListItem>Body</asp:ListItem>
        <asp:ListItem Value="Both">Subject &amp; Body</asp:ListItem>
        <asp:ListItem Value="TStamp">Date</asp:ListItem>
    </asp:DropDownList>
    <span id="OTHERS">
        <asp:DropDownList ID="ddlSearchType" runat="server">
            <asp:ListItem Value="%%">Contains</asp:ListItem>
            <asp:ListItem Value="S%">Begins</asp:ListItem>
            <asp:ListItem Value="%S">Ends</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="tbSearchString" runat="server"></asp:TextBox>
    </span>
    <span id="DATES" style="display:none">
        <asp:DropDownList ID="ddlDateSearchType" runat="server" onchange="date_change(this);">
            <asp:ListItem Value="AFT">Is After</asp:ListItem>
            <asp:ListItem Value="BEF">Is Before</asp:ListItem>
            <asp:ListItem Value="MID">Is In between</asp:ListItem>
        </asp:DropDownList>
        <span id="DATE1">&nbsp<asp:TextBox ID="tbDateMain" runat="server"></asp:TextBox></span>
        <span id="DATE2" style="display:none"> and <asp:TextBox ID="tbDateAfter" runat="server"></asp:TextBox></span>
    </span>
    
    <br />
    Display
    <asp:DropDownList ID="ddlEntryType" runat="server" DataSourceID="NewsTypeSqlDataSource" DataTextField="Desc"
        DataValueField="ID">
    </asp:DropDownList>
    entrys<br />
    Show the
    <asp:DropDownList ID="ddlNumberOfEntrysToShow" runat="server">
        <asp:ListItem Selected="True">10</asp:ListItem>
        <asp:ListItem>25</asp:ListItem>
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
        <asp:ListItem>200</asp:ListItem>
    </asp:DropDownList>
    most relevant storys<br />
    <asp:Button ID="btnUpdate" runat="server" Text="Update" />
    </asp:Panel>
</asp:Content>

