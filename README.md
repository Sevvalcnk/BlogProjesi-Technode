# 📰 Technode – Blog Yönetim Sistemi

**Technode**; ASP.NET Core MVC ile geliştirilmiş, responsive tasarıma sahip, kullanıcı etkileşimli bir blog web uygulamasıdır.

Bu proje, ders kapsamında modern web teknolojilerini uygulamak ve MVC mimarisini profesyonel düzeyde kullanmak amacıyla geliştirilmiştir.

## 🔎 Proje Özeti

Technode Blog Sistemi; kullanıcıların kayıt olup giriş yapabildiği, makale okuyup oluşturabildiği ve içeriklere yorum yapabildiği modern bir blog platformudur. Uygulama; sade arayüzü, mobil uyumlu yapısı ve katmanlı mimarisi ile akademik ve geliştirilebilir bir yapı sunar.

---

## ✨ Özellikler

### 📝 Makale Yönetimi
* Makale oluşturma ve yayınlama
* Makale listeleme
* Makale detay sayfasında içerik görüntüleme

### 💬 Yorum Sistemi
* Makalelere yorum yazabilme özelliği
* Kullanıcı etkileşimini artıran yapı

### 👤 Kullanıcı İşlemleri (Auth)
* Kullanıcı kayıt olma
* Giriş yapma ve oturum yönetimi
* Kullanıcı bazlı işlem kontrolleri

### 📱 Kullanıcı Deneyimi & Arayüz
* **Responsive Tasarım:** Mobil, tablet ve masaüstü uyumlu
* Sade ve kullanıcı dostu arayüz

### 🧩 Mimari Yapı
* ASP.NET Core MVC mimarisi
* **SoC (Separation of Concerns):** Controller, Model ve View katmanlarının ayrıştırılması
* Düzenli ve okunabilir kod yapısı

---

## 🛠️ Kullanılan Teknolojiler

| Alan | Teknoloji |
| --- | --- |
| **Backend** | ASP.NET Core MVC (C#) |
| **Frontend** | HTML5, CSS3 |
| **View Engine** | Razor |
| **IDE** | Visual Studio 2022 |
| **Versiyon Kontrol** | Git & GitHub |

---

## 📂 Proje Yapısı

```text
├── Controllers       # Uygulama mantığı ve yönlendirmeler
├── Models            # Veri yapıları ve iş kuralları
├── Views             # Kullanıcı arayüzü dosyaları
│   ├── Account       # Giriş/Kayıt sayfaları
│   ├── Blog          # Blog işlem sayfaları
│   ├── Home          # Ana sayfa
│   └── Shared        # Ortak şablonlar (Layout vb.)
├── wwwroot           # Statik dosyalar
│   └── css           # Stil dosyaları
├── appsettings.json  # Yapılandırma ayarları
├── Program.cs        # Uygulama giriş noktası
└── README.md         # Proje dokümantasyonu

⚙️ Kurulum ve Gereksinimler
Gereksinimler
Visual Studio 2022 veya üzeri

.NET SDK (ASP.NET Core destekli güncel sürüm)

Not: Projede harici bir veritabanı veya ek NuGet paketi kullanılmamıştır.

🚀 Kurulum Adımları
Projeyi Klonlayın: git clone [https://github.com/kullaniciadi/technode.git](https://github.com/kullaniciadi/technode.git)

Projeyi Açın:

İndirdiğiniz klasörü Visual Studio ile açın.

Paketleri Yükleyin:

NuGet paketlerinin otomatik olarak yüklenmesini bekleyin (Restore).

Çalıştırın:

Projeyi başlatmak için Ctrl + F5 tuşlarına basın veya "Run" butonunu kullanın.

🔐 Güvenlik ve Geliştirme Notları
Güvenlik: Kullanıcı kayıt ve giriş işlemleri kontrol altındadır. Formlarda doğrulama (validation) mekanizmaları aktiftir.

Geliştirilebilirlik: MVC katmanları net bir şekilde ayrılmıştır. Proje şu an in-memory veya statik veri ile çalışmaktadır.

Gelecek Planları:

[ ] Veritabanı (SQL Server/SQLite) entegrasyonu

[ ] Admin paneli eklenmesi

[ ] Rol bazlı yetkilendirme (Admin/User)

📚 Ders Kapsamı
Bu proje, ASP.NET Core MVC dersi kapsamında eğitim amaçlı olarak geliştirilmiştir. Temel amaç; MVC mimarisini öğrenmek, kullanıcı etkileşimli web uygulamaları geliştirmek ve GitHub üzerinde profesyonel bir proje sunumu hazırlamaktır.

👩‍💻 Geliştirici
Şevval Cinek

📄 Lisans
Bu proje eğitim amaçlıdır ve açık kaynak olarak sunulmuştur.
