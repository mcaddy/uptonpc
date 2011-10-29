using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace UptonParishCouncil.Site.News.Admin
{
    public partial class UpdateNewsCategory : System.Web.UI.Page
    {

        public string TeamType(object objTeamType)
        {
            string output = "";

            int TeamType = int.Parse(objTeamType.ToString());

            switch (TeamType)
            {
                case 2:
                    output = "General";
                    break;
                case 3:
                    output = "Mini / Juniors";
                    break;
                case 4:
                    output = "Colts";
                    break;
                case 5:
                    output = "Social / Clubhouse";
                    break;
            }

            return output;

        }
    }
}