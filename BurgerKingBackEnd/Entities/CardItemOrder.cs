namespace BurgerKingBackEnd.Entities
{
    public class CardItemOrder
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int CardItemId { get; set; }
        public CardItem CardItem { get; set; }
    }
}
