using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class MenuVM
    {
        public List<RestaurantProduct> RestaurantProducts { get; set; } = null!;
        public Restaurant Restaurants { get; set; } = null!;

    }
}
