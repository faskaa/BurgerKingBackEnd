using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class OrderVM
    {

        public CustomUser CustomUser { get; set; }
        public List<Order> Orders { get; set; }
        public List<Restaurant> Restaurant { get; set; }
    }
}
