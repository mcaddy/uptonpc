using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UptonParishCouncil.Site
{
    public partial class TwitterControl : System.Web.UI.UserControl
    {
        public string SearchTerm { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScriptManager clientScriptManager = Page.ClientScript;
            if (!clientScriptManager.IsClientScriptBlockRegistered("twitterSearch"))
            {
                clientScriptManager.RegisterStartupScript(this.GetType(), "twitterSearch", "twitterSearch('" + Server.UrlEncode(SearchTerm) + "');", true);
            }
        }
    }
}