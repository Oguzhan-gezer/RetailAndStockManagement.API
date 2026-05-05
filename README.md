# RetailAndStockManagement

Modern bir e-ticaret ve stok yönetimi projesi. Bu projede hem bir API altyapısı (Backend) hem de dinamik, kullanıcı dostu bir arayüz (Frontend) bulunmaktadır.

## 🚀 Projeyi Çalıştırmak İçin Kurulum Rehberi

Projeyi bilgisayarınızda çalıştırmak için aşağıdaki adımları sırasıyla izleyin:

### 1. Kodu İndirin
Projeyi bilgisayarınıza klonlayın:
```bash
git clone https://github.com/Oguzhan-gezer/RetailAndStockManagement.API.git
```

### 2. Veritabanı (Database) Kurulumu
Proje Entity Framework Core ile Code First mimarisiyle geliştirilmiştir.

1. **Bağlantı Ayarları:** Proje klasöründeki `appsettings.json` dosyasını açıp veritabanı bağlantı cümlenizin (`ConnectionString`) doğru olduğundan emin olun (Varsayılan olarak `(localdb)\MSSQLLocalDB` ayarlıdır).
2. **Tabloları Oluşturma:** Visual Studio üzerinden **Package Manager Console**'u açın (Tools > NuGet Package Manager > Package Manager Console) ve şu komutu çalıştırarak tabloları oluşturun:
   ```bash
   Update-Database
   ```

### 3. Örnek Verileri (Seed Data) İçeri Aktarma
Tablolar oluştuktan sonra sistemin dolu ve görsel açıdan zengin gözükmesi için örnek verileri yüklemelisiniz. 

SQL Server Management Studio (SSMS) veya Visual Studio (SQL Server Object Explorer) üzerinden oluşturulan veritabanına bağlanıp projenin ana dizinindeki şu dosyaları sırasıyla çalıştırın:
1. **`SeedData.sql`** dosyasını çalıştırın. (Bu işlem; ülkeleri, şehirleri ve mağazaları yükler.)
2. **`PerfectImagesData.sql`** dosyasını çalıştırın. (Bu işlem; sisteme binlerce örnek ürün ve bu ürünlere ait yüksek kaliteli görselleri yükler.)

### 4. Uygulamayı Çalıştırma
* **Backend:** Visual Studio üzerinde projeyi çalıştırarak (F5) API sunucusunu ayağa kaldırın.
* **Frontend:** Projenin içerisindeki `Frontend` klasörüne giderek **`anasayfa.html`** dosyasını tarayıcınızda açın (Visual Studio Code kullanıyorsanız *Live Server* eklentisiyle açmanız önerilir).

🎉 *Hepsi bu kadar, projeyi incelemeye başlayabilirsiniz!*
