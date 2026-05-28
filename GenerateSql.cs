using System;
using System.IO;

class Program
{
    static void Main()
    {
        string adminHash = BCrypt.Net.BCrypt.HashPassword(""admin123"");
        string managerHash = BCrypt.Net.BCrypt.HashPassword(""123456"");

        string sql = @""
-- COUNTRIES
IF NOT EXISTS (SELECT 1 FROM [Countries])
BEGIN
    INSERT INTO [Countries] (CountryName) VALUES ('Türkiye');
END

-- REGIONS
IF NOT EXISTS (SELECT 1 FROM [Regions])
BEGIN
    INSERT INTO [Regions] (RegionName, CountryId) VALUES ('Adana', 1), ('İstanbul', 1);
END

-- STORES
IF NOT EXISTS (SELECT 1 FROM [Store])
BEGIN
    INSERT INTO [Store] (StoreLocation, StoreLevel, RegionId) VALUES ('TR-Adana-1', 'A', 1), ('TR-Adana-2', 'B', 1), ('TR-İstanbul-1', 'A', 2);
END

-- USERS
IF NOT EXISTS (SELECT 1 FROM [User])
BEGIN
    INSERT INTO [User] (Username, PasswordHash, FullName, Role, IsActive, CreatedAt, StoreId)
    VALUES 
    ('admin', '"" + adminHash + @""', 'Sistem Yöneticisi', 'Admin', 1, GETDATE(), NULL),
    ('manager1', '"" + managerHash + @""', 'Ahmet Yılmaz', 'StoreManager', 1, GETDATE(), 1),
    ('manager2', '"" + managerHash + @""', 'Ayşe Demir', 'StoreManager', 1, GETDATE(), 2),
    ('manager3', '"" + managerHash + @""', 'Mehmet Kaya', 'StoreManager', 1, GETDATE(), 3);
END

-- PRODUCTS
IF NOT EXISTS (SELECT 1 FROM [Product])
BEGIN
    INSERT INTO [Product] (Barcode, ProductCode, Price, ProductProperties)
    VALUES 
    ('8691234567890', 'TSHIRT-BLK-M', 199.99, '{""Renk"":""Siyah"",""Beden"":""M""}'),
    ('8691234567891', 'PANT-BLU-32', 349.50, '{""Renk"":""Mavi"",""Beden"":""32""}'),
    ('8691234567892', 'SHOE-WHT-42', 899.00, '{""Renk"":""Beyaz"",""Numara"":""42""}');
END

-- PRODUCT STORES
IF NOT EXISTS (SELECT 1 FROM [ProductStore])
BEGIN
    INSERT INTO [ProductStore] (Barcode, StoreId, SizeXS, SizeS, SizeM, SizeL, SizeXL, SizeXXL, SizeXXXL, OptionCount)
    VALUES 
    ('8691234567890', 1, 5, 10, 15, 10, 5, 3, 2, 50),
    ('8691234567891', 1, 1, 2, 3, 2, 1, 1, 0, 10),
    ('8691234567892', 2, 2, 4, 6, 4, 2, 1, 1, 20),
    ('8691234567890', 3, 0, 1, 2, 1, 1, 0, 0, 5);
END
"";
        File.WriteAllText(""seed_data.sql"", sql);
    }
}