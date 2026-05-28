using System;
using System.IO;
using System.Text;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = @"Server=(localdb)\MSSQLLocalDB;Database=RetailAndStockManagement;Trusted_Connection=True;TrustServerCertificate=true;";
        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine("-- GÜNCEL ÜRÜN, STOK VE KULLANICI VERİLERİ (UPDATE & INSERT SCRİPTİ)");
        sb.AppendLine("SET ANSI_NULLS ON");
        sb.AppendLine("GO");
        sb.AppendLine("SET QUOTED_IDENTIFIER ON");
        sb.AppendLine("GO");

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // 1. Export Users
            sb.AppendLine("-- USERS EXPORT");
            using (SqlCommand cmd = new SqlCommand("SELECT Username, PasswordHash, FullName, Role, IsActive, CreatedAt, StoreId FROM [User]", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string username = reader.GetString(0);
                    string hash = reader.GetString(1);
                    string fullName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    string role = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    bool isActive = reader.GetBoolean(4);
                    DateTime createdAt = reader.GetDateTime(5);
                    string storeId = reader.IsDBNull(6) ? "NULL" : reader.GetInt32(6).ToString();

                    sb.AppendLine($"IF EXISTS (SELECT 1 FROM [User] WHERE Username='{username}')");
                    sb.AppendLine($"  UPDATE [User] SET PasswordHash='{hash}', FullName='{fullName.Replace("'", "''")}', Role='{role}', IsActive={(isActive ? 1 : 0)}, StoreId={storeId} WHERE Username='{username}';");
                    sb.AppendLine("ELSE");
                    sb.AppendLine($"  INSERT INTO [User] (Username, PasswordHash, FullName, Role, IsActive, CreatedAt, StoreId) VALUES ('{username}', '{hash}', '{fullName.Replace("'", "''")}', '{role}', {(isActive ? 1 : 0)}, '{createdAt:yyyy-MM-dd HH:mm:ss}', {storeId});");
                    sb.AppendLine("GO");
                }
            }

            // 2. Export Products
            int productCount = 0;
            sb.AppendLine("-- PRODUCTS EXPORT");
            using (SqlCommand cmd = new SqlCommand("SELECT Barcode, ProductCode, Price, ProductProperties, ImageBase64 FROM [Product]", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string barcode = reader.GetString(0);
                    string code = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    string props = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    string image = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    
                    sb.AppendLine($"IF EXISTS (SELECT 1 FROM [Product] WHERE Barcode='{barcode}')");
                    sb.AppendLine($"  UPDATE [Product] SET ProductCode='{code.Replace("'", "''")}', Price={price.ToString(System.Globalization.CultureInfo.InvariantCulture)}, ProductProperties='{props.Replace("'", "''")}', ImageBase64='{image}' WHERE Barcode='{barcode}';");
                    sb.AppendLine("ELSE");
                    sb.AppendLine($"  INSERT INTO [Product] (Barcode, ProductCode, Price, ProductProperties, ImageBase64) VALUES ('{barcode}', '{code.Replace("'", "''")}', {price.ToString(System.Globalization.CultureInfo.InvariantCulture)}, '{props.Replace("'", "''")}', '{image}');");
                    sb.AppendLine("GO");
                    productCount++;
                }
            }

            // 3. Export ProductStore
            sb.AppendLine("-- PRODUCT STORE (INVENTORY) EXPORT");
            using (SqlCommand cmd = new SqlCommand("SELECT Barcode, StoreId, SizeXS, SizeS, SizeM, SizeL, SizeXL, SizeXXL, SizeXXXL, OptionCount FROM [ProductStore]", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string barcode = reader.GetString(0);
                    int storeId = reader.GetInt32(1);
                    int xs = reader.GetInt32(2);
                    int s = reader.GetInt32(3);
                    int m = reader.GetInt32(4);
                    int l = reader.GetInt32(5);
                    int xl = reader.GetInt32(6);
                    int xxl = reader.GetInt32(7);
                    int xxxl = reader.GetInt32(8);
                    int count = reader.GetInt32(9);

                    sb.AppendLine($"IF EXISTS (SELECT 1 FROM [ProductStore] WHERE Barcode='{barcode}' AND StoreId={storeId})");
                    sb.AppendLine($"  UPDATE [ProductStore] SET SizeXS={xs}, SizeS={s}, SizeM={m}, SizeL={l}, SizeXL={xl}, SizeXXL={xxl}, SizeXXXL={xxxl}, OptionCount={count} WHERE Barcode='{barcode}' AND StoreId={storeId};");
                    sb.AppendLine("ELSE");
                    sb.AppendLine($"  INSERT INTO [ProductStore] (Barcode, StoreId, SizeXS, SizeS, SizeM, SizeL, SizeXL, SizeXXL, SizeXXXL, OptionCount) VALUES ('{barcode}', {storeId}, {xs}, {s}, {m}, {l}, {xl}, {xxl}, {xxxl}, {count});");
                    sb.AppendLine("GO");
                }
            }
        }

        File.WriteAllText(@"..\GüncelFotolarVeUrunler.sql", sb.ToString());
        Console.WriteLine($"SQL Update/Insert komutları başarıyla oluşturuldu! (Kullanıcılar, Ürünler ve Stoklar eklendi)");
    }
}
