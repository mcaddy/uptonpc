using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mrc.Common;
using UptonParishCouncil.Site.Code;

namespace UptonParishCouncil.Site.Councillors.admin
{
    public partial class UpdateProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            DataView dv = (DataView)profileSqlDataSource.Select(DataSourceSelectArguments.Empty);
            if (dv != null && dv.Table.Rows.Count > 0)
            {
                FirstNameLabel.Text = dv.Table.Rows[0]["FirstName"] as string;
                SurnameLabel.Text = dv.Table.Rows[0]["Surname"] as string;
                EmailLabel.Text = dv.Table.Rows[0]["ContactEmail"] as string;

                PhoneTextBox.Text = dv.Table.Rows[0]["ContactPhone"] as string;
                BioTextBox.Text = dv.Table.Rows[0]["Bio"] as string;
                ResponsibilitesTextBox.Text = dv.Table.Rows[0]["Responsibilites"] as string;

                object resourceId = dv.Table.Rows[0]["ResourceId"];
                if (resourceId != System.DBNull.Value && (int)resourceId > 0)
                {
                    profileImage.Visible = true;
                    profileImage.ImageUrl = string.Format("~/Resources/GetBlob.ashx?id={0}", resourceId);
                }
                else
                {
                    profileImage.Visible = false;
                }
            }
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            profileSqlDataSource.Update();

            SetProfilePhoto();
        }

        private void SetProfilePhoto()
        {
            if (photoFileUpload.HasFile)
            {
                MembershipUser CurrentUser = Membership.GetUser(User.Identity.Name);

                if (ImageUtils.FileIsImage(photoFileUpload.PostedFile.ContentType))
                {
                    byte[] photoBytes = ImageUtils.imageToByteArray(ImageUtils.ResizeJpeg(ImageUtils.byteArrayToImage(photoFileUpload.FileBytes), 160, 160));

                    // Add the Resource
                    int ResourceId = ResourceUtilities.AddResource(CurrentUser.ProviderUserKey.ToString(), 3, photoBytes, photoFileUpload.FileName);

                    // Store the ID of the new Resource
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("UptonPC_SetCouncillorProfileImageResourceId", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
                            command.Parameters.Add("@ResourceId", SqlDbType.Int);

                            command.Parameters["@UserId"].Value = CurrentUser.ProviderUserKey;
                            command.Parameters["@ResourceId"].Value = ResourceId;

                            command.ExecuteNonQuery();
                        }
                    }

                    profileImage.ImageUrl = string.Format("~/Resources/GetBlob.ashx?id={0}", ResourceId);
                }
            }
        }

        protected void profileSqlDataSource_SetUser(object sender, SqlDataSourceCommandEventArgs e)
        {
            MembershipUser CurrentUser = Membership.GetUser(User.Identity.Name); 
            e.Command.Parameters["@UserId"].Value = CurrentUser.ProviderUserKey;
        }
    }
}