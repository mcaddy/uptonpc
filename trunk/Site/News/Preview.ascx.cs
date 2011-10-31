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
using System.Data.OleDb;
using System.Data.SqlClient;
using UptonParishCouncil.Dal;

namespace UptonParishCouncil.Site.News
{
    public partial class Preview : System.Web.UI.UserControl
    {

        public int Count
        {
            get { return (ViewState["Count"] != null ? (int)ViewState["Count"] : 5); }
            set { ViewState["Count"] = value; }
        }

        private Defines.NewsTypeEnum newsType;

        public Defines.NewsTypeEnum NewsType
        {
            get { return newsType; }
            set { newsType = value; }
        }

        private string keyword = string.Empty;

        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }

        private Unit width;

        public Unit Width
        {
            get { return width; }
            set { width = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!width.IsEmpty)
            {
                plNews.Width = this.width;
            }

            switch (newsType)
            {
                case Defines.NewsTypeEnum.General:
                    hlRssLink.NavigateUrl = "/news/General_News.rss";
                    break;
                default:
                    break;
            }

            using (SqlConnection conUptonPC = DbHelper.OpenSqlConnection())
            {
                using (SqlCommand cmdNews = new SqlCommand("UptonPC_GetNews", conUptonPC))
                {
                    cmdNews.CommandType = CommandType.StoredProcedure;

                    cmdNews.Parameters.Add("@Count", SqlDbType.Int);
                    cmdNews.Parameters["@Count"].Value = Count;

                    cmdNews.Parameters.Add("@Catagory", SqlDbType.Int);
                    cmdNews.Parameters["@Catagory"].Value = (int)newsType;

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        cmdNews.Parameters.Add("@Keyword", SqlDbType.NVarChar, 10);
                        cmdNews.Parameters["@Keyword"].Value = "%" + keyword + "%";
                    }

                    rptNews.DataSource = cmdNews.ExecuteReader();
                    rptNews.DataBind();
                }
            }
        }

        public string NewsUrl(object objNewsId)
        {
            return string.Format("~/News/Default.aspx?NewsCategory={0}&id={1}#n{1}", (int)newsType, objNewsId);
        }
    }
}