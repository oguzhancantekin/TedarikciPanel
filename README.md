# 📦 TedarikciPanel (B2B Tedarikçi ve Müşteri Portalı)

TedarikciPanel, B2B (Business-to-Business) odaklı bir **Tedarikçi ve Müşteri Portalidir**. ASP.NET Core MVC mimarisi kullanılarak geliştirilmiştir. Proje; müşterilerin sisteme kayıt talebi göndermesi, yöneticinin bu talepleri onaylaması ve onaylanan müşterilerin sisteme giriş yaparak firmanın ürünlerini sipariş edebilmesini sağlayan tam kapsamlı bir akış sunar.

## 🚀 Özellikler (Features)

- **Müşteri Kayıt ve Onay Sistemi:** Müşteriler sisteme kayıt isteği gönderir. Yönetici (Admin) onaylayana kadar sisteme giriş yapamazlar.
- **Rol Bazlı Erişim (Yetkilendirme):** `Admin` ve `Musteri` olmak üzere iki farklı rol bulunur. Session bazlı basit ve etkili bir yetkilendirme ile güvenli erişim sağlanır.
- **Ürün Yönetimi:** Admin; sisteme yeni ürünler ekleyebilir, mevcut ürünleri güncelleyebilir veya silebilir. Ürünlere ait fiyat, stok ve görsel yönetimi desteklenir.
- **Sepet (Cart) Sistemi:** Müşteriler satın almak istedikleri ürünleri sepete ekleyebilir, sepet üzerinde miktar güncelleyebilir ve ürünleri sepetten çıkarabilir.
- **Sipariş Yönetimi:** Müşteriler sepetlerindeki ürünleri tek bir adımla siparişe dönüştürebilir. Admin verilen tüm siparişleri ve detaylarını görüntüleyebilir.

## 💻 Kullanılan Teknolojiler (Tech Stack)

- **Backend:** C#, ASP.NET Core MVC
- **Veritabanı:** Microsoft SQL Server (LocalDB)
- **ORM:** Entity Framework Core (Code-First Yaklaşımı)
- **Frontend:** HTML5, CSS3, JavaScript, Bootstrap, Razor Views (`.cshtml`)
- **Durum Yönetimi (State Management):** Session (`HttpContext.Session`)

## 🔑 Kullanıcı Rolleri ve Giriş Bilgileri

**1. Yönetici (Admin)**
Ürünleri yönetebilir, bekleyen müşteri taleplerini onaylayabilir veya reddedebilir ve tüm siparişleri görüntüleyebilir.
- **Email:** `admin@admin.com`
- **Şifre:** `12345`

**2. Müşteri (Customer)**
Sisteme giriş yaparak ürünleri listeleyebilir, sepete ekleyebilir ve sipariş verebilir.
- *Müşteri rolüyle giriş yapabilmek için giriş ekranından kayıt olunmalı ve talebin Admin paneli üzerinden onaylanması beklenmelidir.*

## 📂 Proje Katmanları ve Mimari (Folder Structure)

- **`Controllers/`**: Uygulamanın yönlendirme mantığını içerir. (`Account`, `AdminProduct`, `CustomerRequests`, `Siparis`, `Sepet` vb.)
- **`Models/`**: Veritabanı tablolarını temsil eden entity sınıflarını barındırır. (`Product`, `Musteri`, `Order`, `CustomerRequest` vb.)
- **`Views/`**: Kullanıcı arayüzünü (UI) oluşturan Razor Views dosyalarının bulunduğu bölümdür.
- **`Data/`**: Entity Framework `AppDbContext` sınıfı konfigürasyonlarını içerir.
- **`wwwroot/`**: CSS, JavaScript dosyaları ve görsellerin (images) vb. statik kaynakların barındırıldığı yerdir.
- **`Migrations/`**: Entity Framework Core veritabanı göç (migration) geçmişini tutar.

## 🛠️ Kurulum ve Çalıştırma (Setup)

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1. **Projeyi Klonlayın:**
   ```bash
   git clone https://github.com/KULLANICI_ADINIZ/TedarikciPanel.git
   cd TedarikciPanel
   ```

2. **Gerekli Paketleri Yükleyin:**
   ```bash
   dotnet restore
   ```

3. **Veritabanını Hazırlayın:**
   Code-First yaklaşımı kullanıldığı için Entity Framework Core migration'larını çalıştırarak veritabanını (`TedarikciDb`) oluşturmanız gerekir:
   ```bash
   dotnet ef database update
   ```
   *(Not: `appsettings.json` içerisindeki `DefaultConnection` bilgisi varsayılan olarak MSSQLLocalDB kullanmaktadır. Eğer farklı bir SQL Server kurulumunuz varsa bu kısmı değiştirmelisiniz.)*

4. **Projeyi Çalıştırın:**
   ```bash
   dotnet run
   ```

5. Tarayıcınızda açılan adresten uygulamayı test edebilirsiniz. (Örn: `https://localhost:7xxx`)

## 🤝 Katkıda Bulunma (Contributing)

Projeyi geliştirmeye destek olmak isterseniz:
1. Projeyi fork'layın.
2. Yeni bir dal (branch) oluşturun: `git checkout -b ozellik/YeniOzellik`
3. Değişikliklerinizi işleyin (commit): `git commit -m 'Yeni özellik eklendi'`
4. Dalınıza gönderin (push): `git push origin ozellik/YeniOzellik`
5. Pull Request gönderin.

## 📄 Lisans (License)
Bu proje eğitim ve kişisel referans amaçlıdır. Açık kaynak standartlarında dilediğiniz gibi kullanabilirsiniz (MIT License).
