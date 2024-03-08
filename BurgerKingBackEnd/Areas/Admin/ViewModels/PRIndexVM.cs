using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class PRIndexVM
    {
        public List<RestaurantProduct> restaurantProduct { get; set; }
        public List<Restaurant> restaurant { get; set; }
        public List<Product> product { get; set; }
    }
}
