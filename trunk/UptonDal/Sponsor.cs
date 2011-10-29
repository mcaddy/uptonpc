using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UptonParishCouncil.Dal
{
    public class BusinessDirectoryCategory
    {
        public BusinessDirectoryCategory(int id, string name)
        {
            ID = id;
            Name = name;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }

        private List<Sponsor> sponsors = new List<Sponsor>();

        public List<Sponsor> GetSponsors()
        {
            return sponsors;
        }

        public void AddSponsorToCategory(Sponsor theSponsor)
        {
            sponsors.Add(theSponsor);
        }
    }

    public class Sponsor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Tel1 { get; set; }
        public string Fax { get; set; }
        public string URL { get; set; }
        public bool Featured { get; set; }

        public Sponsor(int id, string name, string address1, string address2, string town, string county, string postCode, string tel1, string fax, string url, bool featured)
        {
            ID = id;
            Name = name;
            Address1 = address1;
            Address2 = address2;
            Town = town;
            County = county;
            PostCode = postCode;
            Tel1 = tel1;
            Fax = fax;
            URL = url;
            Featured = featured;
        }
    }

    public class BusinessDirectory
    {
        private List<BusinessDirectoryCategory> catagories = new List<BusinessDirectoryCategory>();

        public List<BusinessDirectoryCategory> GetCatagories()
        {
            return catagories;
        }
        public void AddCategory(int Id, string Name)
        {
            catagories.Add(new BusinessDirectoryCategory(Id, Name));
        }

        public List<BusinessDirectoryCategory> Find(string SearchPrefix)
        {
            return catagories.FindAll(delegate(BusinessDirectoryCategory t) { return t.Name.ToUpper().StartsWith(SearchPrefix.ToUpper()); });
        }

        public List<BusinessDirectoryCategory> Find(int id)
        {
            return catagories.FindAll(delegate(BusinessDirectoryCategory t) { return t.ID.Equals(id); });
        }

        public void LoadFromDB()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UptonPC"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("[uptonpc_GetCategoriesWithSponsors]", conn))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rs = comm.ExecuteReader();
                    int SponsorCategoryIdOrdinal = rs.GetOrdinal("SponsorCategoryId");
                    int CategoryNameOrdinal = rs.GetOrdinal("CategoryName");

                    while (rs.Read())
                    {
                        AddCategory(rs.GetInt32(SponsorCategoryIdOrdinal),rs.GetString(CategoryNameOrdinal));
                    }
                    rs.Close();

                    comm.CommandText = "[uptonpc_GetSponsorsInCategories]";

                    rs = comm.ExecuteReader();
                    int IdOrdinal = rs.GetOrdinal("ID");
                    int NameOrdinal = rs.GetOrdinal("CompanyName");
                    int Address1Ordinal = rs.GetOrdinal("Address1");
                    int Address2Ordinal = rs.GetOrdinal("Address2");
                    int TownOrdinal = rs.GetOrdinal("Town");
                    int CountyOrdinal = rs.GetOrdinal("County");
                    int PostCodeOrdinal = rs.GetOrdinal("PostCode");
                    int Tel1Ordinal = rs.GetOrdinal("Tel1");
                    int FaxOrdinal = rs.GetOrdinal("Fax");
                    int URLOrdinal = rs.GetOrdinal("URL");
                    int CategoryIdOrdinal = rs.GetOrdinal("CategoryId");
                    int FeaturedOrdinal = rs.GetOrdinal("Featured");

                    while (rs.Read())
                    {
                        int CatagoryId = rs.GetInt32(CategoryIdOrdinal);
                        BusinessDirectoryCategory foundCategory = catagories.Find(delegate(BusinessDirectoryCategory t) { return t.ID == CatagoryId; });
                        if (foundCategory != null)
                        {
                            foundCategory.AddSponsorToCategory(
                                new Sponsor(
                                    rs.GetInt32(IdOrdinal),
                                    (rs.IsDBNull(NameOrdinal) ? "" : rs.GetString(NameOrdinal)),
                                    (rs.IsDBNull(Address1Ordinal) ? "" : rs.GetString(Address1Ordinal)),
                                    (rs.IsDBNull(Address2Ordinal) ? "" : rs.GetString(Address2Ordinal)),
                                    (rs.IsDBNull(TownOrdinal) ? "" : rs.GetString(TownOrdinal)),
                                    (rs.IsDBNull(CountyOrdinal) ? "" : rs.GetString(CountyOrdinal)),
                                    (rs.IsDBNull(PostCodeOrdinal) ? "" : rs.GetString(PostCodeOrdinal)),
                                    (rs.IsDBNull(Tel1Ordinal) ? "" : rs.GetString(Tel1Ordinal)),
                                    (rs.IsDBNull(FaxOrdinal) ? "" : rs.GetString(FaxOrdinal)),
                                    (rs.IsDBNull(URLOrdinal) ? "" : rs.GetString(URLOrdinal)),
                                    (rs.IsDBNull(FeaturedOrdinal) ? false : rs.GetBoolean(FeaturedOrdinal))
                                )
                            );
                        }
                    }
                }
            }
        }
    }
}
