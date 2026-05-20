using Microsoft.AspNetCore.Mvc;
using kuaförSistemiiii.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace kuaförSistemiiii.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ProjeDbContext _context;

        public RandevuController(ProjeDbContext context)
        {
            _context = context;
        }

        // Randevu Takvim Sayfası
        public IActionResult Index()
        {
            return View();
        }

        // Takvime veritabanındaki dolu randevuları JSON olarak gönderen metot
        [HttpGet]
        public IActionResult GetRandevular()
        {
            var etkinlikler = _context.Randevular.Select(r => new
            {
                id = r.Id,
                title = r.MusteriAdSoyad + " (" + r.Telefon + ")",
                start = r.BaslangicSaati.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = r.BitisSaati.ToString("yyyy-MM-ddTHH:mm:ss"),
                color = "#ff2a74" // Dolu saatler için neon pembe/kırmızı tonu
            }).ToList();

            return Json(etkinlikler);
        }

        // Takvimden boş saate tıklayınca yeni randevu kaydeden metot
        [HttpPost]
        public async Task<IActionResult> RandevuAl(string musteriAd, string telefon, string baslangic, string notlar)
        {
            try
            {
                DateTime start = DateTime.Parse(baslangic);
                DateTime end = start.AddHours(1); // Her randevu varsayılan 1 saat sürsün

                // O saatte başka randevu var mı kontrolü (Çakışma önleme)
                bool varMi = _context.Randevular.Any(r => (start >= r.BaslangicSaati && start < r.BitisSaati));
                if (varMi)
                {
                    return Json(new { success = false, message = "Seçtiğiniz saat doludur, lütfen başka bir saat seçin!" });
                }

                Randevu yeniRandevu = new Randevu
                {
                    MusteriAdSoyad = musteriAd,
                    Telefon = telefon,
                    BaslangicSaati = start,
                    BitisSaati = end,
                    Notlar = notlar
                };

                _context.Randevular.Add(yeniRandevu);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Randevunuz başarıyla kaydedildi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata oluştu: " + ex.Message });
            }
        }
    }
}