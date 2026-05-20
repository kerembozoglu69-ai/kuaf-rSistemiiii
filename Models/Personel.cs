using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ProjeAdin.Models // <-- BURAYA KENDİ PROJE ADINI YAZ (Örn: KuaforRandevu)
{
    public class Personel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        public DateTime DogumTarihi { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçimi zorunludur.")]
        public string Cinsiyet { get; set; }

        [Required(ErrorMessage = "Medeni durum seçimi zorunludur.")]
        public string MedeniDurum { get; set; }

        [Required(ErrorMessage = "Şehir seçimi zorunludur.")]
        public string Sehir { get; set; }

        [Required(ErrorMessage = "Departman alanı zorunludur.")]
        public string Departman { get; set; }

        [Required(ErrorMessage = "Pozisyon alanı zorunludur.")]
        public string Pozisyon { get; set; }

       [Required(ErrorMessage = "Maaş alanı zorunludur.")]
[Range(0, double.MaxValue, ErrorMessage = "Maaş alanı 0'dan küçük olamaz!")] // EKSİ GİRİŞİ ENGELLER
public decimal Maas { get; set; }

        [Required(ErrorMessage = "Çalışma şekli seçimi zorunludur.")]
        public string CalismaSekli { get; set; }

        [Required(ErrorMessage = "İşe giriş tarihi zorunludur.")]
        public DateTime IseGirisTarihi { get; set; }

        // Veritabanına kaydedilecek string veri
        public string Hobiler { get; set; }

        // Formdan çoklu seçim almak için kullanılacak, SQL'e eklenmeyecek
        [NotMapped]
        public string[] SecilenHobiler { get; set; }

        public string ProfilFotoYolu { get; set; }

        // Formdan dosya almak için kullanılacak, SQL'e eklenmeyecek
        [NotMapped]
        public IFormFile ProfilFoto { get; set; }

        public string Aciklama { get; set; }
    }
}