using BiuroPodrozyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BiuroPodrozyApp.Data
{
    public class BiuroContext : DbContext
    {
        public BiuroContext(DbContextOptions<BiuroContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Wycieczka> Wycieczki { get; set; }
        public DbSet<UserWycieczka> UserWycieczki { get; set; }
    }
}
