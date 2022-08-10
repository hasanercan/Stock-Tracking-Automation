Create Database stok_takip
use stok_takip

Create Table Müþteriler(
tc_no nvarchar(50),
ad_soyad nvarchar(50),
telefon nvarchar(50),
adres nvarchar(50),
email nvarchar(50),
)
Create Table Ürünler(
barkod_no nvarchar(50),
Katagori nvarchar(50),
Marka nvarchar(50),
Ürünadý nvarchar(50),
StokSayýsý int,
Alýsfiyatý decimal(18,2),
Satýsfiyatý decimal(18,2),
tarih nchar(10),
)
Create Table Kategoriler(
Kategori nvarchar(50),
)
Create Table Markalar(
Kategori nvarchar(50),
Marka nvarchar(50),
)
Create Table Sepet(
tc nvarchar(50),
adsoyad nvarchar(50),
telefon nvarchar(50),
barkodno nvarchar(50),
ürünadý nvarchar(50),
stoksayýsý int,
satýþfiyatý decimal(18,2),
toplamfiyat decimal(18,2),
tarih nvarchar(50),
)
Create Table Satýþlar(
tc nvarchar(50),
adsoyad nvarchar(50),
telefon nvarchar(50),
barkodno nvarchar(50),
ürünadý nvarchar(50),
stoksayýsý int,
satýþfiyatý decimal(18,2),
toplamfiyat decimal(18,2),
tarih nvarchar(50),
)

																																												
