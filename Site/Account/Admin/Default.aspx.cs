using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace UptonParishCouncil.Site.Account.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = Utils.GetMasterPage();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MembershipUserCollection users = Membership.GetAllUsers();
                userRepeater.DataSource = users;
                userRepeater.DataBind();
            }
        }

        protected void IsCouncillorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Guid UserKey = Guid.Empty;
            String Role = string.Empty;
            
            CheckBox theCheckBox = sender as CheckBox;
            if (theCheckBox != null)
            {
                // Get the User Key
                HiddenField theHiddenField = theCheckBox.Parent.FindControl("UserKeyHiddenField") as HiddenField;
                if (theHiddenField != null){
                    UserKey = Guid.Parse(theHiddenField.Value);
                }

                // Get the Role
                Role = theCheckBox.ToolTip;
            }
            
            // Update DB
            
        }
    }
}