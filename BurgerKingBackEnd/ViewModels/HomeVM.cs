using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class HomeVM
    {
        public List<Ad> Ads { get; set; } = null!;
        public Restaurant Restaurant { get; set; } = null!;
        public List<Slider> Sliders { get; set; } = null!;
        public List<Download> Downloads { get; set; } = null!;
        public List<Setting> Settings { get; set; } = null!;        
        public CustomUser CustomUser { get; set; } = null!; 
        public decimal CardItemTotalPrice { get; set; }

    }
}
