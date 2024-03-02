namespace BurgerKingBackEnd.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string? Unit { get; set; }
        public string? DeliveryInstruction { get; set; }
        public int? PhoneNumber { get; set; }
        public bool? PickUpType { get; set; }
        public string? PickUpOption { get; set; }
        public bool IsSubmited { get; set; }
        //null= processing , false== En route , true Delivered
        public bool? Status { get; set; }
        public List<CardItem> cardItems { get; set; }


        public string CustomUserId { get; set; }
        public CustomUser CustomUser { get; set; }

        public Card Card { get; set; }



    }
}
