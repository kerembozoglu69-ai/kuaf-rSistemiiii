using System;
using System.ComponentModel.DataAnnotations;

namespace kuaförSistemiiii.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Müşteri adı zorunludur.")]
        public string MusteriAdSoyad { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        public string Telefon { get; set; }

        [Required]
        public DateTime BaslangicSaati { get; set; }

        [Required]
        public DateTime BitisSaati { get; set; }

        public string Notlar { get; set; }
    }
}