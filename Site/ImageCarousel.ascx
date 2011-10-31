<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageCarousel.ascx.cs"
    Inherits="UptonParishCouncil.Site.ImageCarousel" %>
    <style type="text/css">
    #nav { z-index: 50; position: absolute; bottom: 10px; right: 10px }
    #nav a { margin: 0 5px; padding: 3px 5px; border: 1px solid #ccc; background: #3a4f63; color:#dde4ec; font-weight:bold; text-decoration: none; }
    #nav a.activeSlide { background: #4b6c9e; color:#f9f9f9; }
    #nav a:focus { outline: none; }
    </style>
<div id="<%=ModuleDiv %>" style="width: 585px; height:250px">
    <div id="nav"></div>
    <asp:ListView ID="lstPhotos" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </LayoutTemplate>
        <ItemTemplate>
            <img src='<%# ResolveUrl(Container.DataItem.ToString()) %>' alt='<%# System.IO.Path.GetFileNameWithoutExtension(Container.DataItem.ToString()) %>' />
        </ItemTemplate>
    </asp:ListView>
</div>

<script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.js"></script>
<script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.cycle/2.88/jquery.cycle.all.js"></script>
<script type="text/javascript">
    $('#<%=ModuleDiv %>').cycle({fx: '<%=CycleEffect %>', <%=CycleParameters %>});
</script>

