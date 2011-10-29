using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace UptonParishCouncil.Dal
{
    public class DbHelper
    {
        public static SqlConnection OpenSqlConnection()
        {
            SqlConnection newConn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString);
            newConn.Open();
            return newConn;
        }

        public static string GetDBSafeString(SqlDataReader rs, string ColumnName)
        {
            string OutputString = string.Empty;

            if (!rs.IsDBNull(rs.GetOrdinal(ColumnName)))
            {
                OutputString = rs.GetString(rs.GetOrdinal(ColumnName));
            }

            return OutputString;
        }

        public static Guid GetDBSafeGuid(SqlDataReader rs, string ColumnName)
        {
            Guid OutputGiud = Guid.Empty;

            if (!rs.IsDBNull(rs.GetOrdinal(ColumnName)))
            {
                OutputGiud = rs.GetGuid(rs.GetOrdinal(ColumnName));
            }

            return OutputGiud;
        }

        public static decimal GetDBSafeMoney(SqlDataReader rs, string ColumnName)
        {
            decimal OutputDecimal = decimal.MaxValue;

            if (!rs.IsDBNull(rs.GetOrdinal(ColumnName)))
            {
                OutputDecimal = rs.GetDecimal(rs.GetOrdinal(ColumnName));
            }

            return OutputDecimal;
        }

        public static DateTime GetDBSafeDateTime(SqlDataReader rs, string ColumnName)
        {
            DateTime OutputDateTime = DateTime.MinValue;

            if (!rs.IsDBNull(rs.GetOrdinal(ColumnName)))
            {
                OutputDateTime = rs.GetDateTime(rs.GetOrdinal(ColumnName));
            }

            return OutputDateTime;
        }

    }
}
