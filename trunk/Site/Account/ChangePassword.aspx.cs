using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace UptonParishCouncil.Site.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = Utils.GetMasterPage();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["UserName"]) && !string.IsNullOrEmpty(Request["OldPassword"]))
            {
                bool validUser = Membership.ValidateUser(Request["UserName"], Request["OldPassword"]);
                if (validUser)
                {
                    Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                    FormsAuthentication.SetAuthCookie(Request["UserName"], false);
                    Response.Redirect(string.Format("ChangePassword.aspx?OldPassword={0}", Request["OldPassword"]));
                }
                else
                {
                    Response.Redirect("ChangePassword.aspx");
                }
            }

            if (User.Identity == null || User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=%2fAccount%2fChangePassword.aspx");
            }

            if (!string.IsNullOrEmpty(Request["OldPassword"]))
            {
                CurrentPassword.TextMode = TextBoxMode.SingleLine;
                CurrentPassword.Enabled = false;
                CurrentPassword.Text = FakePassword(Request["OldPassword"].ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string FakePassword(string password)
        {
            string fakePassword = string.Empty;
            return fakePassword.PadRight(password.Length, '*');
        }



        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            if (Membership.ValidateUser(User.Identity.Name, GetPassword()))
            {
                Membership.GetUser().ChangePassword(GetPassword(), NewPassword.Text);

                //Show complete Message
                ChangePasswordPanel.Visible = false;
                PasswordChangedPanel.Visible = true;
            }
            else
            {

            }
        }

        private string GetPassword()
        {
            if (CurrentPassword.Enabled == false && !string.IsNullOrEmpty(Request["OldPassword"]))
            {
                return Request["OldPassword"];
            }

            return CurrentPassword.Text;
        }

        protected void CorrectPasswordCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Membership.ValidateUser(User.Identity.Name, GetPassword());
        }
    }
}