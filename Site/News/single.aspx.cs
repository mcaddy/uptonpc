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

namespace UptonParishCouncil.Site.News
{
    public partial class Single : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int NoticeId = 0;

            if (RouteData.Values.Count > 0)
            {
                NoticeId = int.Parse(RouteData.Values["NoticeId"].ToString());
            }
            else
            {
                NoticeId = Utils.ValidateIntQueryStringParam("id");
            }

            if (NoticeId > 0)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("UptonPC_GetNewsItem", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@NoticeId", SqlDbType.Int);
                        command.Parameters["@NoticeId"].Value = NoticeId;
                        NewsComment1.NoticeId = NoticeId;

                        SqlDataReader RS = command.ExecuteReader();
                        if (RS.Read())
                        {
                            string Title = RS.GetString(RS.GetOrdinal("Subject"));
                            lblTitle.Text = Title;
                            NewsComment1.NoticeTitle = Title;
                            Page.Title = Title;

                            lblBody.Text = RS.GetString(RS.GetOrdinal("Body"));
                            lblDate.Text = RS.GetDateTime(RS.GetOrdinal("TStamp")).ToString("dd/MM/yyyy");
                            NewsComment1.NoticeTypeId = RS.GetInt32(RS.GetOrdinal("TypeId"));
                        }
                        RS.Close();
                    }
                }
            }
        }
    }
}