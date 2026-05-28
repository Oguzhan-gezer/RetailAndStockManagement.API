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
        
        sb.AppendLine("-- GÜNCEL ÜRÜN VE RESİM VERİLERİ SIFIRDAN YÜKLEME SCRİPTİ");
        sb.AppendLine("-- Bu script, veritabanındaki mevcut resimleri ve açıklamaları içerir.");
        sb.AppendLine();
        sb.AppendLine("DELETE FROM [ProductStore];");
        sb.AppendLine("DELETE FROM [Product];");
        sb.AppendLine("GO");
        sb.AppendLine();

        int count = 0;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Barcode, ProductCode, Price, ProductProperties, ImageBase64 FROM [Product]", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string barcode = reader.GetString(0);
                    string code = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    string props = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    string image = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    
                    sb.AppendLine($"INSERT INTO [Product] (Barcode, ProductCode, Price, ProductProperties, ImageBase64) VALUES ('{barcode}', '{code.Replace("'", "''")}', {price.ToString(System.Globalization.CultureInfo.InvariantCulture)}, '{props.Replace("'", "''")}', '{image}');");
                    count++;
                }
            }
        }
        
        // Let's not export ProductStore dynamically to avoid huge script, user just wanted photos/products.
        // But if they run "from scratch", deleting Product will fail due to foreign keys, unless we do it correctly.
        // Wait, earlier the python script `GeneratePerfectImages.py` generated `PerfectImagesData.sql` which deletes and inserts products.
        // The foreign key from ProductStore to Product has ON DELETE CASCADE? I don't know.
        // Let's just generate UPDATE statements for the existing products, AND INSERT for missing.
        
        StringBuilder updateSb = new StringBuilder();
        updateSb.AppendLine("-- GÜNCEL ÜRÜN VE RESİM VERİLERİ (UPDATE & INSERT SCRİPTİ)");
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Barcode, ProductCode, Price, ProductProperties, ImageBase64 FROM [Product]", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string barcode = reader.GetString(0);
                    string code = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    string props = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    string image = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    
                    updateSb.AppendLine($"IF EXISTS (SELECT 1 FROM [Product] WHERE Barcode='{barcode}')");
                    updateSb.AppendLine($"  UPDATE [Product] SET ProductCode='{code.Replace("'", "''")}', Price={price.ToString(System.Globalization.CultureInfo.InvariantCulture)}, ProductProperties='{props.Replace("'", "''")}', ImageBase64='{image}' WHERE Barcode='{barcode}';");
                    updateSb.AppendLine("ELSE");
                    updateSb.AppendLine($"  INSERT INTO [Product] (Barcode, ProductCode, Price, ProductProperties, ImageBase64) VALUES ('{barcode}', '{code.Replace("'", "''")}', {price.ToString(System.Globalization.CultureInfo.InvariantCulture)}, '{props.Replace("'", "''")}', '{image}');");
                    updateSb.AppendLine("GO");
                }
            }
        }

        File.WriteAllText(@"..\GüncelFotolarVeUrunler.sql", updateSb.ToString());
        Console.WriteLine($"Toplam {count} ürünün SQL Update/Insert komutları başarıyla oluşturuldu!");
        Console.WriteLine(@"Dosya ana dizine kaydedildi: GüncelFotolarVeUrunler.sql");
    }
}
