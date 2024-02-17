namespace BurgerKingBackEnd.Entities
{
    public class Download
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string GooglePlayImage { get; set; }
        public string AppStoreImage { get; set; }
        public string PlayStoreUrl { get; set; }
        public string AppStoreUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
