import base64
import urllib.request
import time

products_data = [
    {"barcode": "869000000001", "code": "TXT-001", "name": "Beyaz Basic Tişört", "url": "https://images.unsplash.com/photo-1521572267360-ee0c2909d518?auto=format&fit=crop&q=80&w=600", "price": 290.00},
    {"barcode": "869000000002", "code": "TXT-002", "name": "Siyah Klasik Deri Ceket", "url": "https://images.unsplash.com/photo-1551028719-00167b16eac5?auto=format&fit=crop&q=80&w=600", "price": 2490.00},
    {"barcode": "869000000003", "code": "TXT-003", "name": "Mavi Slim-Fit Kot Pantolon", "url": "https://images.unsplash.com/photo-1542272604-787c3835535d?auto=format&fit=crop&q=80&w=600", "price": 750.00},
    {"barcode": "869000000004", "code": "TXT-004", "name": "Sarı Yazlık Elbise", "url": "https://images.unsplash.com/photo-1515886657613-9f3515b0c78f?auto=format&fit=crop&q=80&w=600", "price": 1190.00},
    {"barcode": "869000000005", "code": "TXT-005", "name": "Oduncu Kareli Gömlek", "url": "https://images.unsplash.com/photo-1596755094514-f87e34085b2c?auto=format&fit=crop&q=80&w=600", "price": 550.00},
    {"barcode": "869000000006", "code": "TXT-006", "name": "Siyah Basic Tişört", "url": "https://images.unsplash.com/photo-1503342217505-b0a15ec3261c?auto=format&fit=crop&q=80&w=600", "price": 290.00},
    {"barcode": "869000000007", "code": "TXT-007", "name": "Gri Selanik Hırka", "url": "https://images.unsplash.com/photo-1576566588028-4147f3842f27?auto=format&fit=crop&q=80&w=600", "price": 890.00},
    {"barcode": "869000000008", "code": "TXT-008", "name": "Gri Kışlık Kaşe Kaban", "url": "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?auto=format&fit=crop&q=80&w=600", "price": 3190.00},
    {"barcode": "869000000009", "code": "TXT-009", "name": "Gri Kapüşonlu Sweatshirt", "url": "https://images.unsplash.com/photo-1556821840-3a63f95609a7?auto=format&fit=crop&q=80&w=600", "price": 650.00},
    {"barcode": "869000000010", "code": "TXT-010", "name": "Bej Chino Pantolon", "url": "https://images.unsplash.com/photo-1479064555552-3ef4979f8908?auto=format&fit=crop&q=80&w=600", "price": 790.00},
    {"barcode": "869000000011", "code": "TXT-011", "name": "Yeşil Kargo Pantolon", "url": "https://images.unsplash.com/photo-1517423568366-8b83523034fd?auto=format&fit=crop&q=80&w=600", "price": 890.00},
    {"barcode": "869000000012", "code": "TXT-012", "name": "Mavi Denim Kot Ceket", "url": "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?auto=format&fit=crop&q=80&w=600", "price": 1150.00},
    {"barcode": "869000000013", "code": "TXT-013", "name": "Krem Rengi Boğazlı Kazak", "url": "https://images.unsplash.com/photo-1620799140408-edc6dcb6d633?auto=format&fit=crop&q=80&w=600", "price": 850.00},
    {"barcode": "869000000014", "code": "TXT-014", "name": "Mavi Triko Cardigan", "url": "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?auto=format&fit=crop&q=80&w=600", "price": 950.00},
    {"barcode": "869000000015", "code": "TXT-015", "name": "Gri Kumaş Pantolon", "url": "https://images.unsplash.com/photo-1604176354204-9268737828e4?auto=format&fit=crop&q=80&w=600", "price": 790.00},
    {"barcode": "869000000016", "code": "TXT-016", "name": "Siyah Oversize Kapüşonlu", "url": "https://images.unsplash.com/photo-1556911220-e15b29be8c8f?auto=format&fit=crop&q=80&w=600", "price": 690.00},
    {"barcode": "869000000017", "code": "TXT-017", "name": "Beyaz Klasik Gömlek", "url": "https://images.unsplash.com/photo-1603252109303-2751441dd157?auto=format&fit=crop&q=80&w=600", "price": 590.00},
    {"barcode": "869000000018", "code": "TXT-018", "name": "Mavi Skinny Kot Pantolon", "url": "https://images.unsplash.com/photo-1541099649105-f69ad21f3246?auto=format&fit=crop&q=80&w=600", "price": 750.00},
    {"barcode": "869000000019", "code": "TXT-019", "name": "Siyah Oxford Gömlek", "url": "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?auto=format&fit=crop&q=80&w=600", "price": 620.00},
    {"barcode": "869000000020", "code": "TXT-020", "name": "Polo Yaka Mavi Tişört", "url": "https://images.unsplash.com/photo-1581655353564-df123a1eb820?auto=format&fit=crop&q=80&w=600", "price": 450.00},
    {"barcode": "869000000021", "code": "TXT-021", "name": "Kahverengi Trençkot", "url": "https://images.unsplash.com/photo-1539571696357-5a69c17a67c6?auto=format&fit=crop&q=80&w=600", "price": 2890.00},
    {"barcode": "869000000022", "code": "TXT-022", "name": "Siyah Spor Şort", "url": "https://images.unsplash.com/photo-1591195853828-11db59a44f6b?auto=format&fit=crop&q=80&w=600", "price": 380.00},
    {"barcode": "869000000023", "code": "TXT-023", "name": "V Yaka Siyah Kazak", "url": "https://images.unsplash.com/photo-1614975058789-41316d0e2e9c?auto=format&fit=crop&q=80&w=600", "price": 790.00},
    {"barcode": "869000000024", "code": "TXT-024", "name": "Sarı Yağmurluk", "url": "https://images.unsplash.com/photo-1548883354-7622d03aca27?auto=format&fit=crop&q=80&w=600", "price": 1250.00},
    {"barcode": "869000000025", "code": "TXT-025", "name": "Beyaz Keten Gömlek", "url": "https://images.unsplash.com/photo-1598033129183-c4f50c736f10?auto=format&fit=crop&q=80&w=600", "price": 690.00},
    {"barcode": "869000000026", "code": "TXT-026", "name": "Kırmızı Spor Elbise", "url": "https://images.unsplash.com/photo-1496747611176-843222e1e57c?auto=format&fit=crop&q=80&w=600", "price": 1390.00},
    {"barcode": "869000000027", "code": "TXT-027", "name": "Klasik Siyah Blazer Ceket", "url": "https://images.unsplash.com/photo-1594938298603-c8148c4dae35?auto=format&fit=crop&q=80&w=600", "price": 1890.00},
    {"barcode": "869000000028", "code": "TXT-028", "name": "Siyah Koşu Taytı", "url": "https://images.unsplash.com/photo-1506152983158-b4a74a01c721?auto=format&fit=crop&q=80&w=600", "price": 590.00},
    {"barcode": "869000000029", "code": "TXT-029", "name": "Çizgili Keten Pantolon", "url": "https://images.unsplash.com/photo-1604176354204-9268737828e4?auto=format&fit=crop&q=80&w=600", "price": 850.00},
    {"barcode": "869000000030", "code": "TXT-030", "name": "Haki Bomber Ceket", "url": "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?auto=format&fit=crop&q=80&w=600", "price": 1390.00},
    {"barcode": "869000000031", "code": "TXT-031", "name": "Kırmızı Polo Tişört", "url": "https://images.unsplash.com/photo-1581655353564-df123a1eb820?auto=format&fit=crop&q=80&w=600", "price": 450.00},
    {"barcode": "869000000032", "code": "TXT-032", "name": "Mavi Bisiklet Yaka Kazak", "url": "https://images.unsplash.com/photo-1620799140408-edc6dcb6d633?auto=format&fit=crop&q=80&w=600", "price": 850.00},
    {"barcode": "869000000033", "code": "TXT-033", "name": "Klasik Siyah Takım Elbise", "url": "https://images.unsplash.com/photo-1507679799987-c73779587ccf?auto=format&fit=crop&q=80&w=600", "price": 4200.00},
    {"barcode": "869000000034", "code": "TXT-034", "name": "Mavi Çizgili Klasik Gömlek", "url": "https://images.unsplash.com/photo-1598033129183-c4f50c736f10?auto=format&fit=crop&q=80&w=600", "price": 650.00},
    {"barcode": "869000000035", "code": "TXT-035", "name": "Slim-Fit Siyah Kot Pantolon", "url": "https://images.unsplash.com/photo-1582562124811-c09040d0a901?auto=format&fit=crop&q=80&w=600", "price": 790.00},
    {"barcode": "869000000036", "code": "TXT-036", "name": "Haki Kargo Şort", "url": "https://images.unsplash.com/photo-1591195853828-11db59a44f6b?auto=format&fit=crop&q=80&w=600", "price": 380.00},
    {"barcode": "869000000037", "code": "TXT-037", "name": "Turuncu Kapüşonlu Sweatshirt", "url": "https://images.unsplash.com/photo-1556821840-3a63f95609a7?auto=format&fit=crop&q=80&w=600", "price": 690.00},
    {"barcode": "869000000038", "code": "TXT-038", "name": "Kırmızı Sweatshirt", "url": "https://images.unsplash.com/photo-1578587018452-892bacefd3f2?auto=format&fit=crop&q=80&w=600", "price": 690.00},
    {"barcode": "869000000039", "code": "TXT-039", "name": "Taş Rengi Kargo Pantolon", "url": "https://images.unsplash.com/photo-1517423568366-8b83523034fd?auto=format&fit=crop&q=80&w=600", "price": 890.00},
    {"barcode": "869000000040", "code": "TXT-040", "name": "Lila Triko Kazak", "url": "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?auto=format&fit=crop&q=80&w=600", "price": 950.00},
    {"barcode": "869000000041", "code": "TXT-041", "name": "Lacivert Spor Sweatshirt", "url": "https://images.unsplash.com/photo-1556821840-3a63f95609a7?auto=format&fit=crop&q=80&w=600", "price": 650.00},
    {"barcode": "869000000042", "code": "TXT-042", "name": "Beyaz Oversize Tişört", "url": "https://images.unsplash.com/photo-1521572267360-ee0c2909d518?auto=format&fit=crop&q=80&w=600", "price": 320.00},
    {"barcode": "869000000043", "code": "TXT-043", "name": "Çizgili Yazlık Plaj Tişörtü", "url": "https://images.unsplash.com/photo-1523381210434-271e8be1f52b?auto=format&fit=crop&q=80&w=600", "price": 390.00},
    {"barcode": "869000000044", "code": "TXT-044", "name": "Askılı Saten Elbise", "url": "https://images.unsplash.com/photo-1572804013309-59a88b7e92f1?auto=format&fit=crop&q=80&w=600", "price": 1490.00},
    {"barcode": "869000000045", "code": "TXT-045", "name": "Gri Melanj Eşofman Altı", "url": "https://images.unsplash.com/photo-1485230895905-ec40ba36b9bc?auto=format&fit=crop&q=80&w=600", "price": 490.00},
    {"barcode": "869000000046", "code": "TXT-046", "name": "Mavi Kot Şort", "url": "https://images.unsplash.com/photo-1565084888279-aca607ecce0c?auto=format&fit=crop&q=80&w=600", "price": 550.00},
    {"barcode": "869000000047", "code": "TXT-047", "name": "Çiçek Desenli Elbise", "url": "https://images.unsplash.com/photo-1496747611176-843222e1e57c?auto=format&fit=crop&q=80&w=600", "price": 1290.00},
    {"barcode": "869000000048", "code": "TXT-048", "name": "Siyah Şişme Yelek", "url": "https://images.unsplash.com/photo-1608256246200-53e635b5b65f?auto=format&fit=crop&q=80&w=600", "price": 1190.00},
    {"barcode": "869000000049", "code": "TXT-049", "name": "Kahverengi Trençkot", "url": "https://images.unsplash.com/photo-1539571696357-5a69c17a67c6?auto=format&fit=crop&q=80&w=600", "price": 2890.00},
    {"barcode": "869000000050", "code": "TXT-050", "name": "Ekose Desenli Oduncu Gömleği", "url": "https://images.unsplash.com/photo-1596755094514-f87e34085b2c?auto=format&fit=crop&q=80&w=600", "price": 590.00}
]

print("Yüksek kaliteli giyim görselleri indiriliyor ve Base64 formatına çevriliyor...")

sql = "-- %100 UYUMLU VE GERÇEKÇİ BENZERSİZ GİYİM ÜRÜN GÜNCELLEME SCRIPTI\n"
sql += "USE [RetailAndStockManagement];\nGO\n\n"

for idx, p in enumerate(products_data):
    url = p["url"]
    b64_full = ""
    try:
        req = urllib.request.Request(url, headers={'User-Agent': 'Mozilla/5.0'})
        with urllib.request.urlopen(req, timeout=15) as response:
            img_data = response.read()
            img_str = base64.b64encode(img_data).decode("utf-8")
            b64_full = f"data:image/jpeg;base64,{img_str}"
            print(f"[{idx+1}/50] İndirildi: {p['name']}")
    except Exception as e:
        print(f"Hata: {url} -> {e}")
        # Fallback to single pixel image
        b64_full = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNkYAAAAAYAAjCB0C8AAAAASUVORK5CYII="

    # Her barkod için UPDATE sorgusu oluştur
    sql += f"UPDATE [dbo].[Product] SET [ProductProperties] = N'{p['name']}', [Price] = {p['price']}, [ImageBase64] = '{b64_full}' WHERE [Barcode] = '{p['barcode']}';\n"

sql += "GO\n\nPRINT 'Ürün verileri başarıyla güncellendi!';\n"

with open("PerfectImagesData.sql", "w", encoding="utf-8-sig") as f:
    f.write(sql)

print(f"\nToplam {len(products_data)} adet giyim ürünü UPDATE sorgusu PerfectImagesData.sql dosyasına kaydedildi!")
