namespace BurgerKingBackEnd.Entities
{
    public class RestaurantProduct
    {
        public int Id { get; set; }


        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }

    }
}
