namespace BurgerKingBackEnd.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsOpen { get; set; }

        public List<RestaurantProduct> RestaurantProduct { get; set; }


    }
}
