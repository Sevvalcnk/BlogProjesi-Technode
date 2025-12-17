# 📰 Technode – Blog Yönetim Sistemi

> ASP.NET Core MVC ile geliştirilmiş, responsive tasarıma sahip, kullanıcı etkileşimli modern bir blog web uygulaması.

---

## 🔎 Proje Özeti

**Technode Blog Sistemi**; kullanıcıların kayıt olup giriş yapabildiği, makale okuyup oluşturabildiği ve içeriklere yorum yapabildiği bir platformdur. Uygulama; sade arayüzü, mobil uyumlu yapısı ve katmanlı mimarisi ile akademik ve geliştirilebilir bir yapı sunar. MVC mimarisini profesyonel düzeyde uygulamak amacıyla geliştirilmiştir.

---

## ✨ Özellikler

### 📝 Makale Yönetimi

* Makale oluşturma ve yayınlama
* Makale listeleme
* Makale detay sayfasında içerik görüntüleme

### 👤 Üyelik & Güvenlik

* Kullanıcı kayıt olma ve giriş yapma
* Oturum (Session) yönetimi
* Kullanıcı bazlı işlem kontrolleri

### 💬 Etkileşim Sistemi

* Makalelere yorum yazabilme özelliği
* Kullanıcı etkileşimini artıran dinamik yapı

### 📱 Kullanıcı Deneyimi

* **Responsive Tasarım** (Mobil, tablet ve masaüstü uyumlu)
* Sade ve kullanıcı dostu arayüz

### 🧩 Mimari Yapı

* **ASP.NET Core MVC** mimarisi
* **SoC (Separation of Concerns)** prensibi (Controller, Model, View ayrımı)
* Düzenli ve okunabilir kod yapısı

---

## 🛠️ Teknolojiler

| Katman        | Teknoloji                 |
| ------------- | ------------------------- |
| Backend       | **ASP.NET Core MVC (C#)** |
| Frontend      | **HTML5, CSS3** |
| View Engine   | **Razor** |
| IDE           | **Visual Studio 2022** |
| Sürüm Kontrol | **Git & GitHub** |

---

## 📂 Proje Yapısı

```
├── Controllers
├── Models
├── Services
├── Views
│   ├── Account
│   ├── Makales
│   ├── Home
│   └── Shared
├── Data
├── Migrations
├── wwwroot
│   └── css
│   └── js
├── appsettings.json
├── Program.cs
└── README.md
```
---

## 🚀 Kurulum

Yerel ortamda çalıştırmak için:

1. Depoyu klonlayın

   ```bash
   git clone https://github.com/kullaniciadi/technode.git

2. Visual Studio’da projeyi açın
3. `appsettings.json` içinde **SQL Server Connection String** bilgisini güncelleyin
4. Migration’ları uygulayın

   ```bash
   Update-Database
   ```
5. Uygulamayı çalıştırın (F5)

---
## 🔐 Güvenlik Notları
* Kullanıcı kayıt ve giriş işlemleri kontrol altındadır

* Formlarda doğrulama (validation) mekanizmaları aktiftir

* Proje geliştirmeye açık ve güvenli bir yapıdadır

---

## 🧪 Geliştirme Notları
* MVC katmanları net şekilde ayrıştırılmıştır

* Harici veritabanı kullanılmamıştır (Veriler statik/in-memory tutulmaktadır)

* İleride eklenebilecekler: Veritabanı (SQL), Admin Paneli, Rol bazlı yetkilendirme

## 👩‍💻 Geliştirici
* Bu proje eğitim amaçlı geliştirilmiştir.

* Geliştirici: Şevval Cinek

* GitHub: [https://github.com/Sevvalcnk](https://github.com/Sevvalcnk)

## 📄 Lisans
* Bu proje Eğitim Amaçlı olarak sunulmuştur.
