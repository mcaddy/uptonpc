using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Mrc.Common;
using System.IO;

namespace UptonParishCouncil.Site.Resources.admin
{
    public partial class Default : System.Web.UI.Page
    {
        private int ResourceTypeId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResourceTypeDropDownList.DataBind();
            }

            ResourceTypeId = int.Parse(ResourceTypeDropDownList.SelectedValue);
        }

        private void BuildMinutesSiteMap()
        {
            SiteMapBuilder minutesBuilder = new SiteMapBuilder();
            SiteMapBuilderNode minutesNode = minutesBuilder.AddNode("Resources/Minutes.aspx", "Minutes", "Parish Meeting Minutes");

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UptonPC_GetResourcesByType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ResourceTypeId", SqlDbType.Int);
                    command.Parameters["@ResourceTypeId"].Value = 2;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        minutesBuilder.AddNode(
                            minutesNode,
                            Utils.ResourceUrl(
                                reader.GetInt32(reader.GetOrdinal("ResourceId")),
                                reader.GetString(reader.GetOrdinal("Title"))),
                            reader.GetString(reader.GetOrdinal("Title")),"");
                    }
                    minutesBuilder.WriteSiteMapToFile(Server.MapPath("~/minutes.sitemap"));
                }
            }
        }

        private void BuildNewsletterSiteMap()
        {
            SiteMapBuilder minutesBuilder = new SiteMapBuilder();
            SiteMapBuilderNode minutesNode = minutesBuilder.AddNode("Resources/Newsletters.aspx", "Newsletter", "Parish Meeting Newsletter");

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UptonPC_GetResourcesByType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ResourceTypeId", SqlDbType.Int);
                    command.Parameters["@ResourceTypeId"].Value = 1;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        minutesBuilder.AddNode(
                            minutesNode,
                            Utils.ResourceUrl(
                                reader.GetInt32(reader.GetOrdinal("ResourceId")),
                                reader.GetString(reader.GetOrdinal("Title"))),
                            reader.GetString(reader.GetOrdinal("Title")), "");
                    }
                    minutesBuilder.WriteSiteMapToFile(Server.MapPath("~/newsletters.sitemap"));
                }
            }
        }

        protected void AddResourceLinkButton_Click(object sender, EventArgs e)
        {
            if (ResourceUpload.HasFile)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UptonPC_SetResource", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Title", SqlDbType.NVarChar, 50);
                        command.Parameters["@Title"].Value = TitleTextBox.Text;

                        command.Parameters.Add("@TypeId", SqlDbType.Int);
                        command.Parameters["@TypeId"].Value = ResourceTypeDropDownList.SelectedValue;

                        command.Parameters.Add("@Data", SqlDbType.Image);
                        command.Parameters["@Data"].Value = ResourceUpload.FileBytes;

                        command.Parameters.Add("@FileName", SqlDbType.NVarChar, 50);
                        command.Parameters["@FileName"].Value = ResourceUpload.FileName;

                        command.Parameters.Add("@MIMEType", SqlDbType.NVarChar, 50);
                        command.Parameters["@MIMEType"].Value = Mrc.Common.Utils.GetMimeType(Path.GetExtension(ResourceUpload.FileName));
                        command.ExecuteNonQuery();
                    }
                }
                ResourcesGridView.DataBind();
            }

            RebuildSiteMaps();
        }

        private void RebuildSiteMaps()
        {
            switch (ResourceTypeId)
            {
                case 1:
                    BuildNewsletterSiteMap();
                    break;
                case 2:
                    BuildMinutesSiteMap();
                    break;
            }
        }

        protected void ResourcesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            FileUpload gridViewFileUpload = ((System.Web.UI.WebControls.GridView)(sender)).Rows[0].Cells[0].Controls[3] as FileUpload;

            if (gridViewFileUpload != null && gridViewFileUpload.HasFile)
            {
                e.NewValues["Data"] = gridViewFileUpload.FileBytes;
                e.NewValues["FileName"] = gridViewFileUpload.FileName;
                e.NewValues["MIMEType"] = Mrc.Common.Utils.GetMimeType(Path.GetExtension(gridViewFileUpload.FileName));
            }
        }

        protected void ResourcesSqlDataSource_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["@Data"].DbType = DbType.Binary;
        }

        protected void ResourcesSqlDataSource_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            RebuildSiteMaps();
        }

        protected void ResourcesSqlDataSource_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            RebuildSiteMaps();
        }
    }
}