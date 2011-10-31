using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace UptonParishCouncil.Site.Code
{
    public class ResourceUtilities
    {
        public static int AddResource(string title, int ResourceType, byte[] resourceData, string fileName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UptonPC_SetResource", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Title", SqlDbType.NVarChar, 50);
                    command.Parameters["@Title"].Value = title;

                    command.Parameters.Add("@TypeId", SqlDbType.Int);
                    command.Parameters["@TypeId"].Value = ResourceType;

                    command.Parameters.Add("@Data", SqlDbType.Image);
                    command.Parameters["@Data"].Value = resourceData;

                    command.Parameters.Add("@FileName", SqlDbType.NVarChar, 50);
                    command.Parameters["@FileName"].Value = fileName;

                    command.Parameters.Add("@MIMEType", SqlDbType.NVarChar, 50);
                    command.Parameters["@MIMEType"].Value = Mrc.Common.Utils.GetMimeType(Path.GetExtension(fileName));
                    
                    object returnValue = command.ExecuteScalar();
                    return Convert.ToInt32(returnValue);
                }
            }
        }
    }
}