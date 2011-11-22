using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UptonParishCouncil.Site.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = Utils.GetMasterPage();
        }

        protected void Page_Load(object sender, EventArgs e) { }

        private void Redirect()
        {
            string continueUrl = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            {
                continueUrl = Request.QueryString["ReturnUrl"];
            }
            else
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        private bool CreateUser()
        {
            MembershipCreateStatus status;

            MembershipUser NewUser = Membership.CreateUser(Email.Text, Password.Text, Email.Text,null,null,true, out status);
            
            if (NewUser != null)
            {
                InsertExtraInfo.InsertParameters.Add("UserId", NewUser.ProviderUserKey.ToString());
                InsertExtraInfo.Insert();

                return true;
            }
            else
            {
                ErrorMessage.Text = GetErrorMessage(status);
                return false;
            }
        }

        public string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                case MembershipCreateStatus.DuplicateEmail:
                    return "That e-mail address has already been registered exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            if (CreateUser())
            {
                FormsAuthentication.SetAuthCookie(Email.Text, false);
                Redirect();
            }
        }

    }
}
