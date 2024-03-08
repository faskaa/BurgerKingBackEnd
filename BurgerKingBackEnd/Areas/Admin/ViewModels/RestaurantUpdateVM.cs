using System.ComponentModel.DataAnnotations;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class RestaurantUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true , DataFormatString = "{0:hh\\:mm\\:ss}")]
        public TimeSpan OpeningTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm\\:ss}")]
        public TimeSpan ClosingTime { get; set; }

    }
}
