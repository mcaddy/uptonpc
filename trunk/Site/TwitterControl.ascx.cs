using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace UptonParishCouncil.Site
{
    public partial class TwitterControl : System.Web.UI.UserControl
    {
        public string Search
        {
            get
            {
                return Twitter1.Search;
            }

            set
            {
                Twitter1.Search = value;
            }
        }

        public string ScreenName
        {
            get
            {
                return Twitter1.ScreenName;
            }

            set
            {
                Twitter1.ScreenName = value;
            }
        }

        public TwitterMode Mode
        {
            get
            {
                return Twitter1.Mode;
            }

            set
            {
                Twitter1.Mode = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        
        }
    }
}