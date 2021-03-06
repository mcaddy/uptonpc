﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UptonParishCouncil.Site
{
    public partial class MobileMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                NavigationMenu.MaximumDynamicDisplayLevels = 0;
            }
        }

        protected void DesktopVersionLinkButton_Click(object sender, EventArgs e)
        {
            Session["IsMobile"] = false;
            Response.Redirect("~/");
        }

        protected void NavigationMenu_DataBound(object sender, EventArgs e)
        {
            MenuItem homeMenuItem = new MenuItem("Home");
            homeMenuItem.NavigateUrl = "~/Mobile/Default.aspx";
            NavigationMenu.Items.AddAt(0, homeMenuItem);
        }
    }
}
