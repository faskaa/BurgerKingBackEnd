using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class MenuVM
    {
        public List<RestaurantProduct> RestaurantProducts { get; set; } = null!;
        public Restaurant Restaurants { get; set; } = null!;
        public Product Product { get; set; } = null!;

        public int ProductQuantity { get; set; }
        public int ProductStockQuantity { get; set; }
    }
}
