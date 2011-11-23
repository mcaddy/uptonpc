using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UptonParishCouncil.Site.Councillors
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = Utils.GetMasterPage();
        }

        public int GetDbSafeInt(object dbInt)
        {
            if (dbInt is Int32)
            {
                return (int)dbInt;
            }
            return 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            councillorsLinksPanel.Visible = User.IsInRole("IsCouncillor");

            if (!string.IsNullOrEmpty(Request["UserId"]))
            {
                Guid userId = Guid.Empty;
                try
                {
                    userId = Guid.Parse(Request["UserId"]);
                }
                catch (FormatException)
                {
                    //Do nothing, maybe should email
                }

                if (!userId.Equals(Guid.Empty))
                {
                    profilesSqlDataSource.SelectCommand = "UptonPC_GetCouncillorProfile";
                    profilesSqlDataSource.SelectParameters.Add("UserId", System.Data.DbType.Guid, userId.ToString());
                    titlePanel.Visible = false;
                }
            }
        }
    }
}