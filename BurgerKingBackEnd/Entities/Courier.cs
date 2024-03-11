namespace BurgerKingBackEnd.Entities
{
    public class Courier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public int DriverLicenseNumber { get; set; }
        public int DrivingExperience { get; set; }
        public string VehicleType { get; set; }
        public bool IsDelivering { get; set; }
        public int? DeliveringOrderId { get; set; }


        public string CustomUserId { get; set; }
        public CustomUser CustomUser { get; set; }
    }
}
