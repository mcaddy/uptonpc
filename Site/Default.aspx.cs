using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UptonParishCouncil.Dal;

namespace UptonParishCouncil.Site
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.GetMasterPage();
            if ((bool)Session["IsMobile"])
            {
                Response.Redirect("~/Mobile");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
