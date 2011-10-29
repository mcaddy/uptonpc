using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UptonParishCouncil.Site.Resources
{
    /// <summary>
    /// Summary description for GetBlob
    /// </summary>
    public class GetBlob : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int ResourceId = 0;
            RouteData routeData = context.Request.RequestContext.RouteData;
            if (routeData.Values.Count > 0)
            {
                ResourceId = int.Parse(routeData.Values["ResourceId"].ToString());
            }
            else
            {
                ResourceId = Utils.ValidateIntQueryStringParam("id");
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UptonPC_GetResource", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ResourceId", SqlDbType.Int);
                    command.Parameters["@ResourceId"].Value = ResourceId;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        context.Response.ContentType = reader.GetString(reader.GetOrdinal("MIMEType"));
                        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + reader.GetString(reader.GetOrdinal("FileName")));

                        context.Response.BinaryWrite((byte[])reader["Data"]);

                        context.Response.End();
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}