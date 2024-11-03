# Earsiv Portal

## Proje Açıklaması

Earsiv Portal, e-fatura süreçlerini yönetmek ve belge görüntüleme işlemlerini gerçekleştirmek için tasarlanmış bir web uygulamasıdır. Kullanıcılar, belgeleri yükleyebilir, görüntüleyebilir ve çeşitli işlemler gerçekleştirebilir. Uygulama, kullanıcı dostu bir arayüz ile hızlı ve etkili bir belge yönetimi sunar.

## Kullanılan Teknolojiler ve Kütüphaneler

- **HTML5**: Uygulamanın temel yapısını oluşturur.
- **CSS3**: Uygulamanın stil ve görünümünü tanımlar.
- **JavaScript**: Dinamik içerik yönetimi için kullanılır.
- **AngularJS**: Uygulamanın ön yüzünde kullanılan JavaScript framework'ü. MVC mimarisi ile veri bağlama ve yönlendirme işlemleri yapılmaktadır.
- **Bootstrap**: Responsive tasarım için kullanılan CSS framework'ü. Uygulamanın mobil uyumlu olmasını sağlar.
- **jQuery**: DOM manipülasyonu ve AJAX işlemleri için kullanılır.
- **Toastr.js**: Kullanıcıya bildirimler göstermek için kullanılan bir kütüphane.
- **Angular-Sanitize**: Kullanıcı tarafından sağlanan HTML içeriğini güvenli hale getirmek için kullanılır.

## Proje Modülleri

1. **Belge Yönetimi Modülü**:
   - Kullanıcıların belgeleri yüklemesine, görüntülemesine ve silmesine olanak tanır.
   - Seçilen belgelerin durumunu kontrol eder ve kullanıcıya uygun bildirimler sunar.

2. **Modal Yönetimi Modülü**:
   - Dinamik olarak içerik yükleyen ve görüntüleyen modal pencereleri yönetir.
   - Kullanıcı etkileşimleri ile açılıp kapanan modallar oluşturur.

3. **Veri Filtreleme Modülü**:
   - Kullanıcıların belge listesinde arama yapmasını sağlar.
   - Seçilen kriterlere göre belgeleri filtreler ve görüntüler.

## Kurulum

Projenizi yerel ortamda çalıştırmak için aşağıdaki adımları izleyin:

1. **Depoyu Klonlayın**:
   ```bash
   git clone https://github.com/kullanici_adiniz/earsiv-portal.git
