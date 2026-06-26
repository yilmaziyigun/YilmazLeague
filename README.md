
# ⚽ YilmazLeague

App logo:<img width="200" height="200" alt="yilmazleaguelogo" src="https://github.com/user-attachments/assets/186c657b-454b-4a69-9143-b4ae2591cdc3" />



YilmazLeague, ASP.NET Core Web API ve MVC kullanılarak geliştirilmiş bir futbol ligi yönetim platformudur.

Bu projede takımlar, oyuncular, sezonlar, fikstürler, canlı maçlar, maç olayları ve puan durumu gibi bir futbol ligi sisteminde ihtiyaç duyulan temel yapılar bulunmaktadır.

Proje öğrenme sürecinde geliştirilmiştir. Amaç; katmanlı mimari, Entity Framework Core, SQL Server, Web API ve MVC yapılarını gerçek bir senaryo üzerinden öğrenmek ve uygulamaktır.

---

## 🚀 Proje Amacı

Bu projenin amacı, bir futbol ligi yönetim sisteminin temel işleyişini oluşturmaktır.

Sistem üzerinden:

- Sezonlara göre maçlar listelenebilir.
- Haftalık fikstür takip edilebilir.
- Canlı maçlar görüntülenebilir.
- Maç detayları incelenebilir.
- Gol, kart ve oyuncu değişikliği gibi maç olayları takip edilebilir.
- Puan durumu hesaplanabilir.
- Admin paneli üzerinden maç işlemleri yapılabilir.

---

## 🛠 Kullanılan Teknolojiler

- ASP.NET Core 6.0
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Swagger
- Bootstrap
- HTML
- CSS
- JavaScript

---

## 🧱 Proje Mimarisi

Proje katmanlı mimari mantığıyla hazırlanmıştır.

```text
YilmazLeague
│
├── YilmazLeague.EntityLayer
│   └── Entity sınıfları
│
├── YilmazLeague.DataAccessLayer
│   └── DbContext ve veritabanı ilişkileri
│
├── YilmazLeague.BusinessLayer
│   └── İş katmanı
│
├── YilmazLeague.WebApi
│   └── API controllerları
│ 
└── YilmazLeague.WebUI
    └── MVC kullanıcı arayüzü
```

---

## 📌 Projede Bulunan Temel Yapılar

### Entity Yapıları

Projede kullanılan temel entity sınıfları:

- `Team`
- `Player`
- `LeagueMatch`
- `MatchEvent`
- `Season`

Bu yapılar sayesinde takım, oyuncu, maç, sezon ve maç olayları arasında ilişkiler kurulmuştur.

---

## 🌐 Web API Özellikleri

Web API tarafında futbol ligi verilerini yönetmek için çeşitli endpointler hazırlanmıştır.

Başlıca controllerlar:

- `TeamsController`
- `PlayersController`
- `MatchesController`
- `EventsController`
- `SeasonsController`
- `StandingsController`
- `StatisticsController`

Database üzerinden yapılabilen işlemler:

- Takımları listeleme
- Oyuncuları listeleme
- Maçları listeleme
- Sezona ve haftaya göre maç getirme
- Yeni maç oluşturma
- Maç skorunu güncelleme
- Maç olayı ekleme
- Puan durumunu hesaplama
- Dashboard istatistiklerini getirme

---

## 🖥 Web UI Özellikleri

MVC tarafında kullanıcıların ligi takip edebilmesi için arayüz ekranları hazırlanmıştır.

Bulunan sayfalar:

- Ana sayfa
- Canlı skorlar
- Fikstür
- Maç detay sayfası
- Puan durumu
- Admin giriş sayfası
- Admin paneli

---

## 🔴 Canlı Skor Sistemi

Canlı maçlar ayrı bir ekranda listelenir.

Canlı maçlarda:

- Ev sahibi takım
- Deplasman takımı
- Anlık skor
- Maç dakikası
- Maç detayına gitme bağlantısı

görüntülenebilir.

Bu yapı sayesinde sistem gerçek bir canlı skor uygulaması gibi çalışacak şekilde tasarlanmıştır.

---

## 📅 Fikstür Sistemi

Fikstür ekranında maçlar sezon ve haftaya göre listelenir.

Kullanıcı:

- Sezon seçebilir.
- Hafta seçebilir.
- Geçen haftaya geçebilir.
- Sonraki haftaya geçebilir.
- Maça tıklayarak maç detayına gidebilir.

---

## 🏆 Puan Durumu

Puan durumu maç skorlarına göre hesaplanır.

Takımlar için:

- Oynanan maç
- Galibiyet
- Beraberlik
- Mağlubiyet
- Atılan gol
- Yenilen gol
- Averaj
- Puan

bilgileri gösterilir.

---

## ⚙️ Admin Paneli

Admin paneli, maç yönetimi için hazırlanmıştır.

Admin panelinde yapılabilen işlemler:

- Maç oluşturma
- Maç skoru girme
- Maç olayı ekleme
- Gol ekleme
- Sarı kart ekleme
- Kırmızı kart ekleme
- Oyuncu değişikliği ekleme

Admin paneli normal kullanıcıya gizlidir. Giriş yapıldıktan sonra menüde Admin sekmesi görünür.

Demo admin bilgileri:

```text
Kullanıcı adı: admin
Şifre: 1234
```

> Not: Admin girişi öğrenme amacıyla basit session yapısı ile hazırlanmıştır. Gerçek projelerde daha güvenli kimlik doğrulama yöntemleri kullanılacaktır.

---

## 🗄 Veritabanı

Projede SQL Server kullanılmıştır.

Veritabanı işlemleri Entity Framework Core ile yapılmaktadır.

DbContext dosyası:

```text
YilmazLeague.DataAccessLayer/Context/YilmazLeagueDB.cs
```

## 📂 Örnek Veri

Projede örnek veri olarak sezon, takım, oyuncu, maç ve maç olayı bilgileri hazırlanmıştır.

Örnek sezonlar:

- 2023-2024
- 2024-2025
- 2025-2026
- 2026-2027

---

## 📸 Ekran Görüntüleri


### Ana Sayfa

<img width="970" height="882" alt="Homepage" src="https://github.com/user-attachments/assets/db5b1e94-e2d3-4df0-be57-f21dbef50665" />


### Canlı Skorlar

<img width="985" height="792" alt="canlı skor" src="https://github.com/user-attachments/assets/30d3f425-2294-48f1-aa1b-8262369bb6f9" />


### Güncel Fikstür

<img width="959" height="868" alt="güncel fikstür" src="https://github.com/user-attachments/assets/dce59b1c-a9b8-4666-907f-d7754fb17234" />


### 35. Hafta Fikstürü

<img width="959" height="856" alt="35 fikstür" src="https://github.com/user-attachments/assets/412eace6-d093-4185-bb62-669d431b1bd1" />


### Maç Detay Sayfası

<img width="966" height="885" alt="maç detay" src="https://github.com/user-attachments/assets/442fa2e2-cba7-4498-bebe-479974778417" />


### Puan Durumu

<img width="700" height="880" alt="puan durumu" src="https://github.com/user-attachments/assets/3d3b08ed-c36f-4d73-8028-932fe1016c49" />


### Admin Giriş

<img width="1124" height="783" alt="admin giriş" src="https://github.com/user-attachments/assets/a298274c-9116-42e2-8b7f-70c6d105f4cd" />


### Admin Maç Yönetimi

<img width="751" height="882" alt="admin maç yönetimi" src="https://github.com/user-attachments/assets/ca305ed8-ca69-43ef-a2de-3e947d66d02a" />


### Gol Ekleme

<img width="907" height="343" alt="gol ekleme" src="https://github.com/user-attachments/assets/3e4521e6-bce5-437f-b8a4-a5b9874f65d4" />


### Kart Ekleme

<img width="914" height="339" alt="kart ekleme" src="https://github.com/user-attachments/assets/5de0be0d-24a7-4927-b795-4751565d03ec" />


---

## ✅ Proje Durumu

Proje öğrenme amacıyla geliştirilmiştir.

Temel olarak çalışılan:

- Web API
- MVC arayüz
- Canlı skor ekranı
- Fikstür ekranı
- Maç detay ekranı
- Puan durumu
- Admin paneli


