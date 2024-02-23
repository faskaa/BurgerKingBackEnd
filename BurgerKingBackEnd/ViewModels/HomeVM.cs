using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class HomeVM
    {
        public List<Ad> Ads { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Download> Downloads { get; set; }
        public List<Setting> Settings { get; set; }
        public CustomUser CustomUser { get; set; } = null!;
        public decimal CardItemTotalPrice { get; set; } 

    }
}
