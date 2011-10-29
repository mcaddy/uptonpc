using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Data.SqlClient;

namespace UptonParishCouncil.Dal
{
    public enum ShopCategories
    {
        General,
        Accessory,
        None
    }

    public class ShopInventory
    {
        public List<ShopItem> GeneralItems = new List<ShopItem>();
        public List<ShopItem> AccessoryItems = new List<ShopItem>();

        public ShopInventory()
        {
            LoadFromDB();
        }

        public void LoadFromDB()
        {
            using (SqlConnection conn = DbHelper.OpenSqlConnection())
            {
                using (SqlCommand command = new SqlCommand("SELECT ItemId,Name,Description,Price, Category FROM uptonpc_ShopItems ORDER BY Price DESC", conn))
                {
                    SqlDataReader rs = command.ExecuteReader();
                    while (rs.Read())
                    {
                        if (rs.GetInt32(rs.GetOrdinal("Category")) == 1)
                        {
                            GeneralItems.Add(new ShopItem(DbHelper.GetDBSafeGuid(rs, "ItemId"), DbHelper.GetDBSafeString(rs, "Name"), DbHelper.GetDBSafeString(rs, "Description"), DbHelper.GetDBSafeMoney(rs, "Price")));
                        }
                        if (rs.GetInt32(rs.GetOrdinal("Category")) == 2)
                        {
                            AccessoryItems.Add(new ShopItem(DbHelper.GetDBSafeGuid(rs, "ItemId"), DbHelper.GetDBSafeString(rs, "Name"), DbHelper.GetDBSafeString(rs, "Description"), DbHelper.GetDBSafeMoney(rs, "Price")));
                        }
                    }
                    rs.Close();
                }
                AddProperties(conn, GeneralItems);
                AddProperties(conn, AccessoryItems);
            }
        }

        private void AddProperties(SqlConnection conn, List<ShopItem> theList)
        {
            foreach (ShopItem item in theList)
            {
                using (SqlCommand command = new SqlCommand("SELECT PropId, Name, Value FROM uptonpc_ShopItemProperties WHERE ItemId = @ItemId ORDER BY Name", conn))
                {
                    command.Parameters.Add("@ItemId", System.Data.SqlDbType.Int);
                    command.Parameters["@ItemId"].Value = item.ID;
                    SqlDataReader rs = command.ExecuteReader();
                    while (rs.Read())
                    {
                        item.AddProperty(DbHelper.GetDBSafeGuid(rs, "PropId"), DbHelper.GetDBSafeString(rs, "Name"), DbHelper.GetDBSafeString(rs, "Value"));
                    }
                    rs.Close();
                }
            }
        }

        public void SaveToDB()
        {
            //TODO:
        }
    }

    public class ShopItem
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public List<Property> Properties { get; set; }
    
        public ShopItem()
        {
            Properties = new List<Property>();
        }

        public ShopItem(Guid newId, string newName, string newDescription, decimal newPrice)
        {
            ID = newId;
            Name = newName;
            Description = newDescription;
            Price = newPrice;

            Properties = new List<Property>();
        }

        public ShopItem(string newName, string newDescription, decimal newPrice)
        {
            ID = Guid.NewGuid();
            Name = newName;
            Description = newDescription;
            Price = newPrice;

            Properties = new List<Property>();
        }

        public void AddProperty(string Name, string Value)
        {
            //if ()
            //{
            //    //TODO : Remove if exists
            //}
            Properties.Add(new Property(Name,Value));
        }

        public void AddProperty(Guid Id, string Name, string Value)
        {
            //if ()
            //{
            //    //TODO : Remove if exists
            //}
            Properties.Add(new Property(Id, Name, Value));
        }

        public void DeleteProperty(string Name)
        {
            List<Property> toDelete = new List<Property>();

            foreach (Property prop in Properties)
            {
                if (prop.Name == Name)
                {
                    toDelete.Add(prop);
                }
            }

            foreach (Property PropToDelete in toDelete)
            {
                Properties.Remove(PropToDelete);
            }
        }
    }

    public class Property
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Property() { }

        public Property(Guid newId, string newName, string newValue)
        {
            ID = newId;
            Name = newName;
            Value = newValue;
        }

        public Property(string newName, string newValue)
        {
            ID = Guid.NewGuid();
            Name = newName;
            Value = newValue;
        }
    }

}
