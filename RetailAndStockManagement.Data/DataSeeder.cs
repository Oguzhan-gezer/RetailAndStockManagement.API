using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(RetailAndStockManagementContext ctx)
    {
        if (!await ctx.Countries.AnyAsync())
        {
            // COUNTRIES
            var countries = new List<CountryModel>
            {
                new() { CountryId = 1, CountryName = "Türkiye" },
                new() { CountryId = 2, CountryName = "Almanya" },
                new() { CountryId = 3, CountryName = "Fransa" },
                new() { CountryId = 4, CountryName = "İtalya" },
                new() { CountryId = 5, CountryName = "İspanya" },
                new() { CountryId = 6, CountryName = "Hollanda" },
                new() { CountryId = 7, CountryName = "Polonya" },
                new() { CountryId = 8, CountryName = "Romanya" },
            };
            ctx.Countries.AddRange(countries);
            await ctx.SaveChangesAsync();


            // REGIONS
            var trCityNames = new[]
            {
                "Adana","Adıyaman","Afyonkarahisar","Ağrı","Amasya","Ankara","Antalya","Artvin",
                "Aydın","Balıkesir","Bilecik","Bingöl","Bitlis","Bolu","Burdur","Bursa",
                "Çanakkale","Çankırı","Çorum","Denizli","Diyarbakır","Edirne","Elazığ","Erzincan",
                "Erzurum","Eskişehir","Gaziantep","Giresun","Gümüşhane","Hakkari","Hatay","Isparta",
                "Mersin","İstanbul","İzmir","Kars","Kastamonu","Kayseri","Kırklareli","Kırşehir",
                "Kocaeli","Konya","Kütahya","Malatya","Manisa","Kahramanmaraş","Mardin","Muğla",
                "Muş","Nevşehir","Niğde","Ordu","Rize","Sakarya","Samsun","Siirt",
                "Sinop","Sivas","Tekirdağ","Tokat","Trabzon","Tunceli","Şanlıurfa","Uşak",
                "Van","Yozgat","Zonguldak","Aksaray","Bayburt","Karaman","Kırıkkale","Batman",
                "Şırnak","Bartın","Ardahan","Iğdır","Yalova","Karabük","Kilis","Osmaniye","Düzce"
            };

            var regions = new List<RegionModel>();
            for (int i = 0; i < trCityNames.Length; i++)
                regions.Add(new RegionModel { RegionId = i + 1, RegionName = trCityNames[i], CountryId = 1 });

            // Almanya
            regions.AddRange(new[] { "Berlin","Hamburg","Münih","Frankfurt","Köln" }
                .Select((n, i) => new RegionModel { RegionId = 82 + i, RegionName = n, CountryId = 2 }));
            // Fransa
            regions.AddRange(new[] { "Paris","Lyon","Marsilya","Toulouse","Nice" }
                .Select((n, i) => new RegionModel { RegionId = 87 + i, RegionName = n, CountryId = 3 }));
            // İtalya
            regions.AddRange(new[] { "Roma","Milano","Napoli","Torino","Palermo" }
                .Select((n, i) => new RegionModel { RegionId = 92 + i, RegionName = n, CountryId = 4 }));
            // İspanya
            regions.AddRange(new[] { "Madrid","Barselona","Valensiya","Sevilla","Zaragoza" }
                .Select((n, i) => new RegionModel { RegionId = 97 + i, RegionName = n, CountryId = 5 }));
            // Hollanda
            regions.AddRange(new[] { "Amsterdam","Rotterdam","Lahey","Utrecht","Eindhoven" }
                .Select((n, i) => new RegionModel { RegionId = 102 + i, RegionName = n, CountryId = 6 }));
            // Polonya
            regions.AddRange(new[] { "Varşova","Krakow","Lodz","Wroclaw","Poznan" }
                .Select((n, i) => new RegionModel { RegionId = 107 + i, RegionName = n, CountryId = 7 }));
            // Romanya
            regions.AddRange(new[] { "Bükreş","Cluj-Napoca","Timişoara","Yaş","Constanta" }
                .Select((n, i) => new RegionModel { RegionId = 112 + i, RegionName = n, CountryId = 8 }));

            ctx.Regions.AddRange(regions);
            await ctx.SaveChangesAsync();

            // STORES  (regionId → (location, level)[])
            var storeDefs = new Dictionary<int, (string loc, string lvl)[]>
            {
                // Türkiye
                [1]  = new[] { ("TR-Adana-1","A"), ("TR-Adana-2","B") },
                [2]  = new[] { ("TR-Adıyaman-1","C") },
                [3]  = new[] { ("TR-Afyon-1","C") },
                [4]  = new[] { ("TR-Ağrı-1","C") },
                [5]  = new[] { ("TR-Amasya-1","C") },
                [6]  = new[] { ("TR-Ankara-1","A"), ("TR-Ankara-2","A"), ("TR-Ankara-3","B") },
                [7]  = new[] { ("TR-Antalya-1","A"), ("TR-Antalya-2","B"), ("TR-Antalya-3","C") },
                [8]  = new[] { ("TR-Artvin-1","C") },
                [9]  = new[] { ("TR-Aydın-1","B"), ("TR-Aydın-2","C") },
                [10] = new[] { ("TR-Balıkesir-1","B"), ("TR-Balıkesir-2","C") },
                [11] = new[] { ("TR-Bilecik-1","C") },
                [12] = new[] { ("TR-Bingöl-1","C") },
                [13] = new[] { ("TR-Bitlis-1","C") },
                [14] = new[] { ("TR-Bolu-1","C") },
                [15] = new[] { ("TR-Burdur-1","C") },
                [16] = new[] { ("TR-Bursa-1","A"), ("TR-Bursa-2","B"), ("TR-Bursa-3","C") },
                [17] = new[] { ("TR-Çanakkale-1","B") },
                [18] = new[] { ("TR-Çankırı-1","C") },
                [19] = new[] { ("TR-Çorum-1","C") },
                [20] = new[] { ("TR-Denizli-1","B"), ("TR-Denizli-2","C") },
                [21] = new[] { ("TR-Diyarbakır-1","A"), ("TR-Diyarbakır-2","B") },
                [22] = new[] { ("TR-Edirne-1","B") },
                [23] = new[] { ("TR-Elazığ-1","B") },
                [24] = new[] { ("TR-Erzincan-1","C") },
                [25] = new[] { ("TR-Erzurum-1","B"), ("TR-Erzurum-2","C") },
                [26] = new[] { ("TR-Eskişehir-1","A"), ("TR-Eskişehir-2","B") },
                [27] = new[] { ("TR-Gaziantep-1","A"), ("TR-Gaziantep-2","B"), ("TR-Gaziantep-3","C") },
                [28] = new[] { ("TR-Giresun-1","C") },
                [29] = new[] { ("TR-Gümüşhane-1","C") },
                [30] = new[] { ("TR-Hakkari-1","C") },
                [31] = new[] { ("TR-Hatay-1","A"), ("TR-Hatay-2","B") },
                [32] = new[] { ("TR-Isparta-1","B") },
                [33] = new[] { ("TR-Mersin-1","A"), ("TR-Mersin-2","B") },
                [34] = new[] { ("TR-İstanbul-1","A"), ("TR-İstanbul-2","A"), ("TR-İstanbul-3","A") },
                [35] = new[] { ("TR-İzmir-1","A"), ("TR-İzmir-2","A"), ("TR-İzmir-3","B") },
                [36] = new[] { ("TR-Kars-1","C") },
                [37] = new[] { ("TR-Kastamonu-1","C") },
                [38] = new[] { ("TR-Kayseri-1","A"), ("TR-Kayseri-2","B") },
                [39] = new[] { ("TR-Kırklareli-1","C") },
                [40] = new[] { ("TR-Kırşehir-1","C") },
                [41] = new[] { ("TR-Kocaeli-1","A"), ("TR-Kocaeli-2","B"), ("TR-Kocaeli-3","C") },
                [42] = new[] { ("TR-Konya-1","A"), ("TR-Konya-2","B") },
                [43] = new[] { ("TR-Kütahya-1","C") },
                [44] = new[] { ("TR-Malatya-1","B"), ("TR-Malatya-2","C") },
                [45] = new[] { ("TR-Manisa-1","B") },
                [46] = new[] { ("TR-Kahramanmaraş-1","B"), ("TR-Kahramanmaraş-2","C") },
                [47] = new[] { ("TR-Mardin-1","B") },
                [48] = new[] { ("TR-Muğla-1","B"), ("TR-Muğla-2","C") },
                [49] = new[] { ("TR-Muş-1","C") },
                [50] = new[] { ("TR-Nevşehir-1","B") },
                [51] = new[] { ("TR-Niğde-1","C") },
                [52] = new[] { ("TR-Ordu-1","B") },
                [53] = new[] { ("TR-Rize-1","C") },
                [54] = new[] { ("TR-Sakarya-1","B"), ("TR-Sakarya-2","C") },
                [55] = new[] { ("TR-Samsun-1","A"), ("TR-Samsun-2","B") },
                [56] = new[] { ("TR-Siirt-1","C") },
                [57] = new[] { ("TR-Sinop-1","C") },
                [58] = new[] { ("TR-Sivas-1","B") },
                [59] = new[] { ("TR-Tekirdağ-1","B"), ("TR-Tekirdağ-2","C") },
                [60] = new[] { ("TR-Tokat-1","C") },
                [61] = new[] { ("TR-Trabzon-1","A"), ("TR-Trabzon-2","B") },
                [62] = new[] { ("TR-Tunceli-1","C") },
                [63] = new[] { ("TR-Şanlıurfa-1","A"), ("TR-Şanlıurfa-2","B") },
                [64] = new[] { ("TR-Uşak-1","C") },
                [65] = new[] { ("TR-Van-1","B"), ("TR-Van-2","C") },
                [66] = new[] { ("TR-Yozgat-1","C") },
                [67] = new[] { ("TR-Zonguldak-1","B") },
                [68] = new[] { ("TR-Aksaray-1","C") },
                [69] = new[] { ("TR-Bayburt-1","C") },
                [70] = new[] { ("TR-Karaman-1","C") },
                [71] = new[] { ("TR-Kırıkkale-1","C") },
                [72] = new[] { ("TR-Batman-1","B") },
                [73] = new[] { ("TR-Şırnak-1","C") },
                [74] = new[] { ("TR-Bartın-1","C") },
                [75] = new[] { ("TR-Ardahan-1","C") },
                [76] = new[] { ("TR-Iğdır-1","C") },
                [77] = new[] { ("TR-Yalova-1","B") },
                [78] = new[] { ("TR-Karabük-1","C") },
                [79] = new[] { ("TR-Kilis-1","C") },
                [80] = new[] { ("TR-Osmaniye-1","C") },
                [81] = new[] { ("TR-Düzce-1","C") },
                // Almanya
                [82] = new[] { ("DE-Berlin-1","A"), ("DE-Berlin-2","A"), ("DE-Berlin-3","B") },
                [83] = new[] { ("DE-Hamburg-1","A"), ("DE-Hamburg-2","B") },
                [84] = new[] { ("DE-Münih-1","A"), ("DE-Münih-2","B") },
                [85] = new[] { ("DE-Frankfurt-1","A"), ("DE-Frankfurt-2","B") },
                [86] = new[] { ("DE-Köln-1","B"), ("DE-Köln-2","C") },
                // Fransa
                [87] = new[] { ("FR-Paris-1","A"), ("FR-Paris-2","A"), ("FR-Paris-3","B") },
                [88] = new[] { ("FR-Lyon-1","A"), ("FR-Lyon-2","B") },
                [89] = new[] { ("FR-Marsilya-1","B"), ("FR-Marsilya-2","C") },
                [90] = new[] { ("FR-Toulouse-1","B") },
                [91] = new[] { ("FR-Nice-1","B"), ("FR-Nice-2","C") },
                // İtalya
                [92] = new[] { ("IT-Roma-1","A"), ("IT-Roma-2","A"), ("IT-Roma-3","B") },
                [93] = new[] { ("IT-Milano-1","A"), ("IT-Milano-2","B") },
                [94] = new[] { ("IT-Napoli-1","B"), ("IT-Napoli-2","C") },
                [95] = new[] { ("IT-Torino-1","B") },
                [96] = new[] { ("IT-Palermo-1","C") },
                // İspanya
                [97]  = new[] { ("ES-Madrid-1","A"), ("ES-Madrid-2","A"), ("ES-Madrid-3","B") },
                [98]  = new[] { ("ES-Barselona-1","A"), ("ES-Barselona-2","B") },
                [99]  = new[] { ("ES-Valensiya-1","B") },
                [100] = new[] { ("ES-Sevilla-1","B") },
                [101] = new[] { ("ES-Zaragoza-1","C") },
                // Hollanda
                [102] = new[] { ("NL-Amsterdam-1","A"), ("NL-Amsterdam-2","B") },
                [103] = new[] { ("NL-Rotterdam-1","A"), ("NL-Rotterdam-2","B") },
                [104] = new[] { ("NL-Lahey-1","B") },
                [105] = new[] { ("NL-Utrecht-1","B") },
                [106] = new[] { ("NL-Eindhoven-1","C") },
                // Polonya
                [107] = new[] { ("PL-Varşova-1","A"), ("PL-Varşova-2","B") },
                [108] = new[] { ("PL-Krakow-1","B") },
                [109] = new[] { ("PL-Lodz-1","B") },
                [110] = new[] { ("PL-Wroclaw-1","B") },
                [111] = new[] { ("PL-Poznan-1","C") },
                // Romanya
                [112] = new[] { ("RO-Bükreş-1","A"), ("RO-Bükreş-2","B") },
                [113] = new[] { ("RO-Cluj-1","B") },
                [114] = new[] { ("RO-Timişoara-1","B") },
                [115] = new[] { ("RO-Yaş-1","C") },
                [116] = new[] { ("RO-Constanta-1","C") },
            };

            var stores = storeDefs
                .SelectMany(kv => kv.Value.Select(s => new StoreModel
                {
                    StoreLocation = s.loc,
                    StoreLevel    = s.lvl,
                    RegionId      = kv.Key
                }))
                .ToList();

            ctx.Stores.AddRange(stores);
            await ctx.SaveChangesAsync();
        }

        // USERS (Admin + StoreManagers)
        if (!await ctx.Users.AnyAsync())
        {
            // Veritabanındaki mağazaları çek (Eğer yukarıdaki blok çalışmadıysa lazım olacak)
            var dbStores = await ctx.Stores.ToListAsync();

            var adminUser = new UserModel
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                FullName = "Sistem Yöneticisi",
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            ctx.Users.Add(adminUser);

            var manager1 = new UserModel
            {
                Username = "manager1",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                FullName = "Ahmet Yılmaz",
                Role = "StoreManager",
                StoreId = dbStores.FirstOrDefault(s => s.StoreLocation == "TR-Adana-1")?.StoreId, // TR-Adana-1
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            ctx.Users.Add(manager1);

            var manager2 = new UserModel
            {
                Username = "manager2",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                FullName = "Ayşe Demir",
                Role = "StoreManager",
                StoreId = dbStores.FirstOrDefault(s => s.StoreLocation == "TR-Adana-2")?.StoreId, // TR-Adana-2
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            ctx.Users.Add(manager2);

            var manager3 = new UserModel
            {
                Username = "manager3",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                FullName = "Mehmet Kaya",
                Role = "StoreManager",
                StoreId = dbStores.FirstOrDefault(s => s.StoreLocation == "TR-İstanbul-1")?.StoreId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            ctx.Users.Add(manager3);

            var products = new List<ProductModel>
            {
                new ProductModel { Barcode = "8691234567890", ProductCode = "TSHIRT-BLK-M", Price = 199.99m, ProductProperties = "{\"Renk\":\"Siyah\",\"Beden\":\"M\"}" },
                new ProductModel { Barcode = "8691234567891", ProductCode = "PANT-BLU-32", Price = 349.50m, ProductProperties = "{\"Renk\":\"Mavi\",\"Beden\":\"32\"}" },
                new ProductModel { Barcode = "8691234567892", ProductCode = "SHOE-WHT-42", Price = 899.00m, ProductProperties = "{\"Renk\":\"Beyaz\",\"Numara\":\"42\"}" }
            };
            ctx.Products.AddRange(products);

            // Save to generate IDs
            await ctx.SaveChangesAsync();

            // PRODUCT STORES (Stocks)
            var stocks = new List<ProductStoreModel>
            {
                new ProductStoreModel { Barcode = "8691234567890", StoreId = manager1.StoreId.Value, 
                    SizeXS=5, SizeS=10, SizeM=15, SizeL=10, SizeXL=5, SizeXXL=3, SizeXXXL=2, OptionCount = 50 },
                new ProductStoreModel { Barcode = "8691234567891", StoreId = manager1.StoreId.Value, 
                    SizeXS=1, SizeS=2, SizeM=3, SizeL=2, SizeXL=2, SizeXXL=0, SizeXXXL=0, OptionCount = 10 },
                new ProductStoreModel { Barcode = "8691234567892", StoreId = manager2.StoreId.Value, 
                    SizeXS=0, SizeS=5, SizeM=5, SizeL=5, SizeXL=3, SizeXXL=2, SizeXXXL=0, OptionCount = 20 },
                new ProductStoreModel { Barcode = "8691234567890", StoreId = manager3.StoreId.Value, 
                    SizeXS=0, SizeS=0, SizeM=1, SizeL=2, SizeXL=1, SizeXXL=1, SizeXXXL=0, OptionCount = 5 }
            };
            ctx.ProductStores.AddRange(stocks);
            await ctx.SaveChangesAsync();
        }
    }
}
