﻿namespace BurgerKingBackEnd.Entities
{
    public class CardItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int RestaurantId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        //true = pickup , false = delivery 
        public bool OrderType { get; set; }
        public bool IsSubmited { get; set; }


        public List<Order> Orders { get; set; }





    }
}
