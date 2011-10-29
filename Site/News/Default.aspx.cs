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
using System.Data.SqlClient;
using UptonParishCouncil.Site;

namespace UptonParishCouncil.Site.News
{
    public partial class Default : System.Web.UI.Page
    {
        public Defines.NewsTypeEnum newsType = Defines.NewsTypeEnum.None;

        public string GetLink(object Subject, object NoticeId)
        {
            string theLink = string.Empty;
            //filterContext.RouteData.Values.ContainsKey(idToCheck);
            if (Page.RouteData.Values.Count > 0)
            {
                theLink = Page.GetRouteUrl("SingleStory", 
                    new { 
                        categoryName = newsType.ToString(), 
                        Title = Subject.ToString().Replace(" ", "_"),
                        NoticeId = NoticeId});
            }
            return theLink;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                newsType = (Defines.NewsTypeEnum)Utils.ValidateIntQueryStringParam("NewsCategory");

                if (RouteData.Values.Count > 0)
                {
                    switch (RouteData.Values["categoryName"].ToString())
                    {
                        case "General":
                            newsType = Defines.NewsTypeEnum.General;
                            break;
                        case "Police":
                            newsType = Defines.NewsTypeEnum.Police;
                            break;
                        default:
                            break;
                    }
                }

                switch (newsType)
                {
                    case Defines.NewsTypeEnum.General:
                        NewsTitleLabel.Text = "General News";
                        hlRssLink.NavigateUrl = "/news/General_News.rss";
                        break;
                    case Defines.NewsTypeEnum.Police:
                        NewsTitleLabel.Text = "Police";
                        hlRssLink.NavigateUrl = "/news/Police_News.rss";
                        break;
                }

                //string whereClause = " WHERE ";
                //ClientScript.RegisterStartupScript(this.GetType(), "onload", "setForm('" + ddlSearchContext.ClientID + "','" + ddlDateSearchType.ClientID + "');", true);

                //if (ddlEntryType.SelectedValue != "1" && ddlEntryType.SelectedValue != "")
                //{
                //    whereClause += "(Type = " + ddlEntryType.SelectedValue + ")";
                //}

                //if ((tbSearchString.Text.Length != 0) || (tbDateMain.Text.Length != 0) || (tbDateAfter.Text.Length != 0))
                //{
                //    if (ddlSearchContext.SelectedValue == "TStamp")
                //    {//We need to do a date search
                //        if (ddlDateSearchType.SelectedValue == "BEF")
                //        {
                //            if (whereClause.Length > 10)
                //            {
                //                whereClause += " AND ";
                //            }
                //            whereClause += "(TStamp <= #" + tbDateMain.Text + "#)";

                //        }
                //        else if (ddlDateSearchType.SelectedValue == "AFT")
                //        {
                //            if (whereClause.Length > 10)
                //            {
                //                whereClause += " AND ";
                //            }
                //            whereClause += "(TStamp >= #" + tbDateMain.Text + "#)";
                //        }
                //        else
                //        {
                //            if (whereClause.Length > 10)
                //            {
                //                whereClause += " AND ";
                //            }
                //            whereClause += "((TStamp >= #" + tbDateMain.Text + "#) AND (TStamp <= #" + tbDateAfter.Text + "#))";
                //        }
                //    }
                //    else
                //    {//Now we do a normal search
                //        string pre = ddlSearchType.SelectedValue.Substring(0, 1);
                //        string post = ddlSearchType.SelectedValue.Substring(1);
                //        if (pre == "S") { pre = ""; }
                //        if (post == "S") { post = ""; }
                //        string Search = pre + tbSearchString.Text.Replace("'", "''") + post;
                //        if (ddlSearchContext.SelectedValue == "Subject")
                //        {
                //            if (whereClause.Length > 10)
                //            {
                //                whereClause += " AND ";
                //            }
                //            whereClause += "(Subject LIKE '" + Search + "')";
                //        }
                //        if (ddlSearchContext.SelectedValue == "Body")
                //        {
                //            if (whereClause.Length > 10)
                //            {
                //                whereClause += " AND ";
                //            }
                //            whereClause += "(Body LIKE '" + Search + "')";
                //        }
                //        if (ddlSearchContext.SelectedValue == "Both")
                //        {
                //            if (whereClause.Length > 10)
                //            {
                //                whereClause += " AND ";
                //            }
                //            whereClause += "((Subject LIKE '" + Search + "') OR (Body LIKE '" + Search + "'))";
                //        }
                //    }
                //}

                //if (whereClause.Length <= 10)
                //{
                //    whereClause = " WHERE TeamTypeId = " + newsType.ToString();
                //}
                //else
                //{
                //    whereClause += " AND (TeamTypeId = " + newsType.ToString() + ")";
                //}

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmdNews = new SqlCommand("UptonPC_GetNews", conn))
                    {
                        cmdNews.CommandType = CommandType.StoredProcedure;

                        cmdNews.Parameters.Add("@Count", SqlDbType.Int);
                        cmdNews.Parameters["@Count"].Value = int.Parse(ddlNumberOfEntrysToShow.SelectedValue);

                        cmdNews.Parameters.Add("@Catagory", SqlDbType.Int);
                        cmdNews.Parameters["@Catagory"].Value = (int)newsType;

                        //if (!string.IsNullOrEmpty(keyword))
                        //{
                        //    cmdNews.Parameters.Add("@Keyword", SqlDbType.NVarChar, 10);
                        //    cmdNews.Parameters["@Keyword"].Value = "%" + keyword + "%";
                        //}

                        rptNews.DataSource = cmdNews.ExecuteReader();
                        rptNews.DataBind();
                    }
                }
            }
        }
    }
}