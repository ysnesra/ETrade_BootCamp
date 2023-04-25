# ETrade_BootCamp
Northwind veritabanı üzerinde çalışan bir MVC CORE uygulamasıdır
Bu uygulama techcareer.net BootCampında ödev olarak verildi
//Method Expression- Query Expression yöntemleriyle LINQ kodları yazarak ekrana verileri listelettiğm bir uygulama oldu.

Uygulamanın akışı ve ekranları aşağıdaki gibidir:
1-İlk olarak çalışanların listelendiği ekran açılacak. Bu ekranda çalışanın adı, soyadı ve ünvanı yer alacak.

2-Bu ekranda her çalışanın yanında Siparişler ve Sorumlu Olduğu Bölgeler linkleri yer alacak

3-Siparişler linkine tıklandığında o çalışanın ilgilendiği siparişler listelenecek. Sipariş no, sipariş tarihi, gönderildiği ülke ve sipariş tutarı yer alacak. Ayrıca her siparişin yanında Ürünler linki yer alacak.

4-Ürünler linkine tıklandığında o siparişte yer alan ürünlerin adı ve miktarının listelendiği ekran açılacak.

5-Sorumlu Olduğu Bölgeler linkine tıklandığında o çalışanın sorumlu olduğu bölgelerin listelendiği ekran açılacak.

6-Yeni çalışanın eklenebildiği bir ekran da olacak.Ve hangi amirin altında çalışıyorsa onu seçebilecek.

//Method Expression- Query Expression yöntemleriyle LINQ kodları yazarak ekrana verileri listelettiğm bir uygulama oldu

Refactoring işlemleri yapıldı:
* Bütün view sayfalarına ayrı ayrı yazmamak için _ViewImports.cshtml sayfasına "@using ETrade_BootCamp.ViewModels" namespace'ini yazarız

* Siparişlerin listelenme kısmına Sayfalama( X.PagedList.Mvc.Core) eklendi (OrderControllerın Index actionına)
  
* Employee ekleme ekranında selectList ile gelen Amir seçme kısmını ViewComponent yapısına taşındı.Böylece hem ekleme hem güncelleme ekranında ortak kullanılabilir.

* Personel eklerken adım adım ekleme(farklı sayfalarda) işlemi yapıldı.İkinci ekranda CheckBox ile Terrirory seçilerek eklenecek
  EmployeeForm ekranı iki ekrana ayrılıp EmployeeFormFirst ve EmployeeFormSecond ekranlarından geçip kaydedildi.
  EmployeeController -> Create actionı Sessionın dolu gelip gelmemseine göre iki ekranda da çağırılıyor

