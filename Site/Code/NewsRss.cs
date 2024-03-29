using System.Web;
using System.Text;
using System;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Routing;
using UptonParishCouncil.Site;
using System.Data;

public class NewsRss : IHttpHandler
{
    private string RFC822Date(DateTime inputDate)
    {
        return inputDate.ToString("ddd, dd MMM yyyy HH:mm:ss ")+"GMT";
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/rss+xml";

        Defines.NewsTypeEnum newsType = (Defines.NewsTypeEnum)Utils.ValidateIntQueryStringParam("NewsCategory");
        string FeedName = "";

        if (context.Request.RequestContext.RouteData.Values.Count > 0)
        {
            switch (context.Request.RequestContext.RouteData.Values["categoryName"].ToString())
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
                FeedName = "General News";
                break;
            case Defines.NewsTypeEnum.Police:
                FeedName = "Police";
                break;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
        sb.Append("<rss version=\"2.0\">\r\n");
	    sb.Append(" <channel>\r\n");
        sb.AppendFormat(" <title>Upton PC {0} news</title>\r\n", FeedName);
        sb.AppendFormat(" <link>http://www.uptonpc.org/news/{0}/</link>\r\n",FeedName);
        sb.AppendFormat(" <description>The Latest {0} news from the Upton PC website</description>\r\n",FeedName);
        sb.Append(" <language>en-gb</language>\r\n");
        sb.AppendFormat(" <lastBuildDate>{0}</lastBuildDate>\r\n", RFC822Date(DateTime.Now));
        sb.AppendFormat(" <pubDate>{0}</pubDate>\r\n", RFC822Date(DateTime.Now));
        sb.Append(" <category>LocalGovernment</category>\r\n");
        sb.Append(" <webMaster>info@uptonpc.org</webMaster>\r\n");
        sb.Append(" <ttl>360</ttl>\r\n");

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
        {
            conn.Open();
            using (SqlCommand cmdNews = new SqlCommand("UptonPC_GetNews", conn))
            {
                cmdNews.CommandType = CommandType.StoredProcedure;

                cmdNews.Parameters.Add("@Count", SqlDbType.Int);
                cmdNews.Parameters["@Count"].Value = 15;

                cmdNews.Parameters.Add("@Catagory", SqlDbType.Int);
                cmdNews.Parameters["@Catagory"].Value = (int)newsType;

                SqlDataReader newsSqlDataReader = cmdNews.ExecuteReader();
                while (newsSqlDataReader.Read())
                {
                    sb.Append(" <item>\r\n");
                    sb.AppendFormat("     <title>{0}</title>\r\n", Utils.Strip(newsSqlDataReader.GetString(newsSqlDataReader.GetOrdinal("Subject")), true));
                    sb.AppendFormat("     <link>http://www.uptonpc.org/news/single.aspx?id={0}</link>\r\n", newsSqlDataReader.GetInt32(newsSqlDataReader.GetOrdinal("NoticeID")));
                    sb.AppendFormat("     <guid isPermaLink=\"true\">http://www.uptonpc.org/news/single.aspx?id={0}</guid>\r\n", newsSqlDataReader.GetInt32(newsSqlDataReader.GetOrdinal("NoticeID")));
                    sb.AppendFormat("     <description>{0}</description>\r\n", Utils.Strip(newsSqlDataReader.GetString(newsSqlDataReader.GetOrdinal("Body")), true));
                    sb.AppendFormat("     <pubDate>{0}</pubDate>", RFC822Date(newsSqlDataReader.GetDateTime(newsSqlDataReader.GetOrdinal("TStamp"))));
                    sb.Append(" </item>\r\n");
                }
            }
            
        }

        sb.Append("</channel>\r\n");
        sb.Append("</rss>\r\n");

        context.Response.Write(sb);
    }
 
    public bool IsReusable
    {
        // To enable pooling, return true here.
        // This keeps the handler in memory.
        get { return false; }
    }
}