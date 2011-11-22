using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace UptonParishCouncil.Site
{
    public class CouncillorVCard : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Guid userId = Guid.Empty;
            if (!string.IsNullOrEmpty(context.Request["UserId"]))
            {
                try
                {
                    userId = Guid.Parse(context.Request["UserId"]);
                }
                catch (FormatException)
                {
                    //Do nothing, maybe should email
                }
            }
            string CouncillorVCardFile = "BEGIN:VCARD\r\n"+
"VERSION:2.1\r\n"+
"N:<Surname>;Cllr <GivenName>\r\n"+
"FN:Cllr <GivenName> <Surname>\r\n"+
"ORG:Upton Parish Council\r\n"+
"TITLE:Cllr\r\n"+
"NOTE;ENCODING=QUOTED-PRINTABLE:<Bio>\r\n"+
"TEL;WORK;VOICE:<Phone>\r\n"+
"URL;WORK:http://www.uptonpc.org/Councillors/Default.aspx?UserId=<UserId>\r\n"+
"EMAIL;PREF;INTERNET:<Email>\r\n"+
"ADR;WORK;PREF:;;79 Harlestone Road,;Northampton;Northamptonshire;NN5 7AB;United Kingdom\r\n" +
"REV:<Date>T<Time>Z\r\n" +
"END:VCARD";

            string filename = "unknown";

            if (!userId.Equals(Guid.Empty))
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand comm = new SqlCommand("UptonPC_GetCouncillorProfile", conn))
                    {
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add("@UserId", System.Data.SqlDbType.UniqueIdentifier);
                        comm.Parameters["@UserId"].Value = userId;

                        SqlDataReader rs = comm.ExecuteReader();

                        if (rs.Read())
                        {
                            CouncillorVCardFile = CouncillorVCardFile.Replace("<Surname>", rs.GetString(rs.GetOrdinal("Surname")));
                            CouncillorVCardFile = CouncillorVCardFile.Replace("<GivenName>", rs.GetString(rs.GetOrdinal("FirstName")));

                            filename = string.Format("Cllr{0}{1}", rs.GetString(rs.GetOrdinal("FirstName")), rs.GetString(rs.GetOrdinal("Surname")));

                            string email = rs.GetString(rs.GetOrdinal("ContactEmail"));
                            if (!string.IsNullOrEmpty(email))
                            {
                                CouncillorVCardFile = CouncillorVCardFile.Replace("<Email>", email);
                            }

                            string phone = rs.GetString(rs.GetOrdinal("ContactPhone"));
                            if (!string.IsNullOrEmpty(phone))
                            {
                                CouncillorVCardFile = CouncillorVCardFile.Replace("<Phone>", phone);
                            }

                            //Need to add the BIO, this stops it for now
                            CouncillorVCardFile = CouncillorVCardFile.Replace("<Bio>", "");
                            string bio = rs.GetString(rs.GetOrdinal("Bio"));
                            if (!string.IsNullOrEmpty(bio))
                            {
                                CouncillorVCardFile = CouncillorVCardFile.Replace("<Bio>", bio);
                            }

                            //Need to add Photo
                            //if (!rs.IsDBNull(rs.GetOrdinal("ResourceId")))
                            //{
                            //    card.Photos.Add(new vCardPhoto(new Uri(string.Format("http://{1}/Resources/GetBlob.ashx?id={0}", rs.GetInt32(rs.GetOrdinal("ResourceId")), context.Request.Headers["HOST"]))));
                            //}

                            CouncillorVCardFile = CouncillorVCardFile.Replace("<UserId>", userId.ToString());

                            CouncillorVCardFile = CouncillorVCardFile.Replace("<Date>", DateTime.Now.ToString("yyyyMMdd"));
                            CouncillorVCardFile = CouncillorVCardFile.Replace("<Time>", DateTime.Now.ToString("hhmmss"));
                        }
                    }
                }
            }

            context.Response.ContentType = "text/x-vcard";

            context.Response.AppendHeader("content-disposition", string.Format("attachment;filename={0}.vcf", filename));

            context.Response.Write(CouncillorVCardFile);
            context.Response.End();
        }
    }
}