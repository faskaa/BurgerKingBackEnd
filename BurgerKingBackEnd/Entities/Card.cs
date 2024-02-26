namespace BurgerKingBackEnd.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public string CardName { get; set; }
        public int CardNumber { get; set; }
        public DateTime CardDate { get; set; }
        public int CVV { get; set; }
        public int ZipCode { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
