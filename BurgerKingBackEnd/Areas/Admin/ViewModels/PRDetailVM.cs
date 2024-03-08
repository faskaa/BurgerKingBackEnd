using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class PRDetailVM
    {
        public RestaurantProduct restaurantProduct { get; set; }
        public Restaurant restaurant { get; set; }
        public Product product { get; set; }
    }
}
