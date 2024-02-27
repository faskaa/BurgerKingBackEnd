using Microsoft.AspNetCore.Identity;

namespace BurgerKingBackEnd.Entities
{
    public class CustomUser:IdentityUser
    {
        public int SelectedRestaurantId { get; set; }
        public List<Order> Orders { get; set; }

    }
}
