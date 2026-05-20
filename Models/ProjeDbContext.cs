using Microsoft.EntityFrameworkCore;
using ProjeAdin.Models;

namespace kuaförSistemiiii.Models
{
    public class ProjeDbContext : DbContext
    {
        public ProjeDbContext(DbContextOptions<ProjeDbContext> options) : base(options)
        {
        }

        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
    }
}