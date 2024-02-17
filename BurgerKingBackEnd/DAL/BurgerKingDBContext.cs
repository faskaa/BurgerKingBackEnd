using BurgerKingBackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace BurgerKingBackEnd.DAL
{
    public class BurgerKingDBContext : DbContext
    {
        public BurgerKingDBContext( DbContextOptions<BurgerKingDBContext> options) : base(options)
        {
            
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<RestaurantProduct> RestaurantProduct { get; set; }


    }
}
