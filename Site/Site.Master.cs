﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UptonParishCouncil.Site
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                NavigationMenu.MaximumDynamicDisplayLevels = 0;
            }
        }

        protected void mobileVersionLinkButton_Click(object sender, EventArgs e)
        {
            Session["IsMobile"] = true;
            Response.Redirect("~/Mobile/");
        }
    }
}
