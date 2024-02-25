using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class CardVM
    {
        public List<CardItem> cardItems { get; set; }
        public decimal TotalPrice { get; set; }
        public int RestaurantId { get; set; }



    }
}
