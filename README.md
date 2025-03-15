E-Comemrce FrontEnd Dökümanatasyonu
Bu proje, Blazor WebAssembly kullanılarak geliştirilmiş modern bir e-ticaret platformudur. Kullanıcıların ürünleri görüntüleyebileceği, favorilere ekleyebileceği, sepete ekleyebileceği ve sipariş verebileceği kapsamlı bir web uygulamasıdır.
Temel Bileşenler
1. Sayfa Yapısı
  Ana Sayfa: Ürün listesi ve kategorilerin görüntülendiği giriş sayfası
  Ürün Detay: /product/{ProductId} rotasında ürün detaylarını gösteren sayfa
  Favoriler: /favorites rotasında kullanıcının favori ürünlerini yöneten sayfa
  Sepet: Alışveriş sepeti yönetimi
  Ödeme: Sipariş oluşturma ve ödeme işlemleri
  Admin Panel: Ürün, kategori ve sipariş yönetimi için yönetici arayüzü
2. Servisler
AuthService
Kullanıcı kimlik doğrulama ve yetkilendirme işlemleri
JWT token yönetimi
Oturum durumu kontrolü
ProductService
Ürün CRUD işlemleri
Ürün listeleme ve filtreleme
Stok yönetimi
CartService
Sepet işlemleri
Ürün ekleme/çıkarma
Toplam fiyat hesaplama
LocalStorage entegrasyonu
Özellikler
1. Ürün Yönetimi
  Ürün ekleme, düzenleme, silme
  Kategori bazlı filtreleme
  İndirim yönetimi
  Stok takibi
2. Sepet İşlemleri
  Ürün ekleme/çıkarma
  Miktar güncelleme
  Toplam fiyat hesaplama
  LocalStorage'da sepet verisi saklama
3. Favori Sistemi
  Ürünleri favorilere ekleme/çıkarma
  Favori ürünleri listeleme
  Favori ürünleri sepete ekleme
4. Admin Paneli
  Ürün yönetimi
  Kategori yönetimi
  Sipariş takibi
  Kullanıcı yönetimi
  Sistem ayarları
Güvenlik
1. Kimlik Doğrulama
  JWT tabanlı kimlik doğrulama
  Rol bazlı yetkilendirme
  Session yönetimi
2. Veri Güvenliği
  HTTPS kullanımı
  Input validasyonu
  XSS koruması
UI/UX Özellikleri
1. Responsive Tasarım
  Bootstrap grid sistemi
  Mobil uyumlu arayüz
  Dinamik içerik yükleme
2. Kullanıcı Deneyimi
  Yükleme göstergeleri
  Hata mesajları
  İşlem onay dialogları
  Animasyonlar ve geçiş efektleri
Teknik Detaylar
1. Bağımlılıklar
  Blazor WebAssembly
  Bootstrap 5
  Bootstrap Icons
  System.Net.Http.Json
2. Veri Saklama
  LocalStorage (Sepet verileri)
  SessionStorage (Kimlik bilgileri)
  API entegrasyonu (Uzak sunucu)
3. Performans Optimizasyonları
  Lazy loading
  Önbellek kullanımı
  Durum yönetimi optimizasyonu
