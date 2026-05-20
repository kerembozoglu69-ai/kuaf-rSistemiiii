using kuaförSistemiiii.Models; // Sadece senin kendi model klasörün kalıyor
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjeAdin.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace kuaförSistemiiii.Controllers
{
    public class PersonelController : Controller
    {
        private readonly ProjeDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PersonelController(ProjeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult KayitFormu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KayitFormu(Personel model)
        {
            // Backend validasyonları ve ilk harfi büyütme kuralları
            if (model.SecilenHobiler == null || model.SecilenHobiler.Length == 0)
            {
                ModelState.AddModelError("", "En az bir hobi seçilmelidir.");
            }

            if (ModelState.IsValid)
            {
                // KURAL: İlk harfleri büyütme işlemi
                model.Ad = IlkHarfiBuyut(model.Ad);
                model.Soyad = IlkHarfiBuyut(model.Soyad);

                // Hobileri string dizisinden tek satıra çevirme
                model.Hobiler = string.Join(", ", model.SecilenHobiler);

                // Dosya Yükleme Alanı
                string fotoYolu = "/images/default-avatar.png";
                if (model.ProfilFoto != null && model.ProfilFoto.Length > 0)
                {
                    string klasor = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(klasor)) Directory.CreateDirectory(klasor);

                    string dosyaAdi = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfilFoto.FileName);
                    string tamYol = Path.Combine(klasor, dosyaAdi);

                    using (var stream = new FileStream(tamYol, FileMode.Create))
                    {
                        await model.ProfilFoto.CopyToAsync(stream);
                    }
                    fotoYolu = "/uploads/" + dosyaAdi;
                }
                model.ProfilFotoYolu = fotoYolu;

                _context.Personeller.Add(model);
                await _context.SaveChangesAsync();

                ViewBag.Mesaj = "Personel başarıyla veritabanına kaydedildi!";
                return View();
            }

            return View(model);
        }

        private string IlkHarfiBuyut(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            text = text.Trim();
            if (text.Length == 1) return text.ToUpper();
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }
    }
}