import base64
import random
import urllib.request
import time

base_products = [
    {"base_name": "Beyaz Tişört", "url": "https://i.imgur.com/Y54Bt8J.jpeg", "price": 250.00},
    {"base_name": "Siyah Tişört", "url": "https://i.imgur.com/9DqEOV5.jpeg", "price": 250.00},
    {"base_name": "Kırmızı Eşofman Altı", "url": "https://i.imgur.com/9LFjwpI.jpeg", "price": 450.00},
    {"base_name": "Mavi Şapka", "url": "https://i.imgur.com/wXuQ7bm.jpeg", "price": 150.00},
    {"base_name": "Siyah Şapka", "url": "https://i.imgur.com/KeqG6r4.jpeg", "price": 150.00},
    {"base_name": "Haki Şort", "url": "https://i.imgur.com/UsFIvYs.jpeg", "price": 300.00},
    {"base_name": "Siyah Şort", "url": "https://i.imgur.com/eGOUveI.jpeg", "price": 280.00},
    {"base_name": "Beyaz Bisiklet Yaka Tişört", "url": "https://i.imgur.com/axsyGpD.jpeg", "price": 270.00}
]

modifiers = ["Premium", "Basic", "Pamuklu", "Spor", "Yazlık", "Slim Fit", "Regular Fit", "Rahat Kesim", "Klasik", "Günlük"]

products = []
product_id = 1

print("Gercek e-ticaret gorselleri indiriliyor...")

image_cache = {}

for bp in base_products:
    url = bp["url"]
    if url not in image_cache:
        try:
            req = urllib.request.Request(url, headers={'User-Agent': 'Mozilla/5.0'})
            with urllib.request.urlopen(req, timeout=10) as response:
                img_data = response.read()
                img_str = base64.b64encode(img_data).decode("utf-8")
                b64_full = f"data:image/jpeg;base64,{img_str}"
                image_cache[url] = b64_full
                print(f"Indirildi: {bp['base_name']}")
        except Exception as e:
            print(f"Hata: {url} -> {e}")
            image_cache[url] = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNkYAAAAAYAAjCB0C8AAAAASUVORK5CYII="

# 50 farklı ürün üretmek için base_product ve modifier'ları birleştiriyoruz
import itertools
all_combinations = list(itertools.product(modifiers, base_products))
random.shuffle(all_combinations)

for modifier, bp in all_combinations[:50]:
    final_name = f"{modifier} {bp['base_name']}"
    
    barcode = f"8690000000{product_id:02d}"
    code = f"TXT-{product_id:03d}"
    
    products.append({
        'barcode': barcode,
        'code': code,
        'price': bp['price'] + random.choice([0, 20, 50, -10]), # Fiyatı biraz değiştir
        'name': final_name,
        'b64': image_cache[bp['url']]
    })
    product_id += 1

sql = "-- %100 UYUMLU GERCEK E-TICARET GORSELLERI\n"
sql += "USE [RetailAndStockManagement];\nGO\n\n"
sql += "DELETE FROM [dbo].[ProductStore];\nDELETE FROM [dbo].[Product];\nGO\n\n"

sql += "INSERT INTO [dbo].[Product] ([Barcode], [ProductCode], [Price], [ProductProperties], [ImageBase64]) VALUES\n"

values = []
for p in products:
    values.append(f"('{p['barcode']}', N'{p['code']}', {p['price']}, N'{p['name']}', '{p['b64']}')")
sql += ",\n".join(values) + ";\nGO\n\n"

sql += "SET IDENTITY_INSERT [dbo].[ProductStore] ON;\n"

ps_values = []
ps_id = 1
for store_id in range(1, 173):
    store_products = random.sample(products, 5)
    for sp in store_products:
        opt_cnt = random.randint(15, 100)
        ps_values.append(f"({ps_id}, '{sp['barcode']}', {store_id}, {opt_cnt})")
        ps_id += 1

batch_size = 100
for i in range(0, len(ps_values), batch_size):
    batch = ps_values[i:i+batch_size]
    sql += "INSERT INTO [dbo].[ProductStore] ([Id], [Barcode], [StoreId], [OptionCount]) VALUES\n"
    sql += ",\n".join(batch) + ";\n"

sql += "SET IDENTITY_INSERT [dbo].[ProductStore] OFF;\nGO\n"

with open("PerfectImagesData.sql", "w", encoding="utf-8-sig") as f:
    f.write(sql)

print(f"Toplam {len(products)} adet urun PerfectImagesData.sql dosyasina kaydedildi! SSMS'te calistirabilirsiniz.")
