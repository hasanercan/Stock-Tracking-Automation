Create Database stok_takip
use stok_takip

Create Table M��teriler(
tc_no nvarchar(50),
ad_soyad nvarchar(50),
telefon nvarchar(50),
adres nvarchar(50),
email nvarchar(50),
)
Create Table �r�nler(
barkod_no nvarchar(50),
Katagori nvarchar(50),
Marka nvarchar(50),
�r�nad� nvarchar(50),
StokSay�s� int,
Al�sfiyat� decimal(18,2),
Sat�sfiyat� decimal(18,2),
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
�r�nad� nvarchar(50),
stoksay�s� int,
sat��fiyat� decimal(18,2),
toplamfiyat decimal(18,2),
tarih nvarchar(50),
)
Create Table Sat��lar(
tc nvarchar(50),
adsoyad nvarchar(50),
telefon nvarchar(50),
barkodno nvarchar(50),
�r�nad� nvarchar(50),
stoksay�s� int,
sat��fiyat� decimal(18,2),
toplamfiyat decimal(18,2),
tarih nvarchar(50),
)

																																												
