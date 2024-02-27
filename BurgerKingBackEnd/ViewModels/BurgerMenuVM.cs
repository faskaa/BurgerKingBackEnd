using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class BurgerMenuVM
    {
        public CustomUser CustomUser { get; set; } = null!;
        public Restaurant Restaurant { get; set; } = null!;
    }
}
