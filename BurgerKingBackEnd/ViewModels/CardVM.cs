using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class CardVM
    {
        public CustomUser customUser { get; set; }
        public List<CardItem> cardItems { get; set; }
    }
}
