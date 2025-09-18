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

### Ana Sayfa & Kategori Filtreleme

![Ana Sayfa ve Kategoriler](assets/images/ana-sayfa-ve-kategori-filtreleme.png)

Kullanıcılar ana sayfada kategorilere ve markalara göre ürünleri filtreleyebilir, arama kutusu üzerinden istedikleri ürünü hızlıca bulabilirler.

---

### Arama Sonuçları

![Arama Sonuçları](assets/images/arama-sonuclari.png)

Arama kutusuna ürün adı, model veya marka girildiğinde ilgili sonuçlar kartlar halinde listelenir.

---

### Favori Ürünlerim

![Favori Ürünlerim](assets/images/favori-urunlerim.png)

Kullanıcıların favoriye eklediği ürünler; görsel, fiyat ve stok bilgisi ile kutucuklar halinde listelenir. Favoriden çıkarma işlemi yapılabilir.

---

### Fırsat Köşesi

![Fırsat Köşesi](assets/images/firsat-kosesi.png)

Stokta kalan son ürünler ve fırsat ürünleri, uygun fiyatlarla ve teknik detaylarıyla birlikte listelenir. Her ürünün kalan adedi ve sepete ekleme butonu gösterilmektedir.

---

### Giriş Ekranı

![Giriş Ekranı](assets/images/giris-ekrani.png)

Kullanıcılar e-posta ve şifre ile sisteme giriş yapabilir.

---

### Kampanyalı Ürünler

![Kampanyalı Ürünler](assets/images/kampanyali-urunler.png)

İndirimde olan ürünler, teknik özellikleri, indirim oranı ve kalan stok miktarı ile birlikte sıralanıyor.

---

### Kullanıcılar Tablosu

![Kullanıcılar Tablosu](assets/images/kullanicilar-tablosu.png)

Uygulamanın kullanıcılar tablosu (Users): Id, Ad, Soyad, Email, Şifre, KayıtTarihi, SonGirisTarihi gibi alanlar ile kullanıcı yönetimi sağlanmaktadır.

---

### Laptop Modelleri Listesi

![Laptop Modelleri Listesi](assets/images/laptop-modelleri-listesi.png)

Laptop modelleri kategorisindeki ürünler, teknik özellikleri ve stok durumu ile birlikte kutucuklar halinde gösterilmektedir.

---

### Marka Filtreleme Arayüzü

![Marka Filtreleme Arayüzü](assets/images/marka-filtreleme-arayuzu.png)

Kullanıcılar, ürünleri markalarına göre filtreleyebilir ve istedikleri ürünleri hızlı şekilde bulabilirler.

---

### Model Dosyaları

![Model Dosyaları](assets/images/model-dosyalari.png)

Favori, Sipariş, Sipariş Detay, Ürün ve Kullanıcı gibi ana veri modellerinin C# dosyaları projede organize edilmiştir.

---

### Proje Dosya Yapısı

![Proje Dosya Yapısı](assets/images/proje-dosya-yapisi.png)

Visual Studio'da PixnoriaStore projesinin klasör/dosya organizasyonu. wwwroot altında css, images ve js klasörleri; Properties ve diğer temel klasörler yer almaktadır.

---

### Proje Yapısı & Kod Editörü

![Proje Yapısı & Kod Editörü](assets/images/proje-yapisi-kod-editoru.png)

Proje dosya ve klasör organizasyonu; kod editörü ve çözüm gezgini görünümü.

---

### Sepetim Ekranı

![Sepetim Ekranı](assets/images/sepetim-ekrani.png)

Sepetteki ürünler, miktar ve fiyat bilgisiyle birlikte listelenir. Kullanıcılar toplam tutarı görebilir ve siparişi tamamlayabilir.

---

### Siparişlerim Ekranı

![Siparişlerim Ekranı](assets/images/siparislerim-ekrani.png)

Kullanıcıya ait geçmiş siparişler; sipariş no, tarih, alıcı, ürünler, adet ve toplam fiyat ile birlikte listelenir.

---

### Tablolar ve Alanlar

![Tablolar ve Alanlar](assets/images/tablolar-ve-alanlar.png)

Sipariş, Sipariş Detay ve Ürünler tablolarındaki tüm alanların veri tipi, boyut ve null izni özetlenmiştir.

---

### Ürünler Tablosu

![Ürünler Tablosu](assets/images/urunler-tablosu.png)

Veritabanında ürünlerin id, ad, adet, fiyat, resim url, marka, açıklama, kategori gibi alanları detaylı şekilde tutulur.

---

### Üye Ol Ekranı

![Üye Ol Ekranı](assets/images/uye-ol-ekrani.png)

Kullanıcılar ad, soyad, e-posta ve şifre girerek sisteme kolayca üye olabilirler.

---

## Kurulum & Kullanım

1. Projeyi kopyalayın ve Visual Studio’da açın.
2. SQL Server’da PixnoriaStoreDB veritabanını oluşturun ve tabloları import edin.
3. wwwroot/assets/images klasörüne ürün görsellerinizi ekleyin.
4. Gerekli NuGet paketlerini yükleyin.
5. Uygulamayı çalıştırarak web arayüzüne erişin.

---

## Lisans

Bu proje MIT lisansı ile lisanslanmıştır.

---

## İletişim

Herhangi bir soru veya katkı için lütfen iletişime geçin!
