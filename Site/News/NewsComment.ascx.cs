using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace UptonParishCouncil.Site.News
{
    public partial class NewsComment : System.Web.UI.UserControl
    {
        private string FacebookLink = "http://www.facebook.com/share.php?u={URL}";
        private string ReditLink = "http://reddit.com/submit?url={URL}&title={Title}";
        private string YahooLink = "http://bookmarks.yahoo.com/toolbar/savebm?opener=tb&u={URL}&t={Title}";
        private string TwitterLink = "http://twitter.com/home/?status=#UptonPC+{Tweet}";
        private string DiggLink = "http://digg.com/submit?phase=2&url={URL}&title={Title}";
        private string GoogleLink = "http://www.google.com/bookmarks/mark?op=add&bkmk={URL}&title={Title}";
        private string StumbleUponLink = "http://www.stumbleupon.com/submit?url={URL}&title={Title}";
        private string SlashdotLink = "http://slashdot.org/bookmark.pl?url={URL}&title={Title}";


        [Bindable(true), Category("Behavior"), DefaultValue(0),
        Description("The Notice to display comments for.")]
        public int NoticeId
        {
            get
            {
                return noticeCommentProperties.NoticeID;
            }
            set
            {
                noticeCommentProperties.NoticeID = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(0),
Description("The Notice Type Id to display comments for.")]
        public int NoticeTypeId
        {
            get
            {
                return noticeCommentProperties.TypeId;
            }
            set
            {
                noticeCommentProperties.TypeId = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(0),
        Description("The Notice Title for the Links.")]
        public string NoticeTitle
        {
            get
            {
                return noticeCommentProperties.Title;
            }
            set
            {
                noticeCommentProperties.Title = value;
            }
        }

        CurrentNoticeCommentProperties noticeCommentProperties = new CurrentNoticeCommentProperties(); 

        [Serializable()]
        private struct CurrentNoticeCommentProperties
        {
            public int NoticeID;
            public string Title;
            public int TypeId;
        }

        protected override object SaveControlState()
        {
            return noticeCommentProperties;
        }

        protected override void LoadControlState(object savedState)
        {
            noticeCommentProperties = new CurrentNoticeCommentProperties();
            noticeCommentProperties = (CurrentNoticeCommentProperties)savedState;
        } 

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCommentsLinkButtonText();
            DisplayCommentsLinkButton.OnClientClick = "document.getElementById('" + CommentsPanel.ClientID + "').style.display = ''; document.getElementById('" + SharePanel.ClientID + "').style.display = 'none'; return false;";
            ShareLinkButton.OnClientClick = "document.getElementById('" + CommentsPanel.ClientID + "').style.display = 'none'; document.getElementById('" + SharePanel.ClientID + "').style.display = ''; return false;";

            if (Context.User.Identity.IsAuthenticated)
            {
                AddCommentPanel.Visible = true;
            }
            else
            {
                LoginPanel.Visible = true;
            }
  
            //Set Links
            string PageUrl = string.Empty;
            if (string.IsNullOrEmpty(NoticeTitle))
            {
                PageUrl = "http://www.uptonpc.org/News/" + (Defines.NewsTypeEnum)NoticeTypeId + "/";
            }
            else
            {
                PageUrl = "http://www.uptonpc.org/News/" + (Defines.NewsTypeEnum)NoticeTypeId + "/" + NoticeId + "/" + NoticeTitle.Replace(" ", "");
            }
            
            FacebookHyperLink.NavigateUrl = FacebookLink.Replace("{URL}", Server.UrlEncode(PageUrl));
            RedditHyperLink.NavigateUrl = ReditLink.Replace("{URL}", Server.UrlEncode(PageUrl)).Replace("{Title}", Server.UrlEncode(NoticeTitle));
            YahooHyperLink.NavigateUrl = YahooLink.Replace("{URL}", Server.UrlEncode(PageUrl)).Replace("{Title}", Server.UrlEncode(NoticeTitle));
            TwitterImageButton.CommandName = noticeCommentProperties.Title;
            TwitterImageButton.CommandArgument = noticeCommentProperties.NoticeID.ToString();
            TwitterLinkButton.CommandName = noticeCommentProperties.Title;
            TwitterLinkButton.CommandArgument = noticeCommentProperties.NoticeID.ToString();
            DiggHyperLink.NavigateUrl = DiggLink.Replace("{URL}", Server.UrlEncode(PageUrl)).Replace("{Title}", Server.UrlEncode(NoticeTitle));
            GoogleHyperLink.NavigateUrl = GoogleLink.Replace("{URL}", Server.UrlEncode(PageUrl)).Replace("{Title}", Server.UrlEncode(NoticeTitle));
            StumbleUponHyperLink.NavigateUrl = StumbleUponLink.Replace("{URL}", Server.UrlEncode(PageUrl)).Replace("{Title}", Server.UrlEncode(NoticeTitle));
            SlashdotHyperLink.NavigateUrl = SlashdotLink.Replace("{URL}", Server.UrlEncode(PageUrl)).Replace("{Title}", Server.UrlEncode(NoticeTitle));
        }

        private void SetCommentsLinkButtonText()
        {
            int CommentCount = 0;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UptonPC_GetCommentCountForNotice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add("@NoticeId", SqlDbType.Int);
                    cmd.Parameters["@NoticeId"].Value = NoticeId;

                    CommentCount = (int)cmd.ExecuteScalar();
                }
            }

            if (CommentCount > 0)
            {
                DisplayCommentsLinkButton.Text = string.Format("Show Comments ({0})", CommentCount);
            }
        }

        protected void AddCommentLinkButton_Click(object sender, EventArgs e)
        {
            CommentsSqlDataSource.Insert();
            AddCommentTextBox.Text = string.Empty;
            SetCommentsLinkButtonText();
            CommentsRepeater.DataBind();
            CommentsPanel.Style[HtmlTextWriterStyle.Display] = "";
        }

        protected void CommentsSqlDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@NoticeId"].Value = NoticeId;
        }

        protected void CommentsSqlDataSource_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                e.Command.Parameters["@NoticeId"].Value = NoticeId;
                e.Command.Parameters["@UserId"].Value = new Guid(Membership.GetUser().ProviderUserKey.ToString());
                e.Command.Parameters["@Comment"].Value = AddCommentTextBox.Text;
            }
            else
            {
                e.Cancel = true;
            }
        }

        public static string MakeTinyUrl(string Url)
        {
            try
            {
                if (Url.Length <= 12)
                {
                    return Url;
                }
                if (!Url.ToLower().StartsWith("http") && !Url.ToLower().StartsWith("ftp"))
                {
                    Url = "http://" + Url;
                }
                var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + Url);
                var res = request.GetResponse();
                string text;
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    text = reader.ReadToEnd();
                }
                return text;
            }
            catch (Exception)
            {
                return Url;
            }
        }

        protected void TwitterButton_Click(object sender, EventArgs e)
        {
            string Title = string.Empty;
            int NoticeId = 0;

            if (sender.GetType().Equals(typeof(LinkButton)))
            {
                Title = ((LinkButton)sender).CommandName;
                NoticeId = int.Parse(((LinkButton)sender).CommandArgument);
            }

            if (sender.GetType().Equals(typeof(ImageButton)))
            {
                Title = ((ImageButton)sender).CommandName;
                NoticeId = int.Parse(((ImageButton)sender).CommandArgument);
            }

            string TwitterUrl = TwitterLink.Replace("{Tweet}", Server.UrlEncode(Utils.TruncateString(Title, 110) + ", " +
                MakeTinyUrl("http://www.UptonPC.org/News/single.aspx?id=" + NoticeId)));
            
            Response.Redirect(TwitterUrl);
        }
    }
}