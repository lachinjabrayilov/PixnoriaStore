# PixnoriaStore

PixnoriaStore, modern ve kullanıcı dostu arayüze sahip bir e-ticaret platformudur. Ürün listeleme, sepet yönetimi, favori ürünler, kullanıcı kayıt/giriş işlemleri ve sipariş yönetimi gibi temel e-ticaret özelliklerini sunar. Kolayca özelleştirilebilir, farklı ürün kategorileri ve marka filtreleme ile genişletilebilir bir altyapıya sahiptir. Hem masaüstü hem mobil cihazlarda sorunsuz çalışacak şekilde tasarlanmıştır.

---

## Özellikler

- Ürün ve kategori listeleme
- Dinamik marka filtreleme
- Favori ürünler ekleyip çıkarabilme
- Sepet ve sipariş yönetimi
- Kullanıcı kayıt/giriş işlemleri
- SQL veritabanı ile ilişkili veri modeli
- Modern ve responsive arayüz

---

## Ekran Görüntüleri

### Veritabanı Yapısı

![Veritabanı Tablo Yapısı](image2)

PixnoriaStore veritabanında ürünler, siparişler, kullanıcılar ve favoriler gibi ana tablolar yer almaktadır. Bu yapı, uygulamanın veri ilişkilerini ve temel işleyişini tanımlar.

---

### Ana Sayfa & Kategori Filtreleme

![Ana Sayfa ve Kategoriler](image3)

Farklı marka ve kategorilere göre ürünleri filtreleyebileceğiniz modern ana sayfa. Sistem birimleri ve diğer kategorilerdeki ürünler detaylı şekilde listelenir.

---

### Kampanyalı Ürünler

![Kampanyalı Ürünler](image4)

İndirimde olan ürünler, teknik özellikleri, indirim oranı ve kalan stok miktarı ile birlikte sıralanıyor. Kullanıcılar kampanyalı ürünleri kolayca sepete ekleyebilir.

---

### Fırsat Köşesi

![Fırsat Köşesi](image5)

Stokta kalan son ürünler, uygun fiyatlarla ve teknik detaylarıyla birlikte listeleniyor. Her ürünün kalan adedi, sepete ekleme butonu ve stok durumu gösterilmektedir.

---

### Proje Dosya Yapısı

![Proje Dosya Yapısı](image6)

Visual Studio'da PixnoriaStore projesinin klasör/dosya organizasyonu. wwwroot altında css, images ve js klasörleri; Properties ve diğer temel klasörler yer almaktadır.

---

### Kullanıcılar Tablosu

![Kullanıcılar Tablosu](image7)

Uygulamanın kullanıcılar tablosu (Users): Id, Ad, Soyad, Email, Şifre, KayıtTarihi, SonGirisTarihi gibi alanlar ile kullanıcı yönetimi sağlanmaktadır.

---

### Marka Filtreleme Arayüzü

![Marka Filtreleme](image8)

Kullanıcılar, ürünleri markalarına göre filtreleyebilir ve istedikleri ürünleri hızlı şekilde bulabilirler.

---

### Model Dosyaları

![Model Dosyaları](image9)

Favori, Sipariş, Sipariş Detay, Ürün ve Kullanıcı gibi ana veri modellerinin C# dosyaları projede organize edilmiştir.

---

### Giriş Ekranı

![Giriş Ekranı](image10)

Kullanıcılar e-posta ve şifre ile sisteme giriş yapabilir.

---

### Sepetim Ekranı

![Sepetim](image11)

Sepetteki ürünler, miktar ve fiyat bilgisiyle birlikte listelenir. Kullanıcılar toplam tutarı görebilir ve siparişi tamamlayabilir.

---

### Sipariş Detay Tablosu

![Sipariş Detay Tablosu](image12)

Sipariş Detay tablosunda Id, SiparisId, UrunId, Adet ve Fiyat alanları bulunur.

---

### Üye Ol Ekranı

![Üye Ol Ekranı](image13)

Kullanıcılar ad, soyad, e-posta ve şifre girerek sisteme kolayca üye olabilirler.

---

### Arama Sonuçları

![Arama Sonuçları](image14)

Kullanıcılar arama kutusuna ürün adını yazıp ilgili sonuçlara hızlıca ulaşabilirler.

---

### Hatalı Giriş Ekranı

![Hatalı Giriş Ekranı](image15)

Giriş sırasında yanlış e-posta veya şifre girilirse kullanıcıya uyarı mesajı gösterilir.

---

### Görsel Kaynak Klasörü

![Görsel Kaynak Klasörü](image16)

Proje içinde kullanılan ürün görselleri wwwroot/images klasöründe tutulmaktadır.

---

### Ürünler Tablosu

![Ürünler Tablosu](image17)

Veritabanında ürünlerin id, ad, adet, fiyat, resim url, marka, açıklama, kategori gibi alanları detaylı şekilde tutulur.

---

### Tablolar ve Alanlar

![Tablo ve Alanlar](image18)

Sipariş, Sipariş Detay ve Ürünler tablolarındaki tüm alanların veri tipi, boyut ve null izni özetlenmiştir.

---

### Ürün Kolonları

![Ürün Kolonları](image19)

Urunler tablosunun kolonları ve veri tipleri; ürünün teknik bilgileri, görseli, fiyatı, açıklaması ve stok durumu ile birlikte listelenir.

---

### Siparişlerim Ekranı

![Siparişlerim](image20)

Kullanıcıya ait geçmiş siparişler; sipariş no, tarih, alıcı, ürünler, adet ve toplam fiyat ile birlikte listelenir.

---

### Laptop Modelleri Listesi

![Laptop Modelleri](image21)

Laptop modelleri kategorisindeki ürünler, teknik özellikleri ve stok durumu ile birlikte kutucuklar halinde gösterilmekte.

---

### Favori Ürünlerim

![Favori Ürünlerim](image22)

Kullanıcıya ait favoriye eklenmiş ürünler, görsel, fiyat ve stok bilgisi ile birlikte listelenir. Favoriden çıkarma işlemi yapılabilir.

---

### Geliştirme ve Test Süreci

![Geliştirme ve Test](image23)

Projenin geliştirme aşamasında kod editörü ve tarayıcıda canlı görüntü.

---

### Proje Yapısı & Kod Editörü

![Proje Yapısı](image24)

Proje dosya ve klasör organizasyonu; kod editörü ve çözüm gezgini görünümü.

---

## Kurulum & Kullanım

1. Projeyi kopyalayın ve Visual Studio’da açın.
2. SQL Server’da PixnoriaStoreDB veritabanını oluşturun ve tabloları import edin.
3. wwwroot/images klasörüne ürün görsellerinizi ekleyin.
4. Gerekli NuGet paketlerini yükleyin.
5. Uygulamayı çalıştırarak web arayüzüne erişin.

---

## Lisans

Bu proje MIT lisansı
