using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class SliderCreateVM
    {
        [ValidateNever]
        public string Image { get; set; }

        [Required]
        public string Title { get; set; }

        [ValidateNever]
        public string OrderImage { get; set; }

        [ValidateNever]
        public string DeliveryImage { get; set; }

        [Required]
        [Display(Name ="Image",Prompt = "Please fill this input")]
        public IFormFile IImage { get; set; }

        
        [Required]
        [Display(Name = "Order Image", Prompt = "Please fill this input")]
        public IFormFile IOrderImage { get; set; }

        [Required]
        [Display(Name = "Delivery Image", Prompt = "Please fill this input")]
        public IFormFile IDeliveryImage { get; set; }
    }
}
