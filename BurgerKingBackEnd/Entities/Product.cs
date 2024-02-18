namespace BurgerKingBackEnd.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Calory { get; set; }

        public List<RestaurantProduct> RestaurantProduct { get; set; }


        //public int CategoryId { get; set; }
        //public Category Category { get; set; }

    }
}
