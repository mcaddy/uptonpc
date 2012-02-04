using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Security;
using MrcControls.Utils;
using System.Net.Mail;

namespace UptonParishCouncil.Site.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = Utils.GetMasterPage();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string NewPassword = Password.Generate();

            MembershipUser foundUser = Membership.GetUser(UserName.Text);
            if (foundUser != null)
            {
                string RandomPassword = foundUser.ResetPassword();
                
                foundUser.ChangePassword(RandomPassword, NewPassword);
            }

            //Send email
            using (MailMessage forgotEmailMailMessage = new MailMessage())
            {
                forgotEmailMailMessage.To.Add(new MailAddress(foundUser.UserName));

                forgotEmailMailMessage.Subject = "UPTONPC.ORG - Your new details";

                string bodyTemplate = "<p>Please return to the site and log in using the following information.</p>\r\n<p><b>User Name:</b> <%UserName%><br />\r\n<b>Password:</b> <%Password%></p>\r\n<p>Visit <a href=\"http://<%Url%>/account/ChangePassword.aspx?OldPassword=<%UrlEncodePassword%>&Username=<%UrlEncodeUserName%>\">http://<%Url%>/account/ChangePassword.aspx</a> to Select a new password</p>";

                bodyTemplate = bodyTemplate.Replace("<%UserName%>", foundUser.UserName);
                bodyTemplate = bodyTemplate.Replace("<%UrlEncodeUserName%>", Server.UrlEncode(foundUser.UserName));

                bodyTemplate = bodyTemplate.Replace("<%Password%>", NewPassword);
                bodyTemplate = bodyTemplate.Replace("<%UrlEncodePassword%>", Server.UrlEncode(NewPassword));

                bodyTemplate = bodyTemplate.Replace("<%Url%>", this.Request.Url.Authority);

                forgotEmailMailMessage.Body = bodyTemplate;
                forgotEmailMailMessage.IsBodyHtml = true;

                using (SmtpClient client = new SmtpClient())
                {
                    client.Send(forgotEmailMailMessage);
                }
            }

            //Show complete Message
            ForgotPasswordPanel.Visible = false;
            PasswordSentPanel.Visible = true;
        }
    }
}