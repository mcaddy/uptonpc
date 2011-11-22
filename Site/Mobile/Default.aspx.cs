using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UptonParishCouncil.Dal;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UptonParishCouncil.Site.Mobile
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetLink(1, newsletterHyperLink);
            SetLink(2, minutesHyperLink);
        }

        private void SetLink(int typeId, HyperLink theLink){
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("UptonPC_GetResourcesByType", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ResourceTypeId", SqlDbType.Int);
                    command.Parameters["@ResourceTypeId"].Value = typeId;

                    SqlDataReader rs = command.ExecuteReader();
                    if (rs.Read())
                    {
                        theLink.NavigateUrl = string.Format("~/Resources/GetBlob.ashx?id={0}", rs.GetInt32(rs.GetOrdinal("ResourceId")));
                        theLink.Text = rs.GetString(rs.GetOrdinal("Title"));
                    }
                    else
                    {
                        theLink.NavigateUrl = string.Empty;
                        theLink.Text = "None";
                    }
                }
            }
        }
    }
}
