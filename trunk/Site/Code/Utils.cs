using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml;
using System.Text;
using System.IO;
using Mrc.Common;
using System.Drawing.Imaging;

namespace UptonParishCouncil.Site
{
    [Serializable()]
    public class GalleryItem
    {
        private int _Id;
        private string _location;
        private string _Name;
        private string _Description;
        private int _GalleryId;
        private DateTime _DateAdded;
        private int _Views;
        private string _Copyright;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int GalleryId
        {
            get { return _GalleryId; }
            set { _GalleryId = value; }
        }

        public DateTime DateAdded
        {
            get { return _DateAdded; }
            set { _DateAdded = value; }
        }

        public int Views
        {
            get { return _Views; }
            set { _Views = value; }
        }

        public string Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; }
        }

        public static void IncrementVisitCount(int Id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("UPDATE uptonpc_GalleryItems SET iViews = iViews+1 WHERE iItemId=@ItemId", conn))
                {
                    comm.Parameters.Add("@ItemId", SqlDbType.Int);
                    comm.Parameters["@ItemId"].Value = Id;

                    comm.ExecuteNonQuery();
                }
            }
        }

        public void IncrementVisitCount()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("UPDATE uptonpc_GalleryItems SET iViews = iViews+1 WHERE iItemId=@ItemId", conn))
                {
                    comm.Parameters.Add("@ItemId", SqlDbType.Int);
                    comm.Parameters["@ItemId"].Value = Id;

                    comm.ExecuteNonQuery();
                }
            }
        }

        public GalleryItem(int Id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("SELECT * FROM uptonpc_GalleryItems WHERE iItemId=@ItemId", conn))
                {
                    comm.Parameters.Add("@ItemId", SqlDbType.Int);
                    comm.Parameters["@ItemId"].Value = Id; 
                    
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        this.Id = Id;
                        int CurrentOrdinal = reader.GetOrdinal("szLocation");
                        if (!reader.IsDBNull(CurrentOrdinal))
                        {
                            this.location = reader.GetString(CurrentOrdinal);
                        }
                        CurrentOrdinal = reader.GetOrdinal("szName");
                        if (!reader.IsDBNull(CurrentOrdinal))
                        {
                            this.Name = reader.GetString(CurrentOrdinal);
                        }
                        CurrentOrdinal = reader.GetOrdinal("szDescription");
                        if (!reader.IsDBNull(CurrentOrdinal))
                        {
                            this.Description = reader.GetString(CurrentOrdinal);
                        }
                        CurrentOrdinal = reader.GetOrdinal("dAddedOn");
                        if (!reader.IsDBNull(CurrentOrdinal))
                        {
                            this.DateAdded = reader.GetDateTime(CurrentOrdinal);
                        }
                        CurrentOrdinal = reader.GetOrdinal("iGalleryId");
                        if (!reader.IsDBNull(CurrentOrdinal))
                        {
                            this.GalleryId = reader.GetInt32(CurrentOrdinal);
                        }
                        CurrentOrdinal = reader.GetOrdinal("iViews");
                        if (!reader.IsDBNull(CurrentOrdinal))
                        {
                            this.Views = reader.GetInt32(CurrentOrdinal);
                        }
                        CurrentOrdinal = reader.GetOrdinal("szCopyright");
                        if (!reader.IsDBNull(CurrentOrdinal))
                        {
                            this.Copyright = reader.GetString(CurrentOrdinal);
                        }
                    }
                    reader.Close();
                }
            }
        }

    }

    public class Defines
    {
        public static string CensoredPostMessage = "This post has been Censored by the Moderators";
        public static string TeamCrestImageBase = "~/images/crests/";

        public enum NewsTypeEnum
        {
            None = 0,
            General = 1,
            Police = 2
        }
    }

    /// <summary>
    /// Summary description for Utils
    /// </summary>
    public class Utils
    {
        public static bool GetIsMobile()
        {
            if (HttpContext.Current.Session["IsMobile"] == null)
            {
                HttpContext.Current.Session["IsMobile"] = HttpContext.Current.Request.Browser.IsMobileDevice;
            }

            return (bool)HttpContext.Current.Session["IsMobile"];
        }

        public static string GetMasterPage()
        {
            bool isMobile = GetIsMobile();

            if (isMobile)
            {
                return "~/Mobile.Master";
            }
            else
            {
                return "~/Site.Master";
            }
        }

        public static string ResourceUrl(object objectResourceId, object objectTitle)
        {
            int resourceId = int.Parse(objectResourceId.ToString());
            string title = objectTitle.ToString();

            return string.Format("~/Resources/GetBlob.ashx?id={0}", resourceId);
        }

        public static void CreateOppositionSiteMap(string SavePath)
        {
            SiteMapBuilder OppositionSiteMap = new SiteMapBuilder();
            SiteMapBuilderNode OppositionNode = OppositionSiteMap.AddNode("~/Opposition/index.aspx", "Opposition", "");

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("SELECT TeamName, TeamId FROM uptonpc_Teams WHERE LeagueID = 2 ORDER BY TeamName", conn))
                {
                    SqlDataReader rs = comm.ExecuteReader();
                    while (rs.Read())
                    {
                        OppositionSiteMap.AddNode(OppositionNode, string.Format("~/Opposition/detail.aspx?TeamId={0}", rs.GetInt32(rs.GetOrdinal("TeamId"))), rs.GetString(rs.GetOrdinal("TeamName")).Replace("&", "&amp;"), "");
                    }
                }
            }
            OppositionSiteMap.WriteSiteMapToFile(SavePath);
        }

        public static int ValidateIntQueryStringParam(string IntParamName)
        {
            return ValidateIntQueryStringParam(IntParamName, true);
        }

        public static int ValidateIntQueryStringParam(string IntParamName, bool RaiseError)
        {
            string StringParamValue = HttpContext.Current.Request[IntParamName];
            int ParamValue = int.MinValue;
            if (!string.IsNullOrEmpty(StringParamValue))
            {
                if (!int.TryParse(StringParamValue, out ParamValue))
                {
                    if (RaiseError)
                    {
                        Utils.SendErrorMail(string.Format("[Custom] Invalid Query String Parameter ({0})", IntParamName), HttpContext.Current.Server.UrlDecode(StringParamValue));
                    }
                    ParamValue = int.MinValue;
                }
            }
            else
            {
                return 0;
            }
            return ParamValue;
        }

        public static void SendErrorMail(Exception ex)
        {
            SendErrorMail(ex.Message, ex.StackTrace);
        }

        public static void SendErrorMail(String Message, String Details)
        {
            HttpContext ctx = HttpContext.Current;

            try
            {
                //create the mail message
                using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                {
                    //set the addresses
                    mail.From = new System.Net.Mail.MailAddress("errorMgr_uptonpc@UptonPC.org");
                    mail.To.Add(ConfigurationManager.AppSettings.Get("ErrorMailToAddress"));
                    string newLine = "\r\n";
                    //set the content
                    mail.Subject = Message.Replace("\r", "").Replace("\n", "");

                    if (ctx.Request.ServerVariables.Get("HTTP_USER_AGENT").Contains("Googlebot"))
                    {
                        mail.Subject = string.Format("[GOOGLE_BOT]{0}", mail.Subject);
                    }
                    if (ctx.Request.ServerVariables.Get("HTTP_USER_AGENT").Contains("msnbot"))
                    {
                        mail.Subject = string.Format("[MSN_BOT]{0}", mail.Subject);
                    }
                    if (ctx.Request.ServerVariables.Get("HTTP_USER_AGENT").Contains("Slurp"))
                    {
                        mail.Subject = string.Format("[YAHOO_BOT]{0}", mail.Subject);
                    }
                    if (ctx.User.Identity.IsAuthenticated == true)
                    {
                        mail.Subject = string.Format("[{0}]{1}", ctx.User.Identity.Name, mail.Subject);
                    }
                    if (ctx == null)
                    {
                        mail.Body = Details;
                    }
                    else
                    {
                        mail.Body = "Current Path:" + ctx.Request.Path + newLine;

                        string AttachmentContents = "--------Server Variables---------" + newLine;
                        for (int i = 0; i < ctx.Request.ServerVariables.Count; i++)
                        {

                            //QUERY_STRING
                            if (ctx.Request.ServerVariables.AllKeys[i].StartsWith("QUERY_STRING"))
                            {
                                mail.Body += newLine + ctx.Request.ServerVariables.AllKeys[i] + ": " + ctx.Request.ServerVariables[i] + newLine;
                            }

                            //HTTP_USER_AGENT
                            if (ctx.Request.ServerVariables.AllKeys[i].StartsWith("HTTP_USER_AGENT"))
                            {
                                mail.Body += newLine + ctx.Request.ServerVariables.AllKeys[i] + ": " + ctx.Request.ServerVariables[i] + newLine;
                            }

                            if (!ctx.Request.ServerVariables.AllKeys[i].StartsWith("ALL"))
                            {
                                AttachmentContents += ctx.Request.ServerVariables.AllKeys[i] + ": " + ctx.Request.ServerVariables[i] + newLine;
                            }
                        }

                        //Rest of email
                        mail.Body += newLine + Details;

                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream(UTF32Encoding.Default.GetBytes(AttachmentContents)))
                        {

                            // Rewind the stream.
                            stream.Position = 0;
                            string AttachmentName = "ServerVars.txt";
                            if (ctx.Request.ServerVariables.GetValues("HTTP_FROM") != null)
                            {
                                string values = string.Join(";", ctx.Request.ServerVariables.GetValues("HTTP_FROM"));
                                AttachmentName = string.Format("[{0}] ServerVars.txt", values);
                            }

                            mail.Attachments.Add(new System.Net.Mail.Attachment(stream, AttachmentName));
                        }
                    }
                    //send the message
                    using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.UptonPC.org"))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("errorMgr@UptonPC.org", "errorMgr");
                        smtp.Send(mail);
                    }
                }
            }
            catch
            {
                //should we do something? 
            }
        }

        //public string DescLookup(string Tbl, int Id)
        //{
        //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(string.Format("SELECT ID,Desc FROM {0} WHERE ID=", Id.ToString()), conn))
        //        {
        //            SqlDataReader RS = cmd.ExecuteReader();
        //            if (RS.Read())
        //            {
        //                return RS["Desc"].ToString();
        //            }
        //        }
        //    }
        //    return "";
        //}

        public static string SeasonDesc(int SeasonId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("SELECT [Desc] FROM uptonpc_SeasonType WHERE ID = @ID", conn))
                {
                    comm.Parameters.Add("@ID", SqlDbType.Int);
                    comm.Parameters["@ID"].Value = SeasonId;
                    
                    return (string)comm.ExecuteScalar();
                }
            }
        }

        public static string TeamDesc(int TeamId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("SELECT [Desc] FROM uptonpc_TeamType WHERE ID = @ID", conn))
                {
                    comm.Parameters.Add("@ID", SqlDbType.Int);
                    comm.Parameters["@ID"].Value = TeamId;

                    return (string)comm.ExecuteScalar();
                }
            }
        }

        public static string GetTeamName(int TeamId, out string logo)
        {
            logo = string.Empty;

            using (SqlConnection conuptonpc = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conuptonpc.Open();
                using (SqlCommand cmdFixRes = new SqlCommand("SELECT TeamName, Logo FROM uptonpc_Teams WHERE TeamId = @TeamId", conuptonpc))
                {
                    cmdFixRes.Parameters.Add("@TeamId", SqlDbType.Int);
                    cmdFixRes.Parameters["@TeamId"].Value = TeamId;

                    SqlDataReader drFixRes = cmdFixRes.ExecuteReader();
                    if (drFixRes.Read())
                    {
                        int Ordinal = drFixRes.GetOrdinal("Logo");
                        if (!drFixRes.IsDBNull(Ordinal))
                        {
                            logo = drFixRes.GetString(Ordinal);
                        }
                        return drFixRes.GetString(drFixRes.GetOrdinal("TeamName"));
                    }
                    return string.Empty;
                }
            }
        }

        public int Missing_Result_Count(int Team)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Count(*) AS total FROM uptonpc_FixRes WHERE (Status=1) AND (Team=@Team) AND (TStamp <= GETDATE())", conn))
                {
                    cmd.Parameters.Add("@Team", SqlDbType.Int);
                    cmd.Parameters["@Team"].Value = Team;

                    SqlDataReader RS = cmd.ExecuteReader();
                    if (RS.Read())
                    {
                        return RS.GetInt32(RS.GetOrdinal("total"));
                    }
                }
            }
            return 0;
        }

        public static string Strip(object text)
        {
            return Strip(text, false);
        }

        public static string Strip(object text, bool xmlEntity)
        {
            string tempText = "";
            if (text.GetType() == typeof(string))
            {
                tempText = Regex.Replace((string)text, @"<(.|\n)*?>", string.Empty);
            }
            if (xmlEntity)
            {
                tempText = Regex.Replace(tempText, "&", "&amp;");
                tempText = Regex.Replace(tempText, "&amp;lt;", "&lt;");
                tempText = Regex.Replace(tempText, "&amp;gt;", "&gt;");
                tempText = Regex.Replace(tempText, "&amp;gt;", "&gt;");
                tempText = Regex.Replace(tempText, "&amp;quot;", "&quot;");
                tempText = Regex.Replace(tempText, "&amp;apos;", "&apos;");
                tempText = Regex.Replace(tempText, "&amp;nbsp;", "&#160;");
            }
            else
            {
                tempText = Regex.Replace(tempText, "&", "&amp;");
                tempText = Regex.Replace(tempText, "&amp;nbsp;", "&nbsp;");
            }
            if (tempText.Length > 660)
            {
                tempText = tempText.Substring(0, Math.Min(660, tempText.Length)) + "...";
            }
            return tempText;
        }

        public static string TruncateString(object input, int length)
        {
            string output = "";
            if (input.GetType() == typeof(string))
            {
                output = (string)input;
            }
            //trim to max length
            if (output.Length > length - 4)
            {
                output = output.Substring(0, length - 4);
                //trim to whitespace
                while (output[output.Length - 1] != ' ')
                {
                    output = output.Substring(0, output.Length - 1);
                }
                output += " ...";
            }
            return output;
        }

        public static string SponsorLink(object objSponsorName, object objSponsorUrl)
        {
            string SponsorName = "";
            string SponsorUrl = "";

            if (objSponsorName.GetType() == typeof(string))
            {
                SponsorName = (string)objSponsorName;
            }

            if (objSponsorUrl.GetType() == typeof(string))
            {
                SponsorUrl = (string)objSponsorUrl;
            }

            string output = string.Empty;

            if (SponsorName.Length > 0)
            {
                if (SponsorUrl.Length > 0)
                {
                    output += "<a href=\"" + SponsorUrl + "\">" + SponsorName + "</a>";
                }
                else
                {
                    output += SponsorName;
                }
            }
            return output;
        }

        public static string CalcMembershipValidation(string p)
        {
            string output = "121";

            foreach (char c in p)
            {
                output += ((int)c).ToString();
            }
            return output;
        }

        /// <summary>
        /// RemoveUnsafeCharcters from Company Name [Used for Sponsor image filename]
        /// </summary>
        /// <param name="objCompanyName"></param>
        /// <returns></returns>
        public static string FixCompanyName(object objCompanyName)
        {
            string companyName = string.Empty;

            if (objCompanyName != null)
            {
                if (objCompanyName.GetType() == typeof(string))
                {
                    companyName = (string)objCompanyName;
                }
            }
            return companyName.Replace(" ", "").Replace(".", "");
        }

        /// <summary>
        /// PrePend the HTTP protocol string if not present to URL [Used for Sponsor Url]
        /// </summary>
        /// <param name="objUrl"></param>
        /// <returns></returns>
        public static string FixUrl(object objUrl)
        {
            string url = string.Empty;

            if (objUrl != null)
            {
                if (objUrl.GetType() == typeof(string))
                {
                    url = (string)objUrl;
                }
            }

            if (!url.Contains("http://"))
            {
                url = "http://" + url;
            }

            return url;
        }

        /// <summary>
        /// Returns a control if one by that name exists in the hierarchy of the controls collection of the start control
        /// </summary>
        /// <param name="start"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Control FindControl(Control start, string id)
        {
            Control foundControl;

            if (start != null)
            {
                foundControl = start.FindControl(id);

                if (foundControl != null)
                    return foundControl;

                foreach (Control c in start.Controls)
                {
                    foundControl = FindControl(c, id);
                    if (foundControl != null)
                        return foundControl;
                }
            }
            return null;
        }

        public static void UpdateRole(string username, string role, bool newValue)
        {
            bool CurrentValue = Roles.IsUserInRole(username, role);
            if (CurrentValue != newValue)
            {
                if (newValue)
                {
                    Roles.AddUserToRole(username, role);
                }
                else
                {
                    Roles.RemoveUserFromRole(username, role);
                }
            }
        }

        public static string TestDbString(object dbStringValue, string validReturnValue, string invalidReturnValue)
        {
            if (dbStringValue.GetType() == typeof(string))
            {
                if (dbStringValue.ToString().Length > 0)
                {
                    return validReturnValue;
                }
            }
            return invalidReturnValue;
        }
    }
}