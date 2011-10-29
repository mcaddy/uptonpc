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
using System.IO;

namespace UptonParishCouncil.Site.News.Admin
{
    public partial class UploadImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUploadFile_ServerClick(object sender, EventArgs e)
        {
            string ErrorText = "";
            //check if the fileposted is not null

            if (fuNewFile.PostedFile != null && fuNewFile.PostedFile.FileName != "")
            {
                if (fuNewFile.PostedFile.ContentLength < 1024000)
                {

                    //some string manipulations to extract the filename from the full file path
                    string fileName = Path.GetFileName(fuNewFile.PostedFile.FileName);

                    string saveDirPath = Server.MapPath(string.Format("~/images/news/{0}/", DateTime.Now.ToString("yyyyMMdd")));
                    Directory.CreateDirectory(saveDirPath);

                    fuNewFile.PostedFile.SaveAs(string.Format(@"{0}\{1}", saveDirPath, fileName));

                    //Get the various properties of the Uploaded file
                    lblHelperLink.Text = string.Format("&lt;img src=\"/images/news/{0}/{1}\"/&gt;", DateTime.Now.ToString("yyyyMMdd"), fileName);
                    lblFileSize.Text = (fuNewFile.PostedFile.ContentLength / 1024).ToString();
                    hlImageLink.Text = fileName;
                    hlImageLink.NavigateUrl = string.Format("~/images/news/{0}/{1}", DateTime.Now.ToString("yyyyMMdd"), fileName);
                }
                else
                {//File Too big
                    ErrorText = "The file you specified is too large";
                }
            }
            else
            {//File not found
                ErrorText = "You did not specify a File";
            }
            if (ErrorText.Length > 0)
            {
                plPreviousFileDetails.Visible = false;
                plError.Visible = true;
                lblErrorText.Text = ErrorText;
            }
            else
            {
                plPreviousFileDetails.Visible = true;
                plError.Visible = false;
            }
        }
    }
}