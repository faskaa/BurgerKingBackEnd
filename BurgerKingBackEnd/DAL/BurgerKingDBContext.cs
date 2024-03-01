using BurgerKingBackEnd.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BurgerKingBackEnd.DAL
{
    public class BurgerKingDBContext : IdentityDbContext<CustomUser>
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
        public DbSet<Category> Category { get; set; }
        public DbSet<CustomUser> CustomUsers { get; set; }
        public DbSet<CardItem> CardItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Courier> Courier { get; set; }







        public DbSet<RestaurantProduct> RestaurantProduct { get; set; }


    }
}
