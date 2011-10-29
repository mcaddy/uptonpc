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

namespace UptonParishCouncil.Site.News.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            BindNewsTypes();
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindNewsTypes()
        {
            foreach (Defines.NewsTypeEnum newsType in Enum.GetValues(typeof(Defines.NewsTypeEnum)))
            {
                if (newsType != Defines.NewsTypeEnum.None)
                {
                    newsTypeDropDownList.Items.Add(new ListItem(newsType.ToString(), ((int)newsType).ToString()));
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && (Request["AddNew"] != null))
            {
                fvNewsItem.ChangeMode(FormViewMode.Insert);
                fvNewsItem_ModeChanged(sender, e);
                newsGridView.SelectedIndex = -1;
            }
            BindNewsGridView();
        }

        private void BindNewsGridView()
        {
            using (SqlConnection newsSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                newsSqlConnection.Open();
                using (SqlCommand newsSqlCommand = new SqlCommand("UptonPc_GetNews", newsSqlConnection))
                {
                    newsSqlCommand.CommandType = CommandType.StoredProcedure;
                    newsSqlCommand.Parameters.Add("Count", SqlDbType.Int);
                    newsSqlCommand.Parameters["Count"].Value = 0;
                    newsSqlCommand.Parameters.Add("Catagory", SqlDbType.Int);
                    newsSqlCommand.Parameters["Catagory"].Value = newsTypeDropDownList.SelectedValue;

                    SqlDataReader NewsSqlDataReader = newsSqlCommand.ExecuteReader();
                    newsGridView.DataSource = NewsSqlDataReader;
                    newsGridView.DataBind();
                }
            }
        }

        protected void fvNewsItem_ModeChanged(object sender, EventArgs e)
        {
            switch (fvNewsItem.CurrentMode)
            {
                case FormViewMode.Edit:
                    fvNewsItem.HeaderText = "Edit Existing";
                    break;
                case FormViewMode.Insert:
                    fvNewsItem.HeaderText = "Add New";
                    break;
                case FormViewMode.ReadOnly:
                    fvNewsItem.HeaderText = "View Existing";
                    break;
                default:
                    break;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            fvNewsItem.ChangeMode(FormViewMode.Insert);
            fvNewsItem_ModeChanged(sender, e);
            newsGridView.SelectedIndex = -1;
        }

        protected void sdsNewsItem_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            if (e.Command.Parameters["@TStamp"].Value == null)
            {
                e.Command.Parameters["@TStamp"].Value = DateTime.Now;
            }
        }

        protected void sdsNewsItem_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            BindNewsGridView();
            fvNewsItem.ChangeMode(FormViewMode.ReadOnly);
            fvNewsItem_ModeChanged(sender, e);
        }

        protected void sdsNewsItem_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            BindNewsGridView();
        }

        protected void sdsNewsItem_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            BindNewsGridView();
        }

        protected void NewsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fvNewsItem.CurrentMode == FormViewMode.Insert)
            {
                fvNewsItem.ChangeMode(FormViewMode.ReadOnly);
                fvNewsItem_ModeChanged(sender, e);
            }
        }
    }
}