using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class RestaurantCreateVM
    {
        [Required]
        public string Title { get; set; }


        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm\\:ss}")]
        [Display(Name = "Opening Time", Prompt = "Please fill this input")]
        public DateTime OpeningTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm\\:ss}")]
        [Display(Name = "Closing Time", Prompt = "Please fill this input")]
        public DateTime ClosingTime { get; set; }



        //[DataType(DataType.Date)]
        //public TimeSpan OpeningTime { get; set; }
        ////[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        //public TimeSpan ClosingTime { get; set; }
    }
}
