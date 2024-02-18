namespace BurgerKingBackEnd.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        public List<RestaurantProduct> RestaurantProduct { get; set; }


    }
}
