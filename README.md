# Simple Note API

ASP.NET Core ve EF Core ile geliştirilmiş, not yönetimi için bir REST API. Notları ekleyebilir, güncelleyebilir, silebilir ve listeleyebilir; başlık ve kategoriye göre arama destekler. Veriler PostgreSQL üzerinde, Code First ve migration'larla yönetilir.

## Teknolojiler

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10 (Code First, Migrations)
- Npgsql.EntityFrameworkCore.PostgreSQL 10.0.3
- PostgreSQL 18
- Postman (API testleri)

## Alan Adları

| Türkçe | Kod |
|---|---|
| Id | Id |
| Baslik | Title |
| Icerik | Content |
| OlusturmaTarihi | CreationDate |
| Kategori | Category |

## Endpointler

| Metot | URL | Açıklama | Status Kodları |
|---|---|---|---|
| GET | `/api/notes` | Tüm notları listeler | 200 |
| GET | `/api/notes?title=&category=` | Başlık ve/veya kategoriye göre arar | 200 |
| GET | `/api/notes/{id}` | Tek notu getirir | 200, 404 |
| POST | `/api/notes` | Yeni not ekler | 201, 400 |
| PUT | `/api/notes/{id}` | Notu günceller | 204, 400, 404 |
| DELETE | `/api/notes/{id}` | Notu siler | 204, 404 |

## Kurulum

Gereksinimler: .NET 10 SDK, PostgreSQL 18

```bash
git clone https://github.com/BeratTansu/Simple-Note-API.git
cd Simple-Note-API

# Bağlantı bilgisi User Secrets'ta tutulur (repo'ya girmez)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=SimpleNoteDb;Username=postgres;Password=<sifren>" --project SimpleNote.Api

# NuGet paketlerini geri yükle
dotnet restore

# Veritabanını migration'lardan oluştur
dotnet tool install --global dotnet-ef
dotnet ef database update --project SimpleNote.Api

# Çalıştır
dotnet run --project SimpleNote.Api --launch-profile https
```

API https://localhost:7155 adresinde çalışır (Postman collection bu adresi kullanır).

## Postman

`postman/SimpleNoteApi.postman_collection.json` dosyasını Postman'de **Import** ile içe aktararak tüm endpointleri test edebilirsiniz.

## Tasarım Kararları

- **Tek proje:** Bu ölçekte ayrı katman projeleri boş aracı sınıflar üretecekti; bilinçli olarak tek proje tutuldu. Katmanlı mimari bir sonraki projede uygulanıyor.
- **Interface + DI:** Controller somut sınıfa değil `INoteDal`'a bağımlı. Veri kaynağını değiştirmek yalnızca `Program.cs`'deki tek satırı değiştirmeyi gerektirir. `EfNoteDal`, Scoped DbContext kullandığı için Scoped kaydedildi.
- **Arama veritabanında çalışır:** Sorgu `IQueryable` üzerinde kurulur, filtreleme SQL'de (`WHERE`) yapılır.
- **Eşleşme kuralları:** Başlıkta kısmi, kategoride tam eşleşme; ikisi de büyük/küçük harfe duyarsız.
- **Zaman:** `CreationDate` UTC olarak saklanır.
- **Id:** Veritabanı tarafından (identity) üretilir; silinen Id'ler tekrar kullanılmaz.
- **Gizli bilgiler:** Bağlantı bilgisi `appsettings.json`'da değil, User Secrets'ta tutulur.

## Bilinen Kısıtlar

- Büyük/küçük harf dönüşümü Türkçe I/ı/İ harflerini her zaman doğru eşleştirmeyebilir.